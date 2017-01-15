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


        /// <summary>
        /// Returns the X Coordinates of the object.
        /// </summary>
        /// <returns>Int representing the X value</returns>
        public int GetRealX()
        {
            byte xSec = (byte)(Position.ReigonID >> 8);
            byte ySec = (byte)((ushort)(Position.ReigonID << 8) / 256);
            return (int)((xSec - 135) * 192 + Position.X / 10);
        }

        /// <summary>
        /// Returns the Y Coordinates of the object.
        /// </summary>
        /// <returns>Int representing the Y value</returns>
        public int GetRealY()
        {
            byte xSec = (byte)(Position.ReigonID >> 8);
            byte ySec = (byte)((ushort)(Position.ReigonID << 8) / 256);
            return (int)((ySec - 92) * 192 + Position.Y / 10);
        }
    }
}
