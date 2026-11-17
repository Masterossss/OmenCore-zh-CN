using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace OmenCore.Localization
{
    public sealed class KeyboardDiagnosticDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is string text ? KeyboardDiagnosticDisplayText.Translate(text) : value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }

    internal static class KeyboardDiagnosticDisplayText
    {
        internal static string Translate(string value)
        {
            var known = value switch
            {
                "Ready" => Strings.KeyDiagReady,
                "Detecting devices..." => Strings.KeyDiagDetecting,
                "Probe finished. Report which step number (if any) turned the keyboard red." => Strings.KeyDiagProbeFinished,
                "Running test pattern..." => Strings.KeyDiagTesting,
                "Test pattern applied successfully." => Strings.KeyDiagTestDone,
                "Clearing test pattern..." => Strings.KeyDiagClearing,
                "Test pattern cleared." => Strings.KeyDiagCleared,
                "Connected" => Strings.KeyDiagConnected,
                "Integrated Keyboard" => Strings.KeyDiagIntegrated,
                "Keyboard" => Strings.KeyDiagKeyboard,
                _ => null
            };
            if (known != null) return known;
            var found = Regex.Match(value, @"\ADetection complete\. Found ([0-9]+) device\(s\)\.\z", RegexOptions.CultureInvariant);
            if (found.Success) return string.Format(Strings.KeyDiagDetected, found.Groups[1].Value);
            var probe = Regex.Match(value, @"\AProbe step ([0-9]+) of ([0-9]+): is the keyboard red now\?\z", RegexOptions.CultureInvariant);
            if (probe.Success) return string.Format(Strings.KeyDiagProbeStep, probe.Groups[1].Value, probe.Groups[2].Value);
            foreach (var (prefix, format) in new[] { ("Detection failed: ", Strings.KeyDiagDetectFailed), ("Test pattern failed: ", Strings.KeyDiagTestFailed), ("Clear failed: ", Strings.KeyDiagClearFailed) })
                if (value.StartsWith(prefix, StringComparison.Ordinal)) return string.Format(format, value.Substring(prefix.Length));
            return value;
        }
    }
}
