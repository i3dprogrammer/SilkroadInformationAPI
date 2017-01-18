using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Entity
{
    public class LevelUpAnimation
    {
        /// <summary>
        /// NOTE: This gets called when level up animation occurs(If a character gaiend 5 levels at once -> this gets called one time,
        /// <para>If a character gained 1 level -> this gets called one time) this basically start the level up particles for a specific character.</para>
        /// <para>The object returned is the Information.Objects.Character getting the level up particles.</para>
        /// </summary>
        public static event Action<Information.Objects.Character> OnLevelUpAnimation;
        public static void Parse(Packet p) //TODO: Add events
        {
            uint uid = p.ReadUInt32();

            OnLevelUpAnimation?.Invoke(Client.NearbyCharacters[uid]);
            //if(Client.Info.UniqueID == uid)
            //{
            //    Client.Info.Level += 1;
            //    Client.Info.MaxEXP = Media.Data.MaxEXP[Client.Info.Level];
            //}
        }


    }
}
