using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class PatchRequest
    {
        public static void Send(string Module, byte Locale, uint Version)
        {
            Packet p = new Packet(0x6100, false, true);
            p.WriteUInt8(Locale);
            p.WriteAscii(Module);
            p.WriteUInt32(Version);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
