using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
using SilkroadInformationAPI.Media;

namespace SilkroadInformationAPI.Client.Packets.Spawn
{
    class Solo
    {
        public static void Parse(Packet p)
        {
            Client.InventoryItems.Clear();

            Client.BasicInfo.RegionID = p.ReadInt32(); //Region ID
            Client.BasicInfo.ModelID = p.ReadInt32(); //Model ID
            p.ReadInt8(); //Volume
            Client.BasicInfo.Level = p.ReadInt8(); //Current level
            Client.BasicInfo.MaxGameLevel = p.ReadInt8(); //Max level
            Client.BasicInfo.CurrentExp = p.ReadInt64(); //Current exp
            p.ReadInt32(); //SP bar
            Client.BasicInfo.Gold = p.ReadUInt64(); //Gold
            Client.BasicInfo.SP = p.ReadInt32(); //Skill points
            Client.BasicInfo.StatPoints = p.ReadInt16(); //Stat points
            Client.BasicInfo.Zerk = (p.ReadInt8() == 5)?true:false; //Berserk gauge
            p.ReadInt32(); //Zeroes
            Client.BasicInfo.HP = p.ReadInt32(); //HP
            Client.BasicInfo.MP = p.ReadInt32(); //MP
            p.ReadInt8(); //Beginner icon
            p.ReadInt8(); //Daily PK
            p.ReadInt16(); //Total PK
            p.ReadInt32(); //Total Penalty points
            p.ReadInt8(); //Title
            p.ReadInt8(); //Pvp state
            Client.BasicInfo.MaxInventorySlots = p.ReadInt8(); //Max inventory slots
            #region Items
            int currentItems = Client.BasicInfo.CurrentInvItemsCount =  p.ReadInt8();
            for (int i = 0; i < currentItems; i++)
            {
                var item = Inventory.ParseItem.Parse(p);
                Client.InventoryItems.Add(item.Slot, item);
            }
            #endregion
        }
    }
}
