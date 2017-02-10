using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class HPMPUpdate
    {
        public static event Action OnClientBadStatus;
        public static event Action OnClientBadStatusRemoved;
        public static event Action OnClientHPUpdate;
        public static event Action OnClientMPUpdate;
        public static event Action OnClientHPMPUpdate;

        public static event Action<Information.Objects.COS> OnCOSHPUpdate;
        public static event Action<Information.Objects.COS> OnCOSBadStatus;
        public static event Action<Information.Objects.COS> OnCOSBadStatusRemoved;

        public static event Action<Information.Objects.Character> OnCharacterBadStatus;
        public static event Action<Information.Objects.Character> OnCharacterBadStatusRemoved;

        public static event Action<Information.Objects.Mob> OnMobHPUpdate;
        public static event Action<Information.Objects.Mob> OnMobBadStatus;
        public static event Action<Information.Objects.Mob> OnMobBadStatusRemoved;

        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            p.ReadUInt16(); //UNK
            if (Client.Info.UniqueID == uid)
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x01: //HP update
                        Client.Info.CurrentHP = p.ReadUInt32();
                        OnClientHPUpdate?.Invoke();
                        break;
                    case 0x02: //MP update
                        Client.Info.CurrentMP = p.ReadUInt32();
                        OnClientMPUpdate?.Invoke();
                        break;
                    case 0x03: //HP&MP update
                        Client.Info.CurrentHP = p.ReadUInt32();
                        Client.Info.CurrentMP = p.ReadUInt32();
                        OnClientHPMPUpdate?.Invoke();
                        break;
                    case 0x04: //Bad status update
                        Client.Info.BadStatusID = p.ReadUInt32();
                        if (Client.Info.BadStatusID == 0)
                        {
                            Client.Info.BadStatus = false;
                            OnClientBadStatusRemoved?.Invoke();
                        }
                        else
                        {
                            Client.Info.BadStatus = true;
                            OnClientBadStatus?.Invoke();
                        }
                        break;
                }
            } else if (Client.NearbyCharacters.ContainsKey(uid))
            {
                byte flag = p.ReadUInt8(); //TODO: Check for other flag values.
                switch (flag)
                {
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                        {
                            Client.NearbyCharacters[uid].BadStatus = false;
                            OnCharacterBadStatus?.Invoke(Client.NearbyCharacters[uid]);
                        }
                        else
                        {
                            Client.NearbyCharacters[uid].BadStatus = true;
                            OnCharacterBadStatusRemoved?.Invoke(Client.NearbyCharacters[uid]);
                        }
                        break;
                }
            }
            else if (Client.NearbyMobs.ContainsKey(uid))
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x05: //HP
                        Client.NearbyMobs[uid].CurrentHP = p.ReadUInt32();
                        OnMobHPUpdate?.Invoke(Client.NearbyMobs[uid]);
                        break;
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                        {
                            Client.NearbyMobs[uid].BadStatus = false;
                            OnMobBadStatus?.Invoke(Client.NearbyMobs[uid]);
                        }
                        else
                        {
                            Client.NearbyMobs[uid].BadStatus = true;
                            OnMobBadStatusRemoved?.Invoke(Client.NearbyMobs[uid]);
                        }
                        break;
                }
            }
            else if (Client.NearbyCOSs.ContainsKey(uid))
            {
                byte flag = p.ReadUInt8();
                switch (flag)
                {
                    case 0x05: //HP
                        Client.NearbyCOSs[uid].CurrentHP = p.ReadUInt32();
                        OnCOSHPUpdate?.Invoke(Client.NearbyCOSs[uid]);
                        break;
                    case 0x04: //Bad status update
                        if (p.ReadUInt32() == 0)
                        {
                            Client.NearbyCOSs[uid].BadStatus = false;
                            OnCOSBadStatus?.Invoke(Client.NearbyCOSs[uid]);
                        }
                        else
                        {
                            Client.NearbyCOSs[uid].BadStatus = true;
                            OnCOSBadStatusRemoved?.Invoke(Client.NearbyCOSs[uid]);
                        }
                        break;
                }
            }
        }
    }
}
