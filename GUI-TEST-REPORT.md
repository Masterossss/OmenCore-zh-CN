# 实机 GUI 测试报告

- 源码提交 653724d。Windows 实机运行独立 WPF 展示宿主，载入原 OmenCore 程序集中的 12 个页面；不是普通生产模式启动。
- 自动切换并截图：Dashboard、General、Advanced、Settings、Tuning、Fan Control、Diagnostics、Lighting、Memory Optimizer、System Optimizer、Bloatware Manager、Game Library。最终一次 Release 版宿主运行中 12 页均构造成功，failures.txt 为空。
- 截图位于 Screenshots，来自 WPF RenderTargetBitmap（96 DPI），不是生成式图片。已人工查看仪表盘、设置、调校及灯光页面的首屏；中文可见。其余截图已保存但未逐页全面人工验收。
- 所有页面使用普通模拟数据对象，没有真实 ViewModel，所有页面操作控件禁用。状态可能为空、模拟或默认值，不能代表真实设备。部分布局及图表英文仍存在。
- 发现仪表盘风扇图表绑定非依赖属性导致页面加载失败，已修复并增加纯软件回归检查；调校页图片 URI 已限定到 OmenCore 资源程序集。
- 未验证 125% / 150% DPI、页面滚动全内容、语言切换重启、About、Onboarding、Quick Popup、OSD、弹窗、托盘、真实通知及生产程序启动。未执行硬件操作。
