using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spawn
{
    class SingleSpawn
    {   //Using AGENT_ENTITY_SPAWN structure from DaxterSoul
        public static void Parse(Packet p)
        {
            var surrObject = new Information.Objects.Object();
            try
            {
                uint ObjectID = p.ReadUInt32();
                var obj = Media.Data.MediaModels[ObjectID];
                surrObject.ModelID = ObjectID;
                if (obj.Classes.C == 1)
                {
                    if (obj.Classes.D == 1)
                    {   //Character
                        surrObject.type = 0;
                        surrObject.Scale = p.ReadUInt8();
                        p.ReadUInt8(); //Title
                        surrObject.PVPCape = p.ReadUInt8();
                        p.ReadUInt8(); //Beginner Icon

                        //Inventory
                        p.ReadUInt8(); //Size
                        byte count = p.ReadUInt8();
                        for (int i = 0; i < count; i++)
                        {
                            uint ID = p.ReadUInt32();
                            if (Media.Data.MediaModels[ID].Classes.C == 3 && Media.Data.MediaModels[ID].Classes.D == 1)
                                surrObject.Inventory.Add(new Information.Objects.CharacterInfo.CharacterItem(ID, p.ReadUInt8()));
                        }

                        //AvatarInventory
                        p.ReadUInt8();
                        count = p.ReadUInt8();
                        for (int i = 0; i < count; i++)
                        {
                            uint ID = p.ReadUInt32();
                            if (Media.Data.MediaModels[ID].Classes.C == 3 && Media.Data.MediaModels[ID].Classes.D == 1)
                                surrObject.Inventory.Add(new Information.Objects.CharacterInfo.CharacterItem(ID, p.ReadUInt8()));
                        }

                        //Mask
                        count = p.ReadUInt8();

                        if (count == 1)
                        {
                            surrObject.WearingMask = true;
                            uint ID = p.ReadUInt32();
                            if (Media.Data.MediaModels[ID].Type == ModelType.Character)
                            {
                                p.ReadUInt8();
                                count = p.ReadUInt8();
                                for (int i = 0; i < count; i++)
                                    p.ReadUInt32();
                            }
                        }
                    }
                    else if (obj.Classes.D == 2 && obj.Classes.E == 5)
                    {   //NPC_FORTRESS_STRUCT
                        surrObject.type = 5;
                        surrObject.HP = p.ReadUInt32();
                        surrObject.EventStructID = p.ReadUInt32();
                        surrObject.StructureState = p.ReadUInt16();
                    }

                    surrObject.UniqueID = p.ReadUInt32(); //Unique ID

                    //Position
                    surrObject.Position.ReigonID = p.ReadUInt16();
                    surrObject.Position.X = p.ReadSingle();
                    surrObject.Position.Y = p.ReadSingle();
                    surrObject.Position.Z = p.ReadSingle();
                    surrObject.Position.Angle = p.ReadUInt16();

                    //Movement
                    surrObject.Movement.HasDestination = p.ReadUInt8();
                    surrObject.Movement.Type = p.ReadUInt8();
                    if (surrObject.Movement.HasDestination == 1)
                    {
                        p.ReadUInt16();
                        if (surrObject.Position.ReigonID < short.MaxValue)
                        {   //World
                            surrObject.Movement.DestinationOffsetX = p.ReadUInt16();
                            surrObject.Movement.DestinationOffsetY = p.ReadUInt16();
                            surrObject.Movement.DestinationOffsetZ = p.ReadUInt16();
                        }
                        else
                        {   //Dungeon
                            surrObject.Movement.DestinationOffsetX = p.ReadUInt32(); // Probably 16bit a mistake.
                            surrObject.Movement.DestinationOffsetY = p.ReadUInt32();
                            surrObject.Movement.DestinationOffsetZ = p.ReadUInt32();
                        }
                    }
                    else
                    {
                        surrObject.Movement.Source = p.ReadUInt8();   //0 = Spinning, 1 = Sky-/Key-walking
                        surrObject.Movement.Angle = p.ReadUInt16();   //Represents the new angle, character is looking at
                    }

                    //State
                    surrObject.State.LifeState = (p.ReadUInt8() == 1); //1 = Alive, 2 = Dead
                    p.ReadUInt8(); //unk
                    surrObject.State.MotionState = p.ReadUInt8(); //0 = None, 2 = Walking, 3 = Running, 4 = Sitting
                    surrObject.State.Status = p.ReadUInt8(); //0 = None, 1 = Hwan, 2 = Untouchable, 3 = GM Invincible, 5 = GM Invisible, 6 = Stealth, 7 = Invisible
                    surrObject.State.WalkSpeed = p.ReadSingle();
                    surrObject.State.RunSpeed = p.ReadSingle();
                    surrObject.State.HwanSpeed = p.ReadSingle();
                    surrObject.State.BuffCount = p.ReadUInt8();
                    for (int i = 0; i < surrObject.State.BuffCount; i++)
                    {
                        uint ID = p.ReadUInt32(); //Skill ID
                        uint Duration = p.ReadUInt32(); //Duration
                        if (Media.Data.MediaSkills[ID].Params == "1701213281") //TODO: Read skill params
                            surrObject.Buffs.Add(new Information.Spells.Skill(ID, Duration, p.ReadUInt8())); //IsBuffCreator
                    }

                    if (obj.Classes.D == 1)
                    {
                        //Character
                        surrObject.Name = p.ReadAscii();
                        surrObject.JobType = p.ReadUInt8();
                        surrObject.JobLevel = p.ReadUInt8();
                        surrObject.PVPState = p.ReadUInt8();
                        surrObject.TransportFlag = p.ReadUInt8();
                        surrObject.InCombat = p.ReadUInt8();
                        if (surrObject.TransportFlag == 1)
                            surrObject.TransportUniqueID = p.ReadUInt32();

                        surrObject.UsingScroll = p.ReadUInt8();
                        surrObject.InteractMode = p.ReadUInt8();
                        p.ReadUInt8();

                        //Guild
                        surrObject.Guild.Name = p.ReadAscii();

                        if (surrObject.CharacterInJobSuit() == false)
                        {
                            surrObject.Guild.ID = p.ReadUInt32();
                            surrObject.Guild.Nickname = p.ReadAscii();
                            p.ReadUInt32(); //Last Crest Rev
                            surrObject.Guild.UnionID = p.ReadUInt32();
                            p.ReadUInt32(); //Last Crest Rev
                            surrObject.Guild.IsFriendly = p.ReadUInt8();
                            p.ReadUInt8(); //??
                        }

                        if (surrObject.InteractMode == 4)
                        {
                            surrObject.Stall.StallCreated = true;
                            surrObject.Stall.StallName = p.ReadAscii();
                            surrObject.Stall.DecorationModelID = p.ReadUInt32();
                        }

                        surrObject.PVPEquipCooldown = p.ReadUInt8();
                        p.ReadUInt8();

                    }
                    else if (obj.Classes.D == 2)
                    {
                        //NPC
                        surrObject.TalkableNPC = p.ReadUInt8();
                        if (surrObject.TalkableNPC == 2)
                        {
                            int optionsCount = p.ReadUInt8();
                            for (int i = 0; i < optionsCount; i++)
                                p.ReadUInt8();
                        }

                        if (obj.Classes.E == 1)
                        {   //NPC_MOB
                            surrObject.type = 2;
                            surrObject.Rarity = p.ReadUInt8();
                            if (obj.Classes.F == 2 || obj.Classes.F == 3)
                            {
                                surrObject.Appearance = p.ReadUInt8();
                            }
                        }
                        else if (obj.Classes.E == 2)
                        {
                            surrObject.type = 1;
                        }
                        else if (obj.Classes.E == 3)
                        {   //NPC_COS
                            if (obj.Classes.F == 3 || obj.Classes.F == 4)
                            {   //Attackpet/Pickup
                                surrObject.Name = p.ReadAscii();
                                surrObject.type = 4;
                            }

                            if (obj.Classes.F == 5)
                            {
                                surrObject.PetGuildName = p.ReadAscii();
                            }
                            else
                            {
                                surrObject.Owner = p.ReadAscii();
                            }

                            if (obj.Classes.F == 2 ||
                                obj.Classes.F == 3 ||
                                obj.Classes.F == 4 ||
                                obj.Classes.F == 5)
                            {
                                p.ReadUInt8();
                                if (obj.Classes.F != 4)
                                {
                                    p.ReadUInt8();
                                }
                                if (obj.Classes.F == 5)
                                {
                                    p.ReadUInt32();
                                }
                            }
                            surrObject.COSType = (COS_Type)obj.Classes.F;
                            surrObject.OwnerID = p.ReadUInt32(); //Owner ID
                        }
                        else if (obj.Classes.E == 4)
                        {   //ITEM FORTRESS COS
                            surrObject.type = 4;
                            surrObject.COSType = COS_Type.Fortress;
                            surrObject.Guild.ID = p.ReadUInt32();
                            surrObject.Guild.Name = p.ReadAscii();
                        }
                    }

                }
                else if (obj.Classes.C == 3)
                {   //ITEM
                    surrObject.type = 3;
                    if (obj.Classes.D == 1)
                    {   //ITEM EQUIP
                        surrObject.OptValue = p.ReadUInt8();
                    }
                    else if (obj.Classes.D == 3)
                    {
                        if (obj.Classes.E == 5 && obj.Classes.F == 0) //ITEM_GOLD
                            surrObject.Amount = p.ReadUInt32();
                        else if (obj.Classes.E == 8 || obj.Classes.E == 9) //ITEM TRADE/QUEST
                        {
                            surrObject.Owner = p.ReadAscii();
                        }
                    }

                    surrObject.UniqueID = p.ReadUInt32(); //Unique ID

                    //Position
                    surrObject.Position.ReigonID = p.ReadUInt16();
                    surrObject.Position.X = p.ReadSingle();
                    surrObject.Position.Y = p.ReadSingle();
                    surrObject.Position.Z = p.ReadSingle();
                    surrObject.Position.Angle = p.ReadUInt16();

                    byte hasOwner = p.ReadUInt8();
                    if (hasOwner == 1)
                        surrObject.OwnerID = p.ReadUInt32();
                    surrObject.Rarity = p.ReadUInt8();
                }
                else if (obj.Classes.C == 4)
                {   //PORTALS
                    surrObject.type = 5;
                    surrObject.UniqueID = p.ReadUInt32(); //Unique ID

                    //Position
                    surrObject.Position.ReigonID = p.ReadUInt16();
                    surrObject.Position.X = p.ReadSingle();
                    surrObject.Position.Y = p.ReadSingle();
                    surrObject.Position.Z = p.ReadSingle();
                    surrObject.Position.Angle = p.ReadUInt16();

                    byte unkByte0 = p.ReadUInt8();
                    byte unkByte1 = p.ReadUInt8();
                    byte unkByte2 = p.ReadUInt8();
                    byte unkByte3 = p.ReadUInt8();

                    if (unkByte3 == 1)
                    {
                        p.ReadUInt32();
                        p.ReadUInt32();
                    }
                    else if (unkByte3 == 6)
                    {
                        surrObject.Owner = p.ReadAscii();
                        surrObject.OwnerID = p.ReadUInt32();
                    }

                    if (unkByte1 == 1)
                    {
                        p.ReadUInt32();
                        p.ReadUInt8();
                    }

                }
                else if (ObjectID == uint.MaxValue)
                {   //EVENT_ZONE (Traps, Buffzones, ...)
                    surrObject.type = 6;
                    p.ReadUInt16();
                    surrObject.SkillID = p.ReadUInt32();

                    surrObject.UniqueID = p.ReadUInt32(); //Unique ID

                    //Position
                    surrObject.Position.ReigonID = p.ReadUInt16();
                    surrObject.Position.X = p.ReadSingle();
                    surrObject.Position.Y = p.ReadSingle();
                    surrObject.Position.Z = p.ReadSingle();
                    surrObject.Position.Angle = p.ReadUInt16();
                }

                if (p.Opcode == 0x3015)
                {
                    if (obj.Classes.C == 1 || obj.Classes.C == 4)
                        p.ReadUInt8();
                    else if (obj.Classes.C == 3)
                    {
                        surrObject.DropSource = p.ReadUInt8();
                        surrObject.DropperUniqueID = p.ReadUInt32();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("#MAIN" + ex.Message + Environment.NewLine + ex.StackTrace);
                DEBUGSingleSpawn.Parse(new Debug.DebugPacket(p));
                Utility.LogPacket(p);

                //Console.WriteLine("ENTITY_SINGLE_SPAWN ERROR LOGGING to " + Environment.CurrentDirectory + "\\" + "PacketLog.txt");
                //Utility.LogPacket(p);
                //Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            if(surrObject.UniqueID != 0)
                ProcessObject(surrObject);
        }

        public static void ProcessObject(Information.Objects.Object obj)
        {
            if(obj.type == 0) //Character
            {
                var character = new Information.Objects.Character();
                character.AvatarInventory = obj.AvatarInventory;
                character.Buffs = obj.Buffs;
                character.Guild = obj.Guild;
                character.Inventory = obj.Inventory;
                character.JobLevel = obj.JobLevel;
                character.JobType = (JOB_Type)obj.JobType;
                character.ModelID = obj.ModelID;
                character.Name = obj.Name;
                character.OnTransport = (obj.TransportFlag == 1);
                character.PKState = (PK_State)obj.PVPState;
                character.Position = obj.Position;
                character.PVPCape = (FRPVPMode)obj.PVPCape;
                character.PVPEquipCooldown = obj.PVPEquipCooldown;
                character.Stall = obj.Stall;
                character.State = obj.State;
                character.TransportUniqueID = obj.TransportUniqueID;
                character.UniqueID = obj.UniqueID;
                character.UsingScroll = obj.UsingScroll;
                character.WearingMask = obj.WearingMask;
                Client.NearbyCharacters.Add(character.UniqueID, character);
            } else if(obj.type == 1) //NPC
            {
                var NPC = new Information.Objects.Base();
                NPC.ModelID = obj.ModelID;
                NPC.Position = obj.Position;
                NPC.UniqueID = obj.UniqueID;
                Client.NearbyNPCs.Add(NPC.UniqueID, NPC);
            } else if(obj.type == 2) //MOB
            {
                var mob = new Information.Objects.Mob();
                mob.Appearance = obj.Appearance;
                mob.ModelID = obj.ModelID;
                mob.Position = obj.Position;
                mob.Rarity = obj.Rarity;
                mob.UniqueID = obj.UniqueID;
                Client.NearbyMobs.Add(mob.UniqueID, mob);
            } else if(obj.type == 3) //Item
            {
                var item = new Information.Objects.Item();
                item.Amount = obj.Amount;
                item.DropperUniqueID = obj.DropperUniqueID;
                item.DropSource = obj.DropSource;
                item.ModelID = obj.ModelID;
                item.OwnerID = obj.OwnerID;
                item.OwnerName = obj.Owner;
                item.PlusValue = obj.OptValue;
                item.Position = obj.Position;
                item.Rarity = obj.Rarity;
                item.UniqueID = obj.UniqueID;
                Client.NearbyItems.Add(item.UniqueID, item);
            } else if(obj.type == 4) //COS
            {
                var cos = new Information.Objects.COS();
                cos.COSGuildName = obj.PetGuildName;
                cos.COSName = obj.Name;
                cos.FortressCOSGuildID = obj.Guild.ID;
                cos.FortressCOSGuildName = obj.Guild.Name;
                cos.ModelID = obj.ModelID;
                cos.OwnerName = obj.Owner;
                cos.OwnerUniqueID = obj.OwnerID;
                cos.Position = obj.Position;
                cos.Type = obj.COSType;
                cos.UniqueID = obj.UniqueID;
                Client.NearbyCOSs.Add(cos.UniqueID, cos);

                if (cos.OwnerUniqueID == Client.Info.UniqueID)
                    Client.Info.CharacterCOS.Add(cos.UniqueID, cos);
            } else if(obj.type == 5) //Struct
            {
                var structure = new Information.Objects.Structure();
                structure.HP = obj.HP;
                structure.ModelID = obj.ModelID;
                structure.OwnerName = obj.Owner;
                structure.OwnerUniqueID = obj.OwnerID;
                structure.Position = obj.Position;
                structure.RefEventStructID = obj.EventStructID;
                structure.State = obj.StructureState;
                structure.UniqueID = obj.UniqueID;
                Client.NearbyStructures.Add(structure.UniqueID, structure);
            } else if(obj.type == 6) //Event area
            {
                var area = new Information.Objects.BuffArea();
                area.Position = obj.Position;
                area.UniqueID = obj.UniqueID;
                area.ModelID = obj.ModelID;
                area.ReferenceSkillID = obj.SkillID;
                Client.NearbyBuffAreas.Add(area.UniqueID, area);
            }
        }


    }
}
