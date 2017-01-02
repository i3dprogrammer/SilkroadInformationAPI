using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects.CharacterInfo
{
    public class Stall
    {

        /// <summary>
        /// True if the character have a stall opened ("opened" here means character state, not the stall from inside).
        /// </summary>
        public bool isStall { get; set; } = false;

        /// <summary>
        /// Returns the stall name.
        /// </summary>
        public string StallName { get; set; }

        /// <summary>
        /// Returns the reference id from (characterdata) used as a stall decoration.
        /// </summary>
        public uint DecorationModelID { get; set; }
    }
}
