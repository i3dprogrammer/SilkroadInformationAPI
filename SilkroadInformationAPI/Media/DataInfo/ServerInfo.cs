using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo
{
    public class ServerInfo
    {
        public List<Division> LoginDivisons;
        public uint Version;
        public ushort Port;
        public byte Locale;

        public ServerInfo()
        {
            LoginDivisons = new List<Division>();
        }
    }
}
