using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class COS : Base
    {

        /// <summary>
        /// Pet name only available in case of Pickup pet, Attack pet
        /// </summary>
        public string COSName { get; set; }

        /// <summary>
        /// Guild name only available in case of Guild COS, Fortress COS
        /// </summary>
        public string COSGuildName { get; set; }


        /// <summary>
        /// Guild ID, only available in case of Fortrses COS
        /// </summary>
        public uint FortressCOSGuildID { get; set; }

        /// <summary>
        /// Guild Name, only available in case of Fortrses COS
        /// </summary>
        public string FortressCOSGuildName { get; set; }

        /// <summary>
        /// Owner name in case of Pickup COS, Attack COS
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// The unique ID of the owner
        /// </summary>
        public uint OwnerUniqueID { get; set; }

        /// <summary>
        /// Maximum inventory slots, in case of Job/Pickup pet.
        /// </summary>
        public byte MaxInventorySlots { get; set; }

        /// <summary>
        /// Current PET HP, this is not always available
        /// </summary>
        public uint CurrentHP { get; set; }

        /// <summary>
        /// Whether or not the COS has bad status effect.
        /// </summary>
        public bool BadStatus { get; set; }

        /// <summary>
        /// COS Max Hp
        /// </summary>
        public uint MaxHP { get; set; }

        public ushort MaxHGP { get; set; } = 10000;
        public ushort CurrentHGP { get; set; }

        public Dictionary<int, InventoryItem> Inventory = new Dictionary<int, InventoryItem>();

        /// <summary>
        /// Summon type (Pickup pet, Fortress pet, Guild pet..)
        /// </summary>
        public COS_Type Type = COS_Type.None;
    }
}
