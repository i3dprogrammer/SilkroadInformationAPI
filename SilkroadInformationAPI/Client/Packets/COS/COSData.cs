using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.COS
{
    class COSData
    {
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32(); //Unique ID
            uint refID = p.ReadUInt32(); //Model ID, can skip since the spawn packet comes first.
            var obj = Media.Data.MediaModels[refID];

            Client.NearbyCOSs[uid].MaxHP = p.ReadUInt32(); // MaxHP?
            p.ReadUInt32(); // MaxHP?

            if (obj.Classes.F == 1) //Normal COS
            {
                p.ReadUInt8(); // UNK ?? something with spawned pet count
            } else if(obj.Classes.F == 2) //Job COS
            {
                p.ReadUInt8(); //Max transport slots
                byte currCount = p.ReadUInt8(); //Current transport items
                for (int i = 0; i < currCount; i++)
                    Inventory.InventoryUtility.ParseItem(p);
                p.ReadUInt32(); //Owner Unique ID
            }
            else if(obj.Classes.F == 3) //Attack COS
            {
                p.ReadUInt64(); //Pet EXP
                p.ReadUInt8(); //Level?
                Client.NearbyCOSs[uid].CurrentHGP = p.ReadUInt16(); //HGP Points
                p.ReadUInt32(); //??
                p.ReadAscii(); //COS Name
                p.ReadUInt8(); //??
                p.ReadUInt32(); //Owned unique id
                p.ReadUInt8(); // UNK ?? something with spawned pet count
            } else if(obj.Classes.F == 4) //Pickup COS
            {
                Client.NearbyCOSs[uid].Inventory.Clear();

                p.ReadUInt32(); //Pet settings in binary
                p.ReadAscii(); //COS Name
                p.ReadUInt8(); //COS inventory size
                byte currCount = p.ReadUInt8();
                for (int i = 0; i < currCount; i++)
                {
                    var item = Inventory.InventoryUtility.ParseItem(p);
                    Client.NearbyCOSs[uid].Inventory.Add(item.Slot, item);
                }
                p.ReadUInt32(); //Owner unique id
                p.ReadUInt8(); // UNK ?? something with spawned pet count
            } else if(obj.Classes.F == 5) //Guild COS
            {
                //TODO: test on a server with active Guild COS
            }
        }
    }
}
