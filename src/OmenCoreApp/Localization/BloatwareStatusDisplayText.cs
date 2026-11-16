using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class BloatwareStatusDisplayText
    {
        internal static string Translate(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
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
