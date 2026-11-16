using System.Globalization;
using System.Resources;

namespace OmenCore.Localization
{
    /// <summary>
    /// Typed access to <c>Localization/Strings.resx</c> and its satellite catalogs (roadmap WP-32).
    ///
    /// Written by hand rather than generated: the SDK only produces a designer class when Visual
    /// Studio's custom tool runs, so a CLI build (which is what CI is) would otherwise leave the
    /// class missing. Each property is one line, and <c>StringsResourceTests</c> fails if a key in
    /// the .resx has no property here or the other way round - that test is what replaces the
    /// generator's guarantee.
    ///
    /// A missing key returns its own name instead of throwing or rendering nothing: the user sees
    /// something obviously unfinished on that one label, and the app still starts. The parity test
    /// means that should never reach a release.
    /// </summary>
    public static class Strings
    {
        private static readonly ResourceManager Resources =
            new(typeof(Strings).FullName!, typeof(Strings).Assembly);

        private static string Get(string key) =>
            Resources.GetString(key, CultureInfo.CurrentUICulture) ?? key;

        // Window chrome
        public static string WindowTitle => Get(nameof(WindowTitle));
        public static string WindowMinimize => Get(nameof(WindowMinimize));
        public static string WindowMaximize => Get(nameof(WindowMaximize));
        public static string WindowRestore => Get(nameof(WindowRestore));
        public static string WindowClose => Get(nameof(WindowClose));

        // Navigation rail
        public static string NavGeneral => Get(nameof(NavGeneral));
        public static string NavCustom => Get(nameof(NavCustom));
        public static string NavTuning => Get(nameof(NavTuning));
        public static string NavDiagnostics => Get(nameof(NavDiagnostics));
        public static string NavMonitoring => Get(nameof(NavMonitoring));
        public static string NavOptimizer => Get(nameof(NavOptimizer));
        public static string NavMemory => Get(nameof(NavMemory));
        public static string NavBloatware => Get(nameof(NavBloatware));
        public static string NavRgb => Get(nameof(NavRgb));
        public static string NavSettings => Get(nameof(NavSettings));
        public static string NavGames => Get(nameof(NavGames));

        // Settings page: window behaviour
        public static string SettingsWindowBehaviour => Get(nameof(SettingsWindowBehaviour));
        public static string SettingsStartWithWindows => Get(nameof(SettingsStartWithWindows));
        public static string SettingsStartWithWindowsHelp => Get(nameof(SettingsStartWithWindowsHelp));
        public static string SettingsStartMinimized => Get(nameof(SettingsStartMinimized));
        public static string SettingsStartMinimizedHelp => Get(nameof(SettingsStartMinimizedHelp));
        public static string SettingsLiteMode => Get(nameof(SettingsLiteMode));
        public static string SettingsLiteModeHelp => Get(nameof(SettingsLiteModeHelp));
        public static string SettingsHeadlessMode => Get(nameof(SettingsHeadlessMode));
        public static string SettingsHeadlessModeHelp => Get(nameof(SettingsHeadlessModeHelp));
        public static string SettingsSoftwareRendering => Get(nameof(SettingsSoftwareRendering));
        public static string SettingsSoftwareRenderingHelp => Get(nameof(SettingsSoftwareRenderingHelp));
        public static string SettingsReduceMotion => Get(nameof(SettingsReduceMotion));
        public static string SettingsReduceMotionHelp => Get(nameof(SettingsReduceMotionHelp));
        public static string SettingsLanguage => Get(nameof(SettingsLanguage));
        public static string SettingsLanguageHelp => Get(nameof(SettingsLanguageHelp));
        public static string SettingsMinimizeToTray => Get(nameof(SettingsMinimizeToTray));
        public static string SettingsMinimizeToTrayHelp => Get(nameof(SettingsMinimizeToTrayHelp));
        public static string SettingsStayOnTop => Get(nameof(SettingsStayOnTop));
        public static string SettingsStayOnTopHelp => Get(nameof(SettingsStayOnTopHelp));
        public static string SettingsTopmostBadge => Get(nameof(SettingsTopmostBadge));
        public static string SettingsActiveBadge => Get(nameof(SettingsActiveBadge));

        // Settings page: tray and interface
        public static string SettingsTrayAndUi => Get(nameof(SettingsTrayAndUi));
        public static string SettingsDisplayScale => Get(nameof(SettingsDisplayScale));
        public static string SettingsDisplayScaleHelp => Get(nameof(SettingsDisplayScaleHelp));
        public static string SettingsQuickAccess => Get(nameof(SettingsQuickAccess));
        public static string SettingsQuickAccessHelp => Get(nameof(SettingsQuickAccessHelp));
        public static string SettingsQuickPopup => Get(nameof(SettingsQuickPopup));
        public static string SettingsQuickPopupHelp => Get(nameof(SettingsQuickPopupHelp));
        public static string SettingsTrayTemperature => Get(nameof(SettingsTrayTemperature));
        public static string SettingsTrayTemperatureHelp => Get(nameof(SettingsTrayTemperatureHelp));
        public static string SettingsCorsairHidOnly => Get(nameof(SettingsCorsairHidOnly));
        public static string SettingsCorsairHidOnlyHelp => Get(nameof(SettingsCorsairHidOnlyHelp));
        public static string SettingsCorsairHidOnlyWarning => Get(nameof(SettingsCorsairHidOnlyWarning));
        public static string SettingsTelemetry => Get(nameof(SettingsTelemetry));
        public static string SettingsTelemetryHelp => Get(nameof(SettingsTelemetryHelp));
        public static string AboutTitle => Get(nameof(AboutTitle));
        public static string AboutTagline => Get(nameof(AboutTagline));
        public static string AboutCloseEsc => Get(nameof(AboutCloseEsc));
        public static string AboutDescription => Get(nameof(AboutDescription));
        public static string AboutFanFeature => Get(nameof(AboutFanFeature));
        public static string AboutCpuFeature => Get(nameof(AboutCpuFeature));
        public static string AboutGpuFeature => Get(nameof(AboutGpuFeature));
        public static string AboutMonitoringFeature => Get(nameof(AboutMonitoringFeature));
        public static string AboutGameFeature => Get(nameof(AboutGameFeature));
        public static string AboutKeyboardFeature => Get(nameof(AboutKeyboardFeature));
        public static string AboutDiagnosticsFeature => Get(nameof(AboutDiagnosticsFeature));
        public static string AboutMemoryFeature => Get(nameof(AboutMemoryFeature));
        public static string AboutHeadlessFeature => Get(nameof(AboutHeadlessFeature));
        public static string AboutRealtimeFeature => Get(nameof(AboutRealtimeFeature));
        public static string AboutCheckUpdates => Get(nameof(AboutCheckUpdates));
        public static string AboutCheckUpdatesHelp => Get(nameof(AboutCheckUpdatesHelp));
        public static string AboutCloseHelp => Get(nameof(AboutCloseHelp));
        public static string AboutVersion => Get(nameof(AboutVersion));
        public static string AboutChecking => Get(nameof(AboutChecking));
        public static string AboutUpdateAvailableStatus => Get(nameof(AboutUpdateAvailableStatus));
        public static string AboutOpenRelease => Get(nameof(AboutOpenRelease));
        public static string AboutUpdateAvailable => Get(nameof(AboutUpdateAvailable));
        public static string AboutInstallPrompt => Get(nameof(AboutInstallPrompt));
        public static string AboutDownloading => Get(nameof(AboutDownloading));
        public static string AboutInstalling => Get(nameof(AboutInstalling));
        public static string AboutDownloadFailedStatus => Get(nameof(AboutDownloadFailedStatus));
        public static string AboutDownloadFailedPrompt => Get(nameof(AboutDownloadFailedPrompt));
        public static string AboutDownloadFailed => Get(nameof(AboutDownloadFailed));
        public static string AboutCheckFailed => Get(nameof(AboutCheckFailed));
        public static string CommonCancel => Get(nameof(CommonCancel));
        public static string CommonApply => Get(nameof(CommonApply));
        public static string CommonOk => Get(nameof(CommonOk));
        public static string ColorSelect => Get(nameof(ColorSelect));
        public static string ColorSelectZone => Get(nameof(ColorSelectZone));
        public static string ColorZoneDefault => Get(nameof(ColorZoneDefault));
        public static string ColorCancelEsc => Get(nameof(ColorCancelEsc));
        public static string ColorZoneFormat => Get(nameof(ColorZoneFormat));
        public static string ColorZoneLeft => Get(nameof(ColorZoneLeft));
        public static string ColorZoneRight => Get(nameof(ColorZoneRight));
        public static string ColorZoneFarRight => Get(nameof(ColorZoneFarRight));
        public static string DpiSaveTitle => Get(nameof(DpiSaveTitle));
        public static string DpiSavePrompt => Get(nameof(DpiSavePrompt));
        public static string DpiOverwriteTitle => Get(nameof(DpiOverwriteTitle));
        public static string DpiOverwritePrompt => Get(nameof(DpiOverwritePrompt));
        public static string DashboardText01 => Get(nameof(DashboardText01));
        public static string DashboardText02 => Get(nameof(DashboardText02));
        public static string DashboardText03 => Get(nameof(DashboardText03));
        public static string DashboardText04 => Get(nameof(DashboardText04));
        public static string DashboardText05 => Get(nameof(DashboardText05));
        public static string DashboardText06 => Get(nameof(DashboardText06));
        public static string DashboardText07 => Get(nameof(DashboardText07));
        public static string DashboardText08 => Get(nameof(DashboardText08));
        public static string DashboardText09 => Get(nameof(DashboardText09));
        public static string DashboardText10 => Get(nameof(DashboardText10));
        public static string DashboardText11 => Get(nameof(DashboardText11));
        public static string DashboardText12 => Get(nameof(DashboardText12));
        public static string DashboardText13 => Get(nameof(DashboardText13));
        public static string DashboardText14 => Get(nameof(DashboardText14));
        public static string DashboardText15 => Get(nameof(DashboardText15));
        public static string DashboardText16 => Get(nameof(DashboardText16));
        public static string DashboardText17 => Get(nameof(DashboardText17));
        public static string DashboardText18 => Get(nameof(DashboardText18));
        public static string DashboardText19 => Get(nameof(DashboardText19));
        public static string DashboardText20 => Get(nameof(DashboardText20));
        public static string DashboardText21 => Get(nameof(DashboardText21));
        public static string DashboardText22 => Get(nameof(DashboardText22));
        public static string DashboardText23 => Get(nameof(DashboardText23));
        public static string DashboardText24 => Get(nameof(DashboardText24));
        public static string DashboardText25 => Get(nameof(DashboardText25));
        public static string DashboardText26 => Get(nameof(DashboardText26));
        public static string DashboardText27 => Get(nameof(DashboardText27));
        public static string DashboardText28 => Get(nameof(DashboardText28));
        public static string DashboardText29 => Get(nameof(DashboardText29));
        public static string DashboardText30 => Get(nameof(DashboardText30));
        public static string DashboardText31 => Get(nameof(DashboardText31));
        public static string DashboardText32 => Get(nameof(DashboardText32));
        public static string DashboardText33 => Get(nameof(DashboardText33));
        public static string DashboardText34 => Get(nameof(DashboardText34));
        public static string DashboardText35 => Get(nameof(DashboardText35));
        public static string DashboardText36 => Get(nameof(DashboardText36));
        public static string DashboardText37 => Get(nameof(DashboardText37));
        public static string DashboardText38 => Get(nameof(DashboardText38));
        public static string DashboardText39 => Get(nameof(DashboardText39));
        public static string DashboardText40 => Get(nameof(DashboardText40));
        public static string DashboardText41 => Get(nameof(DashboardText41));
        public static string DashboardText42 => Get(nameof(DashboardText42));
        public static string GeneralViewText126 => Get(nameof(GeneralViewText126));
        public static string GeneralViewText127 => Get(nameof(GeneralViewText127));
        public static string GeneralViewText160 => Get(nameof(GeneralViewText160));
        public static string GeneralViewText161 => Get(nameof(GeneralViewText161));
        public static string GeneralViewText177 => Get(nameof(GeneralViewText177));
        public static string GeneralViewText179 => Get(nameof(GeneralViewText179));
        public static string GeneralViewText193 => Get(nameof(GeneralViewText193));
        public static string GeneralViewText214 => Get(nameof(GeneralViewText214));
        public static string GeneralViewText216 => Get(nameof(GeneralViewText216));
        public static string GeneralViewText222 => Get(nameof(GeneralViewText222));
        public static string GeneralViewText225 => Get(nameof(GeneralViewText225));
        public static string GeneralViewText246 => Get(nameof(GeneralViewText246));
        public static string GeneralViewText266 => Get(nameof(GeneralViewText266));
        public static string GeneralViewText268 => Get(nameof(GeneralViewText268));
        public static string GeneralViewText273 => Get(nameof(GeneralViewText273));
        public static string GeneralViewText276 => Get(nameof(GeneralViewText276));
        public static string GeneralViewText296 => Get(nameof(GeneralViewText296));
        public static string GeneralViewText316 => Get(nameof(GeneralViewText316));
        public static string GeneralViewText318 => Get(nameof(GeneralViewText318));
        public static string GeneralViewText323 => Get(nameof(GeneralViewText323));
        public static string GeneralViewText326 => Get(nameof(GeneralViewText326));
        public static string GeneralViewText346 => Get(nameof(GeneralViewText346));
        public static string GeneralViewText366 => Get(nameof(GeneralViewText366));
        public static string GeneralViewText368 => Get(nameof(GeneralViewText368));
        public static string GeneralViewText373 => Get(nameof(GeneralViewText373));
        public static string GeneralViewText376 => Get(nameof(GeneralViewText376));
        public static string GeneralViewText401 => Get(nameof(GeneralViewText401));
        public static string GeneralViewText413 => Get(nameof(GeneralViewText413));
        public static string GeneralViewText414 => Get(nameof(GeneralViewText414));
        public static string GeneralViewText426 => Get(nameof(GeneralViewText426));
        public static string GeneralViewText427 => Get(nameof(GeneralViewText427));
        public static string GeneralViewText439 => Get(nameof(GeneralViewText439));
        public static string GeneralViewText440 => Get(nameof(GeneralViewText440));
        public static string GeneralViewText503 => Get(nameof(GeneralViewText503));
        public static string GeneralViewText521 => Get(nameof(GeneralViewText521));
        public static string GeneralViewText544 => Get(nameof(GeneralViewText544));
        public static string GeneralViewText614 => Get(nameof(GeneralViewText614));
        public static string GeneralViewText632 => Get(nameof(GeneralViewText632));
        public static string GeneralViewText655 => Get(nameof(GeneralViewText655));
        public static string GeneralViewText696 => Get(nameof(GeneralViewText696));
        public static string GeneralViewText731 => Get(nameof(GeneralViewText731));
        public static string AdvancedViewText55 => Get(nameof(AdvancedViewText55));
        public static string AdvancedViewText56 => Get(nameof(AdvancedViewText56));
        public static string AdvancedViewText88 => Get(nameof(AdvancedViewText88));
        public static string AdvancedViewText90 => Get(nameof(AdvancedViewText90));
        public static string AdvancedViewText125 => Get(nameof(AdvancedViewText125));
        public static string AdvancedViewText127 => Get(nameof(AdvancedViewText127));
        public static string AdvancedViewText133 => Get(nameof(AdvancedViewText133));
        public static string AdvancedViewText136 => Get(nameof(AdvancedViewText136));
        public static string AdvancedViewText146 => Get(nameof(AdvancedViewText146));
        public static string AdvancedViewText180 => Get(nameof(AdvancedViewText180));
        public static string AdvancedViewText182 => Get(nameof(AdvancedViewText182));
        public static string AdvancedViewText187 => Get(nameof(AdvancedViewText187));
        public static string AdvancedViewText190 => Get(nameof(AdvancedViewText190));
        public static string AdvancedViewText199 => Get(nameof(AdvancedViewText199));
        public static string AdvancedViewText233 => Get(nameof(AdvancedViewText233));
        public static string AdvancedViewText235 => Get(nameof(AdvancedViewText235));
        public static string AdvancedViewText240 => Get(nameof(AdvancedViewText240));
        public static string AdvancedViewText243 => Get(nameof(AdvancedViewText243));
        public static string AdvancedViewText252 => Get(nameof(AdvancedViewText252));
        public static string AdvancedViewText267 => Get(nameof(AdvancedViewText267));
        public static string AdvancedViewText276 => Get(nameof(AdvancedViewText276));
        public static string AdvancedViewText288 => Get(nameof(AdvancedViewText288));
        public static string AdvancedViewText313 => Get(nameof(AdvancedViewText313));
        public static string AdvancedViewText318 => Get(nameof(AdvancedViewText318));
        public static string AdvancedViewText321 => Get(nameof(AdvancedViewText321));
        public static string AdvancedViewText343 => Get(nameof(AdvancedViewText343));
        public static string AdvancedViewText382 => Get(nameof(AdvancedViewText382));
        public static string AdvancedViewText396 => Get(nameof(AdvancedViewText396));
        public static string AdvancedViewText415 => Get(nameof(AdvancedViewText415));
        public static string AdvancedViewText428 => Get(nameof(AdvancedViewText428));
        public static string AdvancedViewText441 => Get(nameof(AdvancedViewText441));
        public static string AdvancedViewText444 => Get(nameof(AdvancedViewText444));
        public static string AdvancedViewText458 => Get(nameof(AdvancedViewText458));
        public static string AdvancedViewText469 => Get(nameof(AdvancedViewText469));
        public static string AdvancedViewText488 => Get(nameof(AdvancedViewText488));
        public static string AdvancedViewText502 => Get(nameof(AdvancedViewText502));
        public static string AdvancedViewText526 => Get(nameof(AdvancedViewText526));
        public static string AdvancedViewText527 => Get(nameof(AdvancedViewText527));
        public static string AdvancedViewText530 => Get(nameof(AdvancedViewText530));
        public static string AdvancedViewText533 => Get(nameof(AdvancedViewText533));
        public static string AdvancedViewText552 => Get(nameof(AdvancedViewText552));
        public static string AdvancedViewText553 => Get(nameof(AdvancedViewText553));
        public static string AdvancedViewText561 => Get(nameof(AdvancedViewText561));
        public static string AdvancedViewText562 => Get(nameof(AdvancedViewText562));
        public static string FanDiagnosticsViewText10 => Get(nameof(FanDiagnosticsViewText10));
        public static string FanDiagnosticsViewText13 => Get(nameof(FanDiagnosticsViewText13));
        public static string FanDiagnosticsViewText15 => Get(nameof(FanDiagnosticsViewText15));
        public static string FanDiagnosticsViewText16 => Get(nameof(FanDiagnosticsViewText16));
        public static string FanDiagnosticsViewText19 => Get(nameof(FanDiagnosticsViewText19));
        public static string FanDiagnosticsViewText26 => Get(nameof(FanDiagnosticsViewText26));
        public static string FanDiagnosticsViewText27 => Get(nameof(FanDiagnosticsViewText27));
        public static string FanDiagnosticsViewText33 => Get(nameof(FanDiagnosticsViewText33));
        public static string FanDiagnosticsViewText38 => Get(nameof(FanDiagnosticsViewText38));
        public static string FanDiagnosticsViewText42 => Get(nameof(FanDiagnosticsViewText42));
        public static string FanDiagnosticsViewText50 => Get(nameof(FanDiagnosticsViewText50));
        public static string FanDiagnosticsViewText51 => Get(nameof(FanDiagnosticsViewText51));
        public static string FanDiagnosticsViewText52 => Get(nameof(FanDiagnosticsViewText52));
        public static string FanDiagnosticsViewText73 => Get(nameof(FanDiagnosticsViewText73));
        public static string FanDiagnosticsViewText83 => Get(nameof(FanDiagnosticsViewText83));
        public static string FanDiagnosticsViewText89 => Get(nameof(FanDiagnosticsViewText89));
        public static string FanDiagnosticsViewText97 => Get(nameof(FanDiagnosticsViewText97));
        public static string FanDiagnosticsViewText106 => Get(nameof(FanDiagnosticsViewText106));
    }
}
