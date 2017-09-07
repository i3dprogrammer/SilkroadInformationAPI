using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class EnteredParty
    {

        /// <summary>
        /// This gets called when the client enters a party, the new party information is stored in Client.Party
        /// </summary>
        public static event Action OnClientEnterParty;

        public static void Parse(Packet p)
        {
            Client.Party = new Information.Party.Party();
            p.ReadUInt8(); //0xFF?? PVPFlag maybe?
            p.ReadUInt32(); //??
            Client.Party.MasterUniqueID = p.ReadUInt32();
            Client.Party.Type = (PartyType)p.ReadUInt8();
            Client.Party.MembersCount = p.ReadUInt8();
            for(int i = 0; i < Client.Party.MembersCount; i++)
            {
                var member = PartyUtility.ParseMember(p);
                Client.Party.PartyMembers.Add(member.UniqueID, member);
            }
            Client.Party.PartyMaster = Client.Party.PartyMembers[Client.Party.MasterUniqueID].Name;

            OnClientEnterParty?.Invoke();
        }
    }
}
