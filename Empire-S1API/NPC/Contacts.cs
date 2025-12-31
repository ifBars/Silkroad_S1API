using Empire.DebtHelpers;
using Empire.NPC.Data;
using Empire.NPC.Data.Enums;
using Empire.NPC.S1API_NPCs;
using Empire.NPC.SaveData;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Empire.NPC
{
    public static class Contacts
    {
        public static Dictionary<string, EmpireNPC> Buyers { get; set; } = new Dictionary<string, EmpireNPC>(); // Key: DealerId, Value: EmpireNPC Buyer
		public static Dictionary<string, EmpireNPC> BuyersByDisplayName { get; set; } = new Dictionary<string, EmpireNPC>(); // Key: DealerId, Value: EmpireNPC Buyer
		public static bool IsInitialized { get; set; } = false;
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
            if (AllEmpireNPCs == null)
            {
                MelonLogger.Error("❌ AllEmpireNPCs is null.");
            }
            else
            {
                MelonLogger.Msg($"AllEmpireNPCs count: {AllEmpireNPCs.Count}");
            }

			if (npc == null)
			{
				MelonLogger.Error("❌ Attempted to register a null EmpireNPC.");
				return;
			}

			if (string.IsNullOrEmpty(npc.DealerId))
			{
				MelonLogger.Error("❌ EmpireNPC has no DealerId.");
				return;
			}

			// Prevent duplicate registration
			if (Buyers.ContainsKey(npc.DealerId))
			{
				MelonLogger.Warning($"⚠️ Empire NPC already registered: {npc.DealerId}");
				return;
			}

			// Register
			Buyers[npc.DealerId] = npc;
			BuyersByDisplayName[npc.DisplayName] = npc;
            npc.IsInitialized = true;
			npc.DealerSaveData.IsInitialized = true;    //	just to force serialization without text message hacks

			MelonLogger.Msg($"✅ Registered Empire NPC: {npc.DealerId}; Initialized: {npc.IsInitialized}");

			// Initialization check
			if (Buyers.Count == AllEmpireNPCs.Count)
			{
				IsInitialized = true;
				MelonLogger.Msg($"🎉 All Empire NPCs registered ({Buyers.Count}/{AllEmpireNPCs.Count}). Initialization complete.");
                Contacts.Update();
                MelonLogger.Msg("🔄 Contacts Update called after all NPCs registered.");
			}
		}

		public static EmpireNPC? GetBuyer(string dealerName)
        {
			BuyersByDisplayName.TryGetValue(dealerName, out var buyer);
            MelonLogger.Msg($"🔍 GetBuyer called for dealerName: {dealerName}, Found: {buyer != null}");

            return buyer;
        }

        /// <summary>
        /// Reset Contacts static state between scene loads to avoid leaking over the previous session.
        /// </summary>
        public static void Reset()
        {
            Buyers.Clear();
            IsInitialized = false;
            //IsUnlocked = false;
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
            MelonCoroutines.Start(UpdateCoroutine());
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
                    //if (!canUnlock)
                    //{
                    //    if (buyer.UnlockRequirements?.Any() == true)
                    //    {
                    //        foreach (var req in buyer.UnlockRequirements)
                    //        {
                    //            var unlockBuyer = GetBuyer(req.Name);
                    //            if (unlockBuyer != null)
                    //            {
                    //                MelonLogger.Msg($"Unlock Requirement: {req.Name}, Current Reputation: {unlockBuyer.DealerSaveData.Reputation}, Required Reputation: {req.MinRep}");
                    //            }
                    //            else
                    //            {
                    //                MelonLogger.Msg($"Unlock Requirement: {req.Name} not found.");
                    //            }
                    //        }
                    //    }
                    //}

                    if (canUnlock)
                    {
						MelonLogger.Msg($"✅ Dealer {buyer.DisplayName} unlock requirements met. Initialized: {buyer.IsInitialized}, Unlocked: {buyer.IsUnlocked}, buyer.DealerSaveData.IntroDone: {buyer.DealerSaveData.IntroDone}");
						if (!buyer.IsUnlocked)
                        {
                            buyer.IsUnlocked = true;
							
                            if (buyer.DealerSaveData.IntroDone == false) // First time Intro
							{
								buyer.SendCustomMessage(DialogueType.Intro);
								MelonLogger.Msg($"✅ Dealer {buyer.DisplayName} intro sent.");
								buyer.DealerSaveData.IntroDone = true; // Set IntroDone to true
							}

							MelonLogger.Msg($"✅ Dealer {buyer.DisplayName} is now unlocked.");
						}

						if (buyer.Debt != null && buyer.Debt.TotalDebt > 0 && buyer.DealerSaveData.DebtRemaining > 0)
						{
							buyer.DebtManager = new DebtManager(buyer);
							MelonLogger.Msg($"❌ Dealer {buyer.DisplayName} is locked due to debt: {buyer.Debt.TotalDebt}");
						}

						if (!buyer.IsInitialized)
                        {
                            buyer.IsInitialized = true;
                            MelonLogger.Msg($"✅ Initialized dealer: {buyer.DisplayName}");
                        }
                        buyer.UnlockDrug();
                    }
                    else
                    {
                        MelonLogger.Msg($"⚠️ Dealer {buyer.DisplayName} is locked (unlock requirements not met)");
                    }

                    //MelonLogger.Msg($"✅ Contacts.Buyers now contains {Buyers.Count} buyers.");
                    //IsUnlocked = true;
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