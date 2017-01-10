using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    class StorageInfoResponse
    {

        private static Packet StorageInfoPacket;

        public static void StorageInfoStart()
        {
            StorageInfoPacket = new Packet(0x3013, false, true);
        }

        public static void StorageInfoData(Packet p)
        {
            StorageInfoPacket.WriteUInt8Array(p.GetBytes());
        }

        public static void StorageInfoEnd()
        {
            Parse(StorageInfoPacket);
        }

        private static void Parse(Packet p)
        {
            Client.StorageItems.Clear();

            int maxItems = p.ReadInt8();
            int currentItems = p.ReadInt8();
            for (int i = 0; i < currentItems; i++) {
                Information.InventoryItem item = InventoryUtility.ParseItem(p);
                Client.StorageItems.Add(item.Slot, item);
            }
        }
    }
}
