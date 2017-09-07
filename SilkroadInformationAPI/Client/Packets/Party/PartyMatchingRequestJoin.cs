using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class PartyMatchingRequestJoin
    {
        public delegate void PartyMatchingRequestJoinHandler(PartyMatchingRequestJoinEventArgs e);
        public static event PartyMatchingRequestJoinHandler OnPartyMatchingRequestJoin;

        public static void Parse(Packet p)
        {
            p.ReadUInt32(); //UNK
            uint requesterUID = p.ReadUInt32(); //Unique ID
            uint EntryNumber = p.ReadUInt32();
            p.ReadUInt32(); //Mastery tree 1
            p.ReadUInt32(); //Mastery tree 2
            p.ReadUInt8(); //UNK

            var member = PartyUtility.ParseMember(p);

            OnPartyMatchingRequestJoin?.Invoke(new Party.PartyMatchingRequestJoinEventArgs(member));
        }
    }

    public class PartyMatchingRequestJoinEventArgs : EventArgs
    {
        public Information.Party.Party.PartyMember MemberRequesting;

        public PartyMatchingRequestJoinEventArgs(Information.Party.Party.PartyMember member)
        {
            MemberRequesting = member;
        }
    }
}
