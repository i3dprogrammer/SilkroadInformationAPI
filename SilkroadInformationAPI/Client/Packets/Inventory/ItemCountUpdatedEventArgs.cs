using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class ItemCountUpdatedEventArgs
    {
        Information.InventoryItem Item;

        public ItemCountUpdatedEventArgs(Information.InventoryItem _item)
        {
            Item = _item;
        }
    }
}
