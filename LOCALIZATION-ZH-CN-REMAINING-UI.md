# 已确认的剩余界面英文（非完整清单）

以下条目来自本轮源码与调用链核查，分组条目中的数量未逐项去重；现有扫描仍有未经判定的候选，因此不能用此表行数作为全部未翻译文字数量。行号以本次提交为准。

| 文件和位置 | 模块 | 原文示例 | 显示位置 | 影响理解 | 风险提示 | 优先级 | 未解决原因 |
|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2374` | 灯光 | WMI keyboard commands aren’t verifying on your model / 后端应用结果 | 键盘颜色应用反馈 | 是 | 是 | 高 | 含后端原始状态、探测提示及参数，需逐项核对并保留未验证语义 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:314` | 系统优化 | Sets Windows power plan to Ultimate Performance for maximum CPU/GPU performance | 优化项目说明 | 是 | 涉及系统更改 | 高 | 项目名称、说明、搜索及预检共用数据，需在显示边界转换 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:609` | 系统优化 | 预检具体警告由 BuildWarningRollup 生成 | 执行前确认框 | 是 | 是 | 高 | 风险数量与重启状态已汉化；具体警告保留英文以避免丢失风险内容 |
| `src/OmenCoreApp/ViewModels/BloatwareManagerViewModel.cs:275` | 软件清理 | 服务 StatusChanged 返回原始状态 | 状态栏 | 是 | 可能 | 高 | 服务结果复用及参数化格式尚未穷尽 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:156` | 托盘 | Monitor: Unknown / Fan: Auto / Battery | 状态菜单 | 是 | 否 | 中 | 动态刷新会覆盖初始菜单文字，需同步追踪 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:299` | 托盘 | Fan Control / Auto — System controlled | 高级菜单 | 是 | 可能 | 高 | 菜单动态重建与设备模式显示待处理 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml.cs:48` | 热键 OSD | 调用方传入的 category、modeName 和 hotkeyDescription | 热键 OSD | 可能 | 否 | 中 | 所有调用方和模式显示尚未审核 |
| `src/OmenCoreApp/Localization/MemoryOptimizerDisplayText.cs` | 内存优化 | 未匹配的服务错误、结果末尾 delta 说明 | 状态及清理结果 | 可能 | 可能 | 中 | 未知错误按原文回退；复制数据必须保持不变 |

CPU、GPU、FPS、RTSS、RGB、API、配置键、日志和机器可读字段保留英文；它们是技术标识或非显示数据，不计入上述已确认的 UI 英文。其他界面的未判定项仍须审计，不应理解为已经完成。
