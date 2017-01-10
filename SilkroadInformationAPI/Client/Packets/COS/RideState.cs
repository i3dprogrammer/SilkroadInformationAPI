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
                uint CharUniqueID = p.ReadUInt32(); //Character unique ID associated with the COS Ride State Change
                byte RideState = p.ReadUInt8();
                uint COSUniqueID = p.ReadUInt32();
                if(RideState == 1)
                {
                    Client.NearbyCharacters[CharUniqueID].OnTransport = true;
                    Client.NearbyCharacters[CharUniqueID].TransportUniqueID = COSUniqueID;
                } else
                {
                    Client.NearbyCharacters[CharUniqueID].OnTransport = false;
                    Client.NearbyCharacters[CharUniqueID].TransportUniqueID = 0;
                }
            }
        }
    }
}
