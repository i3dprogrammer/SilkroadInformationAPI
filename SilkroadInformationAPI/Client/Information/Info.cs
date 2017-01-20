using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information
{
    public class Info
    {
        public int RegionID { get; set; }
        public int ModelID { get; set; }
        public uint UniqueID { get; set; }
        public int Level { get; set; }
        public int MaxGameLevel { get; set; }
        public ulong MaxEXP { get; set; }
        public ulong CurrentExp { get; set; }
        public int SPLoadingBar { get; set; }
        public ulong Gold { get; set; }
        public int SP { get; set; }
        public short StatPoints { get; set; }
        public bool Zerk { get; set; }
        public uint MaxHP { get; set; }
        public uint MaxMP { get; set; }
        public uint CurrentHP { get; set; }
        public uint CurrentMP { get; set; }
        public int MaxInventorySlots { get; set; }
        public int CurrentInvItemsCount { get; set; }
        public string CharacterName { get; set; }
        public string JobName { get; set; }
        public byte JobType { get; set; }
        public byte JobLevel { get; set; }
        public uint JobExp { get; set; }
        public uint JobContribution { get; set; }
        public uint JobReward { get; set; }
        public byte PVPState { get; set; }
        public byte TransportFlag { get; set; }
        public uint TransportUniqueID { get; set; }
        public byte InCombat { get; set; }
        public byte PVPFlag { get; set; }
        public ulong GuideFlag { get; set; }
        public uint JID { get; set; } // ?
        public byte GMFlag { get; set; }
        public bool BadStatus { get; set; }
        public bool ZerkOn { get; set; } = false;
        public FRPVPMode PVPCape { get; set; } = FRPVPMode.None;
        //public Dictionary<uint, Objects.COS> CharacterCOSe = new Dictionary<uint, Objects.COS>();

        //STATS
        public uint MinimumPhysicalAttack;
        public uint MaximumPhysicalAttack;
        public uint MinimumMagicalAttack;
        public uint MaximumMagicalAttack;
        public ushort PhysicalDefense;
        public ushort MagicalDefense;
        public ushort HitRate;
        public ushort ParryRate;
        public ushort STR;
        public ushort INT;
    }
}
