using System.Collections.Generic;


namespace Empire.NPC.Data
{
    public class Dialogue
    {
        public List<string> Intro { get; set; } = new List<string>();
        public List<string> DealStart { get; set; } = new List<string>();
        public List<string> Accept { get; set; } = new List<string>();
        public List<string> Incomplete { get; set; } = new List<string>();
        public List<string> Expire { get; set; } = new List<string>();
        public List<string> Fail { get; set; } = new List<string>();
        public List<string> Success { get; set; } = new List<string>();
        public List<string> Reward { get; set; } = new List<string>();
    }
}