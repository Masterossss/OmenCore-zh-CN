using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class GpuPowerStatusDisplay
    {
        internal static string Translate(string status)
        {
            var m = Regex.Match(status, @"\A(Minimum|Medium|Maximum|Extended) \(detected via WMI; saved preference '([^']+)' not yet applied\)\z");
            if (m.Success) return string.Format(Strings.SealGpuSavedNotApplied, m.Groups[1].Value, m.Groups[2].Value);
            m = Regex.Match(status, @"\A(Minimum|Medium|Maximum|Extended) \(detected via (WMI|OGH)\)\z");
            if (m.Success) return string.Format(m.Groups[2].Value == "WMI" ? Strings.SealGpuDetectedWmi : Strings.SealGpuDetectedOgh, m.Groups[1].Value);
            return status switch
            {
            "Detecting..." => Strings.SealGpuDetecting,
            "Not supported — HP Victus BIOS does not expose custom TGP/PPAB control" => Strings.SealGpuUnsupportedVictus,
            "Not available on this model" => Strings.SealGpuUnavailableModel,
            "⚠️ Command sent but GPU power may not have changed (BIOS WMI degraded)" => Strings.SealGpuUnverified,
            "⚠️ GPU power restore attempted but BIOS readback shows no change" => Strings.SealGpuRestoreUnverified,
            "Not available - this BIOS doesn't apply Extended via WMI, and no distinct EC fallback level exists beyond Maximum" => Strings.SealGpuExtendedUnavailable,
            "Failed - WMI/EC commands not supported on this model" => Strings.SealGpuCommandsFailed,
                _ => status
            };
        }
    }
}
