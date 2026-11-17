using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class CpuPowerStatusDisplay
    {
        internal static string Translate(string value)
        {
            var fixedText = value switch
            {
                "Detecting..." => Strings.SealCpuDetecting,
                "No MSR access (install PawnIO for power limit control)" => Strings.SealCpuNoMsr,
                "MSR package limits unavailable on 8D2F Intel path - use OMEN/OGH mode policy control" => Strings.SealCpuMsr8d2f,
                "MSR package limits unavailable - HP firmware may require OMEN performance modes/Unleashed mode" => Strings.SealCpuMsrUnavailable,
                _ => null
            };
            if (fixedText != null) return fixedText;
            var m = Regex.Match(value, @"\A⚠️ BIOS Locked - Read-only \(PL1: ([0-9.,]+)W, PL2: ([0-9.,]+)W\)(\. Mode policy still applies via OMEN/OGH)?\z");
            if (m.Success) return string.Format(m.Groups[3].Success ? Strings.SealCpuBiosLockedOgh : Strings.SealCpuBiosLocked, m.Groups[1].Value, m.Groups[2].Value);
            return value;
        }
    }
}
