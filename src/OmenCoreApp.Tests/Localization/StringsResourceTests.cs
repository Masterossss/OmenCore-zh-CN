using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using FluentAssertions;
using OmenCore.Localization;
using Xunit;

namespace OmenCoreApp.Tests.Localization
{
    /// <summary>
    /// Replaces the designer-file guarantee that a CLI build does not give us: every key in the
    /// neutral catalog has a property, and every key exists and is filled in for every shipped
    /// language. Without this, adding a string and forgetting Russian is silent until a user sees
    /// the English word in a Russian window.
    /// </summary>
    public class StringsResourceTests
    {
        private static readonly string[] Languages = { "en", "ru", "tr", "zh-CN" };

        private static string LocalizationDirectory()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                string candidate = Path.Combine(directory.FullName, "src/OmenCoreApp/Localization");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Could not find src/OmenCoreApp/Localization from the test output folder");
        }

        private static string CataloguePath(string language) =>
            Path.Combine(LocalizationDirectory(), language == "en" ? "Strings.resx" : $"Strings.{language}.resx");

        private static Dictionary<string, string> ReadCatalogue(string language)
        {
            XNamespace none = XNamespace.None;
            return XDocument.Load(CataloguePath(language))
                .Root!
                .Elements(none + "data")
                .ToDictionary(
                    element => element.Attribute("name")!.Value,
                    element => element.Element(none + "value")?.Value ?? string.Empty);
        }

        [Fact]
        public void EveryLanguageHasTheSameKeysAsTheNeutralCatalogue()
        {
            var english = ReadCatalogue("en");

            foreach (string language in Languages.Where(l => l != "en"))
            {
                var translated = ReadCatalogue(language);

                translated.Keys.Should().BeEquivalentTo(english.Keys,
                    $"'{language}' must be a complete translation, not a partial one");
            }
        }

        [Fact]
        public void NoTranslationIsEmpty()
        {
            foreach (string language in Languages)
            {
                foreach (var pair in ReadCatalogue(language))
                {
                    pair.Value.Should().NotBeNullOrWhiteSpace(
                        $"'{pair.Key}' in '{language}' must carry a value");
                }
            }
        }

        [Fact]
        public void TranslationsPreserveCompositeFormatArguments()
        {
            var english = ReadCatalogue("en");
            foreach (string language in Languages.Where(l => l != "en"))
            {
                var translated = ReadCatalogue(language);
                foreach (var pair in english)
                {
                    var pattern = @"(?<!\{)\{\d+(?:,[^}:]+)?(?::[^}]+)?\}(?!\})";
                    var expected = Regex.Matches(pair.Value, pattern).Select(m => m.Value).OrderBy(x => x);
                    var actual = Regex.Matches(translated[pair.Key], pattern).Select(m => m.Value).OrderBy(x => x);
                    actual.Should().Equal(expected, $"'{pair.Key}' in '{language}' must preserve format arguments");
                }
            }
        }

        [Fact]
        public void EveryCatalogueKeyHasTypedAccessor()
        {
            var properties = typeof(Strings)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(property => property.Name)
                .ToHashSet(StringComparer.Ordinal);

            foreach (string key in ReadCatalogue("en").Keys)
            {
                properties.Should().Contain(key,
                    $"Strings.{key} is what XAML and code use to reach that catalogue entry");
            }
        }

        [Fact]
        public void Resolve_MapsEverySupportedCode()
        {
            Language.Resolve("en").TwoLetterISOLanguageName.Should().Be("en");
            Language.Resolve("ru").TwoLetterISOLanguageName.Should().Be("ru");
            Language.Resolve("tr").TwoLetterISOLanguageName.Should().Be("tr");
            Language.Resolve("zh-CN").Name.Should().Be("zh-CN");
        }

        [Fact]
        public void Resolve_FallsBackToTheSystemCultureForAutoAndUnknownCodes()
        {
            var system = CultureInfo.GetCultureInfo("de-DE");

            Language.Resolve("auto", system).Should().Be(system);
            Language.Resolve("klingon", system).Should().Be(system);
            Language.Resolve(null, system).Should().Be(system);
        }

        [Fact]
        public void Normalize_ClampsToWhatWeShip()
        {
            Language.Normalize(null).Should().Be(Language.SystemDefault);
            Language.Normalize("  ").Should().Be(Language.SystemDefault);
            Language.Normalize("RU").Should().Be("ru");
            Language.Normalize(" ZH-cn ").Should().Be("zh-CN");
            Language.Normalize("de").Should().Be(Language.SystemDefault);
        }

        [Fact]
        public void Strings_ResolveFromTheSatelliteCatalogs_NotJustTheNeutralOne()
        {
            // The parity tests read the .resx files; this one proves the built satellites are
            // actually loaded at runtime, which is the difference between "translated" and
            // "ships translated".
            CultureInfo original = CultureInfo.CurrentUICulture;

            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru");
                Strings.NavSettings.Should().Be("Настройки");
                Strings.WindowMinimize.Should().Be("Свернуть");
                Strings.AboutTitle.Should().Be("About OmenCore");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("tr");
                Strings.NavGames.Should().Be("Oyunlar");
                Strings.AboutTitle.Should().Be("About OmenCore");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                Strings.NavSettings.Should().Be("设置");
                Strings.WindowMinimize.Should().Be("最小化");
                Strings.AboutTitle.Should().Be("关于 OmenCore");
                Strings.DashboardText01.Should().Be("系统概览");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                Strings.NavGames.Should().Be("Games");
            }
            finally
            {
                CultureInfo.CurrentUICulture = original;
            }
        }

        [Fact]
        public void DynamicSafetyPromptsResolveAndFormatInChineseAndEnglish()
        {
            var original = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                var warning = string.Format(Strings.NvidiaPowerApplyConfirm, "MAX", 175, Strings.NvidiaPowerMaxRestart);
                warning.Should().Contain("MAX 175 W").And.Contain("散热").And.Contain("重启 Windows").And.Contain("风险");
                string.Format(Strings.FanDiagTooHotDetail, 93.0, 70.0)
                    .Should().Contain("93 C").And.Contain("70 C").And.Contain("未开始");
                string.Format(Strings.FanWarningDangerous, 90, 20, 70, 70)
                    .Should().Contain("危险").And.Contain("90°C").And.Contain("过热");
                Strings.GpuUnlockExperimentalConfirm.Should().Contain("实验性").And.Contain("真实硬件")
                    .And.Contain("20 秒").And.Contain("重启").And.Contain("适配器");
                string.Format(Strings.AdapterClampConfirm, "CPU 限制警告")
                    .Should().Contain("黑屏").And.Contain("崩溃").And.Contain("CPU 限制警告");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                string.Format(Strings.NvidiaPowerApplyConfirm, "CURRENT", 175, Strings.NvidiaPowerCurrentRestart)
                    .Should().Contain("CURRENT 175 W").And.Contain("resets on reboot").And.Contain("risk");
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void UnsupportedResourceCultureFallsBackToEnglish()
        {
            var original = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
                Strings.NvidiaPowerRemoveConfirm.Should().Contain("Windows must be restarted");
                Strings.NavGames.Should().Be("Games");
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void NvidiaBackendDisplayMappingPreservesUnknownDetailsAndSafetyState()
        {
            var original = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                NvidiaPowerDisplayText.Translate("OmenCore could not prepare performance cooling. CURRENT was not sent.")
                    .Should().Contain("无法准备").And.Contain("CURRENT 未发送");
                NvidiaPowerDisplayText.Translate("driver: nvapi64_impl.dll file is unavailable. | policy: Unknown — CURRENT exceeds live MAX.")
                    .Should().Contain("文件不可用").And.Contain("Unknown").And.Contain("超过了实时 MAX");
                NvidiaPowerDisplayText.Translate("unrecognized error 0x1234").Should().Be("unrecognized error 0x1234");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                NvidiaPowerDisplayText.Translate("CURRENT exceeds live MAX.").Should().Be("CURRENT exceeds live MAX.");
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void FanReportDisplayLocalizesLabelsWithoutChangingTheOriginalReport()
        {
            const string report = "=== DIAGNOSTIC COMPLETE: ❌ FAIL ===\r\nBackend: WMI | RPM source: EcDirect\r\nTests: 1/2 passed | Overall Score: 50/100 (Fair)\r\n✗ CPU @ 60%: 0 RPM - Score: 0 [evidence: unknown]\r\nUnrecognized detail: 0x1234\r\n";
            var original = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                var display = FanDiagnosticDisplayText.Translate(report);
                display.Should().Contain("诊断完成：❌ 失败").And.Contain("后端：WMI")
                    .And.Contain("转速来源：EcDirect").And.Contain("CPU @ 60%: 0 RPM")
                    .And.Contain("Unrecognized detail: 0x1234");
                FanDiagnosticDisplayText.Translate("⚠ 2 step(s) interrupted by thermal protection and not counted as failures: CPU @ 30%, GPU @ 60%.\r\nLet the laptop cool below 70 C and run the diagnostic again to complete them.")
                    .Should().Contain("2 个步骤被散热保护中断，未计为失败")
                    .And.Contain("CPU @ 30%, GPU @ 60%").And.Contain("70 C 以下");
                FanDiagnosticDisplayText.Translate("RGB: Skipped: integrated RGB backend unavailable")
                    .Should().Contain("已跳过").And.Contain("不可用");
                FanDiagnosticDisplayText.Translate("Run not started: the CPU or GPU is at 93 C. Let the laptop cool below 70 C and run the diagnostic again.")
                    .Should().Contain("未开始").And.Contain("93 C").And.Contain("70 C 以下");
                report.Should().Contain("Backend: WMI | RPM source: EcDirect");

                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                FanDiagnosticDisplayText.Translate(report).Should().Be(report);
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void GpuUnlockDisplayKeepsRefusalsAndUnknownResultsDistinct()
        {
            var original = CultureInfo.CurrentUICulture;
            const string disabled = "The GPU power unlock is disabled in this build: its EC write path has not been validated on real 8D87 hardware yet.";
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                GpuDiagnosticDisplayText.Translate(disabled).Should().Contain("已禁用").And.Contain("尚未").And.Contain("8D87");
                GpuDiagnosticDisplayText.Translate("No EC access is available - the unlock needs PawnIO.")
                    .Should().Contain("无法访问 EC").And.Contain("PawnIO");
                GpuDiagnosticDisplayText.Translate("unknown result: 0x42").Should().Be("unknown result: 0x42");
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                GpuDiagnosticDisplayText.Translate(disabled).Should().Be(disabled);
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void NvidiaParameterizedMessagesPreservePrecisionAndRejectUnknownVariants()
        {
            var original = CultureInfo.CurrentUICulture;
            const string failure = "CURRENT 150 W exceeds live MAX 115.00 W. Apply MAX first, reboot Windows, then verify the new live MAX before applying CURRENT.";
            const string success = "CURRENT confirmed at 115 W.";
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                NvidiaPowerDisplayText.Translate(failure).Should().Contain("150 W").And.Contain("115.00 W")
                    .And.Contain("超过").And.Contain("重启 Windows").And.Contain("确认");
                NvidiaPowerDisplayText.Translate(success).Should().Contain("已确认").And.Contain("115 W");
                NvidiaPowerDisplayText.Translate(failure + " Rollback could not be confirmed.")
                    .Should().Be(failure + " Rollback could not be confirmed.");
                NvidiaPowerDisplayText.Translate("CURRENT 150 W exceeds live MAX 115,00 W.")
                    .Should().Be("CURRENT 150 W exceeds live MAX 115,00 W.");
                foreach (var culture in new[] { "en", "ru", "tr" })
                {
                    CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                    NvidiaPowerDisplayText.Translate(failure).Should().Be(failure);
                    NvidiaPowerDisplayText.Translate(success).Should().Be(success);
                }
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void MemorySummaryAndRiskDescriptionResolveInBothLanguages()
        {
            var originalUi = CultureInfo.CurrentUICulture;
            var originalFormat = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                string.Format(Strings.MemoryPhysicalSummary, "4.0 GB", "8.0 GB", 1.25)
                    .Should().Contain("已用 4.0 GB").And.Contain("可用 8.0 GB").And.Contain("1.2 GB");
                Strings.MemoryViewText223.Should().Contain("备用列表").And.Contain("短暂卡顿");
                Strings.MemoryGameAwareOn.Should().Contain("内存压力").And.Contain("除外");
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                Strings.MemoryViewText223.Should().Contain("standby list purge").And.Contain("brief stutter");
            }
            finally
            {
                CultureInfo.CurrentUICulture = originalUi;
                CultureInfo.CurrentCulture = originalFormat;
            }
        }

        [Fact]
        public void MemoryDisplayPreservesRawResultAndDistinguishesPartialFailure()
        {
            var original = CultureInfo.CurrentUICulture;
            const string raw = "Freed 128 MB  •  3 ops succeeded  •  1 failed  •  12:34:56\r\nDelta: 128 MB";
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                MemoryOptimizerDisplayText.Result(raw).Should().Contain("已释放 128 MB")
                    .And.Contain("3 项操作成功").And.Contain("1 项失败").And.Contain("Delta: 128 MB");
                MemoryOptimizerDisplayText.Status("Failed: service unavailable").Should().Contain("失败：service unavailable");
                MemoryOptimizerDisplayText.Result("\nunknown").Should().Be("\nunknown");
                raw.Should().StartWith("Freed 128 MB");
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                MemoryOptimizerDisplayText.Result(raw).Should().Be(raw);
            }
            finally { CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void Apply_ChangesOnlyTheUiCulture()
        {
            // Numbers must keep the invariant formatting App installs on every thread; a UI
            // language that also moved CurrentCulture would reintroduce the comma-decimal bug.
            CultureInfo originalUi = CultureInfo.CurrentUICulture;
            CultureInfo originalCulture = CultureInfo.CurrentCulture;

            try
            {
                Language.Apply("ru");

                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Should().Be("ru");
                CultureInfo.CurrentCulture.Should().Be(originalCulture);
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentUICulture = originalUi;
                CultureInfo.CurrentUICulture = originalUi;
                CultureInfo.CurrentCulture = originalCulture;
            }
        }
    }
}
