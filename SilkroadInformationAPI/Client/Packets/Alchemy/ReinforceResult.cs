using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Alchemy
{
    class ReinforceResult
    {
        public delegate void ReinforcementResultHandler(FuseResultEventArgs e);

        /// <summary>
        /// This event gets called whenever packet 0xB150 is received, when an elixir is added on the item.
        /// </summary>
        public static event ReinforcementResultHandler OnReinforce;

        public static void Parse(Packet p)
        {
            byte flag1 = p.ReadUInt8();
            if (flag1 == 0x01) //No errrors
            {
                byte flag2 = p.ReadUInt8();
                if (flag2 == 0x02) // ??
                {
                    bool EnchantSuccess = (p.ReadUInt8() == 1);
                    byte ItemSlotInInventory = p.ReadUInt8();
                    p.ReadUInt32(); //Rent Type
                    p.ReadUInt32(); //Item ref id, why?
                    byte CurrentPlusValue = p.ReadUInt8();
                    p.ReadUInt64(); //White stats
                    p.ReadUInt32(); //DURABILITY
                    byte currBlues = p.ReadUInt8();
                    Client.InventoryItems[ItemSlotInInventory].Blues = AlchemyUtility.ParseBlues(p, currBlues);

                    OnReinforce(new FuseResultEventArgs(EnchantSuccess, Client.InventoryItems[ItemSlotInInventory]));
                }
            }

        }
    }
}
