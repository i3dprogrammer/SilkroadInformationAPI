using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class PartyMatchingEntryDeleted
    {
        public static void Parse(Packet p)
        {
            byte success = p.ReadUInt8();
            if(success == 1)
            {
                uint DeletedEntryNumber = p.ReadUInt32();
            }
        }
    }
}
