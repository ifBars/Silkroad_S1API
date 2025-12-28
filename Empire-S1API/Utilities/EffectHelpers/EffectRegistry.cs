using System.Collections.Generic;
using System.Linq;

namespace Empire.Utilities.EffectHelpers
{
    public static class EffectRegistry
	{
		public static readonly List<EffectInfo> Effects = new List<EffectInfo>()
		{
			new EffectInfo { Name = "AntiGravity", DollarMult = 0.46f },
			new EffectInfo { Name = "Athletic", DollarMult = 0.68f },
			new EffectInfo { Name = "Balding", DollarMult = 0.70f },
			new EffectInfo { Name = "BrightEyed", DollarMult = 0.60f },
			new EffectInfo { Name = "Calming", DollarMult = 0.90f },
			new EffectInfo { Name = "CalorieDense", DollarMult = 0.72f },
			new EffectInfo { Name = "Cyclopean", DollarMult = 0.44f },
			new EffectInfo { Name = "Disorienting", DollarMult = 1.00f },
			new EffectInfo { Name = "Electrifying", DollarMult = 0.50f },
			new EffectInfo { Name = "Energizing", DollarMult = 0.78f },
			new EffectInfo { Name = "Euphoric", DollarMult = 0.82f },
			new EffectInfo { Name = "Explosive", DollarMult = 1.00f },
			new EffectInfo { Name = "Focused", DollarMult = 0.84f },
			new EffectInfo { Name = "Foggy", DollarMult = 0.64f },
			new EffectInfo { Name = "Gingeritis", DollarMult = 0.80f },
			new EffectInfo { Name = "Glowie", DollarMult = 0.52f },
			new EffectInfo { Name = "Jennerising", DollarMult = 0.58f },
			new EffectInfo { Name = "Laxative", DollarMult = 1.00f },
			new EffectInfo { Name = "LongFaced", DollarMult = 0.48f },
			new EffectInfo { Name = "Munchies", DollarMult = 0.88f },
			new EffectInfo { Name = "Paranoia", DollarMult = 1.00f },
			new EffectInfo { Name = "Refreshing", DollarMult = 0.86f },
			new EffectInfo { Name = "Schizophrenic", DollarMult = 1.00f },
			new EffectInfo { Name = "Sedating", DollarMult = 0.74f },
			new EffectInfo { Name = "Seizure", DollarMult = 1.00f },
			new EffectInfo { Name = "Shrinking", DollarMult = 0.40f },
			new EffectInfo { Name = "Slippery", DollarMult = 0.66f },
			new EffectInfo { Name = "Smelly", DollarMult = 1.00f },
			new EffectInfo { Name = "Sneaky", DollarMult = 0.76f },
			new EffectInfo { Name = "Spicy", DollarMult = 0.62f },
			new EffectInfo { Name = "ThoughtProvoking", DollarMult = 0.56f },
			new EffectInfo { Name = "Toxic", DollarMult = 1.00f },
			new EffectInfo { Name = "TropicThunder", DollarMult = 0.54f },
			new EffectInfo { Name = "Zombifying", DollarMult = 0.42f },
			new EffectInfo { Name = "Random", DollarMult = 0.00f }
		};

		public static readonly Dictionary<string, EffectInfo> ByName =
			Effects.ToDictionary(e => e.Name, e => e);
	}
}