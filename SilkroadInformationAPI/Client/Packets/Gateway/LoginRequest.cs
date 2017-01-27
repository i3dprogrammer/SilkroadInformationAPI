using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class LoginRequest
    {
        public static void Send(byte Locale, string username, string password, ushort shardID)
        {
            Packet p = new Packet(0x6102);
            p.WriteUInt8(Locale);
            p.WriteAscii(username);
            p.WriteAscii(password);
            p.WriteUInt16(shardID);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
