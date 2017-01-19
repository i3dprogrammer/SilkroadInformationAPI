using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class Entered
    {
        public static event System.Action OnClientEnterStall;

        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 1) //Successfully entered the stall.
            {
                StallUtility.ClearCurrentStall();
                Client.CharacterInStall = true;

                Client.CurrentStall.UniqueID = p.ReadUInt32();
                Client.CurrentStall.Message = p.ReadAscii();
                Client.CurrentStall.Opened = (p.ReadUInt8() == 1);
                p.ReadUInt8(); //??

                StallUtility.ParseCurrentStallItems(p);

                byte peopleInStallCount = p.ReadUInt8();
                for (int i = 0; i < peopleInStallCount; i++) {
                    uint uid = p.ReadUInt32();
                    Client.CurrentStall.PeopleInStall.Add(uid, Actions.Mapping.GetCharNameFromUID(uid));
                }

                OnClientEnterStall?.Invoke();
            }

        }
    }
}
