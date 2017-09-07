using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Packets.Alchemy
{
    public class FuseResultEventArgs : EventArgs
    {
        /// <summary>
        /// The state of the fuse, whether it succeded or failed.
        /// </summary>
        public bool EnchantSuccess;

        /// <summary>
        /// The item associated with the fuse.
        /// </summary>
        public Information.InventoryItem AssociatedItem;

        public FuseResultEventArgs(bool success, Information.InventoryItem item)
        {
            EnchantSuccess = success;
            AssociatedItem = item;
        }
    }
}
