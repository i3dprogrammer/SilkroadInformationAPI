using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spawn
{
    class GroupSpawn
    {
        private static Packet GroupSpawnPacket;
        private static byte GroupSpawnType;
        private static ushort GroupSpawnAmount;

        public static void GroupSpawnStart(Packet packet)
        {
            GroupSpawnType = packet.ReadUInt8();
            GroupSpawnAmount = packet.ReadUInt16();
            GroupSpawnPacket = new Packet(0x3019);
        }

        public static void GroupSpawnEnd(Packet packet)
        {
            GroupSpawnPacket.Lock();

            for (int i = 0; i < GroupSpawnAmount; i++)
            {
                if (GroupSpawnType == 0x01)
                {
                    SingleSpawn.Parse(GroupSpawnPacket);
                }
                else
                {
                    Despawn.Parse(GroupSpawnPacket);
                }
            }
        }

        public static void GroupSpawnData(Packet packet)
        {
            GroupSpawnPacket.WriteUInt8Array(packet.GetBytes());
        }
    }
}
