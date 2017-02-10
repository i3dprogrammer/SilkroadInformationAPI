using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class Character : Base
    {
        /// <summary>
        /// Character name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// PVP Cape Color
        /// </summary>
        public FRPVPMode PVPCape { get; set; } = FRPVPMode.None;

        /// <summary>
        /// PK State of the character
        /// </summary>
        public PK_State PKState { get; set; } = PK_State.Neutral;

        /// <summary>
        /// True if the character is on COS, false otherwise.
        /// </summary>
        public bool OnTransport { get; set; } = false;

        /// <summary>
        /// Unique ID of Transport only if OnTransport = true.
        /// </summary>
        public uint TransportUniqueID { get; set; }

        /// <summary>
        /// Scroll type If the character is channeling a scroll (0 = None, 1 = Return Scroll, 2 = Bandit Return Scroll).
        /// </summary>
        public byte UsingScroll { get; set; }

        /// <summary>
        /// Character job type.
        /// </summary>
        public JOB_Type JobType { get; set; } = JOB_Type.None;

        /// <summary>
        /// Character job level
        /// </summary>
        public byte JobLevel { get; set; }

        /// <summary>
        /// Character stall information.
        /// </summary>
        public Stalls.Stall Stall = new Stalls.Stall();

        /// <summary>
        /// Character is wearing a mask (rouge/warlock skills).
        /// </summary>
        public bool WearingMask { get; set; } = false;

        /// <summary>
        /// Character guild information.
        /// </summary>
        public Guilds.Guild Guild = new Guilds.Guild();

        /// <summary>
        /// The items character is wearing.
        /// </summary>
        public List<CharacterInfo.CharacterItem> Inventory = new List<CharacterInfo.CharacterItem>();

        /// <summary>
        /// The avatar items character is wearing.
        /// </summary>
        public List<CharacterInfo.CharacterItem> AvatarInventory = new List<CharacterInfo.CharacterItem>();

        /// <summary>
        /// Character state information
        /// </summary>
        public BasicInfo.State State = new BasicInfo.State();

        /// <summary>
        /// Character is in zerk state.
        /// </summary>
        public bool ZerkOn { get; set; } = false;

        /// <summary>
        /// PVP Cape Timeleft for wearing.
        /// </summary>
        public byte PVPEquipCooldown { get; set; }

        public int X; //TODO: Real X
        public int Y; //TODO: Real Y

        /// <summary>
        /// If there's a bad effect on the character stun, sleep, etc..
        /// </summary>
        public bool BadStatus { get; set; } = false;

        /// <summary>
        /// Determines if the character is wearing job suit or no *Checks via ItemTypes*.
        /// </summary>
        /// <returns>True if the character is wearing a suit, no otherwise.</returns>
        public bool CharacterInJobSuit()
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                var item = Media.Data.MediaModels[Inventory[i].ModelID];
                if (item.Classes.C == 3 && item.Classes.D == 1 && item.Classes.E == 7 && item.Classes.F != 5)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
