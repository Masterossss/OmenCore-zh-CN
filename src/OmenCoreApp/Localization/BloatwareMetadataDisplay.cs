using System;
using System.Globalization;
using System.Windows.Data;
using OmenCore.Services.BloatwareManager;

namespace OmenCore.Localization
{
    public sealed class BloatwareMetadataDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
        {
            BloatwareCategory.Unknown => Strings.BloatCategoryUnknown,
            BloatwareCategory.OemSoftware => Strings.BloatCategoryOemSoftware,
            BloatwareCategory.WindowsApps => Strings.BloatCategoryWindowsApps,
            BloatwareCategory.Antivirus => Strings.BloatCategoryAntivirus,
            BloatwareCategory.Games => Strings.BloatCategoryGames,
            BloatwareCategory.Social => Strings.BloatCategorySocial,
            BloatwareCategory.Communication => Strings.BloatCategoryCommunication,
            BloatwareCategory.Promotional => Strings.BloatCategoryPromotional,
            BloatwareCategory.Telemetry => Strings.BloatCategoryTelemetry,
            BloatwareCategory.Startup => Strings.BloatCategoryStartup,
            BloatwareType.AppxPackage => Strings.BloatTypeAppxPackage,
            BloatwareType.Win32App => Strings.BloatTypeWin32App,
            BloatwareType.StartupItem => Strings.BloatTypeStartupItem,
            BloatwareType.ScheduledTask => Strings.BloatTypeScheduledTask,
            _ => value
        };
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }

    internal static class BloatwareDependencyDisplay
    {
        internal static string Translate(string value) => value switch
        {
            "needed for OMEN key" => Strings.BloatDependency0,
            "may affect lighting handoff" => Strings.BloatDependency1,
            "may affect fan/performance ownership" => Strings.BloatDependency2,
            "safe only after OmenCore hardware validation" => Strings.BloatDependency3,
            "may affect HP hotkeys" => Strings.BloatDependency4,
            "Game Pass/Xbox dependency" => Strings.BloatDependency5,
            "security component" => Strings.BloatDependency6,
            _ => value
        };
    }
}
