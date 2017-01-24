using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Character
{
    class CharacterData
    {
        private static Packet CharacterDataPacket;

        public static void CharDataStart()
        {
            CharacterDataPacket = new Packet(0x3013, false, true);
        }

        public static void CharData(Packet p)
        {
            CharacterDataPacket.WriteUInt8Array(p.GetBytes());
        }

        public static void CharDatEnd()
        {
            CharacterDataPacket.Lock();
            ParseCharacterData.Parse(CharacterDataPacket);
        }
    }
}
