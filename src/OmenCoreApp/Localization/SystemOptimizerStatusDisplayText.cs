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
            var partial = Regex.Match(value, @"\ADone: Applied ([0-9]+)/([0-9]+) optimizations \(([0-9]+) failed\)\z", RegexOptions.CultureInvariant);
            if (partial.Success) return string.Format(Strings.OptStatusAppliedPartial, partial.Groups[1].Value, partial.Groups[2].Value, partial.Groups[3].Value);
            var category = Regex.Match(value, @"\ADone: Applied ([0-9]+)/([0-9]+) items in (.+) \(([0-9]+) failed\)\z", RegexOptions.CultureInvariant);
            if (category.Success) return string.Format(Strings.OptStatusCategoryPartial, category.Groups[1].Value, category.Groups[2].Value, category.Groups[3].Value, category.Groups[4].Value);
            var count = Regex.Match(value, @"\ADone: (Applied|Reverted) ([0-9]+)/([0-9]+) optimizations\z", RegexOptions.CultureInvariant);
            if (count.Success) return string.Format(count.Groups[1].Value == "Applied" ? Strings.OptStatusApplied : Strings.OptStatusReverted, count.Groups[2].Value, count.Groups[3].Value);
            const string failed = "Failed: ";
            return value.StartsWith(failed, StringComparison.Ordinal) ? string.Format(Strings.OptStatusFailed, value.Substring(failed.Length)) : value;
        }
    }
}
