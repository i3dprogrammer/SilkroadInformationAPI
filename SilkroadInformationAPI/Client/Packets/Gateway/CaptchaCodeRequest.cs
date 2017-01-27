using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class CaptchaCodeRequest
    {
        public static void Send(string code)
        {
            Packet p = new Packet(0x6323);
            p.WriteAscii(code);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
