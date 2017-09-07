using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    public class ShopTab
    {
        public string TabName { get; set; }
        public List<ShopItemPackage> TabItems;
        public ShopTab(string tabName)
        {
            this.TabName = tabName;
            TabItems = new List<ShopItemPackage>();
        }
    }
}
