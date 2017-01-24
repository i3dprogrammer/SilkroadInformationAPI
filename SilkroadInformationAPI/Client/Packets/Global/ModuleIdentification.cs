using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Global
{
    public class ModuleIdentification
    {

        /// <summary>
        /// Received after connecting to the server with SilkroadSecurityApi, the obj returned is the Service Module Name.
        /// </summary>
        public static event Action<string> OnGlobalModuleIdentification;

        public static void Parse(Packet p)
        {
            string serviceName = p.ReadAscii();

            OnGlobalModuleIdentification?.Invoke(serviceName);
        }
    }
}
