using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class BuffStart
    {
        public static void Parse(Packet p)
        {
            uint BuffedUID = p.ReadUInt32();
            uint BuffRefID = p.ReadUInt32();
            uint SkillUID = p.ReadUInt32();
            if(Client.Info.UniqueID == BuffedUID)
            {
                Client.State.Buffs.Add(SkillUID, new Information.Spells.Skill(BuffRefID, 0));
            } else if (Client.NearbyCharacters.ContainsKey(BuffedUID))
            {
                Client.NearbyCharacters[BuffedUID].State.Buffs.Add(SkillUID, new Information.Spells.Skill(BuffRefID, 0));
            }
        }
    }
}
