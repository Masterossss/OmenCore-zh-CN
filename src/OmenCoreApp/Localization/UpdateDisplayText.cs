using System;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Localizes known update messages for display; unknown details remain unchanged.</summary>
    internal static class UpdateDisplayText
    {
        internal static string Translate(string message)
        {
            if (string.IsNullOrEmpty(message)) return message;
            switch (message)
            {
                case "No release data returned from GitHub.": return Strings.UpdateNoRelease;
                case "Invalid release data from server.": return Strings.UpdateInvalidRelease;
                case "You are running the latest version.": return Strings.UpdateLatest;
                case "Update available on GitHub releases.": return Strings.UpdateGitHubAvailable;
                case "Installer file not found.": return Strings.UpdateInstallerMissing;
                case "Downloaded file is not a valid Windows executable. The download may have been corrupted. Please try again or download manually from GitHub.": return Strings.UpdateInvalidExecutable;
                case "Update installer launched. Application will restart after installation.": return Strings.UpdateInstallerLaunched;
                case "Initializing download...": return Strings.UpdateInitializing;
                case "Update requires manual download (missing SHA256 in release notes).": return Strings.UpdateManualHash;
                case "Download failed. Check Release Notes for manual download.": return Strings.UpdateDownloadFailed;
                case "Install blocked until SHA256 is provided": return Strings.UpdateHashBlocked;
                case "Download failed": return Strings.UpdateDownloadFailedShort;
                case "Launching installer...": return Strings.UpdateLaunching;
                case "Installation failed": return Strings.UpdateInstallationFailed;
                case "Security verification failed": return Strings.UpdateSecurityFailed;
                case "Hash verification failed - update rejected for security": return Strings.UpdateHashRejected;
                case "Error occurred": return Strings.UpdateErrorOccurred;
                case "Checking for updates...": return Strings.AboutChecking;
                case "Installing update...": return Strings.AboutInstalling;
            }
            var patterns = new (string Pattern, string Format)[]
            {
                (@"\AInvalid version format: ([^\r\n]+)\z", Strings.UpdateInvalidVersion),
                (@"\ANetwork error: ([^\r\n]+)\z", Strings.UpdateNetworkError),
                (@"\AUpdate check failed: ([^\r\n]+)\z", Strings.UpdateCheckError),
                (@"\AInstallation failed: ([^\r\n]+)\z", Strings.UpdateInstallError),
                (@"\AUpdate failed: ([^\r\n]+)\z", Strings.UpdateBannerError),
                (@"\Av([0-9A-Za-z._+\-]+) is now available\z", Strings.UpdateVersionAvailable),
                (@"\AUpdate available: v([0-9A-Za-z._+\-]+) \(Current ([^\r\n]+)\)\z", Strings.UpdateBannerAvailable),
                (@"\ADownloading v([0-9A-Za-z._+\-]+) \(([^\r\n]+)\)\z", Strings.UpdateBannerDownloading),
                (@"\AThis is the portable version of OmenCore, which can\x27t replace itself while it\x27s running\. The update has been downloaded to ([^\r\n]+)\. Close OmenCore, then extract that zip over your current OmenCore folder\.\z", Strings.UpdatePortableMessage),
            };
            foreach (var (pattern, format) in patterns)
            {
                var m = Regex.Match(message, pattern, RegexOptions.CultureInvariant);
                if (m.Success)
                    return m.Groups.Count > 2 ? string.Format(format, m.Groups[1].Value, m.Groups[2].Value) : string.Format(format, m.Groups[1].Value);
            }
            return message;
        }
    }
}
