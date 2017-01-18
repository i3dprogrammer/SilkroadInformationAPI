using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class Mob : Base
    {
        /// <summary>
        /// Mob type (General, Party, Giant, Giant Party, etc..)
        /// </summary>
        public byte Rarity { get; set; }

        /// <summary>
        /// TODO: ???
        /// </summary>
        public byte Appearance { get; set; }

        public uint CurrentHP { get; set; }

        public uint MaxHP { get; set; }

        public bool BadStatus { get; set; }
    }
}
