using System;

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
            return value;
        }
    }
}
