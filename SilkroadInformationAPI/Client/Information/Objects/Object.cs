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
        public Stalls.Stall Stall = new Stalls.Stall();
        public bool WearingMask { get; set; } = false;
        public Guilds.Guild Guild = new Guilds.Guild();
        public List<CharacterInfo.CharacterItem> Inventory = new List<CharacterInfo.CharacterItem>();
        public List<CharacterInfo.CharacterItem> AvatarInventory = new List<CharacterInfo.CharacterItem>();
        public BasicInfo.Movement Movement = new BasicInfo.Movement();
        public BasicInfo.State State = new BasicInfo.State();
        public byte PVPEquipCooldown { get; set; }

        //COS
        public string PetGuildName;
        public string Owner;
        public COS_Type COSType = COS_Type.None;

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
        public uint DropperUniqueID;
        public byte DropSource;

        //EVENT_ZONE
        public uint SkillID;

        public bool CharacterInJobSuit()
        {
            for(int i = 0; i < Inventory.Count; i++)
            {
                var item = Media.Data.MediaModels[Inventory[i].ModelID];
                if(item.Classes.C == 3 && item.Classes.D == 1 && item.Classes.E == 7 && item.Classes.F != 5)
                {
                    return true;
                }
            }
            return false;
        }


        public byte type { get; set; } = 0; //Character = 0, NPC = 1, MOB = 2, Item = 3, COS = 4, struct = 5, Buff area = 6;
    }
}
