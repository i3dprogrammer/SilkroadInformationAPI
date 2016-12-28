using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class InventoryItemsUpdated
    {
        public delegate void ItemSlotUpdatedHandler(ItemSlotChangedEventArgs e);
        public static event ItemSlotUpdatedHandler OnItemSlotUpdated;

        public static void Parse(Packet p)
        {
            int succeeded = 0;
            try //Erorrs will occur because Parse(p) recursion.
            {
               succeeded = p.ReadInt8();
            } catch { }

            if (succeeded != 1)
                return;

            int flag = p.ReadInt8();

            ItemSlotChangedEventArgs args = null;

            if (flag == 0x00) //Item update in inventory
            {
                int oldSlot = p.ReadInt8();
                int newSlot = p.ReadInt8();
                int count = p.ReadInt16();

                if (count == 0)
                    count = 1;

                if (!Client.InventoryItems.ContainsKey(oldSlot))
                    return;

                if (Client.InventoryItems.ContainsKey(newSlot) && Client.InventoryItems[newSlot].MediaName == Client.InventoryItems[oldSlot].MediaName)
                {
                    if (Client.InventoryItems[newSlot].Count == Client.InventoryItems[newSlot].MaxStack)
                    {

                        Information.Item temp = Client.InventoryItems[newSlot];
                        temp.Slot = oldSlot;
                        Client.InventoryItems[newSlot] = Client.InventoryItems[oldSlot];
                        Client.InventoryItems[oldSlot] = temp;

                        args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot], Client.InventoryItems[oldSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemSwappedWithAnotherItem;

                    }
                    else if (count != Client.InventoryItems[oldSlot].Count)
                    {
                        Client.InventoryItems[newSlot].Count += count;
                        Client.InventoryItems[oldSlot].Count -= count;

                        args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot], Client.InventoryItems[oldSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemPartiallyAddedOnAnotherInstance;
                    }
                    else
                    {
                        Client.InventoryItems[newSlot].Count += count;
                        Client.InventoryItems.Remove(oldSlot);

                        args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemTotallyAddedOnAnotherInstance;
                    }
                }
                else if (Client.InventoryItems.ContainsKey(newSlot) && Client.InventoryItems[newSlot].MediaName != Client.InventoryItems[oldSlot].MediaName)
                {
                    Information.Item temp = Client.InventoryItems[newSlot];
                    temp.Slot = oldSlot;
                    Client.InventoryItems[newSlot] = Client.InventoryItems[oldSlot];
                    Client.InventoryItems[oldSlot] = temp;

                    args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot], Client.InventoryItems[oldSlot]);
                    args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemSwappedWithAnotherItem;
                }
                else if (!Client.InventoryItems.ContainsKey(newSlot) && count != Client.InventoryItems[oldSlot].Count)
                {
                    Client.InventoryItems[oldSlot].Count -= count;
                    Information.Item item = new Information.Item(Client.InventoryItems[oldSlot].ModelID);
                    item.Blues = Client.InventoryItems[oldSlot].Blues;
                    item.Count = count;
                    item.Slot = newSlot;
                    Client.InventoryItems.Add(newSlot, item);

                    args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot], Client.InventoryItems[oldSlot]);
                    args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemSplitted;
                }
                else
                {
                    Client.InventoryItems.Add(newSlot, Client.InventoryItems[oldSlot]);
                    Client.InventoryItems.Remove(oldSlot);

                    args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlot]);
                    args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemSlotChanged;
                }
                
                Parse(p);
            } else if(flag == 0x01) //Item update in storage
            {
                int oldSlot = p.ReadInt8();
                int newSlot = p.ReadInt8();
                int count = p.ReadInt16();

                if (count == 0)
                    count = 1;

                if (!Client.StorageItems.ContainsKey(oldSlot))
                    return;

                if (Client.StorageItems.ContainsKey(newSlot) && Client.StorageItems[newSlot].MediaName == Client.StorageItems[oldSlot].MediaName)
                {
                    if (Client.StorageItems[newSlot].Count == Client.StorageItems[newSlot].MaxStack)
                    {

                        Information.Item temp = Client.StorageItems[newSlot];
                        temp.Slot = oldSlot;
                        Client.StorageItems[newSlot] = Client.StorageItems[oldSlot];
                        Client.StorageItems[oldSlot] = temp;

                        args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlot], Client.StorageItems[oldSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemSwappedWithAnotherItem;

                    } else if(count != Client.StorageItems[oldSlot].Count)
                    {
                        Client.StorageItems[newSlot].Count += count;
                        Client.StorageItems[oldSlot].Count -= count;

                        args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlot], Client.StorageItems[oldSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemPartiallyAddedOnAnotherInstance;
                    }
                    else
                    {
                        Client.StorageItems[newSlot].Count += count;
                        Client.StorageItems.Remove(oldSlot);

                        args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlot]);
                        args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemTotallyAddedOnAnotherInstance;
                    }
                }
                else if (Client.StorageItems.ContainsKey(newSlot) && Client.StorageItems[newSlot].MediaName != Client.StorageItems[oldSlot].MediaName)
                {
                    Information.Item temp = Client.StorageItems[newSlot];
                    temp.Slot = oldSlot;
                    Client.StorageItems[newSlot] = Client.StorageItems[oldSlot];
                    Client.StorageItems[oldSlot] = temp;

                    args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlot], Client.StorageItems[oldSlot]);
                    args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemSwappedWithAnotherItem;
                }
                else
                {
                    Client.StorageItems.Add(newSlot, Client.StorageItems[oldSlot]);
                    Client.StorageItems.Remove(oldSlot);

                    args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlot]);
                    args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemSlotChanged;
                }

                Parse(p);
            }
            else if (flag == 0x02) //Item added to storage
            {
                int oldSlot = p.ReadInt8();
                int newSlotInStorage = p.ReadInt8();

                var item = Client.InventoryItems[oldSlot];
                item.Slot = newSlotInStorage;

                Client.InventoryItems.Remove(oldSlot);
                Client.StorageItems.Add(newSlotInStorage, item);

                args = new ItemSlotChangedEventArgs(Client.StorageItems[newSlotInStorage]);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemAddedToStorage;
            }
            else if (flag == 0x03) //Item taken from storage
            {
                int itemSlotInStorage = p.ReadInt8();
                int newSlotInInventory = p.ReadInt8();

                Information.Item item = Client.StorageItems[itemSlotInStorage];
                item.Slot = newSlotInInventory;

                Client.InventoryItems.Add(newSlotInInventory, item);
                Client.StorageItems.Remove(itemSlotInStorage);

                args = new ItemSlotChangedEventArgs(Client.InventoryItems[newSlotInInventory]);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Storage_ItemTakenFromStorage;

            } else if(flag == 0x06) //Item picked from the ground
            {
                int itemSlot = p.ReadInt8();

                if (Client.InventoryItems.ContainsKey(itemSlot)) //Removes the item if it's already in the inventory *If the item stacks*
                    Client.InventoryItems.Remove(itemSlot);

                Information.Item item = ParseItem.Parse(p);

                Client.InventoryItems.Add(itemSlot, item); //Re-adds the item with the new info.

                args = new ItemSlotChangedEventArgs(Client.InventoryItems[itemSlot]);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemLooted;
            }
            else if (flag == 0x07) //Item thrown to the ground
            {
                int oldSlot = p.ReadInt8();

                args = new ItemSlotChangedEventArgs(Client.InventoryItems[oldSlot]);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemThrown;

                Client.InventoryItems.Remove(oldSlot);
            } else if (flag == 0x08) //Item bought from NPC
            {

            }
            else if (flag == 0x09) //Item sold to NPC
            {
                if (Client.SoldItems.Count == 5)
                    Client.SoldItems.Remove(Client.SoldItems.Last());

                int itemSlot = p.ReadInt8();
                int countSold = p.ReadInt16();
                int NPCID = p.ReadInt32();
                int indexInNPC = p.ReadInt8();

                var item = Client.InventoryItems[itemSlot].Clone();
                item.Count = countSold;

                Client.SoldItems.Insert(0, item);

                if (countSold == Client.InventoryItems[itemSlot].Count)
                    Client.InventoryItems.Remove(itemSlot);
                else
                    Client.InventoryItems[itemSlot].Count -= countSold;

                args = new ItemSlotChangedEventArgs(item);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemSoldToNPC;

                Console.WriteLine("Sold {0} to NPCID: {1}, Registered with ID: {2}", item.MediaName, NPCID, indexInNPC - 1);
            } else if(flag == 0x18) //Item bought from Item Mall
            {

            }
            else if (flag == 0x22) //Item boughtback from NPC
            {
                int newSlotInInventory = p.ReadInt8();
                int indexInNPC = p.ReadInt8();
                int returnCount = p.ReadInt16();

                Console.WriteLine("Buying back ID: {0}", Client.SoldItems.Count - (1 + indexInNPC));

                var item = Client.SoldItems[Client.SoldItems.Count - (1 + indexInNPC)];

                Client.InventoryItems.Add(newSlotInInventory, item);
                Client.SoldItems.Remove(item);

                args = new ItemSlotChangedEventArgs(item);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemBoughtbackFromNPC;
            }
            else if (flag == 0x0F) //Item disappeared
            {
                int oldSlot = p.ReadInt8();

                args = new ItemSlotChangedEventArgs(Client.InventoryItems[oldSlot]);
                args.ItemChangeType = ItemSlotChangedEventArgs.ChangeType.Inv_ItemDisappeared;

                Client.InventoryItems.Remove(oldSlot);
            }

            if(args != null)
                OnItemSlotUpdated(args);
        }

        public class ItemSlotChangedEventArgs : EventArgs
        {
            Information.Item item;
            Information.Item associated;

            public ChangeType ItemChangeType;

            public ItemSlotChangedEventArgs(Information.Item _item, Information.Item _associated = null)
            {
                item = _item;
                associated = _associated;
                ItemChangeType = ChangeType.None;
            }

            /// <summary>
            /// The item with the change taking place.
            /// </summary>
            /// <returns></returns>
            public Information.Item EffectedItem()
            {
                return item;
            }

            /// <summary>
            /// The item associated with the changes i.e. (if it got swapped with another item, this returns the other item)
            /// </summary>
            /// <returns>The associated SilkroadInformationAPI.Client.Information.Item</returns>
            public Information.Item AssociatedItem()
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
                Storage_ItemSwappedWithAnotherItem,
                Storage_ItemSlotChanged,
                Storage_ItemAddedToStorage,
                Storage_ItemTakenFromStorage,
                Storage_ItemTotallyAddedOnAnotherInstance,
                Storage_ItemPartiallyAddedOnAnotherInstance,
                None
            }

        }
    }
}
