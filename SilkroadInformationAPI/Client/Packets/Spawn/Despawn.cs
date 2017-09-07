using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spawn
{
    class Despawn
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();

            if (Client.NearbyBuffAreas.ContainsKey(uid))
                Client.NearbyBuffAreas.Remove(uid);
            if (Client.NearbyCharacters.ContainsKey(uid))
                Client.NearbyCharacters.Remove(uid);
            if (Client.NearbyCOSs.ContainsKey(uid))
                Client.NearbyCOSs.Remove(uid);
            if (Client.NearbyItems.ContainsKey(uid))
                Client.NearbyItems.Remove(uid);
            if (Client.NearbyMobs.ContainsKey(uid))
                Client.NearbyMobs.Remove(uid);
            if (Client.NearbyNPCs.ContainsKey(uid))
                Client.NearbyNPCs.Remove(uid);
            if (Client.NearbyStructures.ContainsKey(uid))
                Client.NearbyStructures.Remove(uid);
        }
    }
}
