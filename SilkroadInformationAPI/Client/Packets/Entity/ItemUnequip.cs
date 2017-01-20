using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class ItemUnequip
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            if(Client.Info.UniqueID == uid)
            {
                byte SlotUnequipping = p.ReadUInt8();
                uint RefItemID = p.ReadUInt32();
            }
            else if (Client.NearbyCharacters.ContainsKey(uid))
            {
                p.ReadUInt8();
                uint RefItemID = p.ReadUInt32();
                if (Client.NearbyCharacters[uid].Inventory.Where(x => x.ModelID == RefItemID).Count() > 0)
                {
                    Client.NearbyCharacters[uid].Inventory.Remove(
                        Client.NearbyCharacters[uid].Inventory.First(x => x.ModelID == RefItemID));
                }
            }
        }
    }
}
