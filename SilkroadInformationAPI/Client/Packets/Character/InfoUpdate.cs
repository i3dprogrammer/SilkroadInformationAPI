using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    public class InfoUpdate
    {
        public static event Action OnGoldUpdate;
        public static event Action OnZerkActive;
        public static event Action OnSPChange;

        public static void Parse(Packet p)
        {
            int flag = p.ReadInt8();
            if (flag == 0x01)
            {
                Client.Info.Gold = p.ReadUInt64();
                OnGoldUpdate?.Invoke();
            }
            else if (flag == 0x02) //SP UP
            {
                Client.Info.SP = p.ReadUInt32();
                OnSPChange?.Invoke();
            }
            else if (flag == 0x03) //Zerk update
            {
                Client.Info.Zerk = (p.ReadUInt8() == 5);
                OnZerkActive?.Invoke();
            }
        }
    }
}
