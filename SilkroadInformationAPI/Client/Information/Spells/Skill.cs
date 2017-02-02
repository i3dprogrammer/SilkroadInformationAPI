using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Spells
{
    public class Skill : Media.DataInfo.Base
    {
        /// <summary>
        /// Skill reference id in (skilldata).
        /// </summary>
        // public uint SkillID { get; set; }

        /// <summary>
        /// Skill time duration left.
        /// </summary>
        public uint Duration { get; set; }

        /// <summary>
        /// If the character is the creator of the skill (Recovery division, etc..)
        /// </summary>
        public bool isCreator { get; set; }

        /// <summary>
        /// The temporary unique id assigned to the skill.
        /// </summary>
        public uint TemporaryID { get; set; }

        public Skill(uint id, byte _enabled)
        {
            ModelID = id;
            Enabled = _enabled;
        }

        public Skill(uint id, uint dur, byte creator)
        {
            ModelID = id;
            TemporaryID = dur;
            isCreator = (creator == 1);
        }

        public Skill()
        {

        }

        public byte Enabled; //??
    }
}
