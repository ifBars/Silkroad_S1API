using System.Collections.Generic;
using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class DealerData
    {
        public List<string> EffectsName { get; set; }
        public List<float> EffectsDollarMult { get; set; }
        public List<string> QualityTypes { get; set; }
        public List<float> QualitiesDollarMult { get; set; }
        public List<float> RandomNumberRanges { get; set; }
        public bool NoNecessaryEffects { get; set; } = false;
        public List<Dealer> Dealers { get; set; }
    }
}