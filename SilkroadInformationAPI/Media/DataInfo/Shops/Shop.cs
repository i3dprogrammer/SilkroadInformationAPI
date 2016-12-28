using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    class Shop
    {
        public string MediaName { get; set; }
        public List<ShopGroup> ShopGroups;
        public Shop(string name)
        {
            this.MediaName = name;
            ShopGroups = new List<Shops.ShopGroup>();
        }
    }
}
