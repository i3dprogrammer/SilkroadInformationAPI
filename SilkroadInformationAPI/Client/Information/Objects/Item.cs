using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class Item : Base
    {

        /// <summary>
        /// The plus value of the dropped item, this is only available if the item type is enchantable.
        /// </summary>
        public byte PlusValue { get; set; } = 0;

        /// <summary>
        /// If the item has an owner, you cannot pick it for while. If not the OwnerID is 0xFFFF? (Need to be tested)
        /// </summary>
        public uint OwnerID { get; set; } = 0xFFFF;

        /// <summary>
        /// The unique id of the type who dropped the item.
        /// </summary>
        public uint DropperUniqueID { get; set; }

        /// <summary>
        /// This is the source that dropped the item *Used as integers for now*.
        /// </summary>
        public byte DropSource { get; set; }

        /// <summary>
        /// This is only available if the dropped item is either a Quest or a Trade.
        /// </summary>
        public string OwnerName { get; set; }


        /// <summary>
        /// Item rarity state (SOX, Normal, etc..)
        /// </summary>
        public byte Rarity { get; set; }

        /// <summary>
        /// Amount of gold dropped, this will have a value in case of the item is gold only.
        /// </summary>
        public uint Amount { get; set; } = 0;
    }
}
