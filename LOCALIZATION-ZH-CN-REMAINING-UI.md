# 已确认的剩余界面英文（非完整清单）

以下条目来自本轮源码与调用链核查，分组条目中的数量未逐项去重；现有扫描仍有未经判定的候选，因此不能用此表行数作为全部未翻译文字数量。行号以本次提交为准。

| 文件和位置 | 模块 | 原文示例 | 显示位置 | 影响理解 | 风险提示 | 优先级 | 未解决原因 |
|---|---|---|---|---|---|---|---|
| `src/OmenCoreApp/ViewModels/LightingViewModel.cs:2374` | 灯光 | 后端应用结果及灯光区域（Surface） | 键盘颜色应用反馈 | 是 | 是 | 高 | 固定后端结果已处理；V2 参数化结果、区域和未知后端消息仍需逐项核对 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:314` | 系统优化 | Sets Windows power plan to Ultimate Performance for maximum CPU/GPU performance | 优化项目说明 | 是 | 涉及系统更改 | 高 | 21 项名称、简要说明和详细说明已处理；撤销说明、搜索及预检相关数据仍需审查 |
| `src/OmenCoreApp/ViewModels/SystemOptimizerViewModel.cs:609` | 系统优化 | 预检具体警告由 BuildWarningRollup 生成 | 执行前确认框 | 是 | 是 | 高 | 风险数量与重启状态已汉化；具体警告保留英文以避免丢失风险内容 |
| `src/OmenCoreApp/ViewModels/BloatwareManagerViewModel.cs:275` | 软件清理 | 服务 StatusChanged 返回原始状态 | 状态栏 | 是 | 可能 | 高 | 已映射部分服务结果；批量取消、原始回滚说明及其他参数化格式尚未穷尽 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:156` | 托盘 | Monitor: Unknown / Fan: Auto / Battery | 状态菜单 | 是 | 否 | 中 | 动态刷新会覆盖初始菜单文字，需同步追踪 |
| `src/OmenCoreApp/Utils/TrayIconService.cs:299` | 托盘 | 风扇状态栏和动态模式标题 | 高级菜单 | 是 | 可能 | 高 | 部分子菜单初始化及刷新已汉化；状态栏和模式标题仍待处理 |
| `src/OmenCoreApp/Views/HotkeyOsdWindow.xaml.cs:48` | 热键 OSD | 调用方传入的 category、modeName 和 hotkeyDescription | 热键 OSD | 可能 | 否 | 中 | 所有调用方和模式显示尚未审核 |
| `src/OmenCoreApp/Localization/MemoryOptimizerDisplayText.cs` | 内存优化 | 未匹配的服务错误及非标准结果摘要 | 状态及清理结果 | 可能 | 可能 | 中 | 标准七项 delta 已汉化；未知错误按原文回退，复制数据保持不变 |

CPU、GPU、FPS、RTSS、RGB、API、配置键、日志和机器可读字段保留英文；它们是技术标识或非显示数据，不计入上述已确认的 UI 英文。其他界面的未判定项仍须审计，不应理解为已经完成。
