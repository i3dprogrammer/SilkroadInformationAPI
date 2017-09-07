using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class Leave
    {
        public static event System.Action OnClientLeaveStall;
        public static void Parse()
        {
            Client.CharacterInStall = false;
            OnClientLeaveStall?.Invoke();
        }
    }
}
