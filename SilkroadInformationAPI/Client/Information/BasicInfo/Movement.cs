using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.BasicInfo
{
    public class Movement
    {
        public byte HasDestination;
        public byte Type;
        public ushort DestinationRegion;
        public uint DestinationOffsetX;
        public uint DestinationOffsetY;
        public uint DestinationOffsetZ;
        public byte Source;
        public ushort Angle;

        public int GetRealX()
        {
            if (DestinationOffsetX > 32768)
                DestinationOffsetX = 65536 - DestinationOffsetX;
            byte xSec = (byte)(DestinationRegion >> 8);
            byte ySec = (byte)((ushort)(DestinationRegion << 8) / 256);
            return (int)((xSec - 135) * 192 + DestinationOffsetX / 10);
        }

        /// <summary>
        /// Returns the Y Coordinates of the object.
        /// </summary>
        /// <returns>uint representing the Y value</returns>
        public int GetRealY()
        {
            if (DestinationOffsetY > 32768)
                DestinationOffsetY = 6556 - DestinationOffsetY;
            byte xSec = (byte)(DestinationRegion >> 8);
            byte ySec = (byte)((ushort)(DestinationRegion << 8) / 256);
            return (int)((ySec - 92) * 192 + DestinationOffsetY / 10);
        }
    }
}
