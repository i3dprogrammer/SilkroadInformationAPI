using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    public class LogOut
    {
        public static void Send()
        {
            var p = new Packet(0x7005);
            p.WriteUInt8(1);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
