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
        /// Summon type (Pickup pet, Fortress pet, Guild pet..)
        /// </summary>
        public COS_Type Type = COS_Type.None;
    }
}
