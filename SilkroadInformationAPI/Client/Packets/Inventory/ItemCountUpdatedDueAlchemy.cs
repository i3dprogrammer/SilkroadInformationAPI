using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class ItemCountUpdatedDueAlchemy
    {

        public delegate void ItemCountUpdatedDueAlchemyHandler(ItemCountUpdatedDueAlchemyEventArgs e);

        public static event ItemCountUpdatedDueAlchemyHandler OnItemCountUpdatedDueAlchemyEvent;

        public static void Parse(Packet p)
        {
            int slot = p.ReadInt8();
            int flag = p.ReadInt8();
            if(flag == 0x08)
            {
                int newCount = p.ReadInt8();
                Client.InventoryItems[slot].Stack = newCount;
                ItemCountUpdatedDueAlchemyEventArgs args = new ItemCountUpdatedDueAlchemyEventArgs(Client.InventoryItems[slot]);

                OnItemCountUpdatedDueAlchemyEvent?.Invoke(args);
            }
        }
    }
}
