using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class PVPUpdate
    {
        public static Action<FRPVPMode> OnClientFreePVP;
        public static Action<FRPVPMode, Information.Objects.Character> OnCharacterFreePVP;

        public static void Parse(Packet p)
        {
            byte result = p.ReadUInt8();
            if(result == 0x01)
            {
                uint uid = p.ReadUInt32();
                if (Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].PVPCape = (FRPVPMode)p.ReadUInt8();
                    OnCharacterFreePVP?.Invoke(Client.NearbyCharacters[uid].PVPCape, Client.NearbyCharacters[uid]);
                }
                else if (Client.Info.UniqueID == uid)
                {
                    Client.Info.PVPCape = (FRPVPMode)p.ReadUInt8();
                    OnClientFreePVP?.Invoke(Client.Info.PVPCape);
                }
            }
        }
    }
}
