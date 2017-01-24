using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class AgentAuthResponse
    {
        public static event Action OnAgentAuthResponseSuccess;
        public static event Action OnAgentAuthResponseFailed;

        public static void Parse(Packet p)
        {
            byte result = p.ReadUInt8();
            if(result == 0x01)
            {
                OnAgentAuthResponseSuccess?.Invoke();
            } else
            {
                OnAgentAuthResponseFailed?.Invoke();
            }
        }
    }
}
