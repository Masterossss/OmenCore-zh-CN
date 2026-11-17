using System.Globalization;
using FluentAssertions;
using OmenCore.Localization;
using Xunit;

namespace OmenCoreApp.Tests.Localization
{
    public class SafetyNotificationTests
    {
        [Fact]
        public void GpuUnverifiedAndFailureRemainDistinctAndUnknownFallsBack()
        {
            var previous = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                GpuPowerStatusDisplay.Translate("⚠️ Command sent but GPU power may not have changed (BIOS WMI degraded)").Should().Contain("可能没有改变");
                GpuPowerStatusDisplay.Translate("⚠️ GPU power restore attempted but BIOS readback shows no change").Should().Contain("没有变化");
                GpuPowerStatusDisplay.Translate("Failed - WMI/EC commands not supported on this model").Should().StartWith("失败");
                GpuPowerStatusDisplay.Translate("Maximum (detected via WMI; saved preference 'Medium' not yet applied)").Should().Contain("尚未应用").And.Contain("Medium");
                GpuPowerStatusDisplay.Translate("Unexpected failure").Should().Be("Unexpected failure");
            }
            finally { CultureInfo.CurrentUICulture = previous; }
        }

        [Fact]
        public void BiosLockedCpuStatusPreservesReadOnlyAndModePolicy()
        {
            var previous = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                var text = CpuPowerStatusDisplay.Translate("⚠️ BIOS Locked - Read-only (PL1: 45W, PL2: 65W). Mode policy still applies via OMEN/OGH");
                text.Should().Contain("只读").And.Contain("45W").And.Contain("65W").And.Contain("模式策略仍然生效");
                CpuPowerStatusDisplay.Translate("Unknown status").Should().Be("Unknown status");
            }
            finally { CultureInfo.CurrentUICulture = previous; }
        }

        [Fact]
        public void HistoryTranslatesKnownWarningsAndKeepsUnknownMessages()
        {
            var previous = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
                var temp = SafetyNotificationHistoryDisplay.Translate("High Temperature Warning", "CPU: 95°C (threshold 90°C). This is an informational notification only - it's independent of fan behavior and any BIOS thermal/TCC limit. Adjust or disable it under Settings → Notifications.");
                temp.Message.Should().Contain("仅供提示").And.Contain("90°C").And.Contain("无关");
                var clamp = SafetyNotificationHistoryDisplay.Translate("GPU power clamp is back", "Limited to 80 W of 120 W. The firmware told the driver about the adapter again - restarting the GPU drops it.");
                clamp.Message.Should().Contain("80 W").And.Contain("120 W");
                var critical = SafetyNotificationHistoryDisplay.Translate("CRITICAL: CPU Overheating!", "98°C — thermal throttling may occur");
                critical.Title.Should().Contain("过热");
                SafetyNotificationHistoryDisplay.Translate("EC Bridge Unavailable", "Fan control features are disabled. Install LibreHardwareMonitor to enable EC access.").Message.Should().Contain("已禁用");
                var unknown = SafetyNotificationHistoryDisplay.Translate("GPU power clamp is back", "Unexpected driver failure");
                unknown.Should().Be(("GPU power clamp is back", "Unexpected driver failure"));
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                SafetyNotificationHistoryDisplay.Translate("CRITICAL: CPU Overheating!", "98°C — thermal throttling may occur").Title.Should().Be("CRITICAL: CPU Overheating!");
            }
            finally { CultureInfo.CurrentUICulture = previous; }
        }
    }
}
