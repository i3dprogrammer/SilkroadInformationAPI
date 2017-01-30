using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.CharacterSelection
{
    public class CharacterListRequest
    {
        public static void Send()
        {
            Packet answer = new Packet(0x7007); //Character screen
            answer.WriteInt8(0x02); //Request character list
            SroClient.RemoteSecurity.Send(answer);
        }
    }
}
