using System.Collections.Generic;
using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class DealerReward
    {
        [JsonProperty("unlockRep")]
        public int unlockRep { get; set; }
        [JsonProperty("rep_cost")]
        public int RepCost { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("args")]
        public List<string> Args { get; set; }
    }
}