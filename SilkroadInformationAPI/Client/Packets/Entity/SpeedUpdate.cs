using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    class SpeedUpdate
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();

            if(Client.Info.UniqueID == uid)
            {
                Client.State.WalkSpeed = p.ReadSingle();
                Client.State.RunSpeed = p.ReadSingle();
            }

            if (Client.NearbyCharacters.ContainsKey(uid))
            {
                Client.NearbyCharacters[uid].State.WalkSpeed = p.ReadSingle();
                Client.NearbyCharacters[uid].State.RunSpeed = p.ReadSingle();
            }
        }
    }
}
