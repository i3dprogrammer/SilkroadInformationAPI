using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    public class EXP_SP_Update
    {
        /// <summary>
        /// This gets called when the client gains exp.
        /// <para>The returned object represents the amount of exp gained, you can still get the current total EXP from Client.Info.CurrentExp</para>
        /// </summary>
        public static event Action<ulong> OnExpGained;

        public delegate void ClientLevelUpHandler(ClientLevelUpEventArgs e);
        /// <summary>
        /// This is called when the client levels up.
        /// </summary>
        public static event ClientLevelUpHandler OnClientLevelUp;

        public static void Parse(Packet p)
        {
            uint source_uid = p.ReadUInt32();
            ulong exp = p.ReadUInt64();
            ulong SP = p.ReadUInt64(); //TODO: Configure SP
            p.ReadUInt8(); //??
            try {
                
                if (Client.Info.CurrentExp + exp >= Client.Info.MaxEXP) //If current exp + gained exp is > max "current level" exp, if so then the character gained one or more levels.
                {
                    int NewLevel = (p.ReadUInt16() / 3) + 1;
                    OnClientLevelUp?.Invoke(new ClientLevelUpEventArgs(NewLevel, Client.Info.Level));
                    Client.Info.CurrentExp = (Client.Info.CurrentExp + exp) - Media.Data.MaxEXP[NewLevel - 1];
                    Client.Info.Level = NewLevel;
                    Client.Info.MaxEXP = Media.Data.MaxEXP[NewLevel];
                }
            } catch { }
            
        }
    }

    public class ClientLevelUpEventArgs : EventArgs
    {
        public int NewLevel;
        public int OldLevel;

        public ClientLevelUpEventArgs(int _newLevel, int _oldLevel)
        {
            NewLevel = _newLevel;
            OldLevel = _oldLevel;
        }
    }
}
