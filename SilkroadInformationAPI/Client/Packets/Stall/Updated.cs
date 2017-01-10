using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    class Updated
    {
        public static void Parse(Packet p)
        {
            byte result = p.ReadUInt8();
            byte flag = p.ReadUInt8();

            if(flag == 0x01) //Item updated in stall
            {
                byte slot = p.ReadUInt8();

                if (Client.CurrentStall.StallItems.ContainsKey(slot))
                {
                    Client.CurrentStall.StallItems[slot].Stack = p.ReadUInt16();
                    Client.CurrentStall.StallItems[slot].Price = p.ReadUInt64();
                }
            } else if(flag == 0x02) //Item added
            {
                p.ReadUInt16(); //??

                StallUtility.ClearCurrentStall();
                StallUtility.ParseCurrentStallItems(p);
            } else if(flag == 0x03) //Item removed
            {
                p.ReadUInt16(); //??

                byte slot = p.ReadUInt8();
                if (Client.CurrentStall.StallItems.ContainsKey(slot))
                    Client.CurrentStall.StallItems.Remove(slot);
            } else if(flag == 0x05)
            {
                Client.CurrentStall.Opened = (p.ReadUInt8() == 1);
            } else if(flag == 0x06)
            {
                Client.CurrentStall.Message = p.ReadAscii();
            } else if(flag == 0x07)
            {
                NameUpdated.Parse(p);
            }
        }
    }
}
