using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class Closed
    {
        public static event Action<Information.Objects.Character> OnCharacterCloseStall;

        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();

            if (Client.NearbyCharacters.ContainsKey(uid))
            {
                Client.NearbyCharacters[uid].Stall.StallCreated = false;
                OnCharacterCloseStall?.Invoke(Client.NearbyCharacters[uid]);
            }
        }
    }
}
