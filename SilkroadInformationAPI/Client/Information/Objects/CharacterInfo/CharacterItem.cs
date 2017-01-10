using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects.CharacterInfo
{
    public class CharacterItem
    {
        /// <summary>
        /// The item reference id in (itemdata).
        /// </summary>
        public uint ModelID { get; set; }

        /// <summary>
        /// The item plus value.
        /// </summary>
        public uint PlusValue { get; set; }

        public CharacterItem(uint id, uint opt)
        {
            ModelID = id;
            PlusValue = opt;
        }
    }
}
