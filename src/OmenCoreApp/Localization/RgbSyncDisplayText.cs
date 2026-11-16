using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Display-only translation; raw summary remains available to state logic.</summary>
    internal static class RgbSyncDisplayText
    {
        internal static string Translate(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value == "Ready to sync") return Strings.RgbSyncReady;
            if (value == "No RGB targets available") return Strings.RgbSyncNoTargets;
            if (value == "Sync failed: no providers updated") return Strings.RgbSyncFailedProviders;
            if (value == "Sync failed: no targets updated") return Strings.RgbSyncFailedTargets;
            // Preserve English (including singular) for cultures without a translated catalog.
            if (!System.Globalization.CultureInfo.CurrentUICulture.Name.Equals("zh-CN", System.StringComparison.OrdinalIgnoreCase)) return value;
            var full = Regex.Match(value, @"\ASync complete: ([0-9]+) (provider|providers|target|targets) updated\z", RegexOptions.CultureInvariant);
            if (full.Success)
                return string.Format(full.Groups[2].Value.StartsWith("provider", System.StringComparison.Ordinal) ? Strings.RgbSyncProviderComplete : Strings.RgbSyncTargetComplete, full.Groups[1].Value);
            var partial = Regex.Match(value, @"\APartial sync: ([0-9]+)/([0-9]+) (providers|targets) updated\z", RegexOptions.CultureInvariant);
            if (partial.Success)
                return string.Format(partial.Groups[3].Value == "providers" ? Strings.RgbSyncProviderPartial : Strings.RgbSyncTargetPartial, partial.Groups[1].Value, partial.Groups[2].Value);
            return value;
        }
    }
}
