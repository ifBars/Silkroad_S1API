using Newtonsoft.Json;


namespace Empire.NPC.Data
{
    public class Gift
    {
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("rep")]
        public int Rep { get; set; }
    }
}