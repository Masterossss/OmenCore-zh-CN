using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace OmenCore.Localization
{
    /// <summary>Maps known backend text for display only; unrecognized diagnostic details remain intact.</summary>
    internal static class NvidiaPowerDisplayText
    {
        internal static string Translate(string message)
        {
            if (string.IsNullOrEmpty(message)) return message;
            if (message.Contains(" | ", StringComparison.Ordinal))
                return string.Join(" | ", message.Split(new[] { " | " }, StringSplitOptions.None).Select(TranslatePart));
            return TranslatePart(message);
        }

        private static string TranslatePart(string message)
        {
            foreach (var prefix in new[] { "driver: ", "VBIOS: ", "policy: " })
            {
                if (!message.StartsWith(prefix, StringComparison.Ordinal)) continue;
                var body = message.Substring(prefix.Length);
                var separator = body.IndexOf(" — ", StringComparison.Ordinal);
                return separator >= 0
                    ? prefix + body.Substring(0, separator + 3) + TranslateExact(body.Substring(separator + 3))
                    : prefix + TranslateExact(body);
            }
            return TranslateExact(message);
        }

        private static string TranslateExact(string message) => message switch
        {
            "GPU profile is unknown." => Strings.NvidiaReasonGpuProfileUnknown,
            "GPU profile is unavailable." => Strings.NvidiaReasonGpuProfileUnavailable,
            "Unsupported GPU profile." => Strings.NvidiaReasonUnsupportedProfile,
            "Unsupported GPU. Supported family: RTX 4050/4060/4070/4080/4090 and RTX 5050/5060/5070/5070 Ti/5080/5090 Laptop." => Strings.NvidiaReasonUnsupportedGpu,
            "Unknown VBIOS. The automatic read-only ROM resolver must validate this exact GPU/VBIOS before MAX writes are enabled." => Strings.NvidiaReasonVbiosUnknown,
            "Reference RTX 5070 Ti VBIOS profile verified." => Strings.NvidiaReasonVbiosReference,
            "ROM file does not exist." => Strings.NvidiaReasonRomMissing,
            "ROM image is unexpectedly small." => Strings.NvidiaReasonRomSmall,
            "No NVIDIA legacy code-type-0 image was found in the ROM dump." => Strings.NvidiaReasonRomNoLegacy,
            "nvapi64_impl.dll file is unavailable." => Strings.NvidiaReasonDriverDllMissing,
            "Transport signature matched but RVA conversion failed." => Strings.NvidiaReasonDriverRvaFailed,
            "Public NVIDIA CURRENT/MAX readback is unavailable." => Strings.NvidiaReasonReadbackUnavailable,
            "CURRENT exceeds live MAX." => Strings.NvidiaReasonCurrentExceedsMax,
            "Installed MAX override is outside the detected GPU profile range." => Strings.NvidiaReasonOverrideRange,
            "Tool MAX override is installed but the live NVIDIA policy has not rebuilt to that value yet." => Strings.NvidiaReasonOverridePending,
            "Live CURRENT/MAX match the detected stock profile." => Strings.NvidiaReasonPolicyStock,
            "MAX override is active; CURRENT remains at the stock value." => Strings.NvidiaReasonPolicyOverride,
            "CURRENT and MAX are aligned." => Strings.NvidiaReasonPolicyAligned,
            "Live MAX differs from the nominal profile and no tool-owned override is installed. This may be an OEM/external policy." => Strings.NvidiaReasonPolicyExternal,
            "MAX target is outside this GPU profile range." => Strings.NvidiaReasonMaxTargetRange,
            "CURRENT target is outside this GPU profile range." => Strings.NvidiaReasonCurrentTargetRange,
            "NVIDIA driver registry key not found." => Strings.NvidiaReasonDriverRegistryMissing,
            "No MAX override is installed." => Strings.NvidiaReasonNoOverride,
            "MAX override removed. Reboot Windows to restore the factory MAX. After reboot the NVIDIA policy will be rebuilt from stock VBIOS data." => Strings.NvidiaReasonMaxRemoveReboot,
            "Finish OmenCore fan diagnostics before changing GPU CURRENT." => Strings.NvidiaReasonDiagnosticsActive,
            "OmenCore could not prepare performance cooling. CURRENT was not sent." => Strings.NvidiaReasonCoolingFailed,
            "Fan Max could not be preserved. CURRENT was not sent." => Strings.NvidiaReasonFanMaxFailed,
            "E633 no-op changed CURRENT unexpectedly; writes remain locked." => Strings.NvidiaReasonNoopChanged,
            "CURRENT/MAX form a valid ordered policy state." => Strings.NvidiaReasonPolicyOrdered,
            _ => TranslateParameterized(message)
        };

        // Match complete, known backend messages only. Captured digits remain strings so no numeric
        // reformatting occurs; unknown or changed messages are displayed exactly as received.
        private static string TranslateParameterized(string message)
        {
            var patterns = new (string Pattern, string Format)[]
            {
                (@"\ACURRENT confirmed at ([0-9]+) W\.\z", Strings.NvidiaParamCurrentConfirmed),
                (@"\AChange CURRENT while GPU is idle\. Current GPU utilization is ([0-9]+)%\.\z", Strings.NvidiaParamCurrentIdle),
                (@"\AClose GPU workloads before driver validation\. GPU utilization is ([0-9]+)%\.\z", Strings.NvidiaParamCloseWorkloads),
                (@"\ACURRENT ([0-9]+) W exceeds live MAX ([0-9]+\.[0-9]{2}) W\. Apply MAX first, reboot Windows, then verify the new live MAX before applying CURRENT\.\z", Strings.NvidiaParamCurrentAboveMax),
                (@"\AMAX ([0-9]+) W was saved to NVIDIA romOverride\. Reboot is required\. After reboot reopen Prophecy Power Unlocker, verify MAX, then apply CURRENT\.\z", Strings.NvidiaParamMaxSaved),
                (@"\ARestore CURRENT to stock ([0-9]+) W while GPU is idle before removing MAX override\.\z", Strings.NvidiaParamRestoreCurrent)
            };
            foreach (var (pattern, format) in patterns)
            {
                var match = Regex.Match(message, pattern, RegexOptions.CultureInvariant);
                if (!match.Success) continue;
                var args = match.Groups.Cast<Group>().Skip(1).Select(group => (object)group.Value).ToArray();
                return string.Format(format, args);
            }
            return message;
        }
    }
}
