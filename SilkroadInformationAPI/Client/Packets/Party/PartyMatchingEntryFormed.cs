using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class PartyMatchingEntryFormed
    {
        public static void Parse(Packet p)
        {
            byte success = p.ReadUInt8();
            if(success == 1)
            {
                uint EntryNumber = p.ReadUInt32();
                p.ReadUInt32(); // ??
                var Type = (PartyType)p.ReadUInt8();
                var Purpose = (PartyObjective)p.ReadUInt8();
                var MinLevel = p.ReadUInt8();
                var MaxLevel = p.ReadUInt8();
                var Title = p.ReadAscii();

                //TODO: Add event.
            }
        }
    }
}
