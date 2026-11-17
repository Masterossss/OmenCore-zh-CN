# 已确认的剩余界面英文（非完整清单）

以下条目来自本轮源码与调用链核查，分组条目中的数量未逐项去重；现有扫描仍有未经判定的候选，因此不能用此表行数作为全部未翻译文字数量。行号以本次提交为准。

| 文件和位置 | 模块 | 原文示例 | 显示位置 | 影响理解 | 风险提示 | 优先级 | 未解决原因 |
|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2374` | 灯光 | 后端应用结果及灯光区域（Surface） | 键盘颜色应用反馈 | 是 | 是 | 高 | 固定后端及 V2 已知验证结果已处理；区域和未知后端消息仍需核对 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:314` | 系统优化 | Sets Windows power plan to Ultimate Performance for maximum CPU/GPU performance | 优化项目说明 | 是 | 涉及系统更改 | 高 | 21 项名称、简要说明和详细说明已处理；撤销说明、搜索及预检相关数据仍需审查 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:609` | 系统优化 | 预检具体警告由 BuildWarningRollup 生成 | 执行前确认框 | 是 | 是 | 高 | 6 条已知风险警告已汉化；未知警告继续保留英文 |
| `src/OmenCoreApp/Views/BloatwareManagerView.xaml:428` | 软件清理 | Category / Type / Description / LastRemovalDetail | 软件列表及提示 | 是 | 可能 | 高 | 分类、类型及内置已知说明已转换；自定义说明和未识别细节保留原文，剩余状态仍待审查 |
| `src/OmenCoreApp/ViewModels/BloatwareManagerViewModel.cs:275` | 软件清理 | 服务 StatusChanged 返回原始状态 | 状态栏 | 是 | 可能 | 高 | 已映射部分服务结果、批量取消和已知回滚摘要；其他参数化格式及软件元数据尚未穷尽 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:156` | 托盘 | Monitor: Unknown / Fan: Auto / Battery | 状态菜单 | 是 | 否 | 中 | 动态刷新会覆盖初始菜单文字，需同步追踪 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:299` | 托盘 | 风扇状态栏和动态模式标题 | 高级菜单 | 是 | 可能 | 高 | 部分子菜单初始化及刷新已汉化；状态栏和模式标题仍待处理 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml.cs:48` | 热键 OSD | 调用方传入的 category、modeName 和 hotkeyDescription | 热键 OSD | 可能 | 否 | 中 | 所有调用方和模式显示尚未审核 |
| `src/OmenCoreApp/Localization/MemoryOptimizerDisplayText.cs` | 内存优化 | 未匹配的服务错误及非标准结果摘要 | 状态及清理结果 | 可能 | 可能 | 中 | 标准七项 delta 已汉化；未知错误按原文回退，复制数据保持不变 |

CPU、GPU、FPS、RTSS、RGB、API、配置键、日志和机器可读字段保留英文；它们是技术标识或非显示数据，不计入上述已确认的 UI 英文。其他界面的未判定项仍须审计，不应理解为已经完成。

## 当前优先级与发布阻碍（增量审计）

- 已修复误导性显示：`src/OmenCoreApp/Controls/FanCalibrationControl.xaml.cs` 的 `ClearCalibrationButton_Click` 在官方 v5.0.1 即只刷新状态、未看到删除调用；现已明确提示不会删除且未删除数据。清除功能本身仍不可用，未擅自删除用户数据。
- P0：`SettingsView.xaml`、`TuningView.xaml`、`FanControlView.xaml` 仍有未核实的硬件和恢复相关提示，现有审计尚不足以证明全部风险提示已处理。
- P1：`SettingsView.xaml` 的配置枚举显示和动态绑定仍有待核查项；`TrayIconService.cs` 的部分提示和实时值、`OsdOverlayWindow.xaml` 的动态状态；游戏库扫描反馈已处理。
- P1：`BloatwareManagerViewModel.cs` 的其余服务状态与恢复点失败原因、`FanCalibrationControl.xaml` 的静态内容和其他页面动态绑定需继续审计。
- P2：未知后端错误、自定义软件说明、技术报告和无法确认可达的旧控件保留原文或待核查；无安全隔离 Windows VM，GUI、DPI 与真实 Toast 未验证。

- P0/P1：`KeyboardMapViewModel.cs` 的部分非写入状态及设备类型、`KeyboardDiagnosticsViewModel.cs` 的外设类型和技术诊断日志仍需按显示语境核查；已知写入拒绝、未验证与闪存风险已处理。
- P1：`SettingsView.xaml` 的静态字符串与辅助名称已大批处理，下拉选项保留原始配置值并使用显示模板；动态服务状态及 GUI 布局仍待核查。
