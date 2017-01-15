using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Movement
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
                int xsec = p.ReadUInt8();
                int ysec = p.ReadUInt8();
                int xcoord = p.ReadInt16();
                int zcoord = p.ReadInt16();
                int ycoord = p.ReadInt16();

                int realXCoord = xcoord, realYCoord = ycoord;
                if (xcoord > 32768)
                    realXCoord = 65536 - xcoord;
                if (ycoord > 32768)
                    realYCoord = 6556 - ycoord;

                //if (Config.Main.Characters.ContainsKey(UniqueID))
                //{
                //    Config.Main.Characters[UniqueID].X = realXCoord;
                //    Config.Main.Characters[UniqueID].Y = realYCoord;
                //    UpdateTreeView(Config.Main.Characters[UniqueID]);
                //}
                //if (Config.Main.Mobs.ContainsKey(UniqueID))
                //{
                //    Config.Main.Mobs[UniqueID].X = realXCoord;
                //    Config.Main.Mobs[UniqueID].Y = realYCoord;
                //}
            }
        }
    }
}
