using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information
{
    class BasicInfo
    {
        public int RegionID { get; set; }
        public int ModelID { get; set; }
        public int CharacterID { get; set; }
        public int Level { get; set; }
        public int MaxGameLevel { get; set; }
        public long CurrentExp { get; set; }
        public int SPLoadingBar { get; set; }
        public ulong Gold { get; set; }
        public int SP { get; set; }
        public short StatPoints { get; set; }
        public bool Zerk { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int MaxInventorySlots { get; set; }
        public int CurrentInvItemsCount { get; set; }
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();
    }
}
