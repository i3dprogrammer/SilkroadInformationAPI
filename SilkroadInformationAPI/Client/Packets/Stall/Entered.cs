using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    class Entered
    {
        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 1) //Successfully entered the stall.
            {
                Client.CurrentStall.StallItems.Clear();
                Client.CurrentStall.PeopleInStall.Clear();
                Client.CharacterInStall = true;


                Client.CurrentStall.UniqueID = p.ReadUInt32();
                Client.CurrentStall.Message = p.ReadAscii();
                Client.CurrentStall.Opened = (p.ReadUInt8() == 1);
                p.ReadUInt8(); //??

                while (true)
                {
                    var item = Inventory.ParseItem.Parse(p);
                    if (item == null)
                        break;

                    p.ReadUInt8(); // ??
                    item.Stack = p.ReadUInt16();
                    item.Price = p.ReadUInt64();

                    Client.CurrentStall.StallItems.Add((byte)item.Slot, item);
                }

                byte peopleInStallCount = p.ReadUInt8();
                for (int i = 0; i < peopleInStallCount; i++) {
                    uint uid = p.ReadUInt32();
                    Client.CurrentStall.PeopleInStall.Add(uid, Actions.Mapping.GetCharNameFromUID(uid));
                }

            }

        }
    }
}
