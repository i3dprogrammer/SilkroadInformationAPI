using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class ItemCountUpdated
    {
        public delegate void ItemCountUpdatedHandler(ItemCountUpdatedEventArgs e);
        public static event ItemCountUpdatedHandler OnItemCountUpdate;

        public static void Parse(Packet p)
        {
            int flag = p.ReadInt8(); //0x01 = item used succesfully, 0x02 error occured
            if(flag == 0x01)
            {
                int itemSlot = p.ReadInt8();
                int count = p.ReadInt16();
                Client.InventoryItems[itemSlot].Stack = count;

                OnItemCountUpdate?.Invoke(new ItemCountUpdatedEventArgs(Client.InventoryItems[itemSlot]));
            }
        }
    }
}
