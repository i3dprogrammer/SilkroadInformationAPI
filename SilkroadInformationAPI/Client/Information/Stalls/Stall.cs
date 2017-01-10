using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Stalls
{
    public class Stall
    {

        /// <summary>
        /// True if the character have a stall created.
        /// </summary>
        public bool StallCreated { get; set; } = false;

        /// <summary>
        /// Returns the stall name.
        /// </summary>
        public string StallName { get; set; }

        /// <summary>
        /// Returns the reference id from (characterdata) used as a stall decoration.
        /// </summary>
        public uint DecorationModelID { get; set; }

        /// <summary>
        /// Determines whether the stall is opened or closed.
        /// </summary>
        public bool Opened { get; set; } = false;

        /// <summary>
        /// Stall message.
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// List of current stall items.
        /// </summary>
        public Dictionary<byte, InventoryItem> StallItems = new Dictionary<byte, InventoryItem>();


        public Dictionary<uint, string> PeopleInStall = new Dictionary<uint, string>();

        public uint UniqueID { get; set; }

        public void Update(string _StallName, bool _StallCreated, uint _Decoration)
        {
            this.StallCreated = _StallCreated;
            this.StallName = _StallName;
            this.DecorationModelID = _Decoration;
        }
    }
}
