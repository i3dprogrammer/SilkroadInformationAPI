using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class UpdateItemDurability
    {
        public static event Action<int> OnItemDurabilityChange;
        public static void Parse(Packet p) //0x3052
        {
            byte slot = p.ReadUInt8();
            if (Client.InventoryItems.ContainsKey(slot))
                Client.InventoryItems[slot].Stats["DURABILITY"] = p.ReadInt32();
            OnItemDurabilityChange?.Invoke(slot);
        }
    }
}
