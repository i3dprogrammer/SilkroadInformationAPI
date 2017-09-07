using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class BattleArenaScoreEventArgs : EventArgs
    {
        public uint TeamScore;
        public uint EnemyScore;

        public BattleArenaScoreEventArgs(uint team, uint enemy)
        {
            TeamScore = team;
            EnemyScore = enemy;
        }
    }
}
