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
        private float lastCheckTime = 0.0f;
        private Random random;
        private string iniPath = "scripts\\DangerousNpcAttack.ini";
        private int copHash;  // 警察关系组哈希

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
                                UI.Notify("Invalid key in config: " + keyName + ". Using default NumPad3.");
                            }
                        }
                    }
                }
                else
                {
                    File.WriteAllText(iniPath, "[Settings]\nIntervalMinutes=5.0\nForceTriggerKey=NumPad3");
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
        } 
    } 
}
