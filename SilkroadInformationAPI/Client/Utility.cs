using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client
{
    class Utility
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
       
    }
}
