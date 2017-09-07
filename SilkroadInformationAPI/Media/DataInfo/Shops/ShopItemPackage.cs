using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo.Shops
{
    public class ShopItemPackage
    {
        public string PackageName { get; set; }
        public string ItemMediaName { get; set; }
        public int PackagePosition { get; set; }

        public ShopItemPackage(string packName, int packPos)
        {
            this.PackageName = packName;
            this.PackagePosition = packPos;
        }
    }
}
