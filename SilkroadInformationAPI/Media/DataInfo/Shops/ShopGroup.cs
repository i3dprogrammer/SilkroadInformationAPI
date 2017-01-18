using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    public class ShopGroup
    {
        public string GroupName { get; set; }
        public List<ShopTab> GroupTabs;
        public ShopGroup(string name)
        {
            this.GroupName = name;
            GroupTabs = new List<ShopTab>();
        }
    }
}
