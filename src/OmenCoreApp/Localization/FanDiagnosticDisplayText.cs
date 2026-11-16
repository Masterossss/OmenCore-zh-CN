using System;
using System.Linq;

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
            if (content.StartsWith("✓ ", StringComparison.Ordinal) || content.StartsWith("✗ ", StringComparison.Ordinal))
                return content.Replace(" - Score: ", Strings.FanReportScore).Replace("[evidence: ", Strings.FanReportEvidence) + carriage;
            return line;
        }
    }
}
