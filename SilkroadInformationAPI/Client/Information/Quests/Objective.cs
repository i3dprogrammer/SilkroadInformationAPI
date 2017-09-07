using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Quests
{
    public class Objective
    {
        public int ID;
        public int Status;
        public string Name;
        public int TaskCount;
        public List<int> TaskValues = new List<int>();
    }
}
