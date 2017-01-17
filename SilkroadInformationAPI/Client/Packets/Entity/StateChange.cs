using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class StateChange
    {
        public static event Action OnMobDies;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            byte flag1 = p.ReadUInt8();
            byte flag2 = p.ReadUInt8();
            if (flag1 == 0x00 && flag2 == 0x02) //MOB DIED
            {
                OnMobDies?.Invoke();
            }
        }
    }
}
