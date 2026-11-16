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
            const string hint = " WMI keyboard commands aren't verifying on your model - try enabling 'Experimental EC Keyboard' in Settings if colors don't visibly change.";
            var hasHint = value.EndsWith(hint, StringComparison.Ordinal);
            var body = hasHint ? value.Substring(0, value.Length - hint.Length) : value;
            const string restorePrefix = "Restored saved keyboard colors. ";
            var restored = body.StartsWith(restorePrefix, StringComparison.Ordinal);
            if (restored) body = body.Substring(restorePrefix.Length);
            var surface = Regex.Match(body, @"\A(.*) Surface: (.+)\.\z", RegexOptions.CultureInvariant);
            if (surface.Success)
            {
                var apply = surface.Groups[1].Value switch
                {
                    "Experimental EC keyboard write was throttled to protect the EC." => Strings.LightApplyStatus0,
                    "Applied via experimental EC keyboard register writes." => Strings.LightApplyStatus1,
                    "WMI BIOS ColorTable accepted the write. Verify the intended physical surface changed; on some models this may be a light bar rather than the keyboard." => Strings.LightApplyStatus2,
                    "Applied through individual WMI zone writes." => Strings.LightApplyStatus3,
                    "Applied via OMEN Gaming Hub proxy ColorTable fallback." => Strings.LightApplyStatus4,
                    "Applied via experimental EC fallback after WMI methods failed." => Strings.LightApplyStatus5,
                    "All keyboard lighting backends failed. Colors may not be applied." => Strings.LightApplyStatus6,
                    _ => TranslateApplyStatus(surface.Groups[1].Value)
                };
                return (restored ? Strings.LightRestoredPrefix : "") + string.Format(Strings.LightSurfaceFormat, apply, surface.Groups[2].Value) + (hasHint ? Strings.LightWmiVerifyHint : "");
            }
            var probe = Regex.Match(value, @"\AProbe sent via (.+?); select what changed and save\. (.*)\z", RegexOptions.CultureInvariant);
            if (probe.Success) return string.Format(Strings.LightProbeSent, probe.Groups[1].Value, TranslateApplyStatus(probe.Groups[2].Value));
            const string restore = "Restored saved keyboard colors. ";
            if (value.StartsWith(restore, StringComparison.Ordinal))
                return Strings.LightRestoredPrefix + value.Substring(restore.Length);
            if (value.EndsWith(hint, StringComparison.Ordinal))
                return value.Substring(0, value.Length - hint.Length) + Strings.LightWmiVerifyHint;
            return value;
        }
        private static string TranslateApplyStatus(string value)
        {
            var applied = Regex.Match(value, @"\AApplied via V2 (.+?)\. (.+)\z", RegexOptions.CultureInvariant);
            if (applied.Success) return string.Format(Strings.LightV2Applied, applied.Groups[1].Value, TranslateVerification(applied.Groups[2].Value));
            var accepted = Regex.Match(value, @"\AV2 (.+?) accepted the write but did not verify: (.+)\z", RegexOptions.CultureInvariant);
            if (accepted.Success) return string.Format(Strings.LightV2Accepted, accepted.Groups[1].Value, TranslateVerification(accepted.Groups[2].Value));
            var failed = Regex.Match(value, @"\AV2 (.+?) did not verify: (.+)\z", RegexOptions.CultureInvariant);
            if (failed.Success) return string.Format(Strings.LightV2Unverified, failed.Groups[1].Value, TranslateVerification(failed.Groups[2].Value));
            const string error = "Keyboard lighting apply failed: ";
            return value.StartsWith(error, StringComparison.Ordinal)
                ? string.Format(Strings.LightApplyError, value.Substring(error.Length)) : value;
        }

        private static string TranslateVerification(string value) => value switch
        {
            "Readback verified." => Strings.LightReadbackYes,
            "Readback did not verify." => Strings.LightReadbackNo,
            "No reliable readback available." => Strings.LightReadbackUnavailable,
            _ => value
        };
    }
}
