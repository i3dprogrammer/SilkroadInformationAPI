using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class ClientSkillLearn
    {
        public static event Action OnClientSkillLearn;
        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 1) //Skill learnt succesfully.
            {
                uint refID = p.ReadUInt32();
                if(Client.Skills.Exists(x => x.Position == Media.Data.MediaSkills[refID].Position))
                {
                    Client.Skills.Remove(Client.Skills.Single(x => x.Position == Media.Data.MediaSkills[refID].Position));
                    Client.Skills.Add(Media.Data.MediaSkills[refID]);
                } else
                {
                    Client.Skills.Add(Media.Data.MediaSkills[refID]);
                }
                OnClientSkillLearn?.Invoke();
            }
        }
    }
}
