using Empire.DebtHelpers;
using Empire.NPC.Data;
using System.Collections.Generic;

namespace Empire.NPC.S1API_NPCs
{
	public class ComboCostco : EmpireNPC
	{
		public override string DealerId => "combo_costco";
		public override string FirstName => "Combo";
		public override string LastName => "Costco";
		public override int Tier => 1;

		private List<UnlockRequirement>? _unlockedRequirements;
		public override List<UnlockRequirement> UnlockRequirements
		{
			get { return new List<UnlockRequirement>(); }   //	Combo Costco has no unlock requirements
		}

		public override List<string> DealDays
		{
			get
			{
				return new List<string>
				{
					"Monday",
					"Tuesday",
					"Wednesday",
					"Thursday"
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
					new List<float> { 1f, 0.85f, 941f, 8f },
					new List<float> { 2f, 0.7f, 1112f, 12f }
				};
			}
		}

		public override int RefreshCost => 420;

		private DealerReward? _reward;
		public override DealerReward Reward
		{
			get
			{
				if (_reward != null)
					return _reward;
				
				DealerReward reward = new DealerReward();
				reward.unlockRep = 100;
				reward.RepCost = 40;
				reward.Type = "console";
				reward.Args = new List<string> { "give", "cocaine", "5" };

				_reward = reward;

				return _reward;
			}
		}

		public override float RepLogBase => 3f;

		private List<Drug>? _drugs;

		public override List<Drug> Drugs
		{
			get
			{
				if (_drugs != null)
					return _drugs;

				var qualities = new List<Quality>
				{
					new Quality { Type = "poor",     DollarMult = 0f, UnlockRep = 0 },
					new Quality { Type = "standard", DollarMult = 0f, UnlockRep = 58 }
				};

				var effects = new List<Effect>
				{
					new Effect { Name = "Energizing", UnlockRep = 0,   Probability = 2.0f, DollarMult = 0f },
					new Effect { Name = "Euphoric",   UnlockRep = 33,  Probability = 1.0f, DollarMult = 0f },
					new Effect { Name = "Random",     UnlockRep = 78,  Probability = 0.3f, DollarMult = 0f },
					new Effect { Name = "Random",     UnlockRep = 100, Probability = 0.6f, DollarMult = 0f }
				};

				var drug = new Drug
				{
					Type = "weed",
					UnlockRep = 0,
					BaseDollar = 10,
					BaseRep = 9,
					BaseXp = 6,
					RepMult = 0.001f,
					XpMult = 0.001f,
					Qualities = qualities,
					Effects = effects
				};

				// Cache the list
				_drugs = new List<Drug> { drug };

				return _drugs;
			}
		}

		private List<Shipping>? _shippings;
		public override List<Shipping> Shippings
		{
			get
			{
				if (_shippings != null)
					return _shippings;

				_shippings = new List<Shipping>
				{
					new Shipping
					{
						Name = "BMX Brigade",
						Cost = 0,
						UnlockRep = 0,
						MinAmount = 1,
						StepAmount = 1,
						MaxAmount = 10,
						DealModifier = new List<float> { 1f, 1f, 1f, 1f }
					},
					new Shipping
					{
						Name = "Uncle Nelson's Loaner",
						Cost = 5000,
						UnlockRep = 135,
						MinAmount = 5,
						StepAmount = 5,
						MaxAmount = 20,
						DealModifier = new List<float> { 1.25f, 1.25f, 1.25f, 1.25f }
					}
				};

				return _shippings;
			}

		}

		private Dialogue? _dialogue;
		public override Dialogue Dialogue
		{
			get
			{
				if (_dialogue != null)
					return _dialogue;

				Dialogue d = new Dialogue();
				d.Intro = new List<string>
				{
					"Yo, word on the street is you're the supplier. Uncle Nelsons kid, yeah? You holdin'?"
				};
				d.DealStart = new List<string>
				{
					"Alright, check it, I need {amount} of {quality} {product} with required effects: {effects} and optional effects: {optionalEffects}. Got it?",
					"Look, my people need {amount} of {quality} {product}. Ensure it has required effects: {effects} and optional effects: {optionalEffects}. You strapped?",
					"Yo, listen up, {amount} {quality} {product}. It must deliver required effects: {effects} along with optional effects: {optionalEffects}. Don't waste my time."
				};
				d.Accept = new List<string>
				{
					"Deal’s on, man!",
					"Cool, let’s roll with it!"
				};
				d.Incomplete = new List<string>
				{
					"Nah man, this ain't it. Need the full {amount}. Step it up.",
					"Yo, where's the rest? Need {amount} total, bro.",
					"This ain't gonna cut it. Bring the remaining {amount}."
				};
				d.Expire = new List<string>
				{
					"Too slow, man. My corner's dry now. Deal's off.",
					"You took your sweet time, huh? Lost the sale. We're done."
				};
				d.Fail = new List<string>
				{
					"What the hell is this? You trying to get me popped? Get outta here!",
					"You messed up BAD. This ain't gonna fly. Lose my number."
				};
				d.Success = new List<string>
				{
					"Alright, looks good. Solid.",
					"Yeah, this is the stuff. Nice.",
					"Finally. This'll move."
				};
				d.Reward = new List<string>
				{
					"Here's your cut of ${dollar}, man. Keep it coming.",
					"Cash is here.{dollar} Dollars. Good business.",
					"Payment of ${dollar} sent. Don't get sloppy."
				};

				_dialogue = d;
				return _dialogue;
			}
		}

		public override Gift Gift
		{
			get
			{
				Gift g = new Gift();
				g.Cost = 1000;
				g.Rep = 20;
				return g;
			}
		}

        public override DebtManager DebtManager { get => DebtManager; set => DebtManager = value; }
    }
}