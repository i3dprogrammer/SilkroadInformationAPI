using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    class PartyUtility
    {
        public static Information.Party.Party.PartyMember ParseMember(Packet p)
        {
            Information.BasicInfo.Position pos = new Information.BasicInfo.Position();
            p.ReadUInt8(); //0xFF?? PVPFlag maybe?
            uint uid = p.ReadUInt32();
            string name = p.ReadAscii();
            uint modelID = p.ReadUInt32();
            byte Level = p.ReadUInt8();
            byte MPHP = p.ReadUInt8();
            pos.RegionID = p.ReadUInt16();
            if (pos.RegionID < short.MaxValue)
            {   //World
                pos.X = p.ReadUInt16();
                pos.Y = p.ReadUInt16();
                pos.Z = p.ReadUInt16();
            }
            else
            {   //Dungeon
                pos.X = p.ReadUInt32();
                pos.Y = p.ReadUInt32();
                pos.Z = p.ReadUInt32();
            }
            p.ReadUInt32(); //TODO, need to sleep
            string guildName = p.ReadAscii(); //Guild name
            p.ReadUInt8(); //TODO
            p.ReadUInt32(); //Mastery Tree 1
            p.ReadUInt32(); //Mastery Tree 2

            return new Information.Party.Party.PartyMember(uid, name, guildName, Level, modelID,
                    Convert.ToByte(MPHP.ToString("X2")[1].ToString(), 16),
                    Convert.ToByte(MPHP.ToString("X2")[0].ToString(), 16), pos);
        }
    }
}
