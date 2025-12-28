using System;
using System.Collections.Generic;
using Empire.DebtHelpers;
using Empire.NPC.Data;

namespace Empire.NPC.S1API_NPCs
{ 
	public class Krazy8 : EmpireNPC
	{
		public override string DealerId => "krazy8";

		public override string FirstName => "Domingo";
		public override string LastName => "Molina"; // Using real name; DisplayName can show nickname

		public override string DisplayName => "Domingo 'Krazy-8' Molina";

		public override int Tier => 2;

		public override List<UnlockRequirement> UnlockRequirements
		{
			get
			{
				List<UnlockRequirement> reqs = new List<UnlockRequirement>();

				UnlockRequirement r1 = new UnlockRequirement();
				r1.Name = "Combo Costco";
				r1.MinRep = 100;
				reqs.Add(r1);

				return reqs;
			}
		}

		public override List<string> DealDays
		{
			get
			{
				return new List<string>
			{
				"Monday",
				"Wednesday",
				"Friday",
				"Saturday"
			};
			}
		}

		public override bool CurfewDeal => false;

		public override List<List<float>> Deals
		{
			get
			{
				return new List<List<float>>
			{
				new List<float> { 2f, 0.85f, 8186f, 50f },
				new List<float> { 4f, 0.70f, 10783f, 75f }
			};
			}
		}

		public override int RefreshCost => 420;

		public override DealerReward Reward
		{
			get
			{
				DealerReward reward = new DealerReward();
				reward.RepCost = 100;
				reward.unlockRep = 200;
				reward.Type = "console";
				reward.Args = new List<string> { "spawnvehicle", "shitbox" };
				return reward;
			}
		}

		public override float RepLogBase => 4f;

		public override List<Drug> Drugs
		{
			get
			{
				// Qualities
				List<Quality> qualities = new List<Quality>
			{
				new Quality { Type = "poor", DollarMult = 0f, UnlockRep = 0 },
				new Quality { Type = "standard", DollarMult = 0f, UnlockRep = 70 }
			};

				// Effects
				List<Effect> effects = new List<Effect>
			{
				new Effect { Name = "Gingeritis", UnlockRep = 0, Probability = 2.0f, DollarMult = 0f },
				new Effect { Name = "Random", UnlockRep = 130, Probability = 1.2f, DollarMult = 0f },
				new Effect { Name = "CalorieDense", UnlockRep = 375, Probability = 1.0f, DollarMult = 0f }
			};

				Drug weed = new Drug();
				weed.Type = "weed";
				weed.UnlockRep = 0;
				weed.BaseDollar = 17;
				weed.BaseRep = 42;
				weed.BaseXp = 31;
				weed.RepMult = 0.002f;
				weed.XpMult = 0.001f;
				weed.Qualities = qualities;
				weed.Effects = effects;

				return new List<Drug> { weed };
			}
		}

		public override List<Shipping> Shippings
		{
			get
			{
				Shipping s1 = new Shipping();
				s1.Name = "Krazy-8's Quick Drop";
				s1.Cost = 0;
				s1.UnlockRep = 0;
				s1.MinAmount = 1;
				s1.StepAmount = 1;
				s1.MaxAmount = 10;
				s1.DealModifier = new List<float> { 1f, 1f, 1f, 1f };

				Shipping s2 = new Shipping();
				s2.Name = "Krazy-8's Heavy Haul";
				s2.Cost = 5000;
				s2.UnlockRep = 135;
				s2.MinAmount = 5;
				s2.StepAmount = 5;
				s2.MaxAmount = 20;
				s2.DealModifier = new List<float> { 1.25f, 1.25f, 1.25f, 1.25f };

				return new List<Shipping> { s1, s2 };
			}
		}

		public override Dialogue Dialogue
		{
			get
			{
				Dialogue d = new Dialogue();

				d.Intro = new List<string>
			{
				"You got something for me? Let's make this professional."
			};

				d.DealStart = new List<string>
			{
				"I require {amount} of {quality} {product} with required effects: {effects} and optional effects: {optionalEffects}.",
				"Get me {amount} {quality} {product} that includes required effects: {effects} and optional effects: {optionalEffects}.",
				"Ensure {amount} {quality} {product} delivers required effects: {effects} and optional effects: {optionalEffects}."
			};

				d.Accept = new List<string>
			{
				"Good. Stick to the plan.",
				"Alright. Don't deviate."
			};

				d.Incomplete = new List<string>
			{
				"This isn't the full count. Where's the rest? {amount} was the number.",
				"Don't play stupid. You're short {amount}. Fix it.",
				"The job requires {amount}. This is partial. Get the rest."
			};

				d.Expire = new List<string>
			{
				"You missed the window. That's sloppy. Job's off.",
				"Too late. Lack of punctuality creates loose ends. We're done."
			};

				d.Fail = new List<string>
			{
				"This isn't what was agreed upon. Poor quality. Amateur mistake.",
				"You compromised the operation. That's unacceptable. Get lost."
			};

				d.Success = new List<string>
			{
				"Looks right. Professional.",
				"Okay. Meets spec.",
				"Solid work."
			};

				d.Reward = new List<string>
			{
				"Here's the payment. Earned it. {dollar}",
				"Funds transferred. Keep this level of competence. {dollar}",
				"Your cut. No half measures. {dollar}"
			};

				return d;
			}
		}

		public override Gift Gift
		{
			get
			{
				Gift g = new Gift();
				g.Cost = 2000;
				g.Rep = 40;
				return g;
			}
		}

		public override Debt Debt
		{
			get
			{
				Debt d = new Debt();
				d.TotalDebt = 10000f;
				d.InterestRate = 0.1f;
				d.DayMultiple = 10f;
				d.DayExponent = 2.0f;
				d.ProductBonus = 2.0f;
				return d;
			}
		}

        public override DebtManager DebtManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }	
}
