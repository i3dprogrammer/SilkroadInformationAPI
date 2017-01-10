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

/// 
/// HUGE Thanks to DaxterSoul to his awesome documentation and pushedx for SilkroadSecurity.
/// And to everyone else I used their source, mostly are mentioed.
/// 

namespace SilkroadInformationAPI
{
    public class SroClient
    {

        public SroClient()
        {

        }

        /// <summary>
        /// Opens and reads Silkroad Media.pk2 to load Objects, Skills, Items, etc...
        /// </summary>
        /// <param name="MediaPath">The full Media.pk2 path.</param>
        /// <param name="blowfish">The encryption key used, leave blank for the normal 169841.</param>
        public SroClient(string MediaPath, string blowfish = "169841")
        {
            Media.LoadData.InitializeReader(MediaPath, blowfish);
        }

        /// <summary>
        /// Opens and reads Silkroad Media.pk2 to load Objects, Skills, Items, etc...
        /// </summary>
        /// <param name="MediaPath">The full Media.pk2 path.</param>
        /// <param name="blowfish">The encryption key used, leave blank for the normal 169841.</param>
        public void Initialize(string MediaPath, string blowfish = "169841")
        {
            Media.LoadData.InitializeReader(MediaPath, blowfish);
        }



        /// <summary>
        /// Loads media data (Objects, Items, Skills, Translation, etc..)
        /// </summary>
        public void LoadData()
        {
            if (!Media.LoadData.IsInitialized())
                throw new Exception("SroClient is not initialized yet!");
            else
            {
                Console.WriteLine("Loading translation!");
                Media.LoadData.LoadTranslation();
                Console.WriteLine("Loading models!");
                Media.LoadData.LoadCharacterData();
                Console.WriteLine("Loading Buildings/Portals");
                Media.LoadData.LoadTeleportBuildings();
                Console.WriteLine("Loading items!");
                Media.LoadData.LoadItems();
                Console.WriteLine("Loading skills!");
                Media.LoadData.LoadSkills();
                Console.WriteLine("Loading blue options.");
                Media.LoadData.LoadMagicOptions();
                Console.WriteLine("Loading NPC's");
                Media.LoadData.LoadRefShopGroup(); //Maps the Store Group Name to NPC Media Name
                Media.LoadData.LoadRefMappingShopGroup(); //Maps the Store Group Name to Store Name
                Console.WriteLine("Mapping package items to item data.");
                Media.LoadData.LoadRefScrapOfPackageItem(); //Maps the shop package name to item media name
                Console.WriteLine("Loading shop package items.");
                Media.LoadData.LoadRefShopGoods(); //Maps the shop package items to the store tab
                Console.WriteLine("Mapping shop tabs to groups.");
                Media.LoadData.LoadRefShopTabs(); //Maps the store tabs to store group
                Console.WriteLine("Mapping shop groups to stores.");
                Media.LoadData.LoadRefMappingShopWithTab(); //Maps the store group to Store shop
                Console.WriteLine("Loading shops.");
                Media.LoadData.LoadShops(); //Loads the Store
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
        public Dictionary<int, InventoryItem> GetInventoryItems()
        {
            return Client.Client.InventoryItems;
        }

        /// <summary>
        /// #DEBUG# Prints out the current inventory items in a nice way, note that it prints the count of the item only.
        /// </summary>
        public void PrintInventory()
        {
            if (Client.Client.InventoryItems.Count == 0)
                return;

            int current = 0;
            for (int i = 13; i < Client.Client.Info.MaxInventorySlots; i++, current++)
            {
                if (current == 4)
                {
                    Console.WriteLine();
                    current = 0;
                }

                if (Client.Client.InventoryItems.ContainsKey(i))
                    Console.Write(Client.Client.InventoryItems[i].Stack + ", ");
                else
                    Console.Write("-1, ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// #DEBUG# Print all stores data.
        /// </summary>
        public string PrintShops()
        {
            string data = "";
            foreach(var store in Media.Data.MediaShops)
            {
               data += (store.StoreName) + Environment.NewLine;
                foreach(var group in store.ShopGroups)
                {
                    data += ("\t" + group.GroupName) + Environment.NewLine;
                    foreach (var tab in group.GroupTabs)
                    {
                        data += ("\t\t" + tab.TabName) + Environment.NewLine;
                        foreach (var item in tab.TabItems)
                        {
                            data += ("\t\t\t" + item.ItemMediaName) + Environment.NewLine;
                        }
                    }
                }
            }

            Console.WriteLine(data);
            return data;
        }



        /// <summary>
        /// Returns the surrounding characters of the main account.
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Character</returns>
        public Dictionary<uint, Client.Information.Objects.Character> GetSurroundingCharacters()
        {
            return Client.Client.NearbyCharacters;
        }

        /// <summary>
        /// Returns the NPCs near the main character
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Base</returns>
        public Dictionary<uint, Client.Information.Objects.Base> GetNearbyNPCs()
        {
            return Client.Client.NearbyNPCs;
        }

        /// <summary>
        /// Returns the mobs near the main character.
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Mob</returns>
        public Dictionary<uint, Client.Information.Objects.Mob> GetSurroundingMobs()
        {
            return Client.Client.NearbyMobs;
        }

        /// <summary>
        /// Returns the structures near the main character (Portals, Dimension holes, FW Structs, etc..).
        /// Check for owner, if there exists it's a dimension hole,
        /// If it has no HP, it's a portal.
        /// TODO: Add identification filters.
        /// 
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Structure</returns>
        public Dictionary<uint, Client.Information.Objects.Structure> GetSurroundingStructures()
        {
            return Client.Client.NearbyStructures;
        }

        /// <summary>
        /// Returns nearby gold dropped on the ground.
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Item</returns>
        public Dictionary<uint, Client.Information.Objects.Item> GetDroppedGold()
        {
            var list = new Dictionary<uint, Client.Information.Objects.Item>();
            Client.Client.NearbyItems.ToList().ForEach(x =>
            {
                if (x.Value.Amount != 0)
                    list.Add(x.Key, x.Value);
            });
            return list;
        }

        /// <summary>
        /// Returns nearby items dropped on the ground.
        /// </summary>
        /// <returns>Returns a dictionary with the key being object unique ID and value of Client.Information.Objects.Item</returns>
        public Dictionary<uint, Client.Information.Objects.Item> GetDroppedItems()
        {
            var list = new Dictionary<uint, Client.Information.Objects.Item>();
            Client.Client.NearbyItems.ToList().ForEach(x =>
            {
                if (x.Value.Amount == 0)
                    list.Add(x.Key, x.Value);
            });
            return list;
        }
    }
}
