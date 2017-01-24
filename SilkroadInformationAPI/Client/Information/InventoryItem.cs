using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SilkroadInformationAPI.Client.Information
{
    [Serializable]
    public class InventoryItem : Media.DataInfo.Item
    {
        public int Stack;
        public int PlusValue;
        public int Slot;
        public ulong Price;
        public bool HasAdvance;
        public Dictionary<ItemBlues, int> Blues;
        public Dictionary<string, int> Stats;

        public InventoryItem(int ID)
        {
            var mediaItem = Media.Data.MediaItems[ID];

            MediaName = mediaItem.MediaName;
            TranslationName = mediaItem.TranslationName;
            ModelID = ID;
            Type = mediaItem.Type;
            Classes = mediaItem.Classes;
            Degree = mediaItem.Degree;
            Cooldown = mediaItem.Cooldown;
            Duration = mediaItem.Duration;
            MaxStack = mediaItem.MaxStack;

            Stack = 1;
            PlusValue = 0;
            Slot = 0;
            HasAdvance = false;
            Blues = new Dictionary<ItemBlues, int>();
            Stats = new Dictionary<string, int>();
        }

        public InventoryItem Clone()
        {
            return (InventoryItem)this.MemberwiseClone();
        }
    }
}
