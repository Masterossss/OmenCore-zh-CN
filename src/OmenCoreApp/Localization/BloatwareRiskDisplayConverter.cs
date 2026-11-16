using System;
using System.Globalization;
using System.Windows.Data;
using OmenCore.Services.BloatwareManager;

namespace OmenCore.Localization
{
    public sealed class BloatwareRiskDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
        {
            RemovalRisk.Low => Strings.BloatRiskLowDisplay,
            RemovalRisk.Medium => Strings.BloatRiskMediumDisplay,
            RemovalRisk.High => Strings.BloatRiskHighDisplay,
            RemovalRisk.Unknown => Strings.BloatRiskUnknownDisplay,
            _ => value
        };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
