using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class CelestialPosition
    {
        public static event Action OnClientTeleport;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            if(uid == Client.Info.UniqueID)
            {
                OnClientTeleport?.Invoke();
            }
        }
    }
}
