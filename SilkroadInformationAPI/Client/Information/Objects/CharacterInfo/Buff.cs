using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects.CharacterInfo
{
    public class Buff
    {
        /// <summary>
        /// Skill reference id in (skilldata).
        /// </summary>
        public uint SkillID { get; set; }

        /// <summary>
        /// Skill time duration left.
        /// </summary>
        public uint Duration { get; set; }

        /// <summary>
        /// If the character is the creator of the skill (Recovery division, etc..)
        /// </summary>
        public byte isCreator { get; set; }

        public Buff(uint id, uint dur, byte creator)
        {
            SkillID = id;
            Duration = dur;
            isCreator = creator;
        }
    }
}
