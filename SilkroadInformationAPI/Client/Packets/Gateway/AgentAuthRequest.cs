using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class AgentAuthRequest
    {
        public static void Send(uint SessionID, string Username, string Password, byte Locale)
        {
            Packet p = new Packet(0x6103, true);
            p.WriteUInt32(SessionID);
            p.WriteAscii(Username);
            p.WriteAscii(Password);
            p.WriteUInt8(Locale);
            p.WriteUInt32(0);
            p.WriteUInt16(0);
            SroClient.security.Send(p);
        }
    }
}
