using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Actions
{
    public class Utility
    {
        public static Dictionary<string, int> CalculateWhiteStats(ulong variance, ItemType type) //By stratii
        {
            Dictionary<string, int> whiteStats = new Dictionary<string, int>();

            var stats = new List<byte>();

            while (variance > 0)
            {
                var stat = (byte)(variance & 0x1f);
                variance >>= 5;

                stats.Add(stat);
            }

            try
            {
                switch (type)
                {
                    case ItemType.Shield:
                        whiteStats.Add("ShieldDurabilitySlot", stats[0]);
                        whiteStats.Add("ShieldPhySpecSlot", stats[1]);
                        whiteStats.Add("ShieldMagSpecSlot", stats[2]);
                        whiteStats.Add("ShieldBlockRatioSlot", stats[3]);
                        whiteStats.Add("ShieldPhyDefSlot", stats[4]);
                        whiteStats.Add("ShieldPhyDefSlot", stats[5]);
                        break;
                    case ItemType.Protector:
                        whiteStats.Add("ArmorDurabilitySlot", stats[0]);
                        whiteStats.Add("ArmorPhySpecSlot", stats[1]);
                        whiteStats.Add("ArmorMagSpecSlot", stats[2]);
                        whiteStats.Add("ArmorPhyDefSlot", stats[3]);
                        whiteStats.Add("ArmorMagDefSlot", stats[4]);
                        whiteStats.Add("ArmorEvasionRatioSlot", stats[5]);
                        break;
                    case ItemType.Weapon:
                        whiteStats.Add("WeaponDurabilitySlot", stats[0]);
                        whiteStats.Add("WeaponPhySpecSlot", stats[1]);
                        whiteStats.Add("WeaponMagSpecSlot", stats[2]);
                        whiteStats.Add("WeaponHitRatioSlot", stats[3]);
                        whiteStats.Add("WeaponPhyDmgSlot", stats[4]);
                        whiteStats.Add("WeaponMagDmgSlot", stats[5]);
                        whiteStats.Add("WeaponMagDmgSlot", stats[6]);
                        break;
                    case ItemType.Accessory:
                        whiteStats.Add("AccessoryPhyAbsorpSlot", stats[0]);
                        whiteStats.Add("AccessoryMagAbsorpSlot", stats[1]);
                        break;
                }
            } catch
            {

            }
            return whiteStats;

        }
        public static void WalkTo(int X, int Y)
        {
            uint xPos = 0;
            uint yPos = 0;

            if (X > 0 && Y > 0)
            {
                xPos = (uint)((X % 192) * 10);
                yPos = (uint)((Y % 192) * 10);
            }
            else
            {
                if (X < 0 && Y > 0)
                {
                    xPos = (uint)((192 + (X % 192)) * 10);
                    yPos = (uint)((Y % 192) * 10);
                }
                else
                {
                    if (X > 0 && Y < 0)
                    {
                        xPos = (uint)((X % 192) * 10);
                        yPos = (uint)((192 + (Y % 192)) * 10);
                    }
                }
            }

            byte xSector = (byte)((X - (int)(xPos / 10)) / 192 + 135);
            byte ySector = (byte)((Y - (int)(yPos / 10)) / 192 + 92);
            ushort xPosition = (ushort)((X - (int)((xSector - 135) * 192)) * 10);
            ushort yPosition = (ushort)((Y - (int)((ySector - 92) * 192)) * 10);

            var p = new Packet(0x0);

            if(Client.Info.TransportUniqueID == 0)
            {
                p = new Packet(0x7021);
            } else
            {
                p = new Packet(0x70C5);
                p.WriteUInt32(Client.Info.TransportUniqueID);
                p.WriteUInt8(0x01);
            }
            p.WriteUInt8(0x01);
            p.WriteUInt8(xSector);
            p.WriteUInt8(ySector);
            p.WriteUInt16(xPosition);
            p.WriteUInt16(0x0000);
            p.WriteUInt16(yPosition);
            SroClient.RemoteSecurity?.Send(p);
        }

        public static void UseReturn()
        {
            if (Client.State.Returning == false)
            {
                if (!UseItemType(ItemType.ReturnScroll))
                    Console.WriteLine("No return scroll is found!");
            } else
            {
                Console.WriteLine("Client is already returning!");
            }
        }

        public static bool UseItemType(ItemType type)
        {
            if (Client.InventoryItems.Where(x => x.Value.Type == type).Count() > 0)
            {
                SroClient.UseItem(Client.InventoryItems.First(x => x.Value.Type == type).Key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UseItemType(ItemType type, byte otherItemSlot)
        {
            if (Client.InventoryItems.Where(x => x.Value.Type == type).Count() > 0)
            {
                SroClient.UseItem(Client.InventoryItems.First(x => x.Value.Type == type).Key, otherItemSlot);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UseItemType(ItemType type, uint targetUID)
        {
            if (Client.InventoryItems.Where(x => x.Value.Type == type).Count() > 0)
            {
                SroClient.UseItem(Client.InventoryItems.First(x => x.Value.Type == type).Key, targetUID);
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool UseSkillName(string name)
        {
            if (Client.Skills.Where(x => x.TranslationName == name).Count() > 0)
            {
                SroClient.UseSpell(Client.Skills.Single(x => x.TranslationName == name).ObjRefID);
                return true;
            }
            else
                return false;
        }

        public static bool UseSkillName(string name, uint target)
        {
            if (Client.Skills.Where(x => x.TranslationName == name).Count() > 0)
            {
                SroClient.UseSpell(Client.Skills.Single(x => x.TranslationName == name).ObjRefID, target);
                return true;
            }
            else
                return false;
        }


        public static ushort GenerateItemType(uint itemID)
        {
            var item = Media.Data.MediaItems[itemID];
            return (ushort)(1 * item.Classes.A + 2 * item.Classes.B + 4 * item.Classes.C + 32 * item.Classes.D + 128 * item.Classes.E + 2048 * item.Classes.F);
        }
    }
}
