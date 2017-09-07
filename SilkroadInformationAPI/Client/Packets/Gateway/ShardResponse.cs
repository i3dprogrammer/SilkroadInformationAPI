using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Gateway
{
    public class ShardResponse
    {
        public delegate void ShardResponseHandler(ShardResponseEventArgs ServerShards);
        public static event ShardResponseHandler OnGatewayShardResponse;

        public static void Parse(Packet p)
        {
            List<Information.Gateway.Shard> Shards = new List<Information.Gateway.Shard>();
            while (p.ReadUInt8() == 1)
            {
                p.ReadUInt8(); //Farm ID
                p.ReadAscii(); //Farm name
            }

            //Servers
            while (p.ReadUInt8() == 1)
            {
                ushort shardID = p.ReadUInt16();
                string shardName = p.ReadAscii();
                ushort OnlineCount = p.ReadUInt16();
                ushort Capacity = p.ReadUInt16();
                byte Status = p.ReadUInt8();
                p.ReadUInt8(); //Farm ID

                Shards.Add(new Information.Gateway.Shard(shardID, shardName, OnlineCount, Capacity, Status));
            }

            OnGatewayShardResponse?.Invoke(new Gateway.ShardResponseEventArgs(Shards));
        }
    }

    public class ShardResponseEventArgs : EventArgs
    {
        public List<Information.Gateway.Shard> ServerShards;

        public ShardResponseEventArgs(List<Information.Gateway.Shard> shards)
        {
            ServerShards = shards;
        }
    }
}
