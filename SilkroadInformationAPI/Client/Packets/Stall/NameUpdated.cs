using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class NameUpdated
    {
        public static event System.Action<Information.Objects.Character> OnCharacterStallNameUpdated;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            string title = p.ReadAscii();
            if (Client.NearbyCharacters.ContainsKey(uid))
            {
                Client.NearbyCharacters[uid].Stall.StallName = title;
                OnCharacterStallNameUpdated?.Invoke(Client.NearbyCharacters[uid]);
            }
        }
    }
}
