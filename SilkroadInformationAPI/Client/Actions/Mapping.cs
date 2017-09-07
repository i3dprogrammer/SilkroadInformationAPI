using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Actions
{
    class Mapping
    {
        public static string GetCharNameFromUID(uint UniqueID)
        {
            if(UniqueID != 0 && Client.NearbyCharacters.ContainsKey(UniqueID))
            {
                return Client.NearbyCharacters[UniqueID].Name;
            }

            return "";
        }


    }
}
