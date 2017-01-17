using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    class HPMPUpdate
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            p.ReadUInt16(); //UNK
            if (Client.Info.UniqueID == uid)
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x01: //HP update
                        Client.Info.CurrentHP = p.ReadUInt32();
                        break;
                    case 0x02: //MP update
                        Client.Info.CurrentMP = p.ReadUInt32();
                        break;
                    case 0x03: //HP&MP update
                        Client.Info.CurrentHP = p.ReadUInt32();
                        Client.Info.CurrentMP = p.ReadUInt32();
                        break;
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                            Client.Info.BadStatus = false;
                        else
                            Client.Info.BadStatus = true;
                        break;
                }
            }
            else if (Client.NearbyMobs.ContainsKey(uid))
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x05: //HP&MP update
                        Client.NearbyMobs[uid].HP = p.ReadUInt32();
                        break;
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                            Client.NearbyMobs[uid].BadState = false;
                        else
                            Client.NearbyMobs[uid].BadState = true;
                        break;
                }
            } else if (Client.NearbyCOSs.ContainsKey(uid))
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x05: //HP&MP update
                        Client.NearbyCOSs[uid].HP = p.ReadUInt32();
                        break;
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                            Client.NearbyCOSs[uid].BadState = false;
                        else
                            Client.NearbyCOSs[uid].BadState = true;
                        break;
                }
            }
        }
    }
}
