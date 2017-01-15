using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class Base
    {
        /// <summary>
        /// The Reference ID in Media.pk2 (Usually found in characterdata, itemdata, teleportbuildings)
        /// </summary>
        public uint ModelID { get; set; }

        /// <summary>
        /// The Unique ID of the object, each object has it's own distinct unique id.
        /// </summary>
        public uint UniqueID { get; set; } = 0;

        public BasicInfo.Position Position = new BasicInfo.Position();
    }
}
