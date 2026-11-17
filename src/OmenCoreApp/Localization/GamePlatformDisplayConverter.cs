using System;
using System.Globalization;
using System.Windows.Data;
using OmenCore.Services;
using OmenCore.ViewModels;

namespace OmenCore.Localization
{
    public sealed class GamePlatformDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
        {
            GamePlatformFilter.All => Strings.GamePlatformAll,
            GamePlatformFilter.Manual => Strings.GamePlatformManual,
            GamePlatform.Manual => Strings.GamePlatformManual,
            GamePlatform.Unknown => Strings.GamePlatformUnknown,
            _ => value
        };
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
