using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class BuffStarted
    {
        public delegate void BuffStartedHandler(BuffStartedEventArgs e);
        public static event BuffStartedHandler OnBuffAdded;
        public static void Parse(Packet p)
        {
            uint BuffedUID = p.ReadUInt32();
            uint BuffRefID = p.ReadUInt32();
            uint SkillUID = p.ReadUInt32();
            if (SkillUID != 0)
            {
                if (Client.Info.UniqueID == BuffedUID)
                {
                    if (Client.State.Buffs.ContainsKey(SkillUID))
                        Client.State.Buffs.Remove(SkillUID);
                    Client.State.Buffs.Add(SkillUID, new Information.Spells.Skill(BuffRefID, 0));
                }
                else if (Client.NearbyCharacters.ContainsKey(BuffedUID))
                {
                    if (Client.NearbyCharacters[BuffedUID].State.Buffs.ContainsKey(SkillUID))
                        Client.NearbyCharacters[BuffedUID].State.Buffs.Remove(SkillUID);
                    Client.NearbyCharacters[BuffedUID].State.Buffs.Add(SkillUID, new Information.Spells.Skill(BuffRefID, 0));
                }
                OnBuffAdded?.Invoke(new BuffStartedEventArgs() { BuffedCharacterUniqueID = BuffedUID, BuffObjectRefID = BuffRefID, SkillUniqueID = SkillUID });
            }
        }
    }
    public class BuffStartedEventArgs : EventArgs
    {
        public uint BuffedCharacterUniqueID;
        public uint BuffObjectRefID;
        public uint SkillUniqueID;
    }
}
