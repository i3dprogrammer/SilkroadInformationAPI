using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class InventoryOperationEventArgs : EventArgs
    {
        Information.InventoryItem item;
        Information.InventoryItem associated;

        public ChangeType ItemChangeType;

        public InventoryOperationEventArgs(Information.InventoryItem _item, Information.InventoryItem _associated = null)
        {
            item = _item;
            associated = _associated;
            ItemChangeType = ChangeType.None;
        }

        public InventoryOperationEventArgs(Information.InventoryItem _item, ChangeType _itemChangeType, Information.InventoryItem _associated = null)
        {
            item = _item;
            associated = _associated;
            ItemChangeType = _itemChangeType;
        }
        /// <summary>
        /// The item with the change taking place.
        /// </summary>
        /// <returns></returns>
        public Information.InventoryItem EffectedItem()
        {
            return item;
        }

        /// <summary>
        /// The item associated with the changes i.e. (if it got swapped with another item, this returns the other item)
        /// </summary>
        /// <returns>The associated SilkroadInformationAPI.Client.Information.Item</returns>
        public Information.InventoryItem AssociatedItem()
        {
            return associated;
        }

        public enum ChangeType
        {
            Inv_ItemSwappedWithAnotherItem,
            Inv_ItemTotallyAddedOnAnotherInstance,
            Inv_ItemPartiallyAddedOnAnotherInstance,
            Inv_ItemSplitted,
            Inv_ItemSlotChanged,
            Inv_ItemLooted,
            Inv_ItemThrown,
            Inv_ItemSoldToNPC,
            Inv_ItemBoughtbackFromNPC,
            Inv_ItemDisappeared,
            Inv_ItemBought,
            Storage_ItemSwappedWithAnotherItem,
            Storage_ItemSlotChanged,
            Storage_ItemAddedToStorage,
            Storage_ItemTakenFromStorage,
            Storage_ItemTotallyAddedOnAnotherInstance,
            Storage_ItemPartiallyAddedOnAnotherInstance,
            GuildStorage_ItemSwappedWithAnotherItem,
            GuildStorage_ItemSlotChanged,
            GuildStorage_ItemAddedStorage,
            GuildStorage_ItemTakenFromStorage,
            GuildStorage_ItemTotallyAddedOnAnotherInstance,
            GuildStorage_ItemPartiallyAddedOnAnotherInstance,
            PetInventory_ItemSwappedWithAnotherItem,
            PetInventory_ItemSlotChanged,
            PetInventory_ItemAddedToInventory,
            PetInventory_ItemTakenFromInventory,
            PetInventory_ItemTotallyAddedOnAnotherInstance,
            PetInventory_ItemPartiallyAddedOnAnotherInstance,
            None
        }

    }
}
