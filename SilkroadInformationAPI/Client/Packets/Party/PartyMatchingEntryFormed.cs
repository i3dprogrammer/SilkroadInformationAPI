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
        /// <summary>
        /// This is called upon the creation of party matching entry, the entry details is stored in Client.Party.PartyMatching
        /// <para>NOTE: This is only called if the client is creating the matching entry.</para>
        /// </summary>
        public static event Action OnPartyMatchingEntryFormed;

        /// <summary>
        /// This is called upon party matching entry changing.
        /// </summary>
        public static event Action OnPartyMatchingEntryEdited;
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

                Client.Party.PartyMatching = new Information.Party.PartyMatchingEntry(EntryNumber, Client.Info.CharacterName, Client.Info.UniqueID, Title, Purpose, MinLevel, MaxLevel, Client.Party.MembersCount, Type);

                if(p.Opcode == 0xB069) // Entry formed event
                {
                    OnPartyMatchingEntryFormed?.Invoke();
                } else if(p.Opcode == 0xB06A) //Entry changed event
                {
                    OnPartyMatchingEntryEdited?.Invoke();
                }

            }
        }
    }
}
