using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class Created
    {
        public static event Action<Information.Objects.Character> OnCharacterCreateStall;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            string title = p.ReadAscii();
            uint dec = p.ReadUInt32(); //Decoration

            if (Client.NearbyCharacters.ContainsKey(uid))
            {
                Client.NearbyCharacters[uid].Stall.Update(title, true, dec);
                OnCharacterCreateStall?.Invoke(Client.NearbyCharacters[uid]);
            } else if(Client.Info.UniqueID == uid)
            {
                Client.CurrentStall.Message = title;
            }
        }
    }
}
