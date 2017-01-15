using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.BasicInfo
{
    public class Position
    {
        public int ReigonID;
        public float X;
        public float Y;
        public float Z;
        public int Angle;

        /// <summary>
        /// Returns the X Coordinates of the object.
        /// </summary>
        /// <returns>uint representing the X value</returns>
        public int GetRealX()
        {
            byte xSec = (byte)(ReigonID >> 8);
            byte ySec = (byte)((ushort)(ReigonID << 8) / 256);
            return (int)((xSec - 135) * 192 + X / 10);
        }

        /// <summary>
        /// Returns the Y Coordinates of the object.
        /// </summary>
        /// <returns>uint representing the Y value</returns>
        public int GetRealY()
        {
            byte xSec = (byte)(ReigonID >> 8);
            byte ySec = (byte)((ushort)(ReigonID << 8) / 256);
            return (int)((ySec - 92) * 192 + Y / 10);
        }
    }
}
