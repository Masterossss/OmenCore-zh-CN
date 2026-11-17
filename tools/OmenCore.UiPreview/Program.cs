using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using OmenCore.Localization;
using OmenCore.Views;

namespace OmenCore.UiPreview;

// This executable has its own entry point and never constructs OmenCore.App or any ViewModel.
// Only audited view types are instantiated with a plain, non-operational mock DataContext.
internal static class Program
{
    private static readonly (string Name, Func<UserControl> Create)[] Pages =
    {
        ("Dashboard", () => new DashboardView()), ("General", () => new GeneralView()),
        ("Advanced", () => new AdvancedView()), ("Settings", () => new SettingsView()),
        ("Tuning", () => new TuningView()), ("FanControl", () => new FanControlView()),
        ("Diagnostics", () => new DiagnosticsView()), ("Lighting", () => new LightingView()),
        ("MemoryOptimizer", () => new MemoryOptimizerView()), ("SystemOptimizer", () => new SystemOptimizerView()),
        ("BloatwareManager", () => new BloatwareManagerView()), ("GameLibrary", () => new GameLibraryView())
    };

    // Plain presentation data: no commands, services, I/O, or hardware access.
    private sealed class PreviewData
    {
        public bool IsLoading => false;
        public string CpuSummaryDisplay => "模拟 CPU · 45°C";
        public string GpuSummaryDisplay => "模拟 GPU · 42°C";
        public string CpuTempChipDisplay => "45°C（模拟）";
        public string GpuTempChipDisplay => "42°C（模拟）";
        public string CpuFanDisplay => "1500 RPM（模拟）";
        public string GpuFanDisplay => "1400 RPM（模拟）";
        public string MemorySummary => "8 / 16 GB（模拟）";
        public string BatterySummary => "80%（模拟）";
        public string CurrentPerformanceMode => "均衡模式（模拟）";
        public string CurrentFanMode => "自动（模拟）";
    }

    [STAThread]
    private static void Main(string[] args)
    {
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentUICulture;
        var app = new Application { ShutdownMode = ShutdownMode.OnMainWindowClose };
        app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/OmenCore;component/Styles/ModernStyles.xaml") });
        app.Resources["BoolToVisibility"] = new BooleanToVisibilityConverter();
        app.Resources["BoolToVisibilityConverter"] = new BooleanToVisibilityConverter();
        app.Resources["BooleanToVisibilityConverter"] = new BooleanToVisibilityConverter();
        var assembly = typeof(DashboardView).Assembly;
        foreach (var (key, type) in new[] {
            ("InverseBoolToVisibility", "InverseBoolToVisibilityConverter"), ("InverseBooleanConverter", "InverseBooleanConverter"),
            ("NullToVisibilityConverter", "NullToVisibilityConverter"), ("StringNotEmptyToVisibilityConverter", "StringNotEmptyToVisibilityConverter"),
            ("IntGreaterThanZeroToVisibilityConverter", "IntGreaterThanZeroToVisibilityConverter"), ("IntZeroToVisibilityConverter", "IntZeroToVisibilityConverter"),
            ("TemperatureToColorConverter", "TemperatureToColorConverter"), ("LoadToColorConverter", "LoadToColorConverter"),
            ("BoolToYesNo", "BoolToYesNoConverter"), ("BoolToColor", "BoolToColorConverter"), ("HexToBrushConverter", "HexToBrushConverter"),
            ("PowerToTotalConverter", "PowerToTotalConverter"), ("FanSpeedConverter", "FanSpeedConverter"),
            ("LatestFanSpeedConverter", "LatestFanSpeedConverter"), ("ZeroDoubleToPlaceholderConverter", "ZeroDoubleToPlaceholderConverter"),
            ("ModeDisplayNameConverter", "ModeDisplayNameConverter") })
        {
            var converter = assembly.GetType("OmenCore.Utils." + type);
            if (converter != null) app.Resources[key] = Activator.CreateInstance(converter)!;
        }
        var window = new Window { Title = "OmenCore 中文界面预览 — 无硬件连接 / 模拟展示", Width = 1280, Height = 850,
            Background = new SolidColorBrush(Color.FromRgb(20, 22, 30)), WindowStartupLocation = WindowStartupLocation.CenterScreen };
        var root = new DockPanel();
        var notice = new TextBlock { Text = "界面预览：不连接设备，操作已禁用；所有数据与设备状态均为模拟或默认值，不能反映实机。", Foreground = Brushes.White,
            Background = new SolidColorBrush(Color.FromRgb(125, 53, 13)), Padding = new Thickness(12) };
        DockPanel.SetDock(notice, Dock.Top); root.Children.Add(notice);
        var nav = new ListBox { Width = 175, Background = new SolidColorBrush(Color.FromRgb(30, 33, 43)), Foreground = Brushes.White,
            DisplayMemberPath = "Name", ItemsSource = Pages.Select(p => new { Name = p.Name switch
            {
                "Dashboard" => "仪表盘", "General" => "常规", "Advanced" => "高级", "Settings" => "设置",
                "Tuning" => "性能调校", "FanControl" => "风扇控制", "Diagnostics" => "诊断", "Lighting" => "灯光",
                "MemoryOptimizer" => "内存优化", "SystemOptimizer" => "系统优化", "BloatwareManager" => "软件清理",
                "GameLibrary" => "游戏库", _ => p.Name
            } }).ToArray() };
        DockPanel.SetDock(nav, Dock.Left); root.Children.Add(nav);
        var host = new ContentControl { Margin = new Thickness(12) }; root.Children.Add(host);
        window.Content = root;
        var captureDir = args.Length == 2 && args[0] == "--capture" ? args[1] : null;
        var failures = new List<string>();
        void ShowPage(int index)
        {
            try
            {
                var view = Pages[index].Create();
                view.DataContext = new PreviewData();
                view.IsEnabled = false; // all original controls, commands and click handlers are disabled
                host.Content = view;
            }
            catch (Exception ex)
            {
                failures.Add(Pages[index].Name + ": " + ex.GetBaseException().Message);
                host.Content = new TextBlock { Text = failures[^1], Foreground = Brushes.OrangeRed, TextWrapping = TextWrapping.Wrap };
            }
        }
        nav.SelectionChanged += (_, _) => { if (nav.SelectedIndex >= 0) ShowPage(nav.SelectedIndex); };
        window.Loaded += async (_, _) =>
        {
            if (captureDir == null) { nav.SelectedIndex = 0; return; }
            Directory.CreateDirectory(captureDir);
            for (var i = 0; i < Pages.Length; i++)
            {
                nav.SelectedIndex = i;
                await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                window.UpdateLayout();
                var bitmap = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(window);
                var encoder = new PngBitmapEncoder(); encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using var file = File.Create(Path.Combine(captureDir, Pages[i].Name + ".png")); encoder.Save(file);
            }
            File.WriteAllLines(Path.Combine(captureDir, "failures.txt"), failures);
            window.Close();
        };
        app.Run(window);
    }
}
