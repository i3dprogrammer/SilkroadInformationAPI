using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    class Shop
    {
        public string StoreName { get; set; }
        public string StoreGroupName { get; set; }
        public string NPCName { get; set; }
        public List<ShopGroup> ShopGroups;
        public Shop(string name)
        {
            this.StoreName = name;
            ShopGroups = new List<Shops.ShopGroup>();
        }
    }
}
