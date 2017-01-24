using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Alchemy
{
    public class EnchantResult
    {
        public delegate void ReinforcementResultHandler(FuseResultEventArgs e);
        /// <summary>
        /// This event gets called whenever packet 0xB150 is received, after elixir is added on the item.
        /// <para>NOTE: This is called AFTER 0x3040(Item count updated due to alchemy).</para>
        /// </summary>
        public static event ReinforcementResultHandler OnReinforceResult;

        public delegate void EnchantmentResultHandler(FuseResultEventArgs e);
        /// <summary>
        /// This event gets called whenever packet 0xB151 is received, after a stone is added on the item.
        /// <para>NOTE: This is called AFTER 0x3040(Item count updated due to alchemy).</para>
        /// </summary>
        public static event EnchantmentResultHandler OnEnchantResult;

        public static void Parse(Packet p)
        {
            byte flag1 = p.ReadUInt8();
            if(flag1 == 0x01) //No errrors
            {
                byte flag2 = p.ReadUInt8();
                if(flag2 == 0x02) // ??
                {
                    bool EnchantSuccess = (p.ReadUInt8() == 1);
                    byte ItemSlot = p.ReadUInt8();
                    p.ReadUInt32(); //Rent Type
                    p.ReadUInt32(); //Item ref id, why?
                    Client.InventoryItems[ItemSlot].PlusValue = p.ReadUInt8();
                    Client.InventoryItems[ItemSlot].Stats = Actions.Utility.CalculateWhiteStats(p.ReadUInt64(), Client.InventoryItems[ItemSlot].Type); //White stats
                    Client.InventoryItems[ItemSlot].Stats.Add("DURABILITY", p.ReadInt32()); //DURABILITY
                    byte currBlues = p.ReadUInt8();
                    Client.InventoryItems[ItemSlot].Blues = AlchemyUtility.ParseBlues(p, currBlues);

                    if(p.Opcode == 0xB151)
                        OnEnchantResult?.Invoke(new FuseResultEventArgs(EnchantSuccess, Client.InventoryItems[ItemSlot]));
                    else
                        OnReinforceResult?.Invoke(new FuseResultEventArgs(EnchantSuccess, Client.InventoryItems[ItemSlot]));
                }
            }

        }
    }


}
