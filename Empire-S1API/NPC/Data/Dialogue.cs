using System.Collections.Generic;


namespace Empire.NPC.Data
{
    public class Dialogue
    {
        public List<string> Intro { get; set; }
        public List<string> DealStart { get; set; }
        public List<string> Accept { get; set; }
        public List<string> Incomplete { get; set; }
        public List<string> Expire { get; set; }
        public List<string> Fail { get; set; }
        public List<string> Success { get; set; }
        public List<string> Reward { get; set; }
    }
}