using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class LevelUp
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            if(Client.Info.UniqueID == uid)
            {
                Client.Info.Level += 1;
                Client.Info.MaxEXP = Media.Data.MaxEXP[Client.Info.Level];
            }
        }
    }
}
