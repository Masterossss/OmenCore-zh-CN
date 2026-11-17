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
            if (value == "Saved to the keyboard. This picture now survives a power cycle, and shows with OmenCore closed.") return Strings.KeyMapFlashSaved;
            if (value == "Applied, but the keyboard refused the save-to-flash — the picture is showing now and will not survive a power cycle.") return Strings.KeyMapFlashRefused;
            const string visible = ". Only looking confirms it.";
            var visibilityIndex = value.IndexOf(visible, StringComparison.Ordinal);
            if (visibilityIndex >= 0)
            {
                var suffixText = value.Substring(visibilityIndex + visible.Length);
                var brightness = Regex.Match(suffixText, @"\A Brightness ([0-9]+) was NOT applied - this keyboard's backend has no brightness lever\.\z", RegexOptions.CultureInvariant);
                if (suffixText.Length != 0 && !brightness.Success) return value;
                var parts = value.Substring(0, visibilityIndex).Split(new[] { ", " }, StringSplitOptions.None);
                for (var i = 0; i < parts.Length; i++)
                {
                    var part = Regex.Match(parts[i], @"\A([0-9]+) (LEDs|keys|bar zones) (accepted|REFUSED)\z", RegexOptions.CultureInvariant);
                    if (!part.Success) return value;
                    var accepted = part.Groups[3].Value == "accepted";
                    var format = part.Groups[2].Value switch
                    {
                        "LEDs" => accepted ? Strings.KeyMapLedAccepted : Strings.KeyMapLedRefused,
                        "keys" => accepted ? Strings.KeyMapKeysAccepted : Strings.KeyMapKeysRefused,
                        _ => accepted ? Strings.KeyMapBarAccepted : Strings.KeyMapBarRefused
                    };
                    parts[i] = string.Format(format, part.Groups[1].Value);
                }
                return string.Join(", ", parts) + Strings.KeyMapVisualVerify + (brightness.Success ? string.Format(Strings.KeyMapBrightnessNotApplied, brightness.Groups[1].Value) : "");
            }
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
