namespace OmenCore.Localization
{
    internal static class GpuPowerStatusDisplay
    {
        internal static string Translate(string status) => status switch
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
