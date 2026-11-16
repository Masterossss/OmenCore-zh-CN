using FluentAssertions;
using OmenCore.Services.SystemOptimizer;
using OmenCore.ViewModels;
using Xunit;

namespace OmenCoreApp.Tests.ViewModels
{
    public class SystemOptimizerPreflightFormatterTests
    {
        [Fact]
        public void BuildSummary_IncludesRiskCountsAndRebootFlag()
        {
            var report = new PreflightReport
            {
                Items = new[]
                {
                    new PreflightItem { Id = "a", Risk = OptimizationRisk.Low, RequiresReboot = false },
                    new PreflightItem { Id = "b", Risk = OptimizationRisk.Medium, RequiresReboot = true },
                    new PreflightItem { Id = "c", Risk = OptimizationRisk.High, RequiresReboot = false },
                }
            };

            var text = SystemOptimizerPreflightFormatter.BuildSummary(report);

            text.Should().Contain("Low 1");
            text.Should().Contain("Medium 1");
            text.Should().Contain("High 1");
            text.Should().Contain("Reboot: Yes");
        }

        [Fact]
        public void OptimizationDisplayPreservesIdOriginalNameAndRiskWarning()
        {
            var original = System.Globalization.CultureInfo.CurrentUICulture;
            try
            {
                var item = new OptimizationItem { Id = "service_search", Name = "Disable Search Indexing", Warning = "May slow down file searches", Risk = OptimizationRisk.Medium };
                System.Globalization.CultureInfo.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("zh-CN");
                item.DisplayName.Should().Be("禁用搜索索引");
                item.DisplayWarning.Should().Contain("搜索");
                item.RiskText.Should().Be("中风险");
                item.Name.Should().Be("Disable Search Indexing");
                item.Id.Should().Be("service_search");
                item.Warning.Should().Be("May slow down file searches");
                item.Id = "unknown";
                item.DisplayName.Should().Be(item.Name);
            }
            finally { System.Globalization.CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void DisplaySummaryPreservesRiskAndRebootAndUnknownWarnings()
        {
            var original = System.Globalization.CultureInfo.CurrentUICulture;
            try
            {
                var report = new PreflightReport { Items = new[] {
                    new PreflightItem { Id = "a", Risk = OptimizationRisk.High, RequiresReboot = true, Warning = "unknown warning" }
                } };
                System.Globalization.CultureInfo.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("zh-CN");
                SystemOptimizerPreflightFormatter.BuildDisplaySummary(report).Should().Contain("高风险 1 项").And.Contain("需要重启：是");
                SystemOptimizerPreflightFormatter.BuildDisplayWarningRollup(report).Should().Contain("unknown warning");
                System.Globalization.CultureInfo.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en");
                SystemOptimizerPreflightFormatter.BuildDisplaySummary(report).Should().Be(SystemOptimizerPreflightFormatter.BuildSummary(report));
            }
            finally { System.Globalization.CultureInfo.CurrentUICulture = original; }
        }

        [Fact]
        public void BuildWarningRollup_UsesLimitAndReportsRemainder()
        {
            var report = new PreflightReport
            {
                Items = new[] { new PreflightItem { Id = "x", Risk = OptimizationRisk.Low } }
            };
            report.Items[0].Warning = "warning one";

            var report2 = new PreflightReport
            {
                Items = new[]
                {
                    new PreflightItem { Id = "a", Risk = OptimizationRisk.Low, Warning = "warning one" },
                    new PreflightItem { Id = "b", Risk = OptimizationRisk.Medium, Warning = "warning two" },
                    new PreflightItem { Id = "c", Risk = OptimizationRisk.High, Warning = "warning three" },
                }
            };

            var rollup = SystemOptimizerPreflightFormatter.BuildWarningRollup(report2, maxWarnings: 2);

            rollup.Should().Contain("warning one");
            rollup.Should().Contain("warning two");
            rollup.Should().Contain("(+1 more)");
        }
    }
}
