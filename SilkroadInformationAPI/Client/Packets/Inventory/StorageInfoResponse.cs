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
        public static void Parse(Packet p)
        {
            Client.StorageItems.Clear();

            int maxItems = p.ReadInt8();
            int currentItems = p.ReadInt8();
            for (int i = 0; i < currentItems; i++) {
                Information.InventoryItem item = ParseItem.Parse(p);
                Client.StorageItems.Add(item.Slot, item);
            }
        }
    }
}
