using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadInformationAPI.Client.Information;

namespace SilkroadInformationAPI.Client
{
    class Client
    {
        public static BasicInfo BasicInfo = new BasicInfo();
        public static Dictionary<int, Item> InventoryItems = new Dictionary<int, Item>();
        public static Dictionary<int, Item> StorageItems = new Dictionary<int, Item>();
        //public static Dictionary<int, Item> SoldItems = new Dictionary<int, Item>();
        public static List<Item> SoldItems = new List<Item>();
    }
}
