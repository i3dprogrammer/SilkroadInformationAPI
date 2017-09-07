using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.BattleArena
{
    public class Operation
    {
        public delegate void BattleArenaStateHandler(BattleArenaStateEventArgs e);
        /// <summary>
        /// This is called when arena ends, starts, registration ends, 10minutes left, 5minutes left, 1minute left.
        /// </summary>
        public static event BattleArenaStateHandler OnBattleArenaStateChange;

        public delegate void BattleArenaResultHandler(BattleArenaResultEventArgs e);
        /// <summary>
        /// This is only called if the client is in arena and it ends.
        /// </summary>
        public static event BattleArenaResultHandler OnBattleArenaResult;

        public delegate void BattleArenaScoreHandler(BattleArenaScoreEventArgs e);
        /// <summary>
        /// This is called when the score changes in a match.
        /// </summary>
        public static event BattleArenaScoreHandler OnBattleArenaScoreChange;

        public delegate void BattleArenaTimeHandler(BattleArenaTimeEventArgs e);
        /// <summary>
        /// This is called when the score changes in a match.
        /// </summary>
        public static event BattleArenaTimeHandler OnBattleArenaTimeChange;

        public delegate void BattleArenaResponseHandler(BattleArenaResponseEventArgs e);

        /// <summary>
        /// This is called upon SilkroadInformationAPI.ArenaResponse values.
        /// </summary>
        public static event BattleArenaResponseHandler OnBattleArenaReceiveResponse;

        public static void Parse(Packet p)
        {
            byte flag = p.ReadUInt8(); //Operation Flag

            if (flag == 0x02 || flag == 0x0D || flag == 0x0E) //0x02 = 10minutes left, 0x0D = 5minutes left, 0x0E = 1minute left
            {
                byte Type = p.ReadUInt8(); //Arena type [nRegister values]
                if(flag == 0x02)
                    OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.TenMinutesLeftToStartArena));
                if (flag == 0x0D)
                    OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.FiveMinutesLeftToStartArena));
                if (flag == 0x0E)
                    OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.OneMinuteLeftToStartArena));
            }
            else if (flag == 0x05) //Arena ended
            {
                byte Type = p.ReadUInt8(); //Arena type [nRegister values]
                OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.ArenaEnded));
            }
            else if (flag == 0x04) //Arena started
            {
                byte Type = p.ReadUInt8(); //Arena type [nRegister values]
                OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.ArenaStarted));
            }
            else if (flag == 0x03) //Registration closed
            {
                byte Type = p.ReadUInt8(); //Arena type [nRegister values]
                OnBattleArenaStateChange?.Invoke(new BattleArenaStateEventArgs((ArenaType)Type, ArenaState.RegistrationClosed));
            }
            else if (flag == 0xFF) //Registration response
            {
                byte result = p.ReadUInt8();
                BattleArenaResponseEventArgs args = new BattleArenaResponseEventArgs();
                try
                {
                    args.BattleArenaResponse = (ArenaResponse)result;
                }
                catch
                {
                    args.BattleArenaResponse = ArenaResponse.Unknown;
                }

                if (result == 0x80) //Team got flag
                    args.AssociatedCharName = p.ReadAscii();
                else if (result == 0x84) //Someone put flag
                {
                    args.FlagPole = p.ReadUInt8(); //Team flagpole or enemy flagpole [0x00 == Our flagpole[red], 0x01 == Their flagpole[blue]]
                    args.AssociatedCharName = p.ReadAscii(); //character name of the person whom put the flag.
                }

                if (result == 0xF0) //Time update
                {
                    uint totalTime = p.ReadUInt32(); //in milliseconds
                    uint timeElapsed = p.ReadUInt32(); //in milliseconds
                    OnBattleArenaTimeChange?.Invoke(new BattleArenaTimeEventArgs(totalTime, timeElapsed));
                }
                else if (result == 0x88) //Update score
                {
                    uint TeamScore = p.ReadUInt32(); //Team score
                    uint EnemyScore = p.ReadUInt32(); //Enemy score
                    OnBattleArenaScoreChange?.Invoke(new BattleArenaScoreEventArgs(TeamScore, EnemyScore));
                } else
                {
                    OnBattleArenaReceiveResponse?.Invoke(args);
                }

                //TODO: Anaylze more scenario's
            }
            else if (flag == 8) //TODO
            {
                
            }
            else if (flag == 9) //Arena result
            {
                byte type = p.ReadUInt8();
                byte result = p.ReadUInt8();
                byte coins = p.ReadUInt8();
                OnBattleArenaResult?.Invoke(new BattleArenaResultEventArgs(coins, ((result == 0) ? ArenaResult.Won : (result == 2) ? ArenaResult.Lost : ArenaResult.Draw), p.ReadUInt32()));
            }
        }
    }
}
