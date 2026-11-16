# 3.6.3 中文参考源码对照

来源是工作区提供的 `omencore-cn-main.zip`。抽查的版本文件、常规页、设置页和设置 ViewModel 与本地参考仓库 489064a 文本逐行一致（原始文件换行字节不同）。以官方 v3.6.3 标签为英文基线；没有合并或复制旧版硬件代码。

ZIP 内 OmenCoreApp 的 XAML、code-behind 和 C# 扫描中，12 个文件含中文字符：Settings、MainWindow、General、Dashboard 的中文量较多；About、SystemControl、Tuning、FanControl、Diagnostics、GameLibrary、GameProfileManager 含较少中文，SystemRestoreService 含少量中文。中文字符统计包含注释或非用户可见文字，因此不等同于翻译覆盖率；其他文件不能假定已汉化。

自动映射方法：对中文参考与官方英文的同路径、同行进行属性名和位置对齐，提取发生改变且含中文的 Text、Content、ToolTip、Header、Title；再与 v5 本轮候选表按路径、属性和英文原文匹配。映射表共 393 条记录，其中 269 条匹配到 v5 候选；一个旧文可对应多个候选，所以记录数不等于独立原文数。未找到候选可能是已资源化、扫描范围不同或新版结构变化。映射仅是待审核线索；同一英文短语在不同位置不一定同义。语义近似、页面重命名、文本节点、动态拼接和新增功能尚未系统匹配。

实际处理：对设置页中同文件、同属性、同英文的 140 个候选复用参考译文；映射表因重复候选包含 146 条标记为已应用的记录，仍标记需要逐处复核。将 PawnIO-Only Mode 的旧译“PawnIO 独占模式”修正为“仅使用 PawnIO 模式”，避免暗示互斥占用。旧译“休眠恢复”未直接用于 Post-Resume Recovery，因为 Resume 不仅指休眠；“狂暴模式”未直接用于 Boost mode，因为它夸大原意。相关候选和建议记录在映射表中。

参考项目未覆盖或未能由该方法匹配的 v5 功能，包括新版电源适配器限制说明、实验性 GPU 功率解锁及较新的启动恢复策略，必须以 v5 原文和实际上下文翻译，不能移植旧版说明或硬件实现。
