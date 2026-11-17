from pathlib import Path
import re
root=Path(__file__).resolve().parents[2]
host=(root/'tools/OmenCore.UiPreview/Program.cs').read_text(encoding='utf-8-sig')
views={'DashboardView','GeneralView','AdvancedView','SettingsView','TuningView','FanControlView','DiagnosticsView','LightingView','MemoryOptimizerView','SystemOptimizerView','BloatwareManagerView','GameLibraryView'}
constructed=set(re.findall(r'new\s+(\w+View)\s*\(',host))
assert constructed==views,(constructed,views)
for text in ['view.DataContext = new PreviewData();', 'view.IsEnabled = false;', 'host.Content = view;', 'new Application { ShutdownMode']:
 assert text in host,text
assert host.index('view.DataContext = new PreviewData();') < host.index('view.IsEnabled = false;') < host.index('host.Content = view;')
for token in ['new App(', 'new MainViewModel(', 'HardwareWatchdogService', 'FanService(', 'Process.Start(', 'ServiceProvider', 'InstallDriver', 'RestoreSettingsOnStartup']:
 assert token not in host,token
allowed={'FanCurveChart','GpuVcChart','LoadChart','LoadingOverlay','Sparkline','ThermalChart','FanCurveEditor','FanControlView','FanDiagnosticsView','KeyboardDiagnosticsView','ColorPickerControl','KeyboardMapEditor'}
for name in views|{'FanDiagnosticsView','KeyboardDiagnosticsView','KeyboardMapEditor'}:
 xaml=(root/'src/OmenCoreApp/Views'/f'{name}.xaml').read_text(encoding='utf-8')
 found=set(re.findall(r'<(?:controls|views|local):([A-Za-z0-9]+)',xaml))
 assert found<=allowed,(name,found-allowed)
 assert 'ObjectDataProvider' not in xaml,name
print('UI preview entry, mock context, disabled controls and audited nested-view allowlist: PASS')
