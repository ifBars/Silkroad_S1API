using Empire.DebtHelpers;
using Empire.NPC.Data;
using Empire.Reward;
using Empire.Utilities;
using Empire.Utilities.EffectHelpers;
using Empire.Utilities.QualityHelpers;
using MelonLoader;
using S1API.Entities;
using S1API.Internal.Utils;
using S1API.Saveables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Empire.NPC.S1API_NPCs
{
    public abstract class EmpireNPC : S1API.Entities.NPC
    {
        public override bool IsPhysical => false;
		public virtual bool IsInitialized { get; set; } = false;
		protected virtual string IconResourcePath(string fileName) => $"Empire.NPC.S1API_NPCs.Icons.{fileName}";
		public abstract string DealerId { get; }
		public new abstract string FirstName { get; }
		public new abstract string LastName { get; }
		public virtual string DisplayName => $"{FirstName} {LastName}";
		public virtual string Image => $"{DealerId}.png";
		public abstract int Tier { get; }
		public abstract List<UnlockRequirement> UnlockRequirements { get; }
		public abstract List<string> DealDays { get; }
		public abstract bool CurfewDeal { get; }
		public abstract List<List<float>> Deals { get; }
		public abstract int RefreshCost { get; }
		public abstract DealerReward Reward { get; }
		public abstract float RepLogBase { get; }
		public abstract List<Drug> Drugs { get; }
		public abstract List<Shipping> Shippings { get; }
		public new virtual Dialogue Dialogue => new Dialogue();
		public abstract DebtManager DebtManager { get; set; }
		public RewardManager RewardManager { get; set; } // Initialize RewardManager object
		public virtual Gift Gift
		{
			get { return new Gift(); }
		}

		public virtual Debt Debt
		{
			get { return new Debt(); }
		}

		[SaveableField("DealerSaveData")]
		protected DealerSaveData _DealerData = new DealerSaveData();
		public DealerSaveData DealerSaveData => _DealerData;

		protected Sprite? _npcSprite;		

		protected virtual void OnEmpireCreated() { }
		protected virtual void OnEmpireLoaded() { }

		public virtual Sprite? GetNPCSprite()
		{
			if (_npcSprite != null)
				return _npcSprite;

			try
			{
				_npcSprite = EmpireResourceLoader.LoadEmbeddedIcon(Image) ?? EmpireResourceLoader.LoadEmbeddedIcon("fallback-dealer.png");
			}
			catch (Exception e)
			{
				MelonLogger.Error($"❌ Error loading NPC sprite for {Image}: {e}");
				_npcSprite = EmpireResourceLoader.LoadEmbeddedIcon("fallback-dealer.png");
			}

			return _npcSprite;
		}

		protected override void ConfigurePrefab(NPCPrefabBuilder builder)
		{
			builder.WithIdentity(DealerId, FirstName, LastName)
				   .WithIcon(GetNPCSprite());
		}

		protected override void OnCreated()
		{
			base.OnCreated();
			OnEmpireCreated();
			MelonLogger.Msg($"🆕 Created Empire NPC: {DisplayName} (ID: {DealerId})");
		}

		protected override void OnLoaded()
		{
			base.OnLoaded();
			OnEmpireLoaded();
			MelonLogger.Msg($"📂 Loaded Empire NPC: {DisplayName} (ID: {DealerId})");

			Contacts.Buyers.Add(DealerId, this);
			MelonLogger.Msg($"Registered Empire NPC '{DisplayName}' with Contacts.");
		}

		public void IncreaseCompletedDeals(int amount)
		{
			_DealerData.DealsCompleted += amount;
			MelonLogger.Msg($"✅ {DisplayName}'s completed deals increased by {amount}. Total Completed Deals: {DealerSaveData.DealsCompleted}");
		}
		public void GiveReputation(int amount)
		{
			_DealerData.Reputation += amount;
			//if reputation <1 make it 1
			if (_DealerData.Reputation < 1)
			{
				_DealerData.Reputation = 1;
			}
			MelonLogger.Msg($"✅ {DisplayName}'s reputation increased by {amount}. New Reputation: {DealerSaveData.Reputation}");
		}

		//Send the message to the player using the phone app or return the message string only if returnMessage is true
		public string? SendCustomMessage(string messageType, string product = "", int amount = 0, string quality = "", List<string>? necessaryEffects = null, List<string>? optionalEffects = null, int dollar = 0, bool returnMessage = false, int index = -1)
		{
			// Use case-insensitive property lookup to avoid simple casing mismatches
			var prop = Dialogue.GetType().GetProperty(messageType, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			List<string>? messages = prop?.GetValue(Dialogue) as List<string>;

			// If messages is null or empty, log and fallback
			if (messages == null || messages.Count == 0)
			{
				MelonLogger.Msg($"❌ Message type '{messageType}' not found in Dialogue or contains no lines.");
				if (returnMessage)
				{
					return messageType;
				}
				else
				{
					SendTextMessage(messageType);	//	keep it for debugging purposes
					return null;
				} 
			}
			string line = "";
			// Safe random selection (messages.Count > 0 guaranteed here)
			if (index < 0 || index >= messages.Count)
			{
				line = messages[RandomUtils.RangeInt(0, messages.Count)];
			}
			else
			{
				line = messages[index];
			}

			// Resolve quality color safely. Guard against missing quality or missing QualityTypes
			//int qualityindex = -1;
			//try
			//{
			//	if (!string.IsNullOrWhiteSpace(quality))
			//	{
			//		var qualityTypes = JSONDeserializer.dealerData?.QualityTypes ?? new List<string>();
			//		qualityindex = Array.FindIndex(qualityTypes.ToArray(), q => q.Trim().ToLowerInvariant() == quality.Trim().ToLowerInvariant());
			//	}
			//}
			//catch
			//{
			//	MelonLogger.Error("❌ Error finding quality index.");
			//	qualityindex = -1;
			//}

			//string qualityColor = "#FFFFFF"; // default fallback color
			//if (qualityindex >= 0)
			//{
			//	try
			//	{
			//		if (QualityColors.Colors != null && qualityindex >= 0 && qualityindex < QualityColors.Colors.Length)
			//			qualityColor = QualityColors.Colors[qualityindex];
			//	}
			//	catch
			//	{
			//		MelonLogger.Error("❌ Error finding quality color.");
			//		qualityColor = "#FFFFFF";
			//	}
			//}

			string qualityColor = "#FFFFFF"; // fallback
	
			var info = quality?.GetQuality();
			if (info != null)
				qualityColor = info.Color;

			string formatted = line
				.Replace("{product}", $"<color=#FF0004>{product}</color>")
				.Replace("{amount}", $"<color=#FF0004>{amount}x</color>")
				.Replace("{quality}", $"<color={qualityColor}>{(string.IsNullOrWhiteSpace(quality) ? "unknown" : quality)}</color>")
				.Replace("{dollar}", $"<color=#FF0004>{dollar}</color>");

			if (necessaryEffects != null && necessaryEffects.Count > 0)
			{
				string requiredEffects = string.Join(", ", necessaryEffects.Select(e => $"<color=#FF0004>{e}</color>"));
				formatted = formatted.Replace("{effects}", requiredEffects);
			}
			else
			{
				formatted = formatted.Replace("{effects}", "none");
			}

			//if (optionalEffects != null && optionalEffects.Count > 0)
			//{
			//	string effects = string.Join(", ", optionalEffects.Select(e => $"<color=#00FFFF>{e}</color>"));
			//	formatted = formatted.Replace("{optionalEffects}", effects);
			//}
			//else
			//{
			//	formatted = formatted.Replace("{optionalEffects}", "none");
			//}

			string effects = (optionalEffects != null && optionalEffects.Count > 0)
				? string.Join(", ", optionalEffects.Select(e => $"<color=#00FFFF>{e}</color>"))
				: "none";

			formatted = formatted.Replace("{optionalEffects}", effects);

			//UPDATABELE - May change
			// If messageType==accept, add another line to formatted = "Remember that we only accept packages after curfew"
			if (messageType.Equals("accept", StringComparison.OrdinalIgnoreCase))
			{
				if (CurfewDeal)
					formatted += "\nRemember that we only accept packages under cover of night.";
				else
					formatted += "\nWe'll take that delivery any time!";
			}

			if (returnMessage)
			{
				return formatted;
			}
			else
			{
				SendTextMessage(formatted);
				return null;
			}
		}

		//public string GetDrugUnlockInfo()
		//{

		//	System.Text.StringBuilder info = new System.Text.StringBuilder();
		//	info.AppendLine($"<b>Dealer: {DisplayName}</b> (Reputation: <color=#FFFFFF>{DealerSaveData.Reputation}</color>)");
		//	//info.AppendLine("<u>Drug Unlocks</u>:");
		//	foreach (var drug in Drugs)
		//	{
		//		string drugStatus = drug.UnlockRep <= _DealerData.Reputation
		//			? "<color=#00FF00>Unlocked</color>"
		//			: $"<color=#FF4500>Locked (Unlock at: {drug.UnlockRep})</color>";
		//		info.AppendLine($"<b>• {drug.Type}</b> - {drugStatus}");

		//		// Qualities
		//		if (drug.Qualities != null && drug.Qualities.Count > 0)
		//		{
		//			info.AppendLine("  Qualities:");
		//			foreach (var quality in drug.Qualities)
		//			{
		//				float effectiveQuality = quality.DollarMult;
		//				if (QualityRegistry.Qualities.ContainsKey(quality.Type))
		//				{
		//					effectiveQuality += QualityRegistry.Qualities[quality.Type];
		//				}
		//				if (JSONDeserializer.QualitiesDollarMult.ContainsKey(quality.Type))
		//				{
		//					effectiveQuality += JSONDeserializer.QualitiesDollarMult[quality.Type];
		//				}
		//				string qualityStatus = quality.UnlockRep <= _DealerData.Reputation
		//					? "<color=#00FF00>Unlocked</color>"
		//					: $"<color=#FF4500>Locked (Unlock at: {quality.UnlockRep})</color>";
		//				info.AppendLine($"    - {quality.Type} (x<color=#00FFFF>{effectiveQuality:F2}</color>) : {qualityStatus}");
		//			}
		//		}
		//		// Effects
		//		if (drug.Effects != null && drug.Effects.Count > 0)
		//		{
		//			info.AppendLine("  Effects:");
		//			foreach (var effect in drug.Effects)
		//			{
		//				float effectiveEffect = effect.DollarMult;
		//				if (JSONDeserializer.EffectsDollarMult.ContainsKey((effect.Name).ToLower().Trim()))
		//				{
		//					effectiveEffect += JSONDeserializer.EffectsDollarMult[(effect.Name).ToLower().Trim()];
		//				}
		//				string effectStatus = effect.UnlockRep <= _DealerData.Reputation
		//					? "<color=#00FF00>Unlocked</color>"
		//					: $"<color=#FF4500>Locked (Unlock at: {effect.UnlockRep})</color>";
		//				info.AppendLine($"    - {effect.Name} (Prob <color=#FFA500>{effect.Probability:F2}</color>, x<color=#00FFFF>{effectiveEffect:F2}</color>) : {effectStatus}");
		//			}
		//		}
		//		info.AppendLine(""); // spacer
		//	}
		//	return info.ToString();
		//}

		public string GetDrugUnlockInfo()
		{
			var info = new System.Text.StringBuilder();

			info.AppendLine(
				$"<b>Dealer: {DisplayName}</b> " +
				$"(Reputation: <color=#FFFFFF>{DealerSaveData.Reputation}</color>)");

			foreach (var drug in Drugs)
			{
				info.AppendLine($"<b>• {drug.Type}</b> - {Status(drug.UnlockRep)}");

				// Qualities
				if (drug.Qualities?.Count > 0)
				{
					info.AppendLine("  Qualities:");

					foreach (var quality in drug.Qualities)
					{
						var qInfo = quality.Type.GetQuality();
						float effective = quality.DollarMult + (qInfo?.DollarMult ?? 0f);

						info.AppendLine(
							$"    - {quality.Type} " +
							$"(x<color=#00FFFF>{effective:F2}</color>) : {Status(quality.UnlockRep)}");
					}
				}

				// Effects
				if (drug.Effects?.Count > 0)
				{
					info.AppendLine("  Effects:");

					foreach (var effect in drug.Effects)
					{
						var eInfo = effect.Name.GetEffect();
						float effective = effect.DollarMult + (eInfo?.DollarMult ?? 0f);

						info.AppendLine(
							$"    - {effect.Name} " +
							$"(Prob <color=#FFA500>{effect.Probability:F2}</color>, " +
							$"x<color=#00FFFF>{effective:F2}</color>) : {Status(effect.UnlockRep)}");
					}
				}

				info.AppendLine();
			}

			return info.ToString();
		}

		private string Status(int unlockRep)
		{
			return unlockRep <= DealerSaveData.Reputation ? "<color=#00FF00>Unlocked</color>" : $"<color=#FF4500>Locked (Unlock at: {unlockRep})</color>";
		}

		//A method that upgrades ShippingTier to the next available shipping option
		public bool UpgradeShipping()
		{
			if (_DealerData.ShippingTier < Shippings.Count - 1)
			{
				_DealerData.ShippingTier++;
				MelonLogger.Msg($"✅ Shipping upgraded to tier {_DealerData.ShippingTier}.");
				return true;
			}
			else
			{
				MelonLogger.Msg($"⚠️ Shipping already at max tier {_DealerData.ShippingTier}.");
				return false;
			}
		}

		//A method to check if the new reputation unlocks any new drug, quality or effects for the dealer 
		public void UnlockDrug()
		{
			// Initialize the drug list
			var drugList = Drugs ?? new List<Drug>();

			// Filter drugs based on the current reputation and use null-coalescing for qualities and effects
			var validDrugs = drugList
				.Where(d => d.UnlockRep <= _DealerData.Reputation) // Unlock drugs with UnlockRep <= current reputation
				.Select(d => new Drug
				{
					Type = d.Type,
					UnlockRep = d.UnlockRep,
					BaseDollar = d.BaseDollar,
					BaseRep = d.BaseRep,
					BaseXp = d.BaseXp,
					RepMult = d.RepMult,
					XpMult = d.XpMult,
					// Unlock qualities where UnlockRep <= current reputation safely
					Qualities = (d.Qualities?.Where(q => q.UnlockRep <= DealerSaveData.Reputation).ToList()) ?? new List<Quality>(),
					// Unlock effects where UnlockRep <= current reputation safely
					Effects = (d.Effects?.Where(e => e.UnlockRep <= DealerSaveData.Reputation).ToList()) ?? new List<Effect>()
				})
				.ToList();

			// Update the DealerSaveData with the unlocked drugs
			DealerSaveData.UnlockedDrugs = validDrugs;

			// Log the unlocked drugs for debugging
			MelonLogger.Msg($"   Found {validDrugs.Count} drug(s) unlocked for dealer '{DisplayName}' at rep {DealerSaveData.Reputation}.");
			foreach (var drug in validDrugs)
			{
				MelonLogger.Msg($"      Drug: {drug.Type} | UnlockRep: {drug.UnlockRep}");
				MelonLogger.Msg($"         Unlocked Qualities: {string.Join(", ", drug.Qualities.Select(q => q.Type))}");
				MelonLogger.Msg($"         Unlocked Effects: {string.Join(", ", drug.Effects.Select(e => e.Name))}");
			}
		}
	}
}
