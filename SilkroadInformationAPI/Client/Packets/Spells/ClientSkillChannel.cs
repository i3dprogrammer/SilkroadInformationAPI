using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class ClientSkillChannel
    {
        public static event Action OnClientStartChanneling;
        public static event Action OnClientEndChanneling;

        public static void Parse(Packet p)
        {
            byte flag1 = p.ReadUInt8();
            byte flag2 = p.ReadUInt8();
            if(flag1 == 0x01 && flag2 == 0x01)
            {
                OnClientStartChanneling?.Invoke();
            } else if(flag1 == 0x02 && flag2 == 0x00)
            {
                OnClientEndChanneling?.Invoke();
            }
        }
    }
}
