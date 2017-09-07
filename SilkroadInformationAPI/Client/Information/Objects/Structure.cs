using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class Structure : Base
    {

        /// <summary>
        /// HP of the structure, only available in fortress war?
        /// </summary>
        public uint HP { get; set; }

        /// <summary>
        /// ???
        /// </summary>
        public uint RefEventStructID { get; set; }

        /// <summary>
        /// Returns the state of the structure (Burning, destroyed, etc..) *test for values*.
        /// </summary>
        public ushort State { get; set; }

        /// <summary>
        /// Owner name in case of Dimension holes.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// The unique ID of the owner
        /// </summary>
        public uint OwnerUniqueID { get; set; }
    }
}
