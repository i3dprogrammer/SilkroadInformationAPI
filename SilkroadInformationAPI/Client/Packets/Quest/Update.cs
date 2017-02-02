using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Quest
{
    public class Update
    {
        public static event Action OnQuestFinished;

        public static void Parse(Packet p)
        {
            byte flag = p.ReadUInt8();
            if(flag == 0x03) //Quest completed.
            {
                OnQuestFinished?.Invoke();
            } else if(flag == 0x04) //Quest abandoned.
            {
                //..
            }
        }
    }
}
