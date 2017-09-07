using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class BuffEnded
    {
        public static event Action<uint> OnClientBuffEnded;
        public static event Action<uint> OnCharacterBuffEnded;
        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 1)
            {
                uint sUID = p.ReadUInt32();
                if (Client.State.Buffs.ContainsKey(sUID))
                {
                    Client.State.Buffs.Remove(sUID);
                    OnClientBuffEnded?.Invoke(sUID);
                }
                if (Client.NearbyCharacters.Any(x => x.Value.State.Buffs.ContainsKey(sUID)))
                {
                    Client.NearbyCharacters.Single(x => x.Value.State.Buffs.ContainsKey(sUID)).Value.State.Buffs.Remove(sUID);
                    OnCharacterBuffEnded?.Invoke(sUID);
                }
            }
        }
    }
}
