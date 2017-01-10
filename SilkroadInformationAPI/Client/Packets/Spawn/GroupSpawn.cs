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
                    uint uid = GroupSpawnPacket.ReadUInt32();
                    Console.WriteLine(uid + " Dispawned");
                    if (Client.NearbyBuffAreas.ContainsKey(uid))
                        Client.NearbyBuffAreas.Remove(uid);
                    if (Client.NearbyCharacters.ContainsKey(uid))
                        Client.NearbyCharacters.Remove(uid);
                    if (Client.NearbyCOSs.ContainsKey(uid))
                    {
                        if(Client.Info.CharacterCOS.Contains(Client.NearbyCOSs[uid]))
                            Client.Info.CharacterCOS.Remove(Client.NearbyCOSs[uid]);
                        Client.NearbyCOSs.Remove(uid);
                    }
                    if (Client.NearbyItems.ContainsKey(uid))
                        Client.NearbyItems.Remove(uid);
                    if (Client.NearbyMobs.ContainsKey(uid))
                        Client.NearbyMobs.Remove(uid);
                    if (Client.NearbyNPCs.ContainsKey(uid))
                        Client.NearbyNPCs.Remove(uid);
                    if (Client.NearbyStructures.ContainsKey(uid))
                        Client.NearbyStructures.Remove(uid);
                }
            }
        }

        public static void GroupSpawnData(Packet packet)
        {
            GroupSpawnPacket.WriteUInt8Array(packet.GetBytes());
        }
    }
}
