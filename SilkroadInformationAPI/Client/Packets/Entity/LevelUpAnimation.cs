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
        /// <para>If a character gained 1 level -> this gets called one time) this basically starts the level up particles for a specific character.</para>
        /// <para>The object returned is the Information.Objects.Character getting the level up particles.</para>
        /// </summary>
        public static event Action<Information.Objects.Character> OnCharacterLevelUpAnimation;


        /// <summary>
        /// This gets called once the level up particles starts for the client.
        /// <para>NOTE this does not mean the character gained 1 level, could be more than 1.</para>
        /// </summary>
        public static event Action OnClientLevelUpAnimation;

        public static void Parse(Packet p)
        {
            uint uid = p.ReadUInt32();

            if(Client.NearbyCharacters.ContainsKey(uid))
                OnCharacterLevelUpAnimation?.Invoke(Client.NearbyCharacters[uid]);
            if (Client.Info.UniqueID == uid)
                OnClientLevelUpAnimation?.Invoke();
        }


    }
}
