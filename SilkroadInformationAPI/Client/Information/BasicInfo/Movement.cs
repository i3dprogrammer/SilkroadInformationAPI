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
    }
}
