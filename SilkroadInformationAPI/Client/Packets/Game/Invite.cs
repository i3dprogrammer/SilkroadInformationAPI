using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Game
{
    public class Invite
    {
        public static void Parse(Packet p) //0x3080 TODO: Add events
        {
            byte RequestType = p.ReadUInt8();
            switch (RequestType)
            {
                case 0x02: //Party invitation
                case 0x03: //Party invitation
                    break;
                case 0x05: //Guild invitation
                    break;
                case 0x09: //Academy invitation
                    break;
            }
        }
    }
}
