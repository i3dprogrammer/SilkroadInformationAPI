using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadInformationAPI.Client.Information;
using SilkroadInformationAPI.Client.Information.Quests;

namespace SilkroadInformationAPI.Client
{
    class Client
    {
        public static Info Info = new Info();
        public static Dictionary<int, InventoryItem> InventoryItems = new Dictionary<int, InventoryItem>();
        public static Dictionary<int, InventoryItem> StorageItems = new Dictionary<int, InventoryItem>();
        //public static Dictionary<int, Item> SoldItems = new Dictionary<int, Item>();
        public static List<InventoryItem> SoldItems = new List<InventoryItem>();
        public static List<Quest> GameQuests = new List<Quest>();
        public static List<Mastery> GameMasteries = new List<Mastery>();
        public static List<Skill> GameSkills = new List<Skill>();
        public static Information.BasicInfo.Position Position = new Information.BasicInfo.Position();
        public static Information.BasicInfo.Movement Movement = new Information.BasicInfo.Movement();
        public static Information.BasicInfo.State State = new Information.BasicInfo.State();
    }
}
