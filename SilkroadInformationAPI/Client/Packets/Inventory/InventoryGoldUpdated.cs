using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Inventory
{
    public class InventoryGoldUpdated
    {
        public static event Action OnGoldUpdate;

        public static void Parse(Packet p)
        {
            int flag = p.ReadInt8();
            if (flag == 0x01)
                Client.Info.Gold = p.ReadUInt64();

            OnGoldUpdate?.Invoke();
        }
    }
}
