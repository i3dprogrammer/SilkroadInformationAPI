using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
using static SilkroadInformationAPI.Client.Packets.Inventory.InventoryOperationEventArgs;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    class InventoryUtility
    {
        public static Information.InventoryItem ParseItem(Packet p)
        {
            byte slot = p.ReadUInt8();

            if (slot == byte.MaxValue)
                return null;

            int rent = p.ReadInt32(); // Rent Type

            if (rent == 1)
            {
                p.ReadUInt16(); //  item.RentInfo.CanDelete
                p.ReadUInt32(); //    item.RentInfo.PeriodBeginTime
                p.ReadUInt32(); //    item.RentInfo.PeriodEndTime        
            }
            else if (rent == 2)
            {
                p.ReadUInt16(); //  item.RentInfo.CanDelete
                p.ReadUInt16(); //  item.RentInfo.CanRecharge
                p.ReadUInt32(); //    item.RentInfo.MeterRateTime        
            }
            else if (rent == 3)
            {
                p.ReadUInt16(); //  item.RentInfo.CanDelete
                p.ReadUInt16(); //  item.RentInfo.CanRecharge
                p.ReadUInt32(); //    item.RentInfo.PeriodBeginTime
                p.ReadUInt32(); //    item.RentInfo.PeriodEndTime   
                p.ReadUInt32(); //    item.RentInfo.PackingTime        
            }

            int itemID = p.ReadInt32();
            Information.InventoryItem item = new Information.InventoryItem(itemID);
            item.Slot = slot;

            /*Console.WriteLine(item.MediaName);
            Console.WriteLine(item.Type);*/

            if (item.Classes.C == 3 && item.Classes.D == 1)
            { //Armor || Jewlery || Weapon || Shield || Job suites || Devils || Flags
                if (item.Classes.E == 6)
                    item.Type = ItemType.Weapon;
                else if (item.Classes.E == 4)
                    item.Type = ItemType.Shield;
                else if (item.Classes.E == 12 || item.Classes.E == 5)
                    item.Type = ItemType.Accessory;
                else
                    item.Type = ItemType.Protector;

                item.PlusValue = p.ReadInt8();   //Plus value

                ulong variance = p.ReadUInt64();
                item.Stats = Actions.Utility.CalculateWhiteStats(variance, item.Type);
                item.Stats.Add("DURABILITY", p.ReadInt32());

                int countB = p.ReadInt8(); //Blue count
                item.Blues = Alchemy.AlchemyUtility.ParseBlues(p, countB);

                p.ReadInt8(); // Can add sockets
                int socks = p.ReadInt8(); // Sockets
                for (int j = 0; j < socks; j++)
                {
                    p.ReadInt8(); //Sock Slot
                    p.ReadInt32(); //Sock ID
                    p.ReadInt32(); //Sock value
                }
                p.ReadInt8(); // Can add adv
                int advCount = p.ReadInt8(); // Adv count only 1 though, can add more through db only
                for (int i = 0; i < advCount; i++)
                {
                    item.HasAdvance = true;
                    p.ReadInt8(); //Slot
                    p.ReadInt32(); //ADV ID
                    p.ReadInt32(); //ADV plus value
                }
            }
            else if (item.Type == ItemType.PickupPet || item.Type == ItemType.AttackPet)
            {
                int flagCheck = p.ReadInt8(); //State 1=Not opened yet, 2=Summoned, 3=Not summoned, 04=Expired/Dead
                if (flagCheck != 1)
                {
                    p.ReadInt32(); //Model ID
                    p.ReadAscii(); //PET Name
                    if (item.Type == ItemType.PickupPet)
                    {
                        p.ReadInt32(); //Time date?
                    }
                    p.ReadInt8(); //Unk
                }
            }
            else if (item.Type == ItemType.ItemExchangeCoupon)
            {
                p.ReadInt16(); //count
                int countB = p.ReadInt8(); //Blue count
                for (int k = 0; k < countB; k++)
                {
                    p.ReadInt32(); //Magic option ID
                    p.ReadInt32(); //Value
                }
            }
            else if (item.Type == ItemType.Stones)
            {
                item.Stack = p.ReadInt16(); //count
                if (item.Classes.F == 1 || item.Classes.F == 2)
                    p.ReadInt8(); //AttributeAssimilationProbability
            }
            else if (item.Type == ItemType.MagicCube)
            {
                p.ReadInt32(); //Elixirs count
            }
            else if (item.Type == ItemType.MonsterMask)
            {
                p.ReadInt32(); //Model ID
            }
            else
            {
                item.Stack = p.ReadInt16();
            }

            if (item.MediaName.Contains("RECIPE_WEAPON"))
                item.Type = ItemType.WeaponElixir;
            else if (item.MediaName.Contains("RECIPE_SHIELD"))
                item.Type = ItemType.ShieldElixir;
            else if (item.MediaName.Contains("RECIPE_ARMOR"))
                item.Type = ItemType.ProtectorElixir;
            else if (item.MediaName.Contains("RECIPE_ACCESSARY"))
                item.Type = ItemType.AccessoryElixir;
            else if (item.MediaName.Contains("PROB_UP"))
                item.Type = ItemType.LuckyPowder;
            else if (item.MediaName.Contains("MAGICSTONE_ATHANASIA"))
                item.Type = ItemType.ImmortalStone;
            else if (item.MediaName.Contains("MAGICSTONE_LUCK"))
                item.Type = ItemType.LuckStone;
            else if (item.MediaName.Contains("MAGICSTONE_SOLID"))
                item.Type = ItemType.SteadyStone;
            else if (item.MediaName.Contains("MAGICSTONE_ASTRAL"))
                item.Type = ItemType.AstralStone;

            //Console.WriteLine(Data.MediaItems[item.ID].TranslationName + " : " + item.Count + " : " + item.Type);

            return item;
        }

        public static InventoryOperationEventArgs ParseSlotChangedUpdate(Packet p, Dictionary<int, Information.InventoryItem> SpecificInventory, string argName)
        {
            InventoryOperationEventArgs args = null;

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

                    args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSwappedWithAnotherItem"),
                        SpecificInventory[oldSlot]);

                }
                else if (count != SpecificInventory[oldSlot].Stack)
                {
                    SpecificInventory[newSlot].Stack += count;
                    SpecificInventory[oldSlot].Stack -= count;

                    args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemPartiallyAddedOnAnotherInstance"),
                        SpecificInventory[oldSlot]);
                }
                else
                {
                    SpecificInventory[newSlot].Stack += count;
                    SpecificInventory.Remove(oldSlot);

                    args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
                        (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemTotallyAddedOnAnotherInstance"));
                }
            }
            else if (SpecificInventory.ContainsKey(newSlot) && SpecificInventory[newSlot].MediaName != SpecificInventory[oldSlot].MediaName)
            {
                Information.InventoryItem temp = SpecificInventory[newSlot];
                temp.Slot = oldSlot;
                SpecificInventory[newSlot] = SpecificInventory[oldSlot];
                SpecificInventory[oldSlot] = temp;

                args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
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

                args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
                    (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSplitted"),
                    SpecificInventory[oldSlot]);
            }
            else
            {
                SpecificInventory.Add(newSlot, SpecificInventory[oldSlot]);
                SpecificInventory.Remove(oldSlot);

                args = new InventoryOperationEventArgs(SpecificInventory[newSlot],
                    (ChangeType)Enum.Parse(typeof(ChangeType), argName + "_ItemSlotChanged"));
            }

            InventoryItemsUpdated.Parse(p);

            return args;
        }
    }
}
