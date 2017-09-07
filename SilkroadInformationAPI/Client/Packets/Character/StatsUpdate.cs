using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    public class StatsUpdate
    {
        public static event Action OnStatsUpdated;

        public static void Parse(Packet p)
        {
            Client.Info.MinimumPhysicalAttack = p.ReadUInt32();
            Client.Info.MaximumPhysicalAttack = p.ReadUInt32();
            Client.Info.MinimumMagicalAttack = p.ReadUInt32();
            Client.Info.MaximumMagicalAttack = p.ReadUInt32();
            Client.Info.PhysicalDefense = p.ReadUInt16();
            Client.Info.MagicalDefense = p.ReadUInt16();
            Client.Info.HitRate = p.ReadUInt16();
            Client.Info.ParryRate = p.ReadUInt16();
            Client.Info.MaxHP = p.ReadUInt32();
            Client.Info.MaxMP = p.ReadUInt32();
            Client.Info.STR = p.ReadUInt16();
            Client.Info.INT = p.ReadUInt16();

            if (Client.Info.CurrentHP > Client.Info.MaxHP)
                Client.Info.CurrentHP = Client.Info.MaxHP;
            if (Client.Info.CurrentMP > Client.Info.MaxMP)
                Client.Info.CurrentMP = Client.Info.MaxMP;

            OnStatsUpdated?.Invoke();
        }
    }
}
