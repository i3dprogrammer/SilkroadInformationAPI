using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    class EXP_SP_Update
    {
        public static void Parse(Packet p)
        {
            uint source_uid = p.ReadUInt32();
            ulong exp = p.ReadUInt64();
            ulong SP = p.ReadUInt64(); //TODO: Configure SP
            if(Client.Info.CurrentExp + exp >= Client.Info.MaxEXP) //TODO: CHECK
            {
                Client.Info.CurrentExp = (Client.Info.CurrentExp + exp) - Client.Info.MaxEXP;
            }
        }
    }
}
