using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    class Object : Base
    {
        public string Name { get; set; }
        public byte Scale { get; set; }
        public byte PVPCape { get; set; }
        public byte PVPState { get; set; }
        public byte TransportFlag { get; set; }
        public uint TransportUniqueID { get; set; }
        public byte InCombat { get; set; }
        public byte UsingScroll { get; set; }
        public byte InventorySize { get; set; }
        public byte JobType { get; set; }
        public byte JobLevel { get; set; }
        public byte InteractMode { get; set; }
        public Character.Stall Stall = new Character.Stall();
        public bool WearingMask { get; set; } = false;
        public Character.Guild Guild = new Character.Guild();
        public List<Character.CharacterItem> Inventory = new List<Character.CharacterItem>();
        public List<Character.CharacterItem> AvatarInventory = new List<Character.CharacterItem>();
        public List<Character.Buff> Buffs = new List<Character.Buff>();
        public BasicInfo.Movement Movement = new BasicInfo.Movement();
        public BasicInfo.State State = new BasicInfo.State();
        public byte PVPEquipCooldown { get; set; }

        //COS
        public string PetGuildName;
        public string Owner;
        public COS_Type Type = COS_Type.None;

        //Structure
        public uint HP;
        public uint EventStructID;
        public ushort StructureState;

        //NPC
        public byte TalkableNPC;
        public byte Rarity;
        public byte Appearance;

        //ITEMS
        public byte OptValue;
        public uint Amount;
        public uint OwnerID;
        //public uint PickRightsID;
        public uint DropperUniqueID;
        public byte DropSource;

        //EVENT_ZONE
        public uint SkillID;

    }
}
