using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class BattleArenaTimeEventArgs : EventArgs
    {
        /// <summary>
        /// Arena total time in milliseconds.
        /// </summary>
        public uint TotalTime;

        /// <summary>
        /// Time elapsed since arena started in milliseconds.
        /// </summary>
        public uint TimeElapsed;

        public BattleArenaTimeEventArgs(uint total, uint elapsed)
        {
            TotalTime = total;
            TimeElapsed = elapsed;
        }
    }
}
