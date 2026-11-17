using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class BloatwareRestorePointDisplayText
    {
        internal static string Translate(string value)
        {
            var known = value switch
            {
                "Administrator privileges are required to create a restore point." => Strings.BloatRestoreReason0,
                "Unknown restore point failure" => Strings.BloatRestoreReason1,
                "Restore point creation was canceled." => Strings.BloatRestoreReason2,
                "System Restore is disabled on this system. Enable it in System Properties → System Protection to create restore points." => Strings.BloatRestoreReason3,
                "System Restore WMI class not available. System Restore may not be installed." => Strings.BloatRestoreReason4,
                "Access denied. Try running OmenCore as Administrator." => Strings.BloatRestoreReason5,
                "System Restore is disabled. Enable it in System Properties → System Protection." => Strings.BloatRestoreReason6,
                "Restore point creation is currently in progress." => Strings.BloatRestoreReason7,
                "Not enough disk space for restore point." => Strings.BloatRestoreReason8,
                "A restore point was created recently. Windows limits restore point frequency." => Strings.BloatRestoreReason9,
                "Access denied. Please run OmenCore as Administrator to create restore points." => Strings.BloatRestoreReason10,
                "System Restore is not available. This can happen if:\n• System Restore is disabled in System Properties → System Protection\n• The Volume Shadow Copy service is stopped\n• Group Policy has disabled System Restore\n\nTo enable: Control Panel → System → System Protection → Configure" => Strings.BloatRestoreReason11,
                _ => null
            };
            if (known != null) return known;
            var errorCode = Regex.Match(value, @"\ASystem Restore failed with error code ([0-9]+)\z", RegexOptions.CultureInvariant);
            if (errorCode.Success) return string.Format(Strings.BloatRestoreCode, errorCode.Groups[1].Value);
            const string systemError = "System Restore error: ";
            const string createError = "Failed to create restore point: ";
            if (value.StartsWith(systemError, System.StringComparison.Ordinal)) return string.Format(Strings.BloatRestoreSystemError, value.Substring(systemError.Length));
            if (value.StartsWith(createError, System.StringComparison.Ordinal)) return string.Format(Strings.BloatRestoreCreateError, value.Substring(createError.Length));
            return value;
        }
    }
}
