
using static Il2CppScheduleOne.Console;
using Il2CppScheduleOne.Employees;
using Il2CppScheduleOne.ItemFramework;
using Il2CppSystem.Collections.Generic;

//using static ScheduleOne.Console;
//using System.Collections.Generic;
//using ScheduleOne.Employees;
//using ScheduleOne.ItemFramework;

using S1API.Entities;
using S1API.Internal.Utils;
using S1API.Property;
using S1API.Quests.Constants;
using System.Globalization;

namespace S1API.Console
{
    /// <summary>
    /// This class provides easy access to the in-game console system.
    /// </summary>
    public static class ConsoleHelperTemp
    {
        
        
        /// <summary>
        /// Adds an item, with optional quantity, to the player's inventory.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="amount">The amount to add to inventory. Optional.</param>
        public static void AddItemToInventory(string itemName, int? amount = null)
        {
            var command = new AddItemToInventoryCommand();
            var args = new List<string>();

            args.Add(itemName);
            if (amount.HasValue)
            {
                args.Add(amount.ToString()!);
            }

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's bank balance to the given amount.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The amount to set the player's bank balance to.</param>
        public static void SetOnlineBalance(int amount)
        {
            var command = new ChangeOnlineBalanceCommand();
            var args = new List<string>();

            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's inventory.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearInventory()
        {
            var command = new ClearInventoryCommand();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears all trash from the world.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearTrash()
        {
            var command = new ClearTrash();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Clears the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void ClearWanted()
        {
            var command = new ClearWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Adds the given amount of XP to the player.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The amount of XP to give. Must be a non-negative amount.</param>
        public static void GiveXp(int amount)
        {
            var command = new GiveXP();
            var args = new List<string>();
            
            args.Add(amount.ToString());

            command.Execute(args);
        }
        
        /// <summary>
        /// Instantly sets all plants in the world to fully grown.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void GrowPlants()
        {
            var command = new GrowPlants();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Lower the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void LowerWanted()
        {
            var command = new LowerWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
       
        
        /// <summary>
        /// Raise the player's wanted level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void RaiseWanted()
        {
            var command = new RaisedWanted();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Saves the player's game.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void SaveGame()
        {
            var command = new Save();
            var args = new List<string>();

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets a product as discovered.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        public static void DiscoverProduct(string productName)
        {
            var command = new SetDiscovered();
            var args = new List<string>();
            
            args.Add(productName);

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's energy level.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The level of energy to set to. Range is 0 to 100.</param>
        public static void SetPlayerEnergyLevel(float amount)
        {
            var command = new SetEnergy();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's health.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The health value to set to.</param>
        public static void SetPlayerHealth(float amount)
        {
            var command = new SetHealth();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's jump multiplier.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The multiplier to set to. Must be non-negative.</param>
        public static void SetPlayerJumpMultiplier(float amount)
        {
            var command = new SetJumpMultiplier();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the intensity of law enforcement activity.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The level of activity. Ranges from 0 to 10.</param>
        public static void SetLawIntensity(float amount)
        {
            var command = new SetLawIntensity();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the player's move speed multiplier.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="amount">The multiplier to set to. Must be non-negative.</param>
        public static void SetPlayerMoveSpeedMultiplier(float amount)
        {
            var command = new SetMoveSpeedCommand();
            var args = new List<string>();
            
            args.Add(amount.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        
       
        
        /// <summary>
        /// Sets the equipped product's quality.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="quality">The quality to set the current equipped item to.</param>
        public static void SetQuality(EQuality quality)
        {
            var command = new SetQuality();
            var args = new List<string>();
            
            args.Add(quality.ToString());

            command.Execute(args);
        }

        /// <summary>
        /// Sets the state for a given quest.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="quest">The quest to set the state for.</param>
        /// <param name="state">The state to set for the quest.</param>
        public static void SetQuestState(string quest, QuestState state)
        {
            var command = new SetQuestState();
            var args = new List<string>();
            
            args.Add(quest);
            args.Add(state.ToString());

            command.Execute(args);
        }
        
        
        /// <summary>
        /// Sets the relationship for a given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npcId">The ID of the NPC to set the relationship for.</param>
        /// <param name="level">The relationship value to set. Must be between 0 and 5 inclusive.</param>
        public static void SetNpcRelationship(string npcId, float level)
        {
            var command = new SetRelationship();
            var args = new List<string>();
            
            args.Add(npcId);
            args.Add(level.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the relationship for a given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npc">The NPC to set the relationship for.</param>
        /// <param name="level">The relationship value to set. Must be between 0 and 5 inclusive.</param>
        public static void SetNpcRelationship(NPC npc, float level)
        {
            var command = new SetRelationship();
            var args = new List<string>();
            
            args.Add(npc.ID);
            args.Add(level.ToString(CultureInfo.InvariantCulture));

            command.Execute(args);
        }
        
        /// <summary>
        /// Sets the time.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="time">The time to set. Requires a valid value with Regex <c>^([01]?[0-9]|2[0-3])[0-5][0-9]$</c> (e.g. 1530 for 15:30 / 3:30 PM)</param>
        public static void SetTime(string time)
        {
            var command = new SetTimeCommand();
            var args = new List<string>();
            
            args.Add(time);

            command.Execute(args);
        }
        
        /// <summary>
        /// Spawns a vehicle at the player's location.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="vehicle">The vehicle to spawn.</param>
        public static void SpawnVehicle(string vehicle)
        {
            var command = new SpawnVehicleCommand();
            var args = new List<string>();
            
            args.Add(vehicle);

            command.Execute(args);
        }
        
        /// <summary>
        /// Spawns a vehicle at the player's location.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="vehicle">The vehicle to spawn.</param>
       
        
        /// <summary>
        /// Unlocks the connection for the given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npcId">The ID of the NPC to set the relationship for.</param>
        public static void UnlockNpc(string npcId)
        {
            var command = new SetUnlocked();
            var args = new List<string>();
            
            args.Add(npcId);

            command.Execute(args);
        }
        
        /// <summary>
        /// Unlocks the connection for the given NPC.
        /// This method works across both IL2CPP and Mono builds.
        /// </summary>
        /// <param name="npc">The NPC to set the relationship for.</param>
        public static void UnlockNpc(NPC npc)
        {
            var command = new SetUnlocked();
            var args = new List<string>();
            
            args.Add(npc.ID);

            command.Execute(args);
        }
        
       
    }
}