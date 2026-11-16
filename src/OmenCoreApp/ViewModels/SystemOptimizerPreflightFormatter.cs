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
            if (report.Warnings.Count == 0) return OmenCore.Localization.Strings.OptimizerPreflightNoWarnings;
            var selected = report.Warnings.Take(maxWarnings).Select(TranslateWarning).ToList();
            var suffix = report.Warnings.Count > maxWarnings
                ? string.Format(OmenCore.Localization.Strings.OptimizerPreflightMore, report.Warnings.Count - maxWarnings)
                : string.Empty;
            return string.Join(" | ", selected) + suffix;
        }

        private static string TranslateWarning(string warning) => warning switch
        {
            "Increases idle power draw; not recommended on battery." => OmenCore.Localization.Strings.OptimizerPreflightWarning0,
            "May slow app-launch times on HDDs; beneficial on SSDs." => OmenCore.Localization.Strings.OptimizerPreflightWarning1,
            "Windows Search will be slower after this change." => OmenCore.Localization.Strings.OptimizerPreflightWarning2,
            "Some HDR and variable-refresh features may not work in affected apps." => OmenCore.Localization.Strings.OptimizerPreflightWarning3,
            "Some legacy 16-bit or very old programs may fail to launch after this change." => OmenCore.Localization.Strings.OptimizerPreflightWarning4,
            "Backup tools that rely on last-access timestamps may not detect unchanged files correctly." => OmenCore.Localization.Strings.OptimizerPreflightWarning5,
            _ => warning
        };

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
