using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class BattleArenaResponseEventArgs : EventArgs
    {
        public ArenaResponse BattleArenaResponse;
        /// <summary>
        /// Available if ally team got the flag, or someone put the flag.
        /// </summary>
        public string AssociatedCharName;

        /// <summary>
        /// Ignore for now.
        /// </summary>
        public byte FlagPole;
    }
}
