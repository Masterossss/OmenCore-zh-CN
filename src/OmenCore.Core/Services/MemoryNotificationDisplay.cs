using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace OmenCore.Services
{
    // The core service has no dependency on the WPF assembly. When hosted by the app,
    // use its existing catalog for presentation; other hosts retain the original English.
    internal static class MemoryNotificationDisplay
    {
        internal static (string Title, string Message) Format(string amount, int operations, Assembly? resourceAssembly)
        {
            var fallbackTitle = $"Freed {amount}";
            var fallbackMessage = $"{operations} cleanup operation{(operations == 1 ? "" : "s")} completed";
            if (resourceAssembly == null) return (fallbackTitle, fallbackMessage);
            try
            {
                var resources = new ResourceManager("OmenCore.Localization.Strings", resourceAssembly);
                var title = resources.GetString("MemoryToastFreed", CultureInfo.CurrentUICulture);
                var message = resources.GetString(operations == 1 ? "MemoryToastOperationOne" : "MemoryToastOperationMany", CultureInfo.CurrentUICulture);
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message)) return (fallbackTitle, fallbackMessage);
                return (string.Format(title, amount), string.Format(message, operations));
            }
            catch (MissingManifestResourceException) { return (fallbackTitle, fallbackMessage); }
        }

        internal static (string Title, string Message) Format(string amount, int operations) =>
            Format(amount, operations, AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "OmenCore"));
    }
}
