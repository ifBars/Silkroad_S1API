using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Empire.NPC.Data
{
    public class BuyerSaveData
    {
        public Dictionary<string, DealerSaveData> Dealers;

        public BuyerSaveData()
        {
            Dealers = new Dictionary<string, DealerSaveData>();
        }
    }
}