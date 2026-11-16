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
                return string.Format(Strings.MemoryResultSuccess, match.Groups[1].Value, match.Groups[2].Value, failed, match.Groups[4].Value) + DeltaSuffix(suffix);
            }
            const string failedPrefix = "Failed: ";
            return line.StartsWith(failedPrefix, StringComparison.Ordinal)
                ? string.Format(Strings.MemoryResultFailure, Error(line.Substring(failedPrefix.Length))) + suffix
                : value;
        }

        private static string DeltaSuffix(string suffix)
        {
            if (string.IsNullOrEmpty(suffix)) return suffix;
            var newlineLength = suffix.StartsWith("\r\n", StringComparison.Ordinal) ? 2 : suffix.StartsWith("\n", StringComparison.Ordinal) ? 1 : 0;
            if (newlineLength == 0) return suffix;
            var parts = suffix.Substring(newlineLength).Split(new[] { " | " }, StringSplitOptions.None);
            if (parts.Length != 7) return suffix;
            var labels = new[] { "Physical used", "Available", "Standby", "Cache", "Commit", "Page file", "Modified" };
            var translated = new[] { Strings.MemDeltaPhysical, Strings.MemDeltaAvailable, Strings.MemDeltaStandby, Strings.MemDeltaCache, Strings.MemDeltaCommit, Strings.MemDeltaPagefile, Strings.MemDeltaModified };
            for (var i = 0; i < parts.Length; i++)
            {
                var match = Regex.Match(parts[i], @"\A" + Regex.Escape(labels[i]) + @" ([0-9]+) -> ([0-9]+) MB \((unchanged|increased [0-9]+ MB|reduced [0-9]+ MB)\)\z", RegexOptions.CultureInvariant);
                if (!match.Success) return suffix;
                var change = match.Groups[3].Value;
                var text = change == "unchanged" ? Strings.MemDeltaUnchanged
                    : change.StartsWith("increased ", StringComparison.Ordinal)
                        ? string.Format(Strings.MemDeltaIncreased, change.Substring(10, change.Length - 13))
                        : string.Format(Strings.MemDeltaReduced, change.Substring(8, change.Length - 11));
                parts[i] = string.Format(Strings.MemDeltaSegment, translated[i], match.Groups[1].Value, match.Groups[2].Value, text);
            }
            return suffix.Substring(0, newlineLength) + string.Join(" | ", parts);
        }

        internal static string Status(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value == "Done") return Strings.MemoryStatusDone;
            if (value == "Cleaning memory...") return Strings.MemoryStatusCleaning;
            if (value == "Enabling memory compression...") return Strings.MemoryStatusEnableCompression;
            if (value == "Disabling memory compression...") return Strings.MemoryStatusDisableCompression;
            var scheduled = Regex.Match(value, @"\AAuto-clean: scheduled every ([0-9]+) minute\(s\)\.\.\.\z", RegexOptions.CultureInvariant);
            if (scheduled.Success) return string.Format(Strings.MemAutoScheduled, scheduled.Groups[1].Value);
            var pressure = Regex.Match(value, @"\AAuto-clean: memory ([0-9]+)% load, ([0-9.,]+)% available, ([0-9.,]+)% commit\.\.\.\z", RegexOptions.CultureInvariant);
            if (pressure.Success) return string.Format(Strings.MemAutoPressure, pressure.Groups[1].Value, pressure.Groups[2].Value, pressure.Groups[3].Value);
            var progress = value switch
            {
                "Trimming process working sets..." => Strings.MemProgressTrim,
                "Flushing system file cache..." => Strings.MemProgressCache,
                "Purging low-priority standby list..." => Strings.MemProgressLowStandby,
                "Purging standby list..." => Strings.MemProgressStandby,
                "Flushing modified page list..." => Strings.MemProgressModified,
                "Combining memory pages..." => Strings.MemProgressCombine,
                "Done: Memory compression enabled" => Strings.MemStatusEnabled,
                "Done: Memory compression disabled" => Strings.MemStatusDisabled,
                _ => null
            };
            if (progress != null) return progress;
            var partial = Regex.Match(value, @"\AFreed ([0-9]+) MB \(([0-9]+) operations, ([0-9]+) failed\)\.\.\.\z", RegexOptions.CultureInvariant);
            if (partial.Success) return string.Format(Strings.MemStatusPartial, partial.Groups[1].Value, partial.Groups[2].Value, partial.Groups[3].Value);
            var copied = Regex.Match(value, @"\ADone: Copied (.+)\z", RegexOptions.CultureInvariant);
            if (copied.Success) return string.Format(Strings.MemStatusCopied, copied.Groups[1].Value);
            var excluded = Regex.Match(value, @"\ADone: Excluded (.+) from working-set trims\z", RegexOptions.CultureInvariant);
            if (excluded.Success) return string.Format(Strings.MemStatusExcluded, excluded.Groups[1].Value);
            var freed = Regex.Match(value, @"\ADone: Freed ([0-9]+) MB\z", RegexOptions.CultureInvariant);
            if (freed.Success) return string.Format(Strings.MemoryStatusDoneFreed, freed.Groups[1].Value);
            const string failedPrefix = "Failed: ";
            return value.StartsWith(failedPrefix, StringComparison.Ordinal)
                ? string.Format(Strings.MemoryResultFailure, Error(value.Substring(failedPrefix.Length)))
                : value;
        }
        private static string Error(string value) => value switch
        {
            "A memory clean operation is already in progress" => Strings.MemErrorBusy,
            "Failed to acquire required privileges. Run as administrator." => Strings.MemErrorPrivileges,
            "Failed to change memory compression state" => Strings.MemStatusFailedCompression,
            "Process location unavailable" => Strings.MemStatusProcessUnavailable,
            "Failed to open process location" => Strings.MemStatusOpenFailed,
            _ => value
        };
    }
}
