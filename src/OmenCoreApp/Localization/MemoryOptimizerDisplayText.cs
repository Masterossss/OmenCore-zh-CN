using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Display-only formatting; the copied result and service data remain unchanged.</summary>
    internal static class MemoryOptimizerDisplayText
    {
        internal static string Result(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var boundary = value.IndexOf('\n');
            var line = boundary < 0 ? value : value.Substring(0, boundary).TrimEnd('\r');
            var suffix = boundary < 0 ? "" : value.Substring(boundary > 0 && value[boundary - 1] == '\r' ? boundary - 1 : boundary);
            var match = Regex.Match(line, @"\AFreed ([0-9]+) MB  •  ([0-9]+) ops succeeded(?:  •  ([0-9]+) failed)?  •  ([0-9]{2}:[0-9]{2}:[0-9]{2})\z", RegexOptions.CultureInvariant);
            if (match.Success)
            {
                var failed = match.Groups[3].Success ? string.Format(Strings.MemoryResultFailedCount, match.Groups[3].Value) : "";
                return string.Format(Strings.MemoryResultSuccess, match.Groups[1].Value, match.Groups[2].Value, failed, match.Groups[4].Value) + suffix;
            }
            const string failedPrefix = "Failed: ";
            return line.StartsWith(failedPrefix, StringComparison.Ordinal)
                ? string.Format(Strings.MemoryResultFailure, line.Substring(failedPrefix.Length)) + suffix
                : value;
        }

        internal static string Status(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value == "Done") return Strings.MemoryStatusDone;
            if (value == "Cleaning memory...") return Strings.MemoryStatusCleaning;
            if (value == "Enabling memory compression...") return Strings.MemoryStatusEnableCompression;
            if (value == "Disabling memory compression...") return Strings.MemoryStatusDisableCompression;
            var freed = Regex.Match(value, @"\ADone: Freed ([0-9]+) MB\z", RegexOptions.CultureInvariant);
            if (freed.Success) return string.Format(Strings.MemoryStatusDoneFreed, freed.Groups[1].Value);
            const string failedPrefix = "Failed: ";
            return value.StartsWith(failedPrefix, StringComparison.Ordinal)
                ? string.Format(Strings.MemoryResultFailure, value.Substring(failedPrefix.Length))
                : value;
        }
    }
}
