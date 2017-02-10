using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK2Reader;
using PK2Reader.Framework;
using System.IO;
using System.Threading;
using SilkroadSecurityApi;
using SilkroadInformationAPI.Client.Information;
using SilkroadInformationAPI.Client.Network;
using SilkroadInformationAPI.Client.Packets.Inventory;
using System.Diagnostics;

/// 
/// HUGE Thanks to DaxterSoul to his awesome documentation and pushedx for SilkroadSecurity.
/// And to everyone else I used their source, mostly are mentioned.
/// 

namespace SilkroadInformationAPI
{
    public class SroClient
    {

        public static Security RemoteSecurity;
        public static Security LocalSecurity;

        private static ClientlessConnection con_Clientless = new ClientlessConnection();
        private static string GamePath = "";

        public SroClient()
        {
            
        }

        /// <summary>
        /// Opens and reads Silkroad Media.pk2 to load Objects, Skills, Items, etc...
        /// </summary>
        /// <param name="Path">The path of the game folder.</param>
        /// <param name="blowfish">The encryption key used, leave blank for the normal 169841.</param>
        public SroClient(string Path, string blowfish = "169841")
        {
            GamePath = Path;
            if (File.Exists(GamePath + "\\sro_client.exe") && File.Exists(GamePath + "\\Media.pk2"))
                Media.LoadData.InitializeReader(Path, blowfish);
            else
                throw new FileNotFoundException("Couldn't find all required files in that folder!", "sro_client.exe, Media.pk2");
        }

        /// <summary>
        /// Opens and reads Silkroad Media.pk2 to load Objects, Skills, Items, etc...
        /// </summary>
        /// <param name="Path">The path of the game folder.</param>
        /// <param name="blowfish">The encryption key used, leave blank for the normal 169841.</param>
        public void Initialize(string Path, string blowfish = "169841")
        {
            GamePath = Path;
            if (File.Exists(GamePath + "\\sro_client.exe") && File.Exists(GamePath + "\\Media.pk2"))
                Media.LoadData.InitializeReader(Path, blowfish);
            else
                throw new FileNotFoundException("Couldn't find all required files in that folder!", "sro_client.exe, Media.pk2");
        }

        public Process StartClient(ushort port)
        {
            var loader = new Client.Loader.Loader();
            return loader.StartClient(GamePath, port, Media.Data.ServerInfo.Locale);
        }

        /// <summary>
        /// This waits for client to load with the redirected hosts, then starts a connection to the server and acts as a proxy.
        /// <para>The last optional parameter specifies whether the connection switches to clientless automatically upon client disconnection.</para>
        /// </summary>
        /// <param name="remote_ip">Silkroad login server IP</param>
        /// <param name="local_port">The local port of the redirected client.</param>
        public void StartProxyConnection(string LoginServerIP, ushort local_port, bool AutomaticClientless = true)
        {
            Client.Network.ProxyClient.AutoSwitchToClientless = AutomaticClientless;
            //TODO: Should check if the IP is not direct. (i.e. sro.gameserver.com)
            var thread = new Thread(() => Client.Network.ProxyClient.StartProxy(LoginServerIP, Media.Data.ServerInfo.Port, "127.0.0.1", local_port));
            thread.Start();
        }


        //TODO: REMOVE USELESS CONFIGURATION!

        /// <summary>
        /// Connect clientlessly to the login server.
        /// <para>NOTE: You MUST Configure the clientless before starting the connection, and after receving the LoginResponse</para>
        /// </summary>
        /// <param name="LoginServerIP">Silkroad login server IP</param>
        public void StartClientlessConnection(string LoginServerIP, ushort ServerPort)
        {
            con_Clientless.TerminateConnection();
            con_Clientless.Start(LoginServerIP, ServerPort);
        }

        /// <summary>
        /// Call this twice, once before starting the connection. And once when you receive the session id succesfully
        /// <para>Because they are used to instaniate the agent server.</para>
        /// </summary>
        /// <param name="_SessionID">Received from Packets.Gateway.LoginResponse, YOU CAN LEAVE IT 0 AT FIRST TIME.</param>
        /// <param name="_Username">User input</param>
        /// <param name="_Password">User input</param>
        /// <param name="_Locale">Media.Data.ServerInfo.Locale</param>
        /// <param name="_GameVersion">Media.Data.ServerInfo.Version</param>
        public void ConfigureClientlessSettings(uint _SessionID, string _Username, string _Password, byte _Locale, uint _GameVersion)
        {
            con_Clientless.cl_SessionID = _SessionID;
            con_Clientless.cl_Username = _Username;
            con_Clientless.cl_Password = _Password;
            con_Clientless.cl_Locale = _Locale;
            con_Clientless.cl_GameVersion = _GameVersion;
        }


        /// <summary>
        /// Closes the connection socket.
        /// </summary>
        private void TerminateClientlessConnection()
        {
            con_Clientless.TerminateConnection();
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
                Console.WriteLine("Loading login servers!");
                Media.LoadData.LoadDivisonInfo();
                Console.WriteLine("Loading login server port");
                Media.LoadData.LoadGateport();
                Console.WriteLine("Loading server version.");
                Media.LoadData.LoadServerVersion();
                Console.WriteLine("Loading zone names!");
                Media.LoadData.LoadTextZoneNames();
                Console.WriteLine("Loading region info.");
                Media.LoadData.LoadRefRegion();
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
                //Media.LoadData.LoadRefShop(); //Loads the Store
                Media.LoadData.LoadRefShopGroup(); //Maps the Store Group Name to NPC Media Name
                Media.LoadData.LoadLevelData(); //Loads maximum exp
            }

            Console.WriteLine("Finished!");
        }



        /// <summary>
        /// Routes all the packets through the packet dispatcher, all game packets should pass through here.
        /// <para>THIS should only be used if you're using a custom conenction, not the ones provided with the API(Client.Network).</para>
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
        public void PrintInventoryCount()
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

        public static void UseItem(int slot)
        {
            var p = new Packet(0x704C, true);
            p.WriteInt8(slot);
            p.WriteInt16(Client.Actions.Utility.GenerateItemType(Client.Client.InventoryItems[slot].ObjRefID));
            RemoteSecurity?.Send(p);
        }

        public static void UseItem(int slot, uint TargetUID)
        {
            var p = new Packet(0x704C, true);
            p.WriteInt8(slot);
            p.WriteInt16(Client.Actions.Utility.GenerateItemType(Client.Client.InventoryItems[slot].ObjRefID));
            p.WriteUInt32(TargetUID);
            RemoteSecurity?.Send(p);
        }

        public static void UseItem(int slot, byte OtherItemSlot)
        {
            var p = new Packet(0x704C, true);
            p.WriteInt8(slot);
            p.WriteInt16(Client.Actions.Utility.GenerateItemType(Client.Client.InventoryItems[slot].ObjRefID));
            p.WriteUInt8(OtherItemSlot);
            RemoteSecurity?.Send(p);
        }

        public static void UseSpellOnTarget(uint spellRefID, uint targetUID)
        {
            var p = new Packet(0x7074);
            p.WriteUInt8(0x01);
            p.WriteUInt8(0x04);
            p.WriteUInt32(spellRefID);
            p.WriteUInt8(0x01);
            p.WriteUInt32(targetUID);
            RemoteSecurity?.Send(p);
        }

        public static void UseSpell(uint spellRefID)
        {
            var p = new Packet(0x7074);
            p.WriteUInt8(0x01);
            p.WriteUInt8(0x04);
            p.WriteUInt32(spellRefID);
            p.WriteUInt8(0x00);
            RemoteSecurity?.Send(p);
        }

        public static void AutoAttack(uint targetUID)
        {
            var p = new Packet(0x7074);
            p.WriteUInt8(0x01);
            p.WriteUInt8(0x01);
            p.WriteUInt8(0x01);
            p.WriteUInt32(targetUID);
            RemoteSecurity?.Send(p);
        }

        public static void WalkTo(int X, int Y)
        {
            Client.Actions.Utility.WalkTo(X, Y);
        }
    }
}
