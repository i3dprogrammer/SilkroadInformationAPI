using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Party
{
    public class MatchListParty
    {
        public uint PartyNumber { get; set; }
        public PartyRace Race { get; set; }
        public string MasterName { get; set; }
        public uint MasterUniqueID { get; set; }
        public string Title { get; set; }
        public Objective Purpose { get; set; } = Objective.Hunting;
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public byte CurrMembers { get; set; }
        public byte MaxMembers { get; set; }
        public PartyType Type { get; set; }


        public MatchListParty(uint _number, PartyRace _race, string _name, uint _uid, string _title, Objective _purpose, byte _min, byte _max, byte _currMembers, PartyType _type)
        {
            PartyNumber = _number;
            Race = _race;
            MasterName = _name;
            MasterUniqueID = _uid;
            Title = _title;
            Purpose = _purpose;
            MinLevel = _min;
            MaxLevel = _max;
            CurrMembers = _currMembers;
            MaxMembers = 8;
            Type = _type;

            if (Type == PartyType.ExpFreeForAll_ItemFreeForAll || Type == PartyType.ExpFreeForAll_ItemShare)
                MaxMembers = 4;
        }
    }
}
