using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class Quality
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        public int UnlockRep { get; set; }
        public float DollarMult { get; set; }
        //public bool TakeFromList { get; set; } // Change to use JSON "take_from_list"
    }
}