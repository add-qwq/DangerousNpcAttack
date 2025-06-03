
## English:  
### What's this?  
A script designed for **Grand Theft Auto V (GTA 5)** that periodically triggers nearby NPCs to become hostile and attack the player. Adds dynamic combat tension to GTA 5 gameplay.  


### Functionality  
- **Automatic Trigger**: By default, randomly selects a nearby NPC to attack the player every 5 minutes (configurable via INI).  
- **Force Trigger**: Use a customizable key (default: NumPad3) to manually trigger an NPC attack at any time.  
- **Smart NPC Selection**:  
  - Chooses the **closest valid NPC** (alive, not in a vehicle, not the player, and human) within a configurable range (optimized in v1: uses squared distance calculation to reduce computation).  


### How to Use  
1. **Automatic Activation**: The script will automatically attempt to trigger an attack every IntervalMinutes (configured in the INI file).  
2. **Force Activation**: Press the ForceTriggerKey (configured in the INI file) to manually trigger an attack immediately.  
3. **Reload Config**: Press F10 to reload the configuration file (useful after modifying settings).  


### Installation Steps  
1. **Install Dependencies**:  
   - [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (required execution environment for ScriptHookVDotNet).  
   - [ScriptHookV](https://www.dev-c.com/gtav/scripthookv/) (essential for GTA 5 script execution).  
   - [ScriptHookVDotNet](https://github.com/crosire/scripthookvdotnet) (enables .NET-based scripts).  
2. **Deploy the Script**:  
   - Place the compiled DangerousNpcAttack.dll and DangerousNpcAttack.ini files into the scripts folder of your GTA 5 root directory (e.g., ...\Grand Theft Auto V\scripts\).  


### Configuration  
The DangerousNpcAttack.ini file (auto-generated if missing) supports the following settings:  
- IntervalMinutes: Time interval (in minutes) for automatic attacks (e.g., 5.0 for 5 minutes).  
- ForceTriggerKey: Key to manually trigger attacks (use [Keys](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys) enum names, e.g., NumPad3, F5).  


### Compilation (Optional)  
If you prefer to compile the script yourself:  
1. Ensure you have the .NET Framework SDK installed.  
2. Run the following command in a terminal (replace paths as needed):  
   ```bash  
   csc /target:library /out:scripts\DangerousNpcAttack.dll /reference:ScriptHookVDotNet2.dll /reference:System.Windows.Forms.dll DangerousNpcAttack.cs  
   ```  
   *Note: This requires ScriptHookVDotNet2.dll and System.Windows.Forms.dll to be accessible in the build environment.*  


### Important Notes  
- **Compatibility**: Works on PC version of GTA 5. Tested with Steam/Epic Games versions.  
- **Safety**: If you distrust pre-compiled DLLs, review the source code and use the provided compilation command to build your own.  
- **NPC Requirements**: The script will notify you if no valid NPCs are found nearby (e.g., all NPCs are in vehicles, dead, or non-human).  
- **Performance Optimization**: The script uses squared distance calculation (instead of direct distance) to efficiently find the closest NPC, reducing computational overhead.  


## 中文：  
### 这是什么？  
一个为 **《侠盗猎车手5 (GTA 5)》** 设计的脚本，定期触发玩家附近的NPC变得敌对并攻击玩家，为游戏增加动态战斗紧张感。  


### 功能  
- **自动触发**：默认每5分钟（可通过INI配置）随机选择附近一个NPC攻击玩家。  
- **强制触发**：通过自定义按键（默认：小键盘3）手动立即触发NPC攻击。  
- **智能NPC选择**：  
  - 在配置范围内选择最近的有效NPC（存活、不在载具中、非玩家、人类），并为其装备RPG（v1优化：采用平方距离计算减少运算量）。  


### 使用方法  
1. **自动触发**：脚本会按 IntervalMinutes（INI文件中配置）设定的时间间隔自动尝试触发攻击。  
2. **强制触发**：按下 ForceTriggerKey（INI文件中配置的按键）可立即手动触发攻击。  
3. **重载配置**：按下 F10 键可重新加载配置文件（修改设置后使用）。  


### 安装步骤  
1. **安装依赖**：  
   - [.NET 8 运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)（ScriptHookVDotNet 依赖的执行环境）。  
   - [ScriptHookV](https://www.dev-c.com/gtav/scripthookv/)（GTA 5 脚本运行必需）。  
   - [ScriptHookVDotNet](https://github.com/crosire/scripthookvdotnet)（支持 .NET 脚本）。  
2. **部署脚本**：  
   - 将编译好的 DangerousNpcAttack.dll 和 DangerousNpcAttack.ini 文件放入GTA 5根目录的 scripts 文件夹（如 ...\Grand Theft Auto V\scripts\）。  


### 配置说明  
DangerousNpcAttack.ini 文件（缺失时自动生成）支持以下设置：  
- IntervalMinutes：自动攻击的时间间隔（单位：分钟，如 5.0 表示5分钟）。  
- ForceTriggerKey：手动触发攻击的按键（使用[Keys](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.forms.keys)枚举名称，如 NumPad3、F5）。  


### 自行编译（可选）  
若需自行编译脚本：  
1. 确保已安装 .NET Framework SDK。  
2. 在终端中运行以下命令（根据实际路径调整）：  
   ```bash  
   csc /target:library /out:scripts\DangerousNpcAttack.dll /reference:ScriptHookVDotNet2.dll /reference:System.Windows.Forms.dll DangerousNpcAttack.cs  
   ```  
   *注：编译需确保环境中可访问 ScriptHookVDotNet2.dll 和 System.Windows.Forms.dll。*  


### 注意事项  
- **兼容性**：仅支持GTA 5 PC版，已在Steam/Epic平台版本测试。  
- **安全性**：若不信任预编译DLL，可审查源代码后使用提供的编译命令自行构建。  
- **NPC限制**：若附近无有效NPC（如所有NPC在载具中、已死亡或非人类），脚本会提示“未找到附近有效NPC”。  
- **性能优化**：脚本通过平方距离计算（而非直接距离）高效定位最近NPC，降低计算开销。
