using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class BloatwareStatusDisplayText
    {
        internal static string Translate(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var known = value switch
            {
                "Failed: OmenCore is not running as Administrator. Bloatware removal requires admin rights." => Strings.BloatStatusFixed0,
                "Scanning for bloatware..." => Strings.BloatStatusFixed1,
                "Failed: Cannot remove: OmenCore is not running as Administrator." => Strings.BloatStatusFixed2,
                "Done: No low-risk installed items are currently available for removal" => Strings.BloatStatusFixed3,
                "Failed: No apps selected in removal preview." => Strings.BloatStatusFixed4,
                "Restore point creation skipped by user..." => Strings.BloatStatusFixed5,
                "Creating pre-removal restore point..." => Strings.BloatStatusFixed6,
                "Failed: Proceeding without restore point by user choice" => Strings.BloatStatusFixed7,
                "Failed: Removal canceled because restore point was not created" => Strings.BloatStatusFixed8,
                "Failed: Nothing to export - scan first or no installed candidates are available." => Strings.BloatStatusFixed9,
                "Failed: Nothing to export — no removal attempts recorded in this session." => Strings.BloatStatusFixed10,
                "Failed: No report is available yet." => Strings.BloatStatusFixed11,
                "Cancel requested..." => Strings.BloatStatusFixed12,
                "Done: No telemetry packages detected" => Strings.BloatStatusFixed13,
                "Done: No conflicting OMEN packages detected" => Strings.BloatStatusFixed14,
                "Done: No Windows consumer apps detected" => Strings.BloatStatusFixed15,
                "Done: No packages match the Full Standalone preset criteria" => Strings.BloatStatusFixed16,
                _ => null
            };
            if (known != null) return known;
            var scanned = Regex.Match(value, @"\ADone: Scan completed: ([0-9]+) items detected\z", RegexOptions.CultureInvariant);
            if (scanned.Success) return string.Format(Strings.BloatScanCount, scanned.Groups[1].Value);
            var removingProgress = Regex.Match(value, @"\ARemoving ([0-9]+)/([0-9]+): (.+)\.\.\.\z", RegexOptions.CultureInvariant);
            if (removingProgress.Success) return string.Format(Strings.BloatRemoveProgress, removingProgress.Groups[1].Value, removingProgress.Groups[2].Value, removingProgress.Groups[3].Value);
            var removing = Regex.Match(value, @"\ARemoving (.+)\.\.\.\z", RegexOptions.CultureInvariant);
            if (removing.Success) return string.Format(Strings.BloatRemoveOne, removing.Groups[1].Value);
            var partial = Regex.Match(value, @"\AFailed: Bulk restore finished with ([0-9]+) failures\. Restored ([0-9]+)/([0-9]+) items\.\z", RegexOptions.CultureInvariant);
            if (partial.Success) return string.Format(Strings.BloatRestorePartial, partial.Groups[1].Value, partial.Groups[2].Value, partial.Groups[3].Value);
            var canceled = Regex.Match(value, @"\AFailed: Bulk restore canceled at (.+)\. ([0-9]+) completed, ([0-9]+) skipped\.\z", RegexOptions.CultureInvariant);
            if (canceled.Success) return string.Format(Strings.BloatRestoreCanceled, canceled.Groups[1].Value, canceled.Groups[2].Value, canceled.Groups[3].Value);
            var count = Regex.Match(value, @"\ADone: Restored ([0-9]+) bloatware items\z", RegexOptions.CultureInvariant);
            if (count.Success) return string.Format(Strings.BloatRestoreCount, count.Groups[1].Value);
            var progress = Regex.Match(value, @"\ARestoring ([0-9]+)/([0-9]+): (.+)\.\.\.\z", RegexOptions.CultureInvariant);
            if (progress.Success) return string.Format(Strings.BloatRestoreProgress, progress.Groups[1].Value, progress.Groups[2].Value, progress.Groups[3].Value);
            var working = Regex.Match(value, @"\ARestoring (.+)\.\.\.\z", RegexOptions.CultureInvariant);
            if (working.Success) return string.Format(Strings.BloatRestoreWorking, working.Groups[1].Value);
            var done = Regex.Match(value, @"\ADone: Restored (.+)\z", RegexOptions.CultureInvariant);
            if (done.Success) return string.Format(Strings.BloatRestoreDone, done.Groups[1].Value);
            return value;
        }
    }
}
