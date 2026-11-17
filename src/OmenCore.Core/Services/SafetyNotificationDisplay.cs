using System;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace OmenCore.Services
{
    // Optional WPF catalog at the presentation boundary; non-WPF hosts retain English.
    internal static class SafetyNotificationDisplay
    {
        internal static string Format(string key, string fallback, params object[] args)
        {
            string text = fallback;
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "OmenCore");
            if (assembly != null)
            {
                try { text = new ResourceManager("OmenCore.Localization.Strings", assembly).GetString(key, CultureInfo.CurrentUICulture) ?? fallback; }
                catch (MissingManifestResourceException) { }
            }
            return string.Format(CultureInfo.CurrentCulture, text, args);
        }
    }
}
