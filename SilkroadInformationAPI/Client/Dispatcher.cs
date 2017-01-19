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
                #region CHARACTER
                if (p.Opcode == 0x3013)
                {
                    Packets.Character.CharInfo.CharInfoData(p);
                }
                else if(p.Opcode == 0x34A5)
                {
                    Packets.Character.CharInfo.CharInfoStart();
                }
                else if (p.Opcode == 0x34A6)
                {
                    Packets.Character.CharInfo.CharInfoEnd();
                }
                else if (p.Opcode == 0x304E)
                {
                    Packets.Character.InfoUpdate.Parse(p);
                } 
                else if (p.Opcode == 0x3056)
                {
                    Packets.Character.EXP_SP_Update.Parse(p);
                }
                #endregion

                #region SPAWN
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
                } else if (p.Opcode == 0x3016)
                {
                    Packets.Spawn.Despawn.Parse(p);
                }
                #endregion

                #region INVENTORY
                else if (p.Opcode == 0x3040)
                {
                    Packets.Inventory.ItemCountUpdatedDueAlchemy.Parse(p);
                }
                else if (p.Opcode == 0xB034)
                {
                    Packets.Inventory.InventoryOperation.Parse(p);
                }
                else if (p.Opcode == 0xB04C)
                {
                    Packets.Inventory.ItemCountUpdated.Parse(p);
                }
                #endregion

                #region STORAGE
                else if (p.Opcode == 0x3049)
                {
                    Packets.Inventory.StorageInfoResponse.StorageInfoData(p);
                }
                else if (p.Opcode == 0x3047)
                {
                    Packets.Inventory.StorageInfoResponse.StorageInfoStart();
                }
                else if (p.Opcode == 0x3048)
                {
                    Packets.Inventory.StorageInfoResponse.StorageInfoEnd();
                }
                #endregion

                #region COS
                else if (p.Opcode == 0xB0CB)
                {
                    Packets.COS.RideState.Parse(p);
                }
                else if (p.Opcode == 0x30C8)
                {
                    Packets.COS.COSData.Parse(p);
                }
                #endregion

                else if (p.Opcode == 0x3026)
                {
                    Packets.Chat.ChatUpdated.Parse(p);
                }

                #region PARTY
                else if (p.Opcode == 0xB06C)
                {
                    Packets.Party.PartyMatchingResponse.Parse(p);
                } 
                else if (p.Opcode == 0x3065)
                {
                    Packets.Party.EnteredParty.Parse(p);
                }
                else if (p.Opcode == 0x3864)
                {
                    Packets.Party.PartyUpdate.Parse(p);
                }
                #endregion

                #region ENTITY
                else if (p.Opcode == 0xB021)
                {
                    Packets.Entity.PositionUpdate.Parse(p);
                }
                else if (p.Opcode == 0x30BF)
                {
                    Packets.Entity.StateChange.Parse(p);
                }
                else if (p.Opcode == 0x30D0)
                {
                    Packets.Entity.SpeedUpdate.Parse(p);
                } 
                else if(p.Opcode == 0x3054)
                {
                    Packets.Entity.LevelUpAnimation.Parse(p);
                }
                else if(p.Opcode == 0x3057)
                {
                    Packets.Entity.HPMPUpdate.Parse(p);
                }
                else if(p.Opcode == 0xB516)
                {
                    Packets.Entity.PVPUpdate.Parse(p);
                }
                #endregion

                #region STALL
                else if (p.Opcode == 0xB0B3)
                {
                    Packets.Stall.Entered.Parse(p);
                }
                else if(p.Opcode == 0xB0B5)
                {
                    Packets.Stall.Leave.Parse();
                }
                else if(p.Opcode == 0x30B7)
                {
                    Packets.Stall.Action.Parse(p);
                }
                else if (p.Opcode == 0x30B8)
                {
                    Packets.Stall.Created.Parse(p);
                }
                else if (p.Opcode == 0x30B9)
                {
                    Packets.Stall.Closed.Parse(p);
                }
                else if (p.Opcode == 0xB0BA)
                {
                    Packets.Stall.Updated.Parse(p);
                }
                else if (p.Opcode == 0x30BB)
                {
                    Packets.Stall.NameUpdated.Parse(p);
                }
                #endregion
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
