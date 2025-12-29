using System.Collections.Generic;
using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class Shipping
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("unlockRep")]
        public int UnlockRep { get; set; }
        [JsonProperty("minAmount")]
        public int MinAmount { get; set; }
        [JsonProperty("stepAmount")]
        public int StepAmount { get; set; }
        [JsonProperty("maxAmount")]
        public int MaxAmount { get; set; }
        [JsonProperty("dealModifier")]
        public List<float> DealModifier { get; set; }
    }
}