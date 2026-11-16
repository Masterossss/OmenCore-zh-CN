namespace OmenCore.Localization
{
    /// <summary>Maps known service results for display without changing service outcomes or logs.</summary>
    internal static class GpuDiagnosticDisplayText
    {
        internal static string Translate(string message) => message switch
        {
            "The GPU power unlock is disabled in this build: its EC write path has not been validated on real 8D87 hardware yet." => Strings.GpuUnlockDisabledReason,
            "No EC access is available - the unlock needs PawnIO." => Strings.GpuUnlockNoEcReason,
            "The connected adapter's power verdict could not be read." => Strings.GpuUnlockAdapterUnknownReason,
            "This is a USB-C Power Delivery supply. The GPU power unlock is not offered there - it was only ever measured on barrel adapters." => Strings.GpuUnlockUsbCReason,
            "The connected adapter's rating was not reported, so there is no way to judge whether it can carry the unlock safely." => Strings.GpuUnlockAdapterRatingUnknown,
            _ => message
        };
    }
}
