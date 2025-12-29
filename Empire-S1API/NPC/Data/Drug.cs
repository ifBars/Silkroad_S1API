using System.Collections.Generic;
using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class Drug
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("unlockRep")]
        public int UnlockRep { get; set; }
        [JsonProperty("base_dollar")]
        public int BaseDollar { get; set; } // Changed from BonusDollar: now reflects JSON "base_dollar"
        [JsonProperty("base_rep")]
        public int BaseRep { get; set; } // Changed from BonusRep
        [JsonProperty("base_xp")]
        public int BaseXp { get; set; }
        [JsonProperty("rep_mult")]
        public float RepMult { get; set; } // Changed to use JSON "rep_mult"
        [JsonProperty("xp_mult")]
        public float XpMult { get; set; } // Changed to use JSON "xp_mult"
        [JsonProperty("qualities")]
        public List<Quality> Qualities { get; set; }
        [JsonProperty("effects")]
        public List<Effect> Effects { get; set; }
    }
}