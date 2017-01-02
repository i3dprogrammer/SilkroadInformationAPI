using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client
{
    class Dispatcher
    {
        public static void Process(Packet p)
        {
            try
            {
                if (p.Opcode == 0x3013)
                {
                    Packets.CharacterData.Parse(p);
                }
                else if (p.Opcode == 0x3017)
                {
                    Packets.Spawn.GroupSpawn.GroupSpawnStart(p);
                }
                else if (p.Opcode == 0x3019)
                {                 
                    Packets.Spawn.GroupSpawn.GroupSpawnData(p);
                }
                else if (p.Opcode == 0x3018)
                {
                    Packets.Spawn.GroupSpawn.GroupSpawnEnd(p);
                }
                else if (p.Opcode == 0x3015)
                {
                    Packets.Spawn.SingleSpawn.Parse(p);
                }
                else if (p.Opcode == 0x3040)
                {
                    Packets.Inventory.ItemCountUpdatedDueAlchemy.Parse(p);
                }
                else if (p.Opcode == 0xB034)
                {
                    Packets.Inventory.InventoryItemsUpdated.Parse(p);
                }
                else if (p.Opcode == 0x3049)
                {
                    Packets.Inventory.StorageInfoResponse.Parse(p);
                }
                else if (p.Opcode == 0xB04C)
                {
                    Packets.Inventory.ItemCountUpdated.Parse(p);
                }
                else if (p.Opcode == 0x304E)
                {
                    Packets.Inventory.GoldUpdated.Parse(p);
                }
            } catch (Exception ex){
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
