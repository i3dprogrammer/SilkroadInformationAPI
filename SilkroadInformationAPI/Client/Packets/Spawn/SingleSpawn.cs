using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spawn
{
    class SingleSpawn
    {   //Using AGENT_ENTITY_SPAWN packet from DaxterSoul
        public static void Parse(Packet p)
        {
            bool job = false;
            uint ObjectID = p.ReadUInt32();
            var obj = Media.Data.MediaModels[ObjectID];
            var surrObject = new Information.Objects.Object();
            if(obj.Classes.C == 1)
            {
                if(obj.Classes.D == 1)
                {   //Character
                    surrObject.Scale = p.ReadUInt8();
                    p.ReadUInt8(); //HwanLevel
                    surrObject.PVPCape = p.ReadUInt8();
                    p.ReadUInt8();

                    //Inventory
                    p.ReadUInt8();
                    byte count = p.ReadUInt8();
                    for(int i = 0; i < count; i++)
                    {
                        uint ID = p.ReadUInt32();
                        Console.WriteLine(ID);
                        if (Media.Data.MediaModels[ID].Type == ModelType.PlusItem)
                            surrObject.Inventory.Add(new Information.Objects.Character.CharacterItem(ID, p.ReadUInt8()));
                    }

                    //AvatarInventory
                    p.ReadUInt8();
                    count = p.ReadUInt8();
                    for(int i = 0; i < count; i++)
                    {
                        uint ID = p.ReadUInt32();
                        if (Media.Data.MediaModels[ID].Type == ModelType.PlusItem)
                            surrObject.AvatarInventory.Add(new Information.Objects.Character.CharacterItem(ID, p.ReadUInt8()));
                    }

                    //Mask
                    count = p.ReadUInt8();

                    if(count == 1)
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
                } else if (obj.Classes.D == 2 && obj.Classes.E == 5)
                {   //NPC_FORTRESS_STRUCT
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
                        surrObject.Movement.DestinationOffsetX = p.ReadUInt32();
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
                surrObject.State.LifeState = p.ReadUInt8(); //1 = Alive, 2 = Dead
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
                        surrObject.Buffs.Add(new Information.Objects.Character.Buff(ID, Duration, p.ReadUInt8())); //IsBuffCreator
                }

                if(obj.Classes.D == 1)
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

                    if(job == false)
                    {
                        surrObject.Guild.ID = p.ReadUInt32();
                        surrObject.Guild.Nickname = p.ReadAscii();
                        p.ReadUInt32(); //Last Crest Rev
                        surrObject.Guild.UnionID = p.ReadUInt32();
                        p.ReadUInt32(); //Last Crest Rev
                        p.ReadUInt8(); //IsFriendly 0 = Hostile, 1 = Friendly
                        p.ReadUInt8(); //??
                    }

                    if(surrObject.InteractMode == 4)
                    {
                        surrObject.Stall.isStall = true;
                        surrObject.Stall.StallName = p.ReadAscii();
                        surrObject.Stall.DecorationModelID = p.ReadUInt32();
                    }

                    surrObject.PVPEquipCooldown = p.ReadUInt8();
                    p.ReadUInt8();

                } else if(obj.Classes.D == 2)
                {
                    //NPC
                    surrObject.TalkableNPC = p.ReadUInt8();
                    if(surrObject.TalkableNPC == 2)
                    {
                        int optionsCount = p.ReadUInt8();
                        for (int i = 0; i < optionsCount; i++)
                            p.ReadUInt8();
                    }

                    if(obj.Classes.E == 2)
                    {   //NPC_MOB
                        surrObject.Rarity = p.ReadUInt8();
                        if(obj.Classes.F == 2 || obj.Classes.F == 3)
                        {
                            surrObject.Appearance = p.ReadUInt8();
                        }
                    } else if(obj.Classes.E == 3)
                    {   //NPC_COS
                        if(obj.Classes.F == 3 || obj.Classes.F == 4)
                        {   //Pickup/Attackpet
                            surrObject.Name = p.ReadAscii();
                        }

                        if (obj.Classes.F == 5)
                        {
                            surrObject.PetGuildName = p.ReadAscii();
                        } else
                        {  
                            surrObject.Owner = p.ReadAscii();
                        }

                        if(obj.Classes.F == 2 ||
                            obj.Classes.F == 3 || 
                            obj.Classes.F == 4 || 
                            obj.Classes.F == 5)
                        {
                            p.ReadUInt8();
                            if(obj.Classes.F != 4)
                            {
                                p.ReadUInt8();
                            }
                            if(obj.Classes.F == 5)
                            {
                                p.ReadUInt32();
                            }
                        }
                        surrObject.OwnerID = p.ReadUInt32(); //Owner ID
                    } else if (obj.Classes.E == 4)
                    {
                        surrObject.Guild.ID = p.ReadUInt32();
                        surrObject.Guild.Name = p.ReadAscii();
                    }
                }

            } else if(obj.Classes.C == 3)
            {   //ITEM
                if(obj.Classes.D == 1)
                {
                    surrObject.OptValue = p.ReadUInt8();
                } else if(obj.Classes.D == 3)
                {
                    if (obj.Classes.E == 5 && obj.Classes.F == 0)
                        surrObject.Amount = p.ReadUInt32();
                    else if(obj.Classes.E == 8 || obj.Classes.E == 9)
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
            } else if(obj.Classes.C == 4)
            {
                //PORTALS

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

                if(unkByte3 == 1)
                {
                    p.ReadUInt32();
                    p.ReadUInt32();
                } else if(unkByte3 == 6)
                {
                    surrObject.Owner = p.ReadAscii();
                    surrObject.OwnerID = p.ReadUInt32();
                }

                if(unkByte1 == 1)
                {
                    p.ReadUInt32();
                    p.ReadUInt8();
                }

            } else if(ObjectID == uint.MaxValue)
            {   //EVENT_ZONE (Traps, Buffzones, ...)
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

            if(p.Opcode == 0x3015)
            {
                if (obj.Classes.C == 1 || obj.Classes.C == 4)
                    p.ReadUInt8();
                else if(obj.Classes.C == 3)
                {
                    surrObject.DropSource = p.ReadUInt8();
                    surrObject.DropperUniqueID = p.ReadUInt32();
                }
            }

            Console.WriteLine(surrObject.Name);
        }
    }
}
