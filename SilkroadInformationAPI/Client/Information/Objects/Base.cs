using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    class Base
    {
        public uint ModelID;
        public uint UniqueID;
        public BasicInfo.Position Position = new BasicInfo.Position();
    }
}
