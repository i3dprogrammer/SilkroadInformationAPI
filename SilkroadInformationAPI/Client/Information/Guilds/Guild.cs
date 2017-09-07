using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Guilds
{
    public class Guild
    {
        /// <summary>
        /// Guild name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guild ID.
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Union ID, if there is any.
        /// </summary>
        public uint UnionID { get; set; }

        /// <summary>
        /// Guild grant name.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Determines whether the guild is friendly or hostile (0 = Hostile, 1 = Friendly)
        /// </summary>
        public byte IsFriendly { get; set; }
    }
}
