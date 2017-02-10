using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;


namespace SilkroadInformationAPI.Client.Packets.COS
{
    class RideState
    {
        public static void Parse(Packet p)
        {
            int success = p.ReadInt8();
            if(success == 1)
            {
                uint charUID = p.ReadUInt32(); //Character unique ID associated with the COS Ride State Change
                byte RideState = p.ReadUInt8();
                uint cosUID = p.ReadUInt32();
                if (Client.NearbyCharacters.ContainsKey(charUID))
                {
                    if (RideState == 1)
                    {
                        Client.NearbyCharacters[charUID].OnTransport = true;
                        Client.NearbyCharacters[charUID].TransportUniqueID = cosUID;
                    }
                    else
                    {
                        Client.NearbyCharacters[charUID].OnTransport = false;
                        Client.NearbyCharacters[charUID].TransportUniqueID = 0;
                    }
                } else if (Client.Info.UniqueID == charUID)
                {
                    Client.Info.TransportFlag = RideState;
                    if (RideState == 0)
                        Client.Info.TransportUniqueID = 0;
                    else
                        Client.Info.TransportUniqueID = cosUID;
                }
            }

        }
    }
}
