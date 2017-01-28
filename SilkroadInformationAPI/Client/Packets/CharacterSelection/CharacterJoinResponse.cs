using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
namespace SilkroadInformationAPI.Client.Packets.CharacterSelection
{
    public class CharacterJoinResponse
    {
        public static event Action OnCharacterSuccessfullyJoined;
        public static void Parse(Packet p)
        {
            if (p.ReadUInt8() == 1)
                OnCharacterSuccessfullyJoined?.Invoke();
        }
    }
}
