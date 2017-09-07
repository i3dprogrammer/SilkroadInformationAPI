using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class StateChange
    {
        public static event Action<uint> OnMobDie;
        public static event Action<uint> OnCharacterLifeStateChange;
        public static event Action OnClientLifeStateChange;
        public static event Action<uint> OnCharacterMotionStateChange;
        public static event Action OnClientMotionStateChange;
        public static event Action<uint> OnCharacterStatusChange;
        public static event Action OnClientStatusChange;
        public static event Action<uint> OnCharacterReturnStateChange;
        public static event Action OnClientReturnStateChange;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            byte flag1 = p.ReadUInt8();
            if (flag1 == 0x00) //Object died
            {
                byte idr = p.ReadUInt8();
                if (Client.NearbyMobs.ContainsKey(uid) && idr == 2)
                    OnMobDie?.Invoke(uid);
                else if (Client.Info.UniqueID == uid)
                {
                    Client.State.LifeState = (idr == 1);
                    OnClientLifeStateChange?.Invoke();
                } else if(Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.LifeState = (idr == 1);
                    OnCharacterLifeStateChange?.Invoke(uid);
                }
            } else if(flag1 == 0x01) //Motion state change
            {
                if (Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.MotionState = (CharMotionState)p.ReadUInt8();
                    OnCharacterMotionStateChange?.Invoke(uid);
                } else if(Client.Info.UniqueID == uid)
                {
                    Client.State.MotionState = (CharMotionState)p.ReadUInt8();
                    OnClientMotionStateChange?.Invoke();
                }
            } else if(flag1 == 0x04) //Status change
            {
                if (Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.Status = (CharStatus)p.ReadUInt8();
                    if(Client.NearbyCharacters[uid].State.Status == CharStatus.Hwan)
                        p.ReadUInt8(); //Zerk level
                    OnCharacterStatusChange?.Invoke(uid);
                } else if(Client.Info.UniqueID == uid)
                {
                    Client.State.Status = (CharStatus)p.ReadUInt8();
                    OnClientStatusChange?.Invoke();
                }
            } else if(flag1 == 0x0B)
            {
                bool returning = (p.ReadUInt8() == 1);
                if(Client.Info.UniqueID == uid)
                {
                    Client.State.Returning = returning;
                    OnClientReturnStateChange?.Invoke();
                } else if(Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.Returning = returning;
                    OnCharacterReturnStateChange?.Invoke(uid);
                }
            }
        }
    }
}
