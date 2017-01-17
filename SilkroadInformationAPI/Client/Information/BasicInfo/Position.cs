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
            if (X > 32768)
                X = 65536 - X;
            byte xSec = (byte)((ushort)(ReigonID << 8) / 256);
            return (int)((xSec - 135) * 192 + X / 10);
        }

        /// <summary>
        /// Returns the Y Coordinates of the object.
        /// </summary>
        /// <returns>uint representing the Y value</returns>
        public int GetRealY()
        {
            if (Y > 32768)
                Y = 6556 - Y;
            byte ySec = (byte)(ReigonID >> 8);
            return (int)((ySec - 92) * 192 + Y / 10);
        }

        public Position()
        {

        }

        public Position(int _ReigonID, float _X, float _Y, float _Z)
        {
            ReigonID = _ReigonID;
            X = _X;
            Y = _Y;
            Z = _Z;
        }
    }
}
