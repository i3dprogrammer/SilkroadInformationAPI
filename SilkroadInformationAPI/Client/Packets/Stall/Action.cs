using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Stall
{
    public class Action
    {
        public static event System.Action<string> OnCharacterEnterCurrentStall;
        public static event System.Action<string> OnCharacterLeaveCurrentStall;
        public static event System.Action<Information.InventoryItem, string> OnCharacterBuyingItem;

        public static void Parse(Packet p)
        {
            byte action = p.ReadUInt8();
            if(action == 1) //Character left
            {
                uint uid = p.ReadUInt32();
                if (Client.CurrentStall.PeopleInStall.ContainsKey(uid))
                {
                    OnCharacterLeaveCurrentStall?.Invoke(Client.CurrentStall.PeopleInStall[uid]);
                    Client.CurrentStall.PeopleInStall.Remove(uid);
                }
            } else if(action == 2) //Character entered
            {
                uint uid = p.ReadUInt32();
                Client.CurrentStall.PeopleInStall.Add(uid, Actions.Mapping.GetCharNameFromUID(uid));
                OnCharacterEnterCurrentStall?.Invoke(Client.CurrentStall.PeopleInStall[uid]);
            } else if(action == 3) //Item bought
            {
                byte slot = p.ReadUInt8(); //Bought slot
                string name = p.ReadAscii(); //Buying character name
                OnCharacterBuyingItem?.Invoke(Client.CurrentStall.StallItems[slot], name);
                Client.CurrentStall.StallItems.Remove(slot);
            }
        }
    }
}
