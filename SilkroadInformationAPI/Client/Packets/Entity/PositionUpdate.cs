using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class PositionUpdate
    {

        public delegate void CharacterPositionUpdateHandler(CharacterPositionUpdateEventArgs e);
        /// <summary>
        /// This is called upon receiving 0xB021 Packet(Position update packet), this is not called for the main client.
        /// </summary>
        public static event CharacterPositionUpdateHandler OnCharacterPositionChange;

        /// <summary>
        /// This is called AFTER client changing position.
        /// </summary>
        public static event Action OnClientPositionUpdate;

        public static void Parse(Packet p)
        {
            bool onHorse = false;
            uint UniqueID = p.ReadUInt32();
            if (Client.NearbyCharacters.Where(x => x.Value.TransportUniqueID == UniqueID).Count() > 0)
            {
                onHorse = true;
                UniqueID = Client.NearbyCharacters.First(x => x.Value.TransportUniqueID == UniqueID).Key;
            }

            int moving = p.ReadInt8();
            if (moving == 1)
            {
                ushort RegionID = p.ReadUInt16();
                short xcoord = p.ReadInt16();
                short zcoord = p.ReadInt16();
                short ycoord = p.ReadInt16();

                if (Client.NearbyCharacters.ContainsKey(UniqueID))
                {
                    int oldX = Client.NearbyCharacters[UniqueID].Position.GetRealX();
                    int oldY = Client.NearbyCharacters[UniqueID].Position.GetRealY();

                    Client.NearbyCharacters[UniqueID].Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);
                    //Console.WriteLine("{ " + Actions.Mapping.GetCharNameFromUID(UniqueID) + " } [" + Client.NearbyCharacters[UniqueID].Position.GetRealX() + ", " +
                    //    Client.NearbyCharacters[UniqueID].Position.GetRealY() + "]");
                    OnCharacterPositionChange?.Invoke(new CharacterPositionUpdateEventArgs(oldX, oldY, Client.NearbyCharacters[UniqueID].Position.GetRealX(),
                        Client.NearbyCharacters[UniqueID].Position.GetRealY(), Client.NearbyCharacters[UniqueID], onHorse));

                }
                if (Client.NearbyCOSs.ContainsKey(UniqueID))
                    Client.NearbyCOSs[UniqueID].Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);
                if (Client.NearbyMobs.ContainsKey(UniqueID))
                    Client.NearbyMobs[UniqueID].Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);
                if (Client.NearbyNPCs.ContainsKey(UniqueID))
                    Client.NearbyNPCs[UniqueID].Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);
                if (Client.NearbyStructures.ContainsKey(UniqueID))
                    Client.NearbyStructures[UniqueID].Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);

                if (Client.Info.UniqueID == UniqueID || Client.Info.TransportUniqueID == UniqueID)
                {
                    Client.Position = new Information.BasicInfo.Position(RegionID, xcoord, ycoord, zcoord);
                    OnClientPositionUpdate?.Invoke();

                }
            } else
            {
                //Sky walking, spinning.
            }
        }
    }

    public class CharacterPositionUpdateEventArgs : EventArgs
    {
        public int oldX;
        public int oldY;

        public int newX;
        public int newY;

        public bool OnHorse = false;
        public Information.Objects.Character Character;

        public CharacterPositionUpdateEventArgs(int _oldX, int _oldY, int _newX, int _newY, Information.Objects.Character _char, bool horse)
        {
            oldX = _oldX;
            oldY = _oldY;
            newX = _newX;
            newY = _newY;
            Character = _char;
            OnHorse = horse;
        }
    }
}
