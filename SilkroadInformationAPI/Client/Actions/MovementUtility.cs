using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Actions
{
    class MovementUtility
    {
        public static int CalculatePositionX(int xSector, float X)
        {
            return (int)((xSector - 135) * 192 + X / 10);
        }
        public static int CalculatePositionY(int ySector, float Y)
        {
            return (int)((ySector - 92) * 192 + Y / 10);
        }
    }
}
