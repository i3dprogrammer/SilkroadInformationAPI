using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media
{
    class Data
    {
        public static Dictionary<long, DataInfo.Object> MediaModels = new Dictionary<long, DataInfo.Object>();
        public static Dictionary<long, DataInfo.Item> MediaItems = new Dictionary<long, DataInfo.Item>();
        public static Dictionary<long, DataInfo.Skill> MediaSkills = new Dictionary<long, DataInfo.Skill>();
        public static Dictionary<int, string> MediaBlues = new Dictionary<int, string>();
        public static Dictionary<string, string> Translation = new Dictionary<string, string>();
    }
}
