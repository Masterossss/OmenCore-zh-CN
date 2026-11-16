using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    internal static class MemoryNotificationHistoryDisplay
    {
        internal static (string Title, string Message) Translate(string title, string message)
        {
            var amount = Regex.Match(title, @"\AFreed ([0-9]+(?:[.,][0-9]+)? (?:MB|GB))\z", RegexOptions.CultureInvariant);
            var count = Regex.Match(message, @"\A([0-9]+) cleanup operations? completed\z", RegexOptions.CultureInvariant);
            if (!amount.Success || !count.Success || !int.TryParse(count.Groups[1].Value, out var operations)) return (title, message);
            if ((operations == 1) != message.Contains(" operation completed")) return (title, message);
            return (string.Format(Strings.MemoryToastFreed, amount.Groups[1].Value),
                string.Format(operations == 1 ? Strings.MemoryToastOperationOne : Strings.MemoryToastOperationMany, operations));
        }
    }
}
