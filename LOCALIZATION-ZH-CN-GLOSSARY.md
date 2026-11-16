# OmenCore 简体中文术语表（持续审核）

来源：v5.0.1 现有资源、官方 v3.6.3 英文 XAML 与用户提供的 omencore-cn-main.zip。下表为本轮初审建议；涉及硬件状态和警告的完整句子仍须逐处复核。

| 英文 | 标准中文 | 场景与歧义 | 缩写 | 来源与状态 |
|---|---|---|---|---|
| Performance mode | 性能模式 | 指模式名称，具体模式须结合上下文 | 保留 CPU/GPU | v5 既有，初审 |
| Balanced / Quiet | 均衡模式 / 安静模式 | 性能配置选项 | 不适用 | v5 既有，初审 |
| Tuning | 性能调校 | CPU/GPU 性能设置 | 保留 CPU/GPU | v5 既有，初审 |
| Power limit | 功率限制 | 非“电源限制” | 保留 PL1/PL2、TDP | v5 既有，初审 |
| TGP / TDP | TGP / TDP | 分别为显卡总功率、热设计功耗，不能混同 | 保留 | 技术术语，初审 |
| Dynamic Boost | Dynamic Boost | NVIDIA 功率动态分配功能 | 保留 | 产品名称，初审 |
| GPU power boost | GPU 功率增强 | 固件提供的相对增强档位，不保证绝对功率 | 保留 GPU/TGP | v5 既有，初审 |
| GPU power unlock | GPU 功率解锁 | 实验性功能，保留适配器、固件及重启警告 | 保留 GPU/EC/WMI | v5 既有，待风险句复核 |
| Undervolt | 降压 | 电压偏移，不能省略不稳定风险 | 保留 CPU/GPU | v5 既有，初审 |
| Fan curve / preset | 风扇曲线 / 风扇预设 | 曲线与预设不是同义词 | 保留 RPM | v5 与旧版，初审 |
| EC / WMI / MSR | EC / WMI / MSR | 底层接口或寄存器访问 | 保留 | 技术术语，初审 |
| MUX / hybrid / discrete | MUX / 混合显卡模式 / 独立显卡 | 切换路径可能需要重启 | 保留 MUX | v5 既有，初审 |
| Battery care / charge limit | 电池保养 / 充电限制 | 配置电池充电阈值 | 不适用 | 旧版，初审 |
| RGB lighting | RGB 灯光 | 指键盘或外设灯效，区分灯光配置文件 | 保留 RGB | 旧版与 v5，初审 |
| Diagnostics | 诊断 | 设备或服务故障排查 | 保留设备名称 | 旧版与 v5，初审 |
| Game profile | 游戏配置文件 | 区分游戏库、程序配置及风扇预设 | 不适用 | 旧版与 v5，初审 |
| Startup restore | 启动恢复 | 可能重新写入硬件设置，保留风险及开关条件 | 保留型号 | 旧版与 v5，待风险句复核 |
| Thermal protection | 散热保护 | 必须保留紧急转速及关闭后责任的说明 | 保留温度单位 | v5，待风险句复核 |

旧版不直接复用示例：`Boost mode (Performance)` 译为“狂暴模式（性能）”含义夸张，建议“增强模式（性能）”；`Post-Resume Recovery` 译为“休眠恢复”范围过窄，系统恢复也可能来自睡眠，建议“系统恢复后的修复”。这两项目前仅记录建议，不表示已修改界面。
