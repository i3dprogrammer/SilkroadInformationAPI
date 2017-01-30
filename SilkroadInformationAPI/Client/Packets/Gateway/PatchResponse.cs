using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class PatchResponse
    {
        public static event Action OnPatchResponseSuccess;
        public static event Action OnPatchResponseFailed;
        public static void Parse(Packet p)
        {
            byte result = p.ReadUInt8();
            if(result == 0x01) //No patching is required -> success
            {
                OnPatchResponseSuccess?.Invoke();
            } else
            {
                OnPatchResponseFailed?.Invoke();
            }
        }
    }
}
