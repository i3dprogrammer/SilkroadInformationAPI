using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK2Reader;
using PK2Reader.Framework;
using System.IO;
using SilkroadSecurityApi;
using SilkroadInformationAPI.Client.Information;
using SilkroadInformationAPI.Client.Packets.Inventory;
namespace SilkroadInformationAPI
{
    public class SroClient
    {
        private Reader reader;

        public SroClient()
        {

        }


        /// <summary>
        /// Opens and reads Silkroad Media.pk2 to load Objects, Skills, Items, etc...
        /// </summary>
        /// <param name="MediaPath">The full Media.pk2 path.</param>
        /// <param name="blowfish">The encryption key used, leave blank for the normal 169841.</param>
        public void Initialize(string MediaPath, string blowfish = "169841")
        {
            if (!File.Exists(MediaPath))
            {
                throw new FileNotFoundException("Couldn't find the media path, please check it.");
            }

            try
            {
                reader = new Reader(MediaPath, blowfish);
            }
            catch
            {
                throw new Exception("Error during loading the media, please check the pk2 blowfish.");
            }
        }

        /// <summary>
        /// Loads media data (Objects, Items, Skills, Translation, etc..)
        /// </summary>
        public void LoadData()
        {
            if (reader == null)
                throw new Exception("SroClient is not initialized yet!");
            else
            {
                Console.WriteLine("Loading translation!");
                Media.LoadData.LoadTranslation(reader);
                Console.WriteLine("Loading models!");
                Media.LoadData.LoadCharacterData(reader);
                Console.WriteLine("Loading Buildings/Portals");
                Media.LoadData.LoadTeleportBuildings(reader);
                Console.WriteLine("Loading items!");
                Media.LoadData.LoadItems(reader);
                Console.WriteLine("Loading skills!");
                Media.LoadData.LoadSkills(reader);
                Console.WriteLine("Loading blue options");
                Media.LoadData.LoadMagicOptions(reader);

            }

            Console.WriteLine("Finished!");
        }

        /// <summary>
        /// Routes all the packets through the packet dispatcher, all game packets should pass through here.
        /// </summary>
        /// <param name="p">The packet to dispatch.</param>
        public void Route(Packet p)
        {
            Client.Dispatcher.Process(p);
        }


        /// <summary>
        /// Returns a list of all inventory items.
        /// </summary>
        /// <returns>A dictionary of Key int of the item slot and Value of SilkroadInformationAPI.Client.Information.Item> </returns>
        public Dictionary<int, Item> GetInventoryItems()
        {
            return Client.Client.InventoryItems;
        }

        /// <summary>
        /// Prints out the current inventory items in a nice way, note that it prints the count of the item only.
        /// </summary>
        public void PrintInventory()
        {
            if (Client.Client.InventoryItems.Count == 0)
                return;

            int current = 0;
            for (int i = 13; i <= Client.Client.BasicInfo.MaxInventorySlots; i++, current++)
            {
                if (current == 4)
                {
                    Console.WriteLine();
                    current = 0;
                }

                if (Client.Client.InventoryItems.ContainsKey(i))
                    Console.Write(Client.Client.InventoryItems[i].Count + ", ");
                else
                    Console.Write("-1, ");
            }
            Console.WriteLine();
        }
    }
}
