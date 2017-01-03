using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    class Leave
    {
        public static void Parse()
        {
            Client.CharacterInStall = false;
        }
    }
}
