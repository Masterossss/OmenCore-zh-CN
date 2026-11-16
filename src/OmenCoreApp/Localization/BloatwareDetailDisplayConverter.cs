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
            "HP security software - can conflict with third-party AV" => Strings.BloatDescription0,
            "HP diagnostics/telemetry tool - safe to remove" => Strings.BloatDescription1,
            "HP OMEN software - conflict-sensitive until OmenCore fan, RGB, and hotkey control are verified" => Strings.BloatDescription2,
            "OMEN lighting/control component - remove only after OmenCore keyboard lighting and OMEN key behavior are verified" => Strings.BloatDescription3,
            "HP audio enhancement software" => Strings.BloatDescription4,
            "HP system control software - OmenCore replaces this" => Strings.BloatDescription5,
            "Trial antivirus - can impact gaming performance" => Strings.BloatDescription6,
            "Xbox app - keep if you use Xbox Game Pass" => Strings.BloatDescription7,
            "Microsoft video editor" => Strings.BloatDescription8,
            "Microsoft Bing news/weather app" => Strings.BloatDescription9,
            "Windows tips and help app" => Strings.BloatDescription10,
            "Microsoft Solitaire with ads" => Strings.BloatDescription11,
            "Groove Music / Movies & TV app" => Strings.BloatDescription12,
            "Windows People app" => Strings.BloatDescription13,
            "Skype messaging app" => Strings.BloatDescription14,
            "Phone Link app" => Strings.BloatDescription15,
            "3D/Mixed Reality app - rarely used" => Strings.BloatDescription16,
            "Promotional app shortcut" => Strings.BloatDescription17,
            "Pre-installed casual game" => Strings.BloatDescription18,
            "Social media app" => Strings.BloatDescription19,
            "Dolby audio enhancement - may affect audio quality" => Strings.BloatDescription20,
            "OneDrive startup - disable if not using cloud sync" => Strings.BloatDescription21,
            "Cortana assistant startup" => Strings.BloatDescription22,
            "Microsoft Teams startup - disable if not using" => Strings.BloatDescription23,
            "HP software startup item" => Strings.BloatDescription24,
            "App startup - disable if not needed immediately" => Strings.BloatDescription25,
            "HP scheduled task" => Strings.BloatDescription26,
            "Telemetry data collection task" => Strings.BloatDescription27,
            "OneDrive scheduled sync task" => Strings.BloatDescription28,
            _ => value
        };
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
