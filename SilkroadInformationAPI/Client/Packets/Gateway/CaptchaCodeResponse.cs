using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class CaptchaCodeResponse
    {

        /// <summary>
        /// This is received upon entering a successful captcha.
        /// </summary>
        public static event Action OnCaptchaCodeResponseSuccess;

        /// <summary>
        /// This is received upon entering a wrong captcha.
        /// <para>first obj is the Current attempts to enter the captcha, the second obj is the maximum attempts.</para>
        /// </summary>
        public static event Action<uint, uint> OnCaptchaCodeResponseFailed;
        public static void Parse(Packet p)
        {
            byte result = p.ReadUInt8();
            if(result == 0x01) //Captcha is correct
            {
                OnCaptchaCodeResponseSuccess?.Invoke();
            } else
            {
                uint MaxAttempts = p.ReadUInt32();
                uint CurrentAttempts = p.ReadUInt32();
                OnCaptchaCodeResponseFailed?.Invoke(CurrentAttempts, MaxAttempts);
            }
        }
    }
}
