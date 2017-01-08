using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Character
{
    class CharInfo
    {
        private static Packet CharInfoPacket;

        public static void CharInfoStart()
        {
            CharInfoPacket = new Packet(0x3013, false, true);
        }

        public static void CharInfoData(Packet p)
        {
            CharInfoPacket.WriteUInt8Array(p.GetBytes());
        }

        public static void CharInfoEnd()
        {
            ParseData.Parse(CharInfoPacket);
        }
    }
}
