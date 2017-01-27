using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class InventoryOperation
    {
        public delegate void ItemSlotUpdatedHandler(InventoryOperationEventArgs e);
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

            InventoryOperationEventArgs args = null;

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

                args = new InventoryOperationEventArgs(Client.StorageItems[newSlotInStorage]);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Storage_ItemAddedToStorage;
            }
            else if (flag == 0x03) //Item taken from storage
            {
                int itemSlotInStorage = p.ReadInt8();
                int newSlotInInventory = p.ReadInt8();

                Information.InventoryItem item = Client.StorageItems[itemSlotInStorage];
                item.Slot = newSlotInInventory;

                Client.InventoryItems.Add(newSlotInInventory, item);
                Client.StorageItems.Remove(itemSlotInStorage);

                args = new InventoryOperationEventArgs(Client.InventoryItems[newSlotInInventory]);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Storage_ItemTakenFromStorage;

            } else if(flag == 0x06) //Item picked from the ground
            {
                int itemSlot = p.ReadInt8();

                if (Client.InventoryItems.ContainsKey(itemSlot)) //Removes the item if it's already in the inventory *If the item stacks*
                    Client.InventoryItems.Remove(itemSlot);

                Information.InventoryItem item = InventoryUtility.ParseItem(p);

                Client.InventoryItems.Add(itemSlot, item); //Re-adds the item with the new info.

                args = new InventoryOperationEventArgs(Client.InventoryItems[itemSlot]);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemLooted;
            }
            else if (flag == 0x07) //Item thrown to the ground
            {
                int oldSlot = p.ReadInt8();

                args = new InventoryOperationEventArgs(Client.InventoryItems[oldSlot]);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemThrown;

                Client.InventoryItems.Remove(oldSlot);
            } else if (flag == 0x08) //Item bought from NPC
            {
                uint RefID = Client.NearbyNPCs[Client.SelectedUniqueID].ModelID;
                string MediaName = Media.Data.MediaModels[RefID].MediaName;
                byte TabIndex = p.ReadUInt8();
                byte ItemIndex = p.ReadUInt8();
                var shopItem = Media.Data.MediaShops.First(x => x.NPCName == MediaName).GetTabFromIndex(TabIndex).TabItems.First(x => x.PackagePosition == ItemIndex); //WTF???
                byte CountBought = p.ReadUInt8();
                for (int i = 0; i < CountBought; i++)
                {
                    var item = new Information.InventoryItem(Media.Data.MediaItems.First(x => x.Value.MediaName == shopItem.ItemMediaName).Value.ModelID);
                    Console.WriteLine("BOUGHT: " + item.MediaName);
                    item.Slot = p.ReadUInt8();
                    Client.InventoryItems.Add(item.Slot, item);
                }

                args = new InventoryOperationEventArgs(null);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemBought;
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

                args = new InventoryOperationEventArgs(item);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemSoldToNPC;

                //Console.WriteLine("Sold {0} to NPCID: {1}, Registered with ID: {2}", item.MediaName, NPCID, indexInNPC - 1);
            } else if(flag == 0x18) //Item bought from Item Mall
            {
                //TODO: PARSE PACKET
            }
            else if (flag == 0x22) //Item boughtback from NPC
            {
                int newSlotInInventory = p.ReadInt8();
                int indexInNPC = p.ReadInt8();
                int returnCount = p.ReadInt16();

                //Console.WriteLine("Buying back ID: {0}", Client.SoldItems.Count - (1 + indexInNPC));

                var item = Client.SoldItems[Client.SoldItems.Count - (1 + indexInNPC)];

                Client.InventoryItems.Add(newSlotInInventory, item);
                Client.SoldItems.Remove(item);

                args = new InventoryOperationEventArgs(item);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemBoughtbackFromNPC;
            } else if (flag == 0x0E) //Item appeared in inventory due to dismantling
            {
                int invSlot = p.ReadUInt8();
                var item = InventoryUtility.ParseItem(p);
                item.Slot = invSlot;
                Client.InventoryItems.Add(invSlot, item);
            }
            else if (flag == 0x0F) //Item disappeared
            {
                int oldSlot = p.ReadInt8();

                args = new InventoryOperationEventArgs(Client.InventoryItems[oldSlot]);
                args.ItemChangeType = InventoryOperationEventArgs.ChangeType.Inv_ItemDisappeared;

                Client.InventoryItems.Remove(oldSlot);
            } else if(flag == 0x1B) //Item moved from inventory to pet
            {
                uint COS_uid = p.ReadUInt32();
                if(Client.NearbyCOSs.ContainsKey(COS_uid))
                {
                    byte oldSlot = p.ReadUInt8();
                    byte newSlotInPet = p.ReadUInt8();
                    Client.NearbyCOSs[COS_uid].Inventory.Add(newSlotInPet, Client.InventoryItems[oldSlot]);
                    Client.InventoryItems.Remove(oldSlot);
                }
            } else if(flag == 0x1A) //Item moved from pet to inventory
            {
                uint COS_uid = p.ReadUInt32();
                if (Client.NearbyCOSs.ContainsKey(COS_uid))
                {
                    byte oldSlot = p.ReadUInt8();
                    byte newSlotInPet = p.ReadUInt8();
                    Client.InventoryItems.Add(newSlotInPet, Client.NearbyCOSs[COS_uid].Inventory[oldSlot]);
                    Client.NearbyCOSs[COS_uid].Inventory.Remove(oldSlot);
                }
            } else if(flag == 0x10) //Item slot changed within pet
            {
                uint COS_uid = p.ReadUInt32();
                if (Client.NearbyCOSs.ContainsKey(COS_uid))
                {
                    args = InventoryUtility.ParseSlotChangedUpdate(p, Client.NearbyCOSs[COS_uid].Inventory, "PetInventory");
                }
            }

            if(args != null)
                OnItemSlotUpdated?.Invoke(args);
        }

        
    }
}
