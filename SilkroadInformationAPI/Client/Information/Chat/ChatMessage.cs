using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Chat
{
    public class ChatMessage
    {
        /// <summary>
        /// The message sent in chat.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Character name who sent the message, this is not available in case of NPC ChatType.
        /// </summary>
        public string CharacterName { get; set; } = "";

        /// <summary>
        /// The Unique ID of the person who sent the message.
        /// </summary>
        public uint UniqueID { get; set; } = 0;

        /// <summary>
        /// Chat type.
        /// </summary>
        public ChatType Type { get; set; } = ChatType.All;

        public ChatMessage(string msg, string name, uint UID, ChatType type)
        {
            this.Message = msg;
            this.CharacterName = name;
            this.UniqueID = UID;
            this.Type = type;
        }
    }
}
