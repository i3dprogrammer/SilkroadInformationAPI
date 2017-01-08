using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
using static SilkroadInformationAPI.Client.Packets.Inventory.InventoryItemsUpdated.ItemSlotChangedEventArgs;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    class InventoryUtility
    {
        public static InventoryItemsUpdated.ItemSlotChangedEventArgs ParseSlotChangedUpdate(Packet p, Dictionary<int, Information.InventoryItem> SpecificInventory, string argName)
        {
            InventoryItemsUpdated.ItemSlotChangedEventArgs args = null;

            int oldSlot = p.ReadInt8();
            int newSlot = p.ReadInt8();
            int count = p.ReadInt16();

            if (count == 0)
                count = 1;

            if (!SpecificInventory.ContainsKey(oldSlot))
                return null;

            if (SpecificInventory.ContainsKey(newSlot) && SpecificInventory[newSlot].MediaName == SpecificInventory[oldSlot].MediaName)
            {
                if (SpecificInventory[newSlot].Stack == SpecificInventory[newSlot].MaxStack)
                {

                    Information.InventoryItem temp = SpecificInventory[newSlot];
                    temp.Slot = oldSlot;
                    SpecificInventory[newSlot] = SpecificInventory[oldSlot];
                    SpecificInventory[oldSlot] = temp;

                    args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSwappedWithAnotherItem"),
                        SpecificInventory[oldSlot]);

                }
                else if (count != SpecificInventory[oldSlot].Stack)
                {
                    SpecificInventory[newSlot].Stack += count;
                    SpecificInventory[oldSlot].Stack -= count;

                    args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemPartiallyAddedOnAnotherInstance"),
                        SpecificInventory[oldSlot]);
                }
                else
                {
                    SpecificInventory[newSlot].Stack += count;
                    SpecificInventory.Remove(oldSlot);

                    args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemTotallyAddedOnAnotherInstance"));
                }
            }
            else if (SpecificInventory.ContainsKey(newSlot) && SpecificInventory[newSlot].MediaName != SpecificInventory[oldSlot].MediaName)
            {
                Information.InventoryItem temp = SpecificInventory[newSlot];
                temp.Slot = oldSlot;
                SpecificInventory[newSlot] = SpecificInventory[oldSlot];
                SpecificInventory[oldSlot] = temp;

                args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                    (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSwappedWithAnotherItem"),
                    SpecificInventory[oldSlot]);
            }
            else if (!SpecificInventory.ContainsKey(newSlot) && count != SpecificInventory[oldSlot].Stack)
            {
                SpecificInventory[oldSlot].Stack -= count;
                Information.InventoryItem item = new Information.InventoryItem(SpecificInventory[oldSlot].ModelID);
                item.Blues = SpecificInventory[oldSlot].Blues;
                item.Stack = count;
                item.Slot = newSlot;
                SpecificInventory.Add(newSlot, item);

                args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                    (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSplitted"),
                    SpecificInventory[oldSlot]);
            }
            else
            {
                SpecificInventory.Add(newSlot, SpecificInventory[oldSlot]);
                SpecificInventory.Remove(oldSlot);

                args = new InventoryItemsUpdated.ItemSlotChangedEventArgs(SpecificInventory[newSlot],
                    (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSlotChanged"));
            }

            InventoryItemsUpdated.Parse(p);

            return args;
        }
    }
}
