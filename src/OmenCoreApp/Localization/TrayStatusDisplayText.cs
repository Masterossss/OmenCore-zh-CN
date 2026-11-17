using System;

namespace OmenCore.Localization
{
    internal static class TrayStatusDisplayText
    {
        internal static string Value(string value) => value switch
        {
            "Unknown" => Strings.TrayStateUnknown,
            "Healthy" => Strings.TrayStateHealthy,
            "Degraded" => Strings.TrayStateDegraded,
            "Stale" => Strings.TrayStateStale,
            "Firmware default" => Strings.TrayStateFirmwareDefault,
            "Custom" => Strings.TrayStateCustom,
            _ => HotkeyOsdDisplayText.Value(value)
        };

        internal static string Fan(string current, string? pending, bool linked)
        {
            var active = string.IsNullOrWhiteSpace(current) ? "Unknown" : current.Trim();
            var request = string.IsNullOrWhiteSpace(pending) ? null : pending.Trim();
            var suffix = linked ? Strings.TrayHeaderLinked : string.Empty;
            return request != null && !string.Equals(request, active, StringComparison.OrdinalIgnoreCase)
                ? string.Format(Strings.TrayHeaderFanPending, Value(active), suffix, Value(request))
                : string.Format(Strings.TrayHeaderFan, Value(active), suffix);
        }

        internal static string Performance(string value) => string.Format(Strings.TrayHeaderPerformance, Value(string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim()));
        internal static string Monitor(string value) => string.Format(Strings.TrayHeaderMonitor, Value(string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim()));
    }
}
