using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class Effect
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("unlockRep")]
        public int UnlockRep { get; set; }
        [JsonProperty("probability")]
        public float Probability { get; set; }
        [JsonProperty("dollar_mult")]
        public float DollarMult { get; set; }
        //public bool TakeFromList { get; set; } // Change to use JSON "take_from_list"
    }
}