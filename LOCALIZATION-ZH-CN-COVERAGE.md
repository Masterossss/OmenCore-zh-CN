# 界面汉化覆盖矩阵（未完成）

本矩阵列出 WPF Views/Controls 源文件作为核查范围，文件存在不代表运行时实际加载；实际使用情况仍需逐项追踪。静态和动态列为保守状态，不因存在资源而标记完成。全局资源一致性以最近一次测试为准，不证明每个绑定都正确。GUI 尚未执行。

| 文件 | 静态文字 | 动态文字 | 风险提示 | 资源一致性 | GUI | 未解决位置 | 状态 |
|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/Controls/DiagnosticExportControl.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCalibrationControl.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCurveChart.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/FanCurveEditor.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/GpuVcChart.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/HardwareMonitoringDashboard.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Controls/LoadChart.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/LoadingOverlay.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/ThermalChart.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/AboutWindow.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/AdvancedView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/BloatwareManagerView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/ColorPickerDialog.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/DashboardView.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/DiagnosticsView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/FanControlView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/FanDiagnosticsView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GameLibraryView.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GameProfileManagerView.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/GeneralView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/InputPromptWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/KeyboardDiagnosticsView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/KeyboardMapEditor.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/LightingView.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/MainWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/MemoryOptimizerView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | ViewModel 清理结果、错误、辅助文本、绑定值 | 进行中 |
| `src/OmenCoreApp/Views/OnboardingWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/OsdOverlayWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/QuickPopupWindow.xaml` | 部分处理 | 部分处理 | 待确认 | 全局通过，引用待核查 | 未执行 | 托盘调用、绑定与布局仍需核查 | 进行中 |
| `src/OmenCoreApp/Views/SettingsView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Views/SplashWindow.xaml` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/SystemOptimizerView.xaml` | 待审查 | 待审查 | 待核查 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Views/TuningView.xaml` | 部分处理 | 部分处理 | 待核查 | 全局通过，引用待核查 | 未执行 | 文本节点、绑定状态及相关服务需核查 | 进行中 |
| `src/OmenCoreApp/Controls/CircularGauge.cs` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |
| `src/OmenCoreApp/Controls/Sparkline.cs` | 待审查 | 待审查 | 待确认 | 全局通过，引用待核查 | 未执行 | 静态候选及 code-behind/绑定需核查 | 进行中 |

全局范围还包括托盘菜单、OSD、通知、更新服务及非 View 生成的对话框；这些调用链尚未逐项验收。
审计快照 `LOCALIZATION-ZH-CN-AUDIT-SNAPSHOT.csv` 由 `scripts/audit-localization.py` 生成，ID 由路径、来源、原文与同文序号计算；纯移动行号通常不变，同一文件中重复原文的插入或修改会使序号变化。快照是候选，不是覆盖率。
扫描是正则初筛，不能保证覆盖跨行 XAML 属性、嵌套 markup extension、所有 C# 原始字符串、运行时拼接与外部资源；技术值、日志和未使用页面混在 E 类中，必须人工追踪。

结构化 XAML 初筛：`LOCALIZATION-ZH-CN-XAML-STRUCTURED.csv` 由 `scripts/audit-xaml-structured.py` 基于 XML 命名空间与元素路径生成，覆盖可见属性、辅助功能名称/说明、文本节点及部分绑定 StringFormat。路径中的同名兄弟序号可能因插入控件而变化；E 类须人工核实，XML 解析不证明 WPF 运行时可见性。C# 目前仍是正则候选，尚未完成 Roslyn 语义追踪。
