using System.Linq;
using OmenCore.Services.SystemOptimizer;

namespace OmenCore.ViewModels
{
    public static class SystemOptimizerPreflightFormatter
    {
        public static string BuildSummary(PreflightReport report)
        {
            var rebootTag = report.RequiresReboot ? " | Reboot: Yes" : " | Reboot: No";
            return $"Preflight: Low {report.LowRiskCount}, Medium {report.MediumRiskCount}, High {report.HighRiskCount}{rebootTag}";
        }

        public static string BuildDisplaySummary(PreflightReport report)
        {
            return string.Format(OmenCore.Localization.Strings.OptimizerPreflightDisplay,
                report.LowRiskCount, report.MediumRiskCount, report.HighRiskCount,
                report.RequiresReboot ? OmenCore.Localization.Strings.OptimizerPreflightYes : OmenCore.Localization.Strings.OptimizerPreflightNo);
        }

        public static string BuildDisplayWarningRollup(PreflightReport report, int maxWarnings = 2)
        {
            return report.Warnings.Count == 0
                ? OmenCore.Localization.Strings.OptimizerPreflightNoWarnings
                : BuildWarningRollup(report, maxWarnings);
        }

        public static string BuildWarningRollup(PreflightReport report, int maxWarnings = 2)
        {
            if (report.Warnings.Count == 0)
            {
                return "No additional preflight warnings.";
            }

            var selected = report.Warnings.Take(maxWarnings).ToList();
            var suffix = report.Warnings.Count > maxWarnings ? $" (+{report.Warnings.Count - maxWarnings} more)" : string.Empty;
            return string.Join(" | ", selected) + suffix;
        }
    }
}
