using System;
using System.Globalization;
using System.Windows.Data;

namespace OmenCore.Localization
{
    public sealed class BloatwareDetailDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value switch
        {
            "Item was already removed in this session." => Strings.BloatDetail0,
            "Package was already absent before removal attempt." => Strings.BloatDetail1,
            "AppX package removal verified across current user, all users, and provisioned scopes." => Strings.BloatDetail2,
            "Could not parse Win32 uninstall command." => Strings.BloatDetail3,
            "Win32 app uninstall verified; registry no longer reports the app as installed." => Strings.BloatDetail4,
            "Startup file moved out of the startup folder successfully." => Strings.BloatDetail5,
            "Startup registry key could not be opened for removal." => Strings.BloatDetail6,
            "Startup registry entry removed successfully." => Strings.BloatDetail7,
            "Scheduled task disabled successfully." => Strings.BloatDetail8,
            "Restore canceled before completion." => Strings.BloatDetail9,
            "Skipped because bulk restore was canceled before this item was processed." => Strings.BloatDetail10,
            _ => value
        };
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
