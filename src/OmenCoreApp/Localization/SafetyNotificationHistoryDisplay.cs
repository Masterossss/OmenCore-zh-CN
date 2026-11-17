using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class SafetyNotificationHistoryDisplay
    {
        internal static (string Title, string Message) Translate(string title, string message)
        {
            Match m;
            if (title == "High Temperature Warning")
            {
                m = Regex.Match(message, @"\A(.+): ([0-9.,]+)°C \(threshold ([0-9.,]+)°C\)\. This is an informational notification only - it's independent of fan behavior and any BIOS thermal/TCC limit\. Adjust or disable it under Settings → Notifications\.\z");
                if (m.Success) return (Strings.SealTempTitle, string.Format(Strings.SealTempHistory, m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value));
            }
            if (title == "GPU power clamp is back")
            {
                m = Regex.Match(message, @"\ALimited to ([0-9.,]+) W of ([0-9.,]+) W\. The firmware told the driver about the adapter again - restarting the GPU drops it\.\z");
                if (m.Success) return (Strings.SealClampTitle, string.Format(Strings.SealClampHistory, m.Groups[1].Value, m.Groups[2].Value));
            }
            m = Regex.Match(title, @"\ACRITICAL: (.+) Overheating!\z");
            var value = Regex.Match(message, @"\A([0-9.,]+)°C — thermal throttling may occur\z");
            if (m.Success && value.Success) return (string.Format(Strings.SealCriticalTitle, m.Groups[1].Value), string.Format(Strings.SealCriticalHistory, value.Groups[1].Value));
            m = Regex.Match(title, @"\AThermal Protection: (.+)\z");
            value = Regex.Match(message, @"\A([0-9.,]+)°C — fans boosted to max\z");
            if (m.Success && value.Success) return (string.Format(Strings.SealProtectionTitle, m.Groups[1].Value), string.Format(Strings.SealProtectionHistory, value.Groups[1].Value));
            if (title == "EC Bridge Unavailable" && message == "Fan control features are disabled. Install LibreHardwareMonitor to enable EC access.")
                return (Strings.SealEcTitle, Strings.SealEcHistory);
            return (title, message);
        }
    }
}
