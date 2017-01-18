using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Party
{
    public class Party
    {
        public string PartyLeader { get; set; }
        public bool PartyMatching { get; set; } = false;
        public bool PartyFull { get; set; } = false;
        public byte MembersCount { get; set; } = 0;
        public PartyType Type { get; set; }
        public List<PartyMember> PartyMembers = new List<PartyMember>();

        public struct PartyMember
        {
            uint UniqueID { get; set; }
            string Name { get; set; }
            byte Level { get; set; }
            byte Race { get; set; }
            byte HPPercentage { get; set; }
            byte MPPercentage { get; set; }
            BasicInfo.Position Position { get; set; }
        }
    }
}
