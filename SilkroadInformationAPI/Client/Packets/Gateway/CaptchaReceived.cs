using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class CaptchaReceived
    {
        public static event Action<uint[]> OnGatewayCaptchaReceived;

        public static void Parse(Packet p)
        {
            uint[] Pixels = Actions.CaptchaImage.GeneratePacketCaptcha(p);

            OnGatewayCaptchaReceived?.Invoke(Pixels);
        }
    }
}
