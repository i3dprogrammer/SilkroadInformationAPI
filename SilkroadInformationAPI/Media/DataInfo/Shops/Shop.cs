using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    public class Shop
    {
        public string StoreName { get; set; }
        public string StoreGroupName { get; set; }
        public string NPCName { get; set; }
        public List<ShopGroup> ShopGroups;

        public Shop(string name)
        {
            this.StoreName = name;
            ShopGroups = new List<ShopGroup>();
        }

        public ShopTab GetTabFromIndex(byte index)
        {
            int total = 0;
            for(int i=0;i<ShopGroups.Count;i++)
            {
                for(int j = 0; j < ShopGroups[i].GroupTabs.Count; j++)
                {
                    if (index == total)
                        return ShopGroups[i].GroupTabs[j];
                    total++;
                }
            }

            return null;
        }

    }
}
