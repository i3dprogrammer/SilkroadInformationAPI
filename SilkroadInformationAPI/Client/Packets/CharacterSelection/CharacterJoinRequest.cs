using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.CharacterSelection
{
    public class CharacterJoinRequest
    {
        public static void Send(string characterName)
        {
            Packet p = new Packet(0x7001);
            p.WriteAscii(characterName);
            SroClient.RemoteSecurity.Send(p);
        }
    }
}
