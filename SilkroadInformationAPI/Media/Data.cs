using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media
{
    public class Data
    {
        public static Dictionary<uint, DataInfo.MediaModel> MediaModels = new Dictionary<uint, DataInfo.MediaModel>();
        public static Dictionary<uint, DataInfo.Item> MediaItems = new Dictionary<uint, DataInfo.Item>();
        public static Dictionary<uint, DataInfo.Skill> MediaSkills = new Dictionary<uint, DataInfo.Skill>();
        public static List<DataInfo.Shops.Shop> MediaShops = new List<DataInfo.Shops.Shop>();
        public static Dictionary<uint, DataInfo.Region> MediaRegions = new Dictionary<uint, DataInfo.Region>();
        public static Dictionary<string, List<DataInfo.Shops.ShopTab>> refshoptab = new Dictionary<string, List<DataInfo.Shops.ShopTab>>();
        public static Dictionary<string, List<DataInfo.Shops.ShopItemPackage>> refshopgoods = new Dictionary<string, List<DataInfo.Shops.ShopItemPackage>>();
        public static Dictionary<string, string> refscrapofpackageitem = new Dictionary<string, string>();
        public static Dictionary<string, List<DataInfo.Shops.ShopGroup>> refmappingshopwithtab = new Dictionary<string, List<DataInfo.Shops.ShopGroup>>();
        public static Dictionary<string, string> refmappingshopgroup = new Dictionary<string, string>();
        public static Dictionary<string, string> refshopgroup = new Dictionary<string, string>();
        public static Dictionary<int, string> MediaBlues = new Dictionary<int, string>();
        public static Dictionary<string, string> Translation = new Dictionary<string, string>();
        public static Dictionary<int, ulong> LevelDataMaxEXP = new Dictionary<int, ulong>();
        public static Dictionary<int, string> TextZoneName = new Dictionary<int, string>();
        public static DataInfo.ServerInfo ServerInfo = new DataInfo.ServerInfo();
    }
}
