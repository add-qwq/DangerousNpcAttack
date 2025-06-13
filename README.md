[已上传至gta5-mods.com](https://www.gta5-mods.com/scripts/dangerous-npc-attack-bazooka-npc)  
[Already uploaded to gta5-mods.com](https://www.gta5-mods.com/scripts/dangerous-npc-attack-bazooka-npc)  


## English:  
### What's this?  
A script designed for **Grand Theft Auto V (GTA 5)** that periodically triggers nearby NPCs to become hostile and attack the player. Adds dynamic combat tension to GTA 5 gameplay with enhanced super attack mechanics in Version 2.  


### Functionality  
- **Automatic Trigger**: By default, randomly selects a nearby NPC to attack the player every 5 minutes (configurable via INI).  
- **Force Trigger**: Use a customizable key (default: NumPad3) to manually trigger an NPC attack at any time.  
- **Super Attack (V2 New Feature)**:  
  - **Mass Hostility**: When enabled, triggers all nearby valid NPCs to arm RPGs and attack simultaneously.  
  - **Automatic Activation**: 50% chance to trigger after avoiding 5 consecutive attacks.  
  - **Manual Activation**: Use a configurable key (default: NumPad6) to force super attacks.  
- **Smart NPC Selection**:  
  - Chooses the closest valid NPC (alive, not in a vehicle, not police, not friendly to the player).  
  - Tracks player avoidance of attacks, escalating to super attacks after 5 evasions.  


### How to Use  
1. **Automatic Activation**: The script will auto-attempt attacks every IntervalMinutes.  
2. **Force Activation**: Press ForceTriggerKey (default: NumPad3) for immediate attacks.  
3. **Super Attack**:  
   - Auto-trigger: After 5 successful avoids, 50% chance to activate.  
   - Manual trigger: Press ForceSuperAttackKey (default: NumPad6) when enabled.  
4. **Reload Config**: Press F10 to apply INI changes.  


### Installation Steps  
1. Install [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), [ScriptHookV](https://www.dev-c.com/gtav/scripthookv/), and [ScriptHookVDotNet](https://github.com/crosire/scripthookvdotnet).  
2. Place DLL and INI into `...\Grand Theft Auto V\scripts\`.  


### Configuration  
The `DangerousNpcAttack.ini` now supports:  
- `IntervalMinutes`: Attack interval (e.g., 5.0).  
- `ForceTriggerKey`: Manual attack key (e.g., NumPad3).  
- `EnableSuperAttack`: Toggle super attack (default: true).  
- `ForceSuperAttackKey`: Manual super attack key (default: NumPad6).  


### Important Notes (V2 Updates)  
- **Super Attack Mechanics**:  
  - Avoiding 5 attacks triggers a potential super attack with nearby NPCs.  
  - Ensure `EnableSuperAttack=true` in INI to use this feature.  
- **NPC Tracking**: The script now monitors the current attacking NPC's status (alive/position).  
- **Performance**: Optimized NPC filtering maintains low overhead even during super attacks.  


## 中文：  
### 这是什么？  
为 **《侠盗猎车手5 (GTA 5)》** 设计的脚本，定期触发附近NPC敌对并攻击玩家。V2版本新增超级攻击机制，为游戏添加更具动态张力的战斗体验。  


### 功能特性  
- **自动触发**：默认每5分钟（INI可配置）随机选择附近NPC攻击玩家。  
- **强制触发**：通过自定义按键（默认：小键盘3）手动立即触发NPC攻击。  
- **超级攻击（V2新增）**：  
  - **群体敌对**：启用后，触发附近所有有效NPC装备RPG并同时攻击。  
  - **自动激活**：连续躲避5次攻击后，有50%概率自动触发。  
  - **手动激活**：通过配置按键（默认：小键盘6）强制触发（需INI启用）。  
- **智能NPC筛选**：  
  - 优先选择最近的有效NPC（存活、非载具内、非警察、非友好关系）。  
  - 追踪玩家躲避攻击次数，累计5次后升级为超级攻击。  


### 使用方法  
1. **自动触发**：按INI中IntervalMinutes设定的间隔自动尝试攻击。  
2. **强制触发**：按下ForceTriggerKey（默认：小键盘3）立即触发普通攻击。  
3. **超级攻击**：  
   - 自动触发：连续躲避5次攻击后，50%概率激活。  
   - 手动触发：启用状态下按下ForceSuperAttackKey（默认：小键盘6）。  
4. **重载配置**：按F10键应用INI修改内容。  


### 安装步骤  
1. 安装 [.NET 8 运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)、[ScriptHookV](https://www.dev-c.com/gtav/scripthookv/) 和 [ScriptHookVDotNet](https://github.com/crosire/scripthookvdotnet)。  
2. 将DLL和INI文件放入`...\Grand Theft Auto V\scripts\`目录。  


### 配置说明  
`DangerousNpcAttack.ini` 新增/支持配置项：  
- `IntervalMinutes`：攻击间隔时间（单位：分钟，如5.0）。  
- `ForceTriggerKey`：手动触发普通攻击的按键（如NumPad3）。  
- `EnableSuperAttack`：是否启用超级攻击（默认：true）。  
- `ForceSuperAttackKey`：手动触发超级攻击的按键（默认：NumPad6）。  


### 重要更新说明（V2）  
- **超级攻击机制**：  
  - 躲避5次攻击后可能触发附近NPC集体攻击，需INI中启用功能。  
- **NPC状态监控**：脚本会实时监控当前攻击NPC的存活状态和位置。  
- **性能优化**：即使在超级攻击状态下，NPC筛选逻辑仍保持低开销运行。
