using System;
using System.Collections.Generic;
using System.Linq;
using Empire.DebtHelpers;
using Empire.NPC.Data;
using Empire.NPC.S1API_NPCs;
using MelonLoader;

namespace Empire.NPC
{
    public static class Contacts
    {
        public static Dictionary<string, EmpireNPC> Buyers { get; set; } = new Dictionary<string, EmpireNPC>(); // Key: DealerId, Value: EmpireNPC Buyer
		public static Dictionary<string, EmpireNPC> BuyersByDisplayName { get; set; } = new Dictionary<string, EmpireNPC>(); // Key: DealerId, Value: EmpireNPC Buyer
		public static bool IsInitialized { get; set; } = false;
        public static bool IsUnlocked { get; set; } = false;
        //public static BlackmarketBuyer saveBuyer { get; set; }
        public static Dealer standardDealer { get; set; } = new Dealer { Name = "Blackmarket Buyer", Image = "EmpireIcon_quest.png" };
        private static bool _isUpdateCoroutineRunning = false;

		public static readonly List<Type> AllEmpireNPCs = typeof(EmpireNPC)
	        .Assembly
	        .GetTypes()
	        .Where(t => t.IsSubclassOf(typeof(EmpireNPC)) && !t.IsAbstract)
	        .ToList();

        public static void RegisterEmpireNPC(EmpireNPC npc)
        {
            Buyers[npc.DealerId] = npc; // Use indexer to add or update
            BuyersByDisplayName[npc.DisplayName] = npc;

			MelonLogger.Msg($"✅ Registered Empire NPC: {npc.DealerId}");

            if (!Buyers.ContainsKey(npc.DealerId))
            {
                if (!Buyers.TryAdd(npc.DealerId, npc))
                    MelonLogger.Msg($"❌ Failed to add Empire NPC: {npc.DealerId} to Buyers dictionary.");
                else
                    MelonLogger.Msg($"✅ Registered Empire NPC: {npc.DealerId}");
            }
            else
            {
                MelonLogger.Warning($"⚠️ Empire NPC already registered: {npc.DealerId}");
            }
        }

		public static EmpireNPC? GetBuyer(string dealerName)
        {
			BuyersByDisplayName.TryGetValue(dealerName, out var buyer);
            MelonLogger.Msg($"🔍 GetBuyer called for dealerName: {dealerName}, Found: {buyer != null}");

            return buyer;
        }

        //GetDealerDataByName
        public static Dealer? GetDealerDataByName(string dealerName)
        {
            //If dealerName is null or empty or not found, return null
            if (string.IsNullOrEmpty(dealerName))
            {
                MelonLogger.Error("❌ dealerName is null or empty.");
                return null;
            }
            var dealer = JSONDeserializer.dealerData.Dealers.FirstOrDefault(d => d.Name == dealerName);
            if (dealer == null)
            {
                MelonLogger.Error($"❌ Dealer not found: {dealerName}");
            }
            return dealer;
        }

        //GetDealerDataByIndex
        public static Dealer GetDealerDataByIndex(int index)
        {
            // Ensure dealer data is loaded
            if (JSONDeserializer.dealerData.Dealers == null || JSONDeserializer.dealerData.Dealers.Count == 0)
            {
                MelonLogger.Warning("⚠️ Dealer data not loaded yet, returning standard dealer");
                return standardDealer;
            }

            // If index out of range, return standard dealer
            if (index < 0 || index >= JSONDeserializer.dealerData.Dealers.Count)
            {
                //MelonLogger.Msg($"❌ Index {index} is out of range for dealers (count: {JSONDeserializer.dealerData.Dealers.Count}).");
                return standardDealer;
            }
            return JSONDeserializer.dealerData.Dealers.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Reset Contacts static state between scene loads to avoid leaking over the previous session.
        /// </summary>
        public static void Reset()
        {
            Buyers.Clear();
            IsInitialized = false;
            IsUnlocked = false;
            //BlackmarketBuyer.dealerDataIndex = 0;
            // Reset the dealer field to force re-initialization
            //BlackmarketBuyer.dealer = null;
            _isUpdateCoroutineRunning = false; // Allow coroutine to be restarted
            MelonLogger.Msg("🧹 Empire Contacts state reset complete");
        }

        public static void Update()
        {
            // Prevent multiple coroutines from running
            if (_isUpdateCoroutineRunning)
            {
                MelonLogger.Msg("⚠️ Contacts Update coroutine already running, skipping...");
                return;
            }

            _isUpdateCoroutineRunning = true;
            MelonLoader.MelonCoroutines.Start(UpdateCoroutine());
        }
        

        private static System.Collections.IEnumerator UpdateCoroutine()
        {
            while (!IsInitialized)
            {
                yield return null;
            }
            try
            {
                // Melonlogger Test
                //MelonLogger.Msg("Testing 101}");
                foreach (var buyer in Buyers.Values)
                {

                    bool canUnlock = buyer.UnlockRequirements == null ||
                                     !buyer.UnlockRequirements.Any() ||
                                     buyer.UnlockRequirements.All(req =>
                                         GetBuyer(req.Name)?.DealerSaveData.Reputation >= req.MinRep);

                    ////Log the buyer name and unlock status
                    MelonLogger.Msg($"Buyer: {buyer.DisplayName}, Unlock Status: {canUnlock}");
                    //If cannot unlock, log the requirements and the current reputation
                    if (!canUnlock)
                    {
                        if (buyer.UnlockRequirements?.Any() == true)
                        {
                            foreach (var req in buyer.UnlockRequirements)
                            {
                                var unlockBuyer = GetBuyer(req.Name);
                                if (unlockBuyer != null)
                                {
                                    MelonLogger.Msg($"Unlock Requirement: {req.Name}, Current Reputation: {unlockBuyer.DealerSaveData.Reputation}, Required Reputation: {req.MinRep}");
                                }
                                else
                                {
                                    MelonLogger.Msg($"Unlock Requirement: {req.Name} not found.");
                                }
                            }
                        }
                    }

                    if (canUnlock)
                    {
                        if (!buyer.IsInitialized)
                        {
                            buyer.IsInitialized = true;
                            if (buyer.DealerSaveData.IntroDone == false) // First time Intro
                            {
                                buyer.SendCustomMessage("Intro");
                                MelonLogger.Msg($"✅ Dealer {buyer.DisplayName} intro sent.");
                                buyer.DealerSaveData.IntroDone = true; // Set IntroDone to true
                            }
                            
                            
                                if (buyer.Debt != null && buyer.Debt.TotalDebt > 0 && buyer.DealerSaveData.DebtRemaining > 0)
                                {
                                    buyer.DebtManager = new DebtManager(buyer);
                                    MelonLogger.Msg($"❌ Dealer {buyer.DisplayName} is locked due to debt: {buyer.Debt.TotalDebt}");
                                }

                            
                            MelonLogger.Msg($"✅ Initialized dealer: {buyer.DisplayName}");
                        }
                        buyer.UnlockDrug();
                    }
                    else
                    {
                        MelonLogger.Msg($"⚠️ Dealer {buyer.DisplayName} is locked (unlock requirements not met)");
                    }

                    //MelonLogger.Msg($"✅ Contacts.Buyers now contains {Buyers.Count} buyers.");
                    IsUnlocked = true;
                }

            }
            catch (Exception ex)
            {
                MelonLogger.Error($"❌ Unexpected error during Update: {ex}");
            }
            finally
            {
                // Reset the flag when coroutine completes
                _isUpdateCoroutineRunning = false;
            }
        }
    }
}