using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.COS
{
    public class COSUpdate
    {
        public static event Action<Information.Objects.COS> OnCOSHGPUpdate;
        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();
            byte changeFlag = p.ReadUInt8();
            if(changeFlag == 0x01) //Termination
            {   //WE DON'T NEED TO DESPAWN HERE, SINCE THE 3016 SINGLE DESPAWN PACKET COMES FIRST
                //if (Client.NearbyCOSs.ContainsKey(uid))
                //{
                //    Client.NearbyCOSs.Remove(uid);

                //    if (Client.Info.CharacterCOS.ContainsKey(uid))
                //        Client.Info.CharacterCOS.Remove(uid);
                //}
            } else if(changeFlag == 0x04) //HGP Update??
            {
                //TODO: ADD HGP
                OnCOSHGPUpdate?.Invoke(Client.NearbyCOSs[uid]);
            }
            else if(changeFlag == 0x05) //Name changed
            {
                string newName = p.ReadAscii();
                if (Client.NearbyCOSs.ContainsKey(uid))
                {
                    Client.NearbyCOSs[uid].COSName = newName;
                }

            }
        }
    }
}
