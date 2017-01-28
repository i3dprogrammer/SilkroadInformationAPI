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
                #region CHARACTER SELECTION
                if(p.Opcode == 0xB001)
                {
                    Packets.CharacterSelection.CharacterJoinResponse.Parse(p);
                } else if(p.Opcode == 0xB007)
                {
                    Packets.CharacterSelection.CharacterListResponse.Parse(p);
                }
                #endregion

                #region CHARACTER
                else if (p.Opcode == 0x3013)
                {
                    Packets.Character.CharacterData.CharData(p);
                }
                else if(p.Opcode == 0x34A5)
                {
                    Packets.Character.CharacterData.CharDataStart();
                }
                else if (p.Opcode == 0x34A6)
                {
                    Packets.Character.CharacterData.CharDatEnd();
                }
                else if (p.Opcode == 0x304E)
                {
                    Packets.Character.InfoUpdate.Parse(p);
                } 
                else if (p.Opcode == 0x3056)
                {
                    Packets.Character.ExpSpUpdate.Parse(p);
                } else if (p.Opcode == 0x303D)
                {
                    Packets.Character.StatsUpdate.Parse(p);
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
                } else if (p.Opcode == 0x3052)
                {
                    Packets.Inventory.UpdateItemDurability.Parse(p);
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
                else if(p.Opcode == 0xB069)
                {
                    Packets.Party.PartyMatchingEntryFormed.Parse(p);
                }
                else if(p.Opcode == 0xB06B)
                {
                    Packets.Party.PartyMatchingEntryDeleted.Parse(p);
                }
                else if(p.Opcode == 0x706D)
                {
                    Packets.Party.PartyMatchingRequestJoin.Parse(p);
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
                else if(p.Opcode == 0x3020)
                {
                    Packets.Entity.CelestialPosition.Parse(p);
                }
                else if(p.Opcode == 0x3038)
                {
                    Packets.Entity.ItemEquip.Parse(p);
                } else if(p.Opcode == 0x3039)
                {
                    Packets.Entity.ItemUnequip.Parse(p);
                } else if(p.Opcode == 0xB045)
                {
                    Packets.Entity.EntitySelected.Parse(p);
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
                else if (p.Opcode == 0xB0B1)
                {
                    //TODO: Client opened stall
                } else if(p.Opcode == 0xB0B2)
                {
                    //TODO: Client closed stall
                }
                #endregion

                #region GATEWAY
                else if (p.Opcode == 0xA101)
                {
                    Packets.Gateway.ShardResponse.Parse(p);
                } else if(p.Opcode == 0xA100)
                {
                    Packets.Gateway.PatchResponse.Parse(p);
                } else if(p.Opcode == 0xA102) //NEED TO FIX SHIT
                {
                    Packets.Gateway.LoginResponse.Parse(p);
                } else if(p.Opcode == 0xA323)
                {
                    Packets.Gateway.CaptchaCodeResponse.Parse(p);
                }
                #endregion
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
