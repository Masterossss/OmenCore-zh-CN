using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class HotkeyOsdDisplayText
    {
        internal static string Value(string value) => value switch
        {
            "Profile" => Strings.HotkeyOsdValue0,
            "Fan Mode" => Strings.HotkeyOsdValue1,
            "Performance" => Strings.HotkeyOsdValue2,
            "Boost" => Strings.HotkeyOsdValue3,
            "Quiet Mode" => Strings.HotkeyOsdValue4,
            "Auto (BIOS Control)" => Strings.HotkeyOsdValue5,
            "Maximum Cooling" => Strings.HotkeyOsdValue6,
            "Quiet" => Strings.HotkeyOsdValue7,
            "Balanced" => Strings.HotkeyOsdValue8,
            "Auto" => Strings.HotkeyOsdValue9,
            "Max" => Strings.HotkeyOsdValue10,
            "Off" => Strings.HotkeyOsdValue11,
            "On" => Strings.HotkeyOsdValue12,
            _ => value
        };

        internal static string Mode(string category, string value) =>
            category == "Profile" ? value : Value(value);

        internal static string Trigger(string? value)
        {
            if (value == null) return Strings.OsdViaHotkey;
            var match = Regex.Match(value, @"\Avia Hotkey \((.+)\)\z", RegexOptions.CultureInvariant);
            return match.Success ? string.Format(Strings.HotkeyOsdTriggered, match.Groups[1].Value) : value;
        }
    }
}
