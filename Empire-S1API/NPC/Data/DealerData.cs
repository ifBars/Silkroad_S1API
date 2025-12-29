using System.Collections.Generic;
using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class DealerData
    {
        [JsonProperty("effectsName")]
        public List<string> EffectsName { get; set; }
        [JsonProperty("effectsDollarMult")]
        public List<float> EffectsDollarMult { get; set; }
        [JsonProperty("qualityTypes")]
        public List<string> QualityTypes { get; set; }
        [JsonProperty("qualitiesDollarMult")]
        public List<float> QualitiesDollarMult { get; set; }
        [JsonProperty("randomNumberRanges")]
        public List<float> RandomNumberRanges { get; set; }
        [JsonProperty("noNecessaryEffects")]
        public bool NoNecessaryEffects { get; set; } = false;
        [JsonProperty("dealers")]
        public List<Dealer> Dealers { get; set; }
    }
}