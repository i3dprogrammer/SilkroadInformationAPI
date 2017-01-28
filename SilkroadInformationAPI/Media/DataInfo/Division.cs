using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo
{
    public class Division
    {
        public string Name;
        public List<string> IPs;

        public Division(string _name, List<string> _ip)
        {
            Name = _name;
            IPs = _ip;
        }
    }
}
