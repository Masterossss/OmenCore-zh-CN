using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    // The original provider, configuration and diagnostic values remain untouched.
    internal static class LightingStatusDisplayText
    {
        internal static string Translate(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var fixedText = value switch
            {
                "Ready to restore saved keyboard colors." => Strings.LightReadyRestore,
                "No RGB surface observation recorded." => Strings.LightNoObservation,
                "Keyboard lighting is unavailable on this backend." => Strings.LightUnavailableBackend,
                "Keyboard lighting is unavailable; check backend detection or Settings > Hardware." => Strings.LightUnavailableRestore,
                "RGB probe unavailable because no HP keyboard lighting backend is active." => Strings.LightProbeUnavailable,
                "RGB observation could not be saved because configuration is unavailable." => Strings.LightObservationConfig,
                "Corsair iCUE not configured" => Strings.LightCorsairUnconfigured,
                "Logitech G HUB not configured" => Strings.LightLogitechUnconfigured,
                "Razer Synapse not configured" => Strings.LightRazerUnconfigured,
                _ => null
            };
            if (fixedText != null) return fixedText;
            const string failure = "RGB observation save failed: ";
            if (value.StartsWith(failure, StringComparison.Ordinal))
                return string.Format(Strings.LightObservationError, value.Substring(failure.Length));
            var probe = Regex.Match(value, @"\AProbe sent via (.+?); select what changed and save\. (.*)\z", RegexOptions.CultureInvariant);
            if (probe.Success) return string.Format(Strings.LightProbeSent, probe.Groups[1].Value, probe.Groups[2].Value);
            const string restore = "Restored saved keyboard colors. ";
            if (value.StartsWith(restore, StringComparison.Ordinal))
                return Strings.LightRestoredPrefix + value.Substring(restore.Length);
            const string hint = " WMI keyboard commands aren't verifying on your model - try enabling 'Experimental EC Keyboard' in Settings if colors don't visibly change.";
            if (value.EndsWith(hint, StringComparison.Ordinal))
                return value.Substring(0, value.Length - hint.Length) + Strings.LightWmiVerifyHint;
            return value;
        }
    }
}
