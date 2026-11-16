# 界面汉化覆盖矩阵（未完成）

本矩阵列出 WPF Views/Controls 源文件作为核查范围，文件存在不代表运行时实际加载；实际使用情况仍需逐项追踪。静态和动态列为保守状态，不因存在资源而标记完成。全局资源一致性以最近一次测试为准，不证明每个绑定都正确。GUI 尚未执行。

| 文件 | 运行时引用线索 | 静态文字 | 动态文字 | 风险提示 | 资源一致性 | 纯软件测试 | 中英文页面切换 | GUI | 未解决位置 | 状态 |
|---|---|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/Controls/DiagnosticExportControl.xaml` | 未找到直接引用；待追踪 | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCalibrationControl.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/FanControlViewModel.cs` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCurveChart.xaml` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCurveEditor.xaml` | 源码引用：`src/OmenCoreApp/Views/FanControlView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/GpuVcChart.xaml` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/HardwareMonitoringDashboard.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Controls/LoadChart.xaml` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/LoadingOverlay.xaml` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/ThermalChart.xaml` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/AboutWindow.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/MainViewModel.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/AdvancedView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/BloatwareManagerView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 扫描、删除和还原进度部分处理；卸载服务状态及辅助功能待核查 | 进行中 |
| `src/OmenCoreApp/Views/ColorPickerDialog.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/LightingViewModel.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/DashboardView.xaml` | 未找到直接引用；待追踪 | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/DiagnosticsView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/FanControlView.xaml` | 源码引用：`src/OmenCoreApp/Views/AdvancedView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/FanDiagnosticsView.xaml` | 源码引用：`src/OmenCoreApp/Views/DiagnosticsView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GameLibraryView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GameProfileManagerView.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/MainViewModel.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GeneralView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/MainViewModel.cs` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 热键来源已处理；模式名、调用方与静态内容待核查 | 进行中 |
| `src/OmenCoreApp/Views/InputPromptWindow.xaml` | 源码引用：`src/OmenCoreApp/ViewModels/LightingViewModel.cs` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/KeyboardDiagnosticsView.xaml` | 源码引用：`src/OmenCoreApp/Views/DiagnosticsView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/KeyboardMapEditor.xaml` | 源码引用：`src/OmenCoreApp/Views/LightingView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/LightingView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 已知后端不可用与保存错误已处理；参数化应用状态、探测结果及辅助功能待核查 | 进行中 |
| `src/OmenCoreApp/Views/MainWindow.xaml` | 未找到直接引用；待追踪 | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/MemoryOptimizerView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 清理进度、已知错误和标准 delta 已处理；其他服务错误及辅助文本待核查 | 进行中 |
| `src/OmenCoreApp/Views/OnboardingWindow.xaml` | 未找到直接引用；待追踪 | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/OsdOverlayWindow.xaml` | 源码引用：`src/OmenCoreApp/Services/OsdService.cs` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/QuickPopupWindow.xaml` | 源码引用：`src/OmenCoreApp/Utils/TrayIconService.cs` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 托盘调用、绑定与布局仍需核查 | 进行中 |
| `src/OmenCoreApp/Views/SettingsView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/SplashWindow.xaml` | 未找到直接引用；待追踪 | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/SystemOptimizerView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 项目名称、简要说明和预检风险计数已处理；详细说明及具体警告待核查 | 进行中 |
| `src/OmenCoreApp/Views/TuningView.xaml` | 源码引用：`src/OmenCoreApp/Views/MainWindow.xaml.cs` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Controls/CircularGauge.cs` | 未找到直接引用；待追踪 | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/Sparkline.cs` | 源码引用：`src/OmenCoreApp/Views/DashboardView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 全局 32 项通过；非逐页面验收 | 资源测试通过；页面切换未测 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |

全局范围还包括托盘菜单、OSD、通知、更新服务及非 View 生成的对话框；这些调用链尚未逐项验收。
审计快照 `LOCALIZATION-ZH-CN-AUDIT-SNAPSHOT.csv` 由 `scripts/audit-localization.py` 生成，ID 由路径、来源、原文与同文序号计算；纯移动行号通常不变，同一文件中重复原文的插入或修改会使序号变化。快照是候选，不是覆盖率。
扫描是正则初筛，不能保证覆盖跨行 XAML 属性、嵌套 markup extension、所有 C# 原始字符串、运行时拼接与外部资源；技术值、日志和未使用页面混在 E 类中，必须人工追踪。

结构化 XAML 初筛：`LOCALIZATION-ZH-CN-XAML-STRUCTURED.csv` 由 `scripts/audit-xaml-structured.py` 基于 XML 命名空间与元素路径生成，覆盖可见属性、辅助功能名称/说明、文本节点及部分绑定 StringFormat。路径中的同名兄弟序号可能因插入控件而变化；E 类须人工核实，XML 解析不证明 WPF 运行时可见性。C# 语法候选可由 tools/LocalizationAudit 生成；它使用本机 SDK 的 Roslyn 解析器，不构建或执行应用。语法扫描尚无语义模型和完整调用链，未知项不得视为已分类。

“源码引用”仅表示发现构造、typeof 或 XAML 元素引用，不能证明运行时路径已执行；未找到引用也不代表已废弃。中英文列为资源测试结果，页面切换与重启未验证。
