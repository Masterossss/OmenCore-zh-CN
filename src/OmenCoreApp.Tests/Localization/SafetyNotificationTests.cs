using System.Globalization;
using FluentAssertions;
using OmenCore.Localization;
using Xunit;

namespace OmenCoreApp.Tests.Localization
{
    public class SafetyNotificationTests
    {
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
