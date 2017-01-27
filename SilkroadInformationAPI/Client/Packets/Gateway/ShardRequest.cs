using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class ShardRequest
    {
        public static void Send()
        {
            Packet p = new Packet(0x6101, true);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
