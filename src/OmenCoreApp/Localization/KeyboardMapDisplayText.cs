using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace OmenCore.Localization
{
    public sealed class KeyboardMapDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is string text ? KeyboardMapDisplayText.Translate(text) : value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }

    internal static class KeyboardMapDisplayText
    {
        internal static string Translate(string value)
        {
            const string prefix = "This keyboard (";
            const string suffix = ") matches HP's device list for this protocol but has not been confirmed on hardware here. Per-key colour is expected to work and the layout is read from the keyboard itself, so it should be correct — but if anything misbehaves, that is worth reporting with this device id.";
            if (value.StartsWith(prefix, StringComparison.Ordinal) && value.EndsWith(suffix, StringComparison.Ordinal) && value.Length >= prefix.Length + suffix.Length)
                return string.Format(Strings.KeyMapVerification, value.Substring(prefix.Length, value.Length - prefix.Length - suffix.Length));
            if (value == "Windows Dynamic Lighting is switched on for this keyboard. Your colours will look right while OmenCore is open, and Windows will repaint the keyboard as soon as it closes. Turn Dynamic Lighting off for this device to keep them.") return Strings.KeyMapDynamicWarning;
            const string effect = " A running keyboard effect cannot be dimmed — the MCU draws those itself.";
            if (!value.EndsWith(effect, StringComparison.Ordinal)) return value;
            var body = value.Substring(0, value.Length - effect.Length);
            if (body == "Applies on the next Apply. Select keys to dim them independently.") return Strings.KeyMapBrightnessNone + Strings.KeyMapBrightnessEffect;
            var full = Regex.Match(body, @"\A([0-9]+) selected, at full\. Applies on the next Apply\.\z", RegexOptions.CultureInvariant);
            if (full.Success) return string.Format(Strings.KeyMapBrightnessFull, full.Groups[1].Value) + Strings.KeyMapBrightnessEffect;
            var partial = Regex.Match(body, @"\A([0-9]+) selected at ([0-9]+)% — they land at ([0-9]+)% once the master's ([0-9]+)% is applied\.\z", RegexOptions.CultureInvariant);
            if (partial.Success) return string.Format(Strings.KeyMapBrightnessPartial, partial.Groups[1].Value, partial.Groups[2].Value, partial.Groups[3].Value, partial.Groups[4].Value) + Strings.KeyMapBrightnessEffect;
            return value;
        }
    }
}
