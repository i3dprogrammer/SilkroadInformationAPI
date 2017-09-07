using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class ItemCountUpdatedDueAlchemyEventArgs : EventArgs
    {
        public Information.InventoryItem item;
        public ItemCountUpdatedDueAlchemyEventArgs(Information.InventoryItem _Item)
        {
            item = _Item;
        }
    }
}
