using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Translates known validation messages only for display; unknown messages remain unchanged.</summary>
    internal static class FanCurveValidationDisplay
    {
        internal static string Translate(string message)
        {
            if (message == "Fan curve must have at least 2 points to define a proper cooling response.")
                return Strings.FanValidationMinimum;
            var cases = new (string Pattern, string Format)[]
            {
                (@"\ATemperature (-?[0-9]+)°C is out of range\. Valid range: 0-100°C\.\z", Strings.FanValidationTemperature),
                (@"\AFan speed (-?[0-9]+)% is out of range\. Valid range: 0-100%\.\z", Strings.FanValidationSpeed),
                (@"\ADuplicate temperature point at (-?[0-9]+)°C\. Each temperature can only appear once\.\z", Strings.FanValidationDuplicate),
                (@"\AFan curve should extend to at least 80°C to protect against thermal throttling\. Highest point is (-?[0-9]+)°C\.\z", Strings.FanValidationCoverage),
                (@"\ALow temperature \((-?[0-9]+)°C\) has high fan speed \((-?[0-9]+)%\)\. This may cause unnecessary noise\.\z", Strings.FanValidationNoise)
            };
            foreach (var (pattern, format) in cases)
            {
                var m = Regex.Match(message, pattern, RegexOptions.CultureInvariant);
                if (m.Success) return string.Format(format, m.Groups[1].Value, m.Groups.Count > 2 ? m.Groups[2].Value : "");
            }
            return message;
        }
    }
}
