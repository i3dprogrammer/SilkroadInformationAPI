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
        public static event Action OnMobDies;
        public static void Parse(Packet p) //TODO: Add events
        {
            uint uid = p.ReadUInt32();
            byte flag1 = p.ReadUInt8();
            if (flag1 == 0x00) //Object died
            {
                if (Client.NearbyMobs.ContainsKey(uid) && p.ReadUInt8() == 2)
                    OnMobDies?.Invoke();
                else if (Client.Info.UniqueID == uid)
                    Client.State.LifeState = (p.ReadUInt8() == 1);
            } else if(flag1 == 0x01) //Motion state change
            {
                if (Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.MotionState = (CharMotionState)p.ReadUInt8();
                } else if(Client.Info.UniqueID == uid)
                {
                    Client.State.MotionState = (CharMotionState)p.ReadUInt8();
                }
            } else if(flag1 == 0x04) //Status change
            {
                if (Client.NearbyCharacters.ContainsKey(uid))
                {
                    Client.NearbyCharacters[uid].State.Status = (CharStatus)p.ReadUInt8();
                    if(Client.NearbyCharacters[uid].State.Status == CharStatus.Hwan)
                        p.ReadUInt8(); //Zerk level
                } else if(Client.Info.UniqueID == uid)
                {
                    Client.State.Status = (CharStatus)p.ReadUInt8();
                }
            }
        }
    }
}
