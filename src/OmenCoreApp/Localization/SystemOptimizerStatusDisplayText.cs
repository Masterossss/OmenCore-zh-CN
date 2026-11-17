using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class SystemOptimizerStatusDisplayText
    {
        internal static string Translate(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var fixedText = value switch
            {
                "Scanning system state..." => Strings.OptStatusScanning,
                "Refreshing optimizer state..." => Strings.OptStatusRefreshing,
                "Refreshing optimizer preflight..." => Strings.OptStatusPreflight,
                "Done: Preflight updated" => Strings.OptStatusPreflightDone,
                "Done" => Strings.OptStatusDone,
                "Failed: Revert timed out — check system state manually" => Strings.OptStatusTimeout,
                "Exporting optimization report..." => Strings.OptStatusExporting,
                "Failed: Report file not found" => Strings.OptStatusReportMissing,
                "Failed: No report path to copy" => Strings.OptStatusNoReport,
                "Done: Opened exported report" => Strings.OptStatusReportOpened,
                "Done: Copied report path to clipboard" => Strings.OptStatusReportCopied,
                _ => null
            };
            if (fixedText != null) return fixedText;
            var action = Regex.Match(value, @"\A(Applying|Reverting) (.+)\.\.\.\z", RegexOptions.CultureInvariant);
            if (action.Success) return string.Format(action.Groups[1].Value == "Applying" ? Strings.OptStatusApplying : Strings.OptStatusReverting, Item(action.Groups[2].Value));
            var itemStatus = Regex.Match(value, @"\ADone: (.+) (enabled|disabled)( \(reboot required\))?\z", RegexOptions.CultureInvariant);
            if (itemStatus.Success)
            {
                var enabled = itemStatus.Groups[2].Value == "enabled";
                var reboot = itemStatus.Groups[3].Success;
                var format = enabled ? (reboot ? Strings.OptStatusItemEnabledReboot : Strings.OptStatusItemEnabled) : (reboot ? Strings.OptStatusItemDisabledReboot : Strings.OptStatusItemDisabled);
                return string.Format(format, Item(itemStatus.Groups[1].Value));
            }
            var categoryDone = Regex.Match(value, @"\ADone: Applied ([0-9]+) (.+) optimization\(s\)\z", RegexOptions.CultureInvariant);
            if (categoryDone.Success) return string.Format(Strings.OptStatusCategoryDone, categoryDone.Groups[1].Value, Category(categoryDone.Groups[2].Value));
            var categoryAlready = Regex.Match(value, @"\ADone: (.+) is already fully applied\z", RegexOptions.CultureInvariant);
            if (categoryAlready.Success) return string.Format(Strings.OptStatusCategoryAlready, Category(categoryAlready.Groups[1].Value));
            var partial = Regex.Match(value, @"\ADone: Applied ([0-9]+)/([0-9]+) optimizations \(([0-9]+) failed\)\z", RegexOptions.CultureInvariant);
            if (partial.Success) return string.Format(Strings.OptStatusAppliedPartial, partial.Groups[1].Value, partial.Groups[2].Value, partial.Groups[3].Value);
            var category = Regex.Match(value, @"\ADone: Applied ([0-9]+)/([0-9]+) items in (.+) \(([0-9]+) failed\)\z", RegexOptions.CultureInvariant);
            if (category.Success) return string.Format(Strings.OptStatusCategoryPartial, category.Groups[1].Value, category.Groups[2].Value, Category(category.Groups[3].Value), category.Groups[4].Value);
            var count = Regex.Match(value, @"\ADone: (Applied|Reverted) ([0-9]+)/([0-9]+) optimizations\z", RegexOptions.CultureInvariant);
            if (count.Success) return string.Format(count.Groups[1].Value == "Applied" ? Strings.OptStatusApplied : Strings.OptStatusReverted, count.Groups[2].Value, count.Groups[3].Value);
            const string failed = "Failed: ";
            return value.StartsWith(failed, StringComparison.Ordinal) ? string.Format(Strings.OptStatusFailed, value.Substring(failed.Length)) : value;
        }
        internal static string Category(string value) => value switch
        {
            "Power & Performance" => Strings.OptCategory0,
            "Windows Services" => Strings.OptCategory1,
            "Network & Latency" => Strings.OptCategory2,
            "Input & Gaming" => Strings.OptCategory3,
            "Visual Effects" => Strings.OptCategory4,
            "Storage & Disk" => Strings.OptCategory5,
            _ => value
        };

        private static string Item(string value) => value switch
        {
            "Ultimate Performance Plan" => Strings.OptimizerItem0,
            "Hardware GPU Scheduling" => Strings.OptimizerItem1,
            "Game Mode" => Strings.OptimizerItem2,
            "Foreground Priority Boost" => Strings.OptimizerItem3,
            "Disable Telemetry" => Strings.OptimizerItem4,
            "Disable SysMain (Superfetch)" => Strings.OptimizerItem5,
            "Disable Search Indexing" => Strings.OptimizerItem6,
            "Disable Connected Experiences" => Strings.OptimizerItem7,
            "TCP No Delay" => Strings.OptimizerItem8,
            "TCP ACK Optimization" => Strings.OptimizerItem9,
            "Disable P2P Updates" => Strings.OptimizerItem10,
            "Disable Nagle Algorithm" => Strings.OptimizerItem11,
            "Disable Mouse Acceleration" => Strings.OptimizerItem12,
            "Disable Game DVR" => Strings.OptimizerItem13,
            "Disable Xbox Game Bar" => Strings.OptimizerItem14,
            "Fullscreen Optimizations" => Strings.OptimizerItem15,
            "Disable Transparency" => Strings.OptimizerItem16,
            "Disable Animations" => Strings.OptimizerItem17,
            "TRIM for SSD" => Strings.OptimizerItem18,
            "Disable Last Access Timestamps" => Strings.OptimizerItem19,
            "Disable 8.3 Names" => Strings.OptimizerItem20,
            _ => value
        };
    }
}
