using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Character
{
    class ParseData
    {   //Mostly thanks to DaxterSoul
        public static void Parse(Packet p)
        {
            Client.InventoryItems.Clear();

            Client.Info.RegionID = p.ReadInt32(); //Region ID
            Client.Info.ModelID = p.ReadInt32(); //Model ID
            p.ReadInt8(); //Volume
            Client.Info.Level = p.ReadInt8(); //Current level
            Client.Info.MaxGameLevel = p.ReadInt8(); //Max level
            Client.Info.CurrentExp = p.ReadUInt64(); //Current exp
            Client.Info.MaxEXP = Media.Data.MaxEXP[Client.Info.Level];
            p.ReadInt32(); //SP bar
            Client.Info.Gold = p.ReadUInt64(); //Gold
            Client.Info.SP = p.ReadInt32(); //Skill points
            Client.Info.StatPoints = p.ReadInt16(); //Stat points
            Client.Info.Zerk = (p.ReadInt8() == 5) ? true : false; //Berserk gauge
            p.ReadInt32(); //Zeroes
            Client.Info.CurrentHP = p.ReadUInt32(); //HP
            Client.Info.CurrentMP = p.ReadUInt32(); //MP
            p.ReadInt8(); //Beginner icon
            p.ReadInt8(); //Daily PK
            p.ReadInt16(); //Total PK
            p.ReadInt32(); //Total Penalty points
            p.ReadInt8(); //TitleLevel
            Client.Info.PVPCape = (FRPVPMode) p.ReadInt8(); //Pvp state
            Client.Info.MaxInventorySlots = p.ReadInt8(); //Max inventory slots
            //Inventory
            int currentInventoryItems = Client.Info.CurrentInvItemsCount = p.ReadInt8();
            for (int i = 0; i < currentInventoryItems; i++)
            {
                var item = Inventory.InventoryUtility.ParseItem(p);
                Client.InventoryItems.Add(item.Slot, item);
            }

            //AvatarInventory
            p.ReadInt8(); //Avatar inventory size
            int currentAvatarItems = p.ReadInt8();
            for (int i = 0; i < currentAvatarItems; i++)
            {
                var item = Inventory.InventoryUtility.ParseItem(p);
            }
            p.ReadInt8(); //UNK

            //Masteries
            int nextMastery = p.ReadInt8();
            while (nextMastery == 1)
            {
                var mastery = new Information.Spells.Mastery();
                mastery.ID = p.ReadInt32();
                mastery.Level = p.ReadInt8();
                Client.Masteries.Add(mastery);
                //Console.WriteLine(mastery.ID);
                nextMastery = p.ReadInt8();
            }
            p.ReadInt8(); //UNK

            //Skills
            int nextSkill = p.ReadInt8();
            while (nextSkill == 1)
            {
                var skill = new Information.Spells.Skill();
                skill.SkillID = p.ReadUInt32();
                skill.Enabled = p.ReadInt8();
                Client.Skills.Add(skill);
                //Console.WriteLine(skill.ID);
                nextSkill = p.ReadInt8();
            }

            //Quests    
            int completedQuestsCount = p.ReadUInt16();
            //Console.WriteLine(completedQuestsCount);
            for (int i = 0; i < completedQuestsCount; i++)
                p.ReadInt32();

            int activeQuestsCount = p.ReadInt8();
            for (int i = 0; i < activeQuestsCount; i++)
            {
                var quest = new Information.Quests.Quest();
                quest.QuestID = p.ReadInt32();
                quest.AchievementCount = p.ReadInt8();
                quest.RequiresAutoShareParty = p.ReadInt8();
                quest.Type = p.ReadInt8();
                if (quest.Type == 28)
                {
                    quest.RemainingTime = p.ReadInt32();
                }
                quest.Status = p.ReadInt8();

                if (quest.Type != 8)
                {
                    quest.ObjectiveCount = p.ReadInt8();
                    for (int j = 0; j < quest.ObjectiveCount; j++)
                    {
                        var objective = new Information.Quests.Objective();
                        objective.ID = p.ReadInt8();
                        objective.Status = p.ReadInt8(); //0 = Done, 1  = On
                        objective.Name = p.ReadAscii();
                        //Console.WriteLine(objective.Name);
                        objective.TaskCount = p.ReadInt8();
                        for (int taskIndex = 0; taskIndex < objective.TaskCount; taskIndex++)
                        {
                            objective.TaskValues.Add(p.ReadInt32());
                        }
                        quest.Objectives.Add(objective);
                    }
                }

                if (quest.Type == 88)
                {
                    int objCount = p.ReadInt8();
                    for (int j = 0; j < objCount; j++)
                    {
                        p.ReadInt32(); //NPCs Model ID
                    }
                }
                Client.Quests.Add(quest);
            }

            p.ReadInt8(); //unk

            int collections = p.ReadInt32();
            for (int i = 0; i < collections; i++)
            {
                p.ReadInt32();
                p.ReadInt32();
                p.ReadInt32();
            }

            Client.Info.UniqueID = p.ReadUInt32();

            //Position
            Client.Position.ReigonID = p.ReadUInt16();
            Client.Position.X = p.ReadSingle();
            Client.Position.Y = p.ReadSingle();
            Client.Position.Z = p.ReadSingle();
            Client.Position.Angle = p.ReadUInt16();

            //Movement
            Client.Movement.HasDestination = p.ReadUInt8();
            Client.Movement.Type = p.ReadUInt8();
            if (Client.Movement.HasDestination == 1)
            {
                Client.Movement.DestinationRegion = p.ReadUInt16();
                if (Client.Position.ReigonID < short.MaxValue)
                {   //World
                    Client.Movement.DestinationOffsetX = p.ReadUInt16();
                    Client.Movement.DestinationOffsetY = p.ReadUInt16();
                    Client.Movement.DestinationOffsetZ = p.ReadUInt16();
                }
                else
                {   //Dungeon
                    Client.Movement.DestinationOffsetX = p.ReadUInt32();
                    Client.Movement.DestinationOffsetY = p.ReadUInt32();
                    Client.Movement.DestinationOffsetZ = p.ReadUInt32();
                }
            }
            else
            {
                Client.Movement.Source = p.ReadUInt8();   //0 = Spinning, 1 = Sky-/Key-walking
                Client.Movement.Angle = p.ReadUInt16();   //Represents the new angle, character is looking at
            }

            //State
            Client.State.LifeState = (p.ReadUInt8() == 1); //1 = Alive, 2 = Dead
            p.ReadUInt8(); //unk
            Client.State.MotionState = p.ReadUInt8(); //0 = None, 2 = Walking, 3 = Running, 4 = Sitting
            Client.State.Status = p.ReadUInt8(); //0 = None, 1 = Hwan, 2 = Untouchable, 3 = GM Invincible, 5 = GM Invisible, 6 = Stealth, 7 = Invisible
            Client.State.WalkSpeed = p.ReadSingle();
            Client.State.RunSpeed = p.ReadSingle();
            Client.State.HwanSpeed = p.ReadSingle();
            Client.State.BuffCount = p.ReadUInt8();
            for (int i = 0; i < Client.State.BuffCount; i++)
            {
                int ID = p.ReadInt32(); //Skill ID
                p.ReadInt32(); //Duration
                if (Media.Data.MediaSkills[ID].Params == "1701213281") //TODO: Read skill params
                    p.ReadInt8(); //IsBuffCreator
            }

            Client.Info.CharacterName = p.ReadAscii();
            Client.Info.JobName = p.ReadAscii();
            Client.Info.JobType = p.ReadUInt8();
            Client.Info.JobLevel = p.ReadUInt8();
            Client.Info.JobExp = p.ReadUInt32();
            Client.Info.JobContribution = p.ReadUInt32();
            Client.Info.JobReward = p.ReadUInt32();
            Client.Info.PVPState = p.ReadUInt8(); //0 = White, 1 = Purple, 2 = Red
            Client.Info.TransportFlag = p.ReadUInt8();
            Client.Info.InCombat = p.ReadUInt8();
            if (Client.Info.TransportFlag == 1)
            {
                Client.Info.TransportUniqueID = p.ReadUInt32(); //Transport Unique ID
            }
            Client.Info.PVPFlag = p.ReadUInt8();
            Client.Info.GuideFlag = p.ReadUInt64();
            Client.Info.JID = p.ReadUInt32(); // ?
            Client.Info.GMFlag = p.ReadUInt8();

        }
    }
}
