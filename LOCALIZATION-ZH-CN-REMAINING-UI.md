# 已确认的剩余界面英文（非完整清单）

以下条目来自本轮源码与调用链核查，分组条目中的数量未逐项去重；现有扫描仍有未经判定的候选，因此不能用此表行数作为全部未翻译文字数量。行号以本次提交为准。

| 文件和位置 | 模块 | 原文示例 | 显示位置 | 影响理解 | 风险提示 | 优先级 | 未解决原因 |
|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2342` | 灯光 | Keyboard lighting is unavailable on this backend. | 键盘照明状态 | 是 | 否 | 高 | 显示状态与后端结果需一并追踪 |
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2401` | 灯光 | RGB probe unavailable because no HP keyboard lighting backend is active. | RGB 探测状态 | 是 | 否 | 高 | 尚未完成全部状态映射 |
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2424` | 灯光 | RGB observation could not be saved because configuration is unavailable. | RGB 观察保存状态 | 是 | 否 | 高 | 配置保存与显示调用链待完整审查 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:314` | 系统优化 | Sets Windows power plan to Ultimate Performance for maximum CPU/GPU performance | 优化项目说明 | 是 | 涉及系统更改 | 高 | 项目名称、说明、搜索及预检共用数据，需在显示边界转换 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:609` | 系统优化 | 预检摘要由 BuildSummary / BuildWarningRollup 生成 | 执行前确认框 | 是 | 是 | 高 | 需审查格式化器及导出复用，避免改变报告格式 |
| `src/OmenCoreApp/ViewModels/BloatwareManagerViewModel.cs:275` | 软件清理 | 服务 StatusChanged 返回原始状态 | 状态栏 | 是 | 可能 | 高 | 服务结果复用及参数化格式尚未穷尽 |
| `src/OmenCoreApp/ViewModels/BloatwareManagerViewModel.cs:708` | 软件清理 | Restoring {name} / Restored {name} | 还原状态 | 是 | 否 | 高 | 未完成成功、失败及部分成功映射 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:156` | 托盘 | Monitor: Unknown / Fan: Auto / Battery | 状态菜单 | 是 | 否 | 中 | 动态刷新会覆盖初始菜单文字，需同步追踪 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:299` | 托盘 | Fan Control / Auto — System controlled | 高级菜单 | 是 | 可能 | 高 | 菜单动态重建与设备模式显示待处理 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml.cs:48` | 热键 OSD | 调用方传入的 category、modeName 和 hotkeyDescription | 热键 OSD | 可能 | 否 | 中 | 所有调用方和模式显示尚未审核 |
| `src/OmenCoreApp/Localization/MemoryOptimizerDisplayText.cs` | 内存优化 | 未匹配的服务错误、结果末尾 delta 说明 | 状态及清理结果 | 可能 | 可能 | 中 | 未知错误按原文回退；复制数据必须保持不变 |

CPU、GPU、FPS、RTSS、RGB、API、配置键、日志和机器可读字段保留英文；它们是技术标识或非显示数据，不计入上述已确认的 UI 英文。其他界面的未判定项仍须审计，不应理解为已经完成。
