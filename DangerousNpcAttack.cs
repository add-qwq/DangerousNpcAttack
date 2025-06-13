using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using System.IO;
using System.Collections.Generic;

namespace RandomNPCAttack
{
    public class RandomNPCAttack : Script
    {
        private float intervalMinutes = 5.0f; // 默认5分钟
        private Keys forceTriggerKey = Keys.NumPad3; // 默认强制触发键
        private bool enableSuperAttack = true; // 默认启用超级攻击
        private Keys forceSuperAttackKey = Keys.NumPad6; // 默认强制超级攻击键
        private float lastCheckTime = 0.0f;
        private Random random;
        private string iniPath = "scripts\\DangerousNpcAttack.ini";
        private int copHash;  // 警察关系组哈希
        private int avoidCount = 0; // 躲过攻击的次数
        private Ped attackingPed = null; // 当前攻击的NPC

        public RandomNPCAttack()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            random = new Random();
            LoadConfig();

            copHash = Function.Call<int>(Hash.GET_HASH_KEY, "COP");
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(iniPath))
                {
                    string[] lines = File.ReadAllLines(iniPath);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("IntervalMinutes="))
                        {
                            float.TryParse(line.Substring("IntervalMinutes=".Length), out intervalMinutes);
                        }
                        else if (line.StartsWith("ForceTriggerKey="))
                        {
                            string keyName = line.Substring("ForceTriggerKey=".Length).Trim();
                            try
                            {
                                forceTriggerKey = (Keys)Enum.Parse(typeof(Keys), keyName, true);
                            }
                            catch
                            {
                                UI.Notify("Invalid key for ForceTriggerKey: " + keyName + ". Using default NumPad3.");
                            }
                        }
                        else if (line.StartsWith("EnableSuperAttack="))
                        {
                            string value = line.Substring("EnableSuperAttack=".Length).Trim();
                            enableSuperAttack = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        }
                        else if (line.StartsWith("ForceSuperAttackKey="))
                        {
                            string keyName = line.Substring("ForceSuperAttackKey=".Length).Trim();
                            try
                            {
                                forceSuperAttackKey = (Keys)Enum.Parse(typeof(Keys), keyName, true);
                            }
                            catch
                            {
                                UI.Notify("Invalid key for ForceSuperAttackKey: " + keyName + ". Using default NumPad6.");
                                forceSuperAttackKey = Keys.NumPad6;
                            }
                        }
                    }
                }
                else
                {
                    File.WriteAllText(iniPath, "[Settings]\nIntervalMinutes=5.0\nForceTriggerKey=NumPad3\nEnableSuperAttack=true\nForceSuperAttackKey=NumPad6");
                }
            }
            catch (Exception ex)
            {
                UI.Notify("Error loading config: " + ex.Message);
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            float currentTime = Game.GameTime / 1000.0f / 60.0f;
            if (currentTime - lastCheckTime >= intervalMinutes)
            {
                lastCheckTime = currentTime;
                TrySpawnHostileNPC();
            }

            // 监控当前攻击的NPC（仅当超级攻击启用时）
            if (enableSuperAttack && attackingPed != null)
            {
                if (!attackingPed.IsAlive || Vector3.Distance(Game.Player.Character.Position, attackingPed.Position) > 100.0f)
                {
                    avoidCount++;
                    UI.Notify("You have avoided an NPC attack! Total avoids: " + avoidCount);
                    attackingPed = null; // 重置当前攻击NPC

                    if (avoidCount >= 5)
                    {
                        if (random.NextDouble() < 0.5)
                        {
                            TriggerSuperAttack();
                        }
                        else
                        {
                            UI.Notify("Lucky escape?；逃过一劫？");
                        }
                        avoidCount = 0; // 重置躲过次数
                    }
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                LoadConfig();
                UI.Notify("Config reloaded.");
            }
            else if (e.KeyCode == forceTriggerKey)
            {
                TrySpawnHostileNPC();
                UI.Notify("Force NPC attack triggered!;强制NPC攻击触发！");
            }
            else if (e.KeyCode == forceSuperAttackKey)
            {
                if (enableSuperAttack)
                {
                    TriggerSuperAttack();
                    UI.Notify("Force super attack triggered!;强制超级攻击触发！");
                }
                else
                {
                    UI.Notify("Super attack is disabled in config.;配置文件中超级攻击已禁用。");
                }
            }
        }

        private void TrySpawnHostileNPC()
        {
            Ped player = Game.Player.Character;
            if (player == null || !player.IsAlive) return;

            Ped[] nearbyPeds = World.GetNearbyPeds(player, 30.0f);
            Ped closestPed = null;
            float minSquaredDistance = float.MaxValue;
            Vector3 playerPos = player.Position;

            foreach (Ped ped in nearbyPeds)
            {
                // 排除条件：无效NPC、警察、与玩家关系友好的NPC（包括LSPDFR搭档）
                if (ped == null || !ped.IsAlive || ped.IsPlayer || !ped.IsHuman || ped.IsInVehicle() 
                    || ped.RelationshipGroup == copHash 
                    || Function.Call<int>(Hash.GET_RELATIONSHIP_BETWEEN_PEDS, ped, player) <= 1)
                    continue;

                Vector3 pedPos = ped.Position;
                float squaredDistance = (pedPos.X - playerPos.X) * (pedPos.X - playerPos.X) +
                                       (pedPos.Y - playerPos.Y) * (pedPos.Y - playerPos.Y) +
                                       (pedPos.Z - playerPos.Z) * (pedPos.Z - playerPos.Z);

                if (squaredDistance < minSquaredDistance)
                {
                    minSquaredDistance = squaredDistance;
                    closestPed = ped;
                }
            }

            if (closestPed == null)
            {
                UI.Notify("No valid NPCs found nearby；附近找不到有效的NPC");
                return;
            }

            closestPed.Weapons.Give(WeaponHash.RPG, 10, true, true);
            closestPed.Task.ClearAll();
            closestPed.AlwaysKeepTask = true;
            closestPed.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");
            Function.Call(Hash.SET_PED_AS_ENEMY, closestPed, true);
            closestPed.Task.FightAgainst(player);
            UI.Notify("Closest NPC armed with an RPG and is hostile!；最近的NPC手持RPG，充满敌意！");

            // 仅当超级攻击启用时设置 attackingPed
            if (enableSuperAttack)
            {
                attackingPed = closestPed;
            }
        }

        private void TriggerSuperAttack()
        {
            Ped player = Game.Player.Character;
            if (player == null || !player.IsAlive) return;

            Ped[] nearbyPeds = World.GetNearbyPeds(player, 30.0f);
            List<Ped> hostilePeds = new List<Ped>();

            foreach (Ped ped in nearbyPeds)
            {
                // 排除条件：无效NPC、警察、与玩家关系友好的NPC（包括LSPDFR搭档）
                if (ped == null || !ped.IsAlive || ped.IsPlayer || !ped.IsHuman || ped.IsInVehicle() 
                    || ped.RelationshipGroup == copHash 
                    || Function.Call<int>(Hash.GET_RELATIONSHIP_BETWEEN_PEDS, ped, player) <= 1)
                    continue;

                hostilePeds.Add(ped);
            }

            if (hostilePeds.Count == 0)
            {
                UI.Notify("No valid NPCs found nearby for super attack；附近找不到有效的NPC进行超级攻击");
                return;
            }

            foreach (Ped ped in hostilePeds)
            {
                ped.Weapons.Give(WeaponHash.RPG, 10, true, true);
                ped.Task.ClearAll();
                ped.AlwaysKeepTask = true;
                ped.RelationshipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");
                Function.Call(Hash.SET_PED_AS_ENEMY, ped, true);
                ped.Task.FightAgainst(player);
            }

            UI.Notify("Super attack triggered! All nearby NPCs are hostile with RPGs!；超级攻击触发！附近所有NPC都手持RPG攻击你！");
        }
    }
}
