using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    class PositionUpdate
    {
        public static void Parse(Packet p)
        {
            uint UniqueID = p.ReadUInt32();
            if (Client.NearbyCharacters.Where(x => x.Value.TransportUniqueID == UniqueID).Count() > 0)
            {
                UniqueID = Client.NearbyCharacters.First(x => x.Value.TransportUniqueID == UniqueID).Key;
            }

            int moving = p.ReadInt8();
            if (moving == 1)
            {
                ushort ReigonID = p.ReadUInt16();
                short xcoord = p.ReadInt16();
                short zcoord = p.ReadInt16();
                short ycoord = p.ReadInt16();

                if (Client.NearbyCharacters.ContainsKey(UniqueID))
                {
                    Client.NearbyCharacters[UniqueID].Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);
                    Console.WriteLine("{ " + Actions.Mapping.GetCharNameFromUID(UniqueID) + " } [" + Client.NearbyCharacters[UniqueID].Position.GetRealX() + ", " +
                        Client.NearbyCharacters[UniqueID].Position.GetRealY() + "]");
                }
                if (Client.NearbyCOSs.ContainsKey(UniqueID))
                    Client.NearbyCOSs[UniqueID].Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);
                if (Client.NearbyMobs.ContainsKey(UniqueID))
                    Client.NearbyMobs[UniqueID].Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);
                if (Client.NearbyNPCs.ContainsKey(UniqueID))
                    Client.NearbyNPCs[UniqueID].Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);
                if (Client.NearbyStructures.ContainsKey(UniqueID))
                    Client.NearbyStructures[UniqueID].Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);

                if (Client.Info.UniqueID == UniqueID || Client.Info.TransportUniqueID == UniqueID)
                    Client.Position = new Information.BasicInfo.Position(ReigonID, xcoord, ycoord, zcoord);



            }
        }
    }
}
