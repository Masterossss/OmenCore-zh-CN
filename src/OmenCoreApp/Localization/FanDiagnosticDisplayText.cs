using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Localizes known report labels for display without changing copied or logged diagnostic data.</summary>
    internal static class FanDiagnosticDisplayText
    {
        internal static string Translate(string report)
        {
            if (string.IsNullOrEmpty(report)) return report;
            return string.Join("\n", report.Split('\n').Select(TranslateLine));
        }

        private static string TranslateLine(string line)
        {
            var carriage = line.EndsWith("\r", StringComparison.Ordinal) ? "\r" : "";
            var content = carriage.Length == 0 ? line : line.Substring(0, line.Length - 1);
            if (content == "=== DIAGNOSTIC COMPLETE: ✅ PASS ===") return Strings.FanReportPass + carriage;
            if (content == "=== DIAGNOSTIC COMPLETE: ❌ FAIL ===") return Strings.FanReportFail + carriage;
            if (content.StartsWith("Backend: ", StringComparison.Ordinal))
                return Strings.FanReportBackend + content.Substring("Backend: ".Length).Replace(" | RPM source: ", Strings.FanReportSource) + carriage;
            if (content.StartsWith("Tests: ", StringComparison.Ordinal))
                return Strings.FanReportTests + content.Substring("Tests: ".Length).Replace(" passed | Overall Score: ", Strings.FanReportPassedScore) + carriage;
            if (content == "RGB: Skipped: integrated RGB backend unavailable") return "RGB: " + Strings.FanDiagRgbSkip + carriage;
            if (content == "RGB: Pattern applied and saved colors restored; physical confirmation required") return "RGB: " + Strings.FanDiagRgbConfirmed + carriage;
            const string rejected = "RGB: Pattern write not accepted by ";
            if (content.StartsWith(rejected, StringComparison.Ordinal))
                return "RGB: " + string.Format(Strings.FanDiagRgbRejected, content.Substring(rejected.Length)) + carriage;
            const string restoreFailed = "RGB: Pattern applied; saved colors were not restored by ";
            if (content.StartsWith(restoreFailed, StringComparison.Ordinal))
                return "RGB: " + string.Format(Strings.FanDiagRgbRestoreFailed, content.Substring(restoreFailed.Length)) + carriage;
            if (content.StartsWith("✓ ", StringComparison.Ordinal) || content.StartsWith("✗ ", StringComparison.Ordinal))
                return content.Replace(" - Score: ", Strings.FanReportScore).Replace("[evidence: ", Strings.FanReportEvidence) + carriage;
            var interrupted = Regex.Match(content, @"^⚠ (\d+) step\(s\) interrupted by thermal protection and not counted as failures: (.+)\.$");
            if (interrupted.Success)
                return string.Format(Strings.FanReportInterrupted, interrupted.Groups[1].Value, interrupted.Groups[2].Value) + carriage;
            var cool = Regex.Match(content, @"^Let the laptop cool below ([^ ]+) C and run the diagnostic again to complete them\.$");
            if (cool.Success)
                return string.Format(Strings.FanReportCoolRetry, cool.Groups[1].Value) + carriage;
            var tooHot = Regex.Match(content, @"^Run not started: the CPU or GPU is at ([0-9]+) C\. Let the laptop cool below ([0-9]+) C and run the diagnostic again\.$");
            if (tooHot.Success && int.TryParse(tooHot.Groups[1].Value, out var current) && int.TryParse(tooHot.Groups[2].Value, out var threshold))
                return string.Format(Strings.FanDiagTooHotDetail, current, threshold) + carriage;
            return line;
        }
    }
}
