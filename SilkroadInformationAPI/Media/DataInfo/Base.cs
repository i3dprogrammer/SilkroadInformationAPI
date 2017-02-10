using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Media.DataInfo
{
    public class Base
    {
        public uint ObjRefID;
        public string MediaName;
        public string TranslationName;
        public Types Classes;

        public Base()
        {
            Classes = new Types();
        }

    }

    public struct Types {
        public int A;
        public int B;
        public int C;
        public int D;
        public int E;
        public int F;
    }

}
