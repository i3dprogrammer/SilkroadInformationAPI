using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class ClientSkillWithdraw
    {
        public static event Action OnClientSkillWithdrawl;
        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 1) //Success
            {
                uint refID = p.ReadUInt32();
                if (Client.Skills.Exists(x => x.Position == Media.Data.MediaSkills[refID].Position))
                {
                    Client.Skills.Remove(Client.Skills.Single(x => x.Position == Media.Data.MediaSkills[refID].Position));
                    Client.Skills.Add(Media.Data.MediaSkills[refID]);
                }
                else
                {
                    throw new Exception("WTF?");
                }
                OnClientSkillWithdrawl?.Invoke();
            }
        }
    }
}
