using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    class StallUtility
    {
        public static void ParseCurrentStallItems(Packet p)
        {
            while (true)
            {
                var item = Inventory.InventoryUtility.ParseItem(p);
                if (item == null)
                    break;

                p.ReadUInt8(); // ??
                item.Stack = p.ReadUInt16();
                item.Price = p.ReadUInt64();

                Client.CurrentStall.StallItems.Add((byte)item.Slot, item);
            }
        }

        public static void ClearCurrentStall()
        {
            Client.CurrentStall.StallItems.Clear();
            Client.CurrentStall.PeopleInStall.Clear();
        }
    }
}
