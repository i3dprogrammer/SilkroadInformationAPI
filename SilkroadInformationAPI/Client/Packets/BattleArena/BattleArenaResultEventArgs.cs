using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class BattleArenaResultEventArgs : EventArgs
    {
        public byte CountEarned;
        public ArenaResult ArenaResult;
        public uint EarnedRefItemID;

        public BattleArenaResultEventArgs(byte count, ArenaResult res, uint itemID)
        {
            CountEarned = count;
            ArenaResult = res;
            EarnedRefItemID = itemID;
        }
    }
}
