using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Party
{
    public class Party
    {
        public string PartyMaster { get; set; }
        public uint MasterUniqueID { get; set; }
        public PartyMatchingEntry PartyMatching;
        public byte MembersCount { get; set; } = 0;
        public PartyType Type { get; set; }
        public Dictionary<uint, PartyMember> PartyMembers = new Dictionary<uint, PartyMember>();

        public Party()
        {
            PartyMembers = new Dictionary<uint, PartyMember>();
        }

        public class PartyMember
        {
            public uint UniqueID { get; set; }
            public string Name { get; set; }
            public string GuildName { get; set; }
            public byte Level { get; set; }
            public uint RefModelID { get; set; }
            public byte HPPercentage { get; set; }
            public byte MPPercentage { get; set; }
            public BasicInfo.Position Position { get; set; }

            public PartyMember(uint _unique, string name, string _guildName, byte level, uint modelID, byte HP, byte MP, BasicInfo.Position pos)
            {
                UniqueID = _unique;
                Name = name;
                GuildName = _guildName;
                Level = level;
                RefModelID = modelID;
                HPPercentage = HP;
                MPPercentage = MP;
                Position = pos;
            }
        }
    }
}
