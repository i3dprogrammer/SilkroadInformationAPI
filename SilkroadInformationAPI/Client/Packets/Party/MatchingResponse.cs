using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadInformationAPI.Client.Information.Party;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class MatchingResponse
    {
        public delegate void MatchingListPartyHandler(MatchingListEventArgs e);
        /// <summary>
        /// This is called after requesting party list(0x706C)
        /// </summary>
        public static event MatchingListPartyHandler OnMatchListReceive;

        public static void Parse(Packet p)
        {
            var list = new List<MatchListParty>();

            byte flag1 = p.ReadUInt8();
            byte flag2 = p.ReadUInt8();
            if(flag1 == 1 && flag2 == 1)
            {
                p.ReadUInt8();
                int count = p.ReadUInt8();
                for(int i = 0; i < count; i++)
                {
                    uint number = p.ReadUInt32();
                    uint uid = p.ReadUInt32();
                    string MasterName = p.ReadAscii();
                    var Race = (PartyRace)p.ReadUInt8();
                    byte CurrMembers = p.ReadUInt8();
                    var Type = (PartyType)p.ReadUInt8();
                    var Purpose = (Objective)p.ReadUInt8();
                    byte MinLevel = p.ReadUInt8();
                    byte MaxLevel = p.ReadUInt8();
                    string Title = p.ReadAscii();

                    list.Add(new MatchListParty(number, Race, MasterName, uid, Title, Purpose, MinLevel, MaxLevel, CurrMembers, Type));
                }
            }

            OnMatchListReceive?.Invoke(new MatchingListEventArgs(list));
        }
    }

    public class MatchingListEventArgs : EventArgs
    {
        List<MatchListParty> MatchingListParty;

        public MatchingListEventArgs(List<MatchListParty> _match)
        {
            MatchingListParty = _match;
        }

    }
}
