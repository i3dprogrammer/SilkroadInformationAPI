using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class BattleArenaStateEventArgs : EventArgs 
    {
        public ArenaType BattleArenaType;
        public State BattleArenaState;

        public BattleArenaStateEventArgs(ArenaType _type, State _state)
        {
            BattleArenaType = _type;
            BattleArenaState = _state;
        }
    }
}
