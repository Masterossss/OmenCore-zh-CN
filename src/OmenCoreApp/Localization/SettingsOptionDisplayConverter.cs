using System;
using System.Globalization;
using System.Windows.Data;

namespace OmenCore.Localization
{
    public sealed class SettingsOptionDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value?.ToString() ?? string.Empty;
            return name switch
            {
                "Display Off" => Strings.SettingsOption0,
                "Lock Windows" => Strings.SettingsOption1,
                "Disabled" => Strings.SettingsOption2,
                "Off" => Strings.SettingsOption3,
                "Cooldown" => Strings.SettingsOption4,
                "Timer" => Strings.SettingsOption5,
                "Auto" => Strings.SettingsOption6,
                "Quiet" => Strings.SettingsOption7,
                "Silent" => Strings.SettingsOption8,
                "Gaming" => Strings.SettingsOption9,
                "Extreme" => Strings.SettingsOption10,
                "Max" => Strings.SettingsOption11,
                "Balanced" => Strings.SettingsOption12,
                "Performance" => Strings.SettingsOption13,
                "Turbo" => Strings.SettingsOption14,
                "TopLeft" => Strings.SettingsOption15,
                "TopCenter" => Strings.SettingsOption16,
                "TopRight" => Strings.SettingsOption17,
                "BottomLeft" => Strings.SettingsOption18,
                "BottomCenter" => Strings.SettingsOption19,
                "BottomRight" => Strings.SettingsOption20,
                "Primary" => Strings.SettingsOption21,
                "ActiveWindow" => Strings.SettingsOption22,
                "MouseCursor" => Strings.SettingsOption23,
                "Compact" => Strings.SettingsOption24,
                "Comfortable" => Strings.SettingsOption25,
                "Cyan" => Strings.SettingsOption26,
                "Orange" => Strings.SettingsOption27,
                "Teal" => Strings.SettingsOption28,
                "Magenta" => Strings.SettingsOption29,
                "ShowQuickPopup" => Strings.SettingsOption30,
                "ShowWindow" => Strings.SettingsOption31,
                "ToggleFanMode" => Strings.SettingsOption32,
                "TogglePerformanceMode" => Strings.SettingsOption33,
                "CyclePerformance" => Strings.SettingsOption34,
                "CycleFanMode" => Strings.SettingsOption35,
                "ToggleMaxCooling" => Strings.SettingsOption36,
                "ToggleOsd" => Strings.SettingsOption37,
                "LaunchExternalApp" => Strings.SettingsOption38,
                "DoNothing" => Strings.SettingsOption39,
                "Time" => Strings.SettingsOption40,
                "Battery" => Strings.SettingsOption41,
                "ACPower" => Strings.SettingsOption42,
                "Temperature" => Strings.SettingsOption43,
                "Idle" => Strings.SettingsOption44,
                "Process" => Strings.SettingsOption45,
                "WiFiSSID" => Strings.SettingsOption46,
                "LidState" => Strings.SettingsOption47,
                "Below" => Strings.SettingsOption48,
                "Above" => Strings.SettingsOption49,
                "Connected" => Strings.SettingsOption50,
                "Disconnected" => Strings.SettingsOption51,
                "Closed" => Strings.SettingsOption52,
                "Open" => Strings.SettingsOption53,
                _ => name
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
