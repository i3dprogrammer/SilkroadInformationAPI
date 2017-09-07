using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Quests
{
    public class Quest
    {
        public int QuestID;
        public int AchievementCount;
        public int RequiresAutoShareParty;
        public int Type;
        public int RemainingTime;
        public int Status;
        public int ObjectiveCount;
        public List<Objective> Objectives = new List<Objective>();
    }
}
