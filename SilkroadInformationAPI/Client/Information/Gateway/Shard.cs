using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Gateway
{
    public class Shard
    {
        public ushort ID;
        public string Name;
        public ushort OnlineCount;
        public ushort Capacity;
        public bool ServerStatus;

        public Shard(ushort _id, string _name, ushort _count, ushort _capacity, byte status)
        {
            ID = _id;
            Name = _name;
            OnlineCount = _count;
            Capacity = _capacity;
            ServerStatus = (status == 0);
        }
    }
}
