using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects.Character
{
    class CharacterItem
    {
        public uint ModelID;
        public uint PlusValue;
        public CharacterItem(uint id, uint opt)
        {
            ModelID = id;
            PlusValue = opt;
        }
    }
}
