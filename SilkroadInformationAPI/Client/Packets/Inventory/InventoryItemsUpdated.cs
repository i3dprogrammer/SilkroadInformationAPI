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
                args = InventoryUtility.ParseSlotChangedUpdate(p, Client.InventoryItems, "Inv");
            } else if(flag == 0x01) //Item slot changed in storage
            {
                args = InventoryUtility.ParseSlotChangedUpdate(p, Client.StorageItems, "Storage");
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

                Information.InventoryItem item = Client.StorageItems[itemSlotInStorage];
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

                Information.InventoryItem item = ParseItem.Parse(p);

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
                item.Stack = countSold;

                Client.SoldItems.Insert(0, item);

                if (countSold == Client.InventoryItems[itemSlot].Stack)
                    Client.InventoryItems.Remove(itemSlot);
                else
                    Client.InventoryItems[itemSlot].Stack -= countSold;

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
            } else if(flag == 0x1B) //Item moved from inventory to pet
            {
                uint COS_uid = p.ReadUInt32();
                if(Client.Info.CharacterCOS.Where(x => x.UniqueID == COS_uid && x.Type == COS_Type.PickupPet).Count() > 0)
                {
                    byte oldSlot = p.ReadUInt8();
                    byte newSlotInPet = p.ReadUInt8();
                    Client.SpawnedPetItems.Add(newSlotInPet, Client.InventoryItems[oldSlot]);
                    Client.InventoryItems.Remove(oldSlot);
                }
            } else if(flag == 0x1A) //Item moved from pet to inventory
            {
                uint COS_uid = p.ReadUInt32();
                if (Client.Info.CharacterCOS.Where(x => x.UniqueID == COS_uid && x.Type == COS_Type.PickupPet).Count() > 0)
                {
                    byte oldSlot = p.ReadUInt8();
                    byte newSlotInPet = p.ReadUInt8();
                    Client.InventoryItems.Add(newSlotInPet, Client.SpawnedPetItems[oldSlot]);
                    Client.SpawnedPetItems.Remove(oldSlot);
                }
            } else if(flag == 0x10) //Item slot changed within pet
            {
                uint COS_uid = p.ReadUInt32();
                if (Client.Info.CharacterCOS.Where(x => x.UniqueID == COS_uid && x.Type == COS_Type.PickupPet).Count() > 0)
                {
                    args = InventoryUtility.ParseSlotChangedUpdate(p, Client.SpawnedPetItems, "PetInventory");
                }
            }

            if(args != null)
                OnItemSlotUpdated(args);
        }

        public class ItemSlotChangedEventArgs : EventArgs
        {
            Information.InventoryItem item;
            Information.InventoryItem associated;

            public ChangeType ItemChangeType;

            public ItemSlotChangedEventArgs(Information.InventoryItem _item, Information.InventoryItem _associated = null)
            {
                item = _item;
                associated = _associated;
                ItemChangeType = ChangeType.None;
            }

            public ItemSlotChangedEventArgs(Information.InventoryItem _item, ChangeType _itemChangeType, Information.InventoryItem _associated = null)
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
}
