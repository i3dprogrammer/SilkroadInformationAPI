using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class ItemEquip
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            if(Client.Info.UniqueID == uid) // 0x3038
            {
                byte EquippingSlot = p.ReadUInt8();
                uint RefItemID = p.ReadUInt32();
            }
            else if (Client.NearbyCharacters.ContainsKey(uid))
            {
                byte PlusValue = p.ReadUInt8();
                uint RefItemID = p.ReadUInt32();
                Client.NearbyCharacters[uid].Inventory.Add(new Information.Objects.CharacterInfo.CharacterItem(RefItemID, PlusValue));
            }
        }
    }
}
