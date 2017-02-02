using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI
{
    public enum ModelType
    {
        Structure, //0  1  1   2    5   *
        FortressFlagsNPC,  //0 1   1   2   4   *
        PickupPet, //0	1	1	2	3	4
        AttackPet, //0	1	1	2	3	3	
        NormalPet, //0	1	1	2	3	1
        //AttackAndRidePet, //0	1	1	2	3	3
        JobPet,     //0	1	1	2	3	2	
        JobSilkPet, //1	1	1	2	3	2	
        NPC,        //0	1	1	2	2	0
        NPCMob, //	0	1	1	2	1	2
        Mob,        //0	1	1	2	1	*
        Character,  //0	1	1	1	0	0
        Portal1,    //0	0	4	1	1   *
        Portal2,    //0	0	4	1	2	0
        Gate,
        Gold,       //0	0	3	3	5	0
        QuestItem,  //0 0   3   3   9   0
        CountableItem,  //0	0	3	3	*	*
        PlusItem,   //0	0	3	1	*   *
        MallCountItem,   //1 0   3   3   *   *
        MallPlusItem,    //1 0   3   3   *   *
        StonesType, //0 0   3   3   11  *
        Unknown
    }
    public enum ItemType
    {
        WeaponElixir,
        ProtectorElixir,
        ShieldElixir,
        AccessoryElixir,
        Weapon,
        Protector,
        Shield,
        Accessory,
        LuckStone,
        SteadyStone,
        AstralStone,
        ImmortalStone,
        LuckyPowder,
        PlusItem,
        PickupPet,
        AttackPet,
        ItemExchangeCoupon,
        Stones,
        MagicCube,
        MonsterMask,
        Tablets,
        HpPotion,
        MpPotion,
        VigorPotion,
        Arrows,
        Bolts,
        UniversalPills,
        PurificationPills,
        ReturnScroll,
        None
    }
    public enum COS_Type
    {
        None,
        Normal,
        Job,
        AttackPet,
        PickupPet,
        Guild,
        Fortress,
    }
    public enum PVP_Cape
    {
        None = 0,
        Red = 1,
        Gray = 2,
        Blue = 3,
        White = 4,
        Orange = 5,
    }
    public enum PK_State
    {
        Neutral = 0,
        Assaulter = 1,
        Murderer = 2,
    }

    public enum JOB_Type
    {
        None = 0,
        Trader = 1,
        Thief = 2,
        Hunter = 3,
    }

    public enum ChatType
    {
        All = 1,
        PM = 2,
        AllGM = 3,
        Party = 4,
        Guild = 5,
        Global = 6,
        Notice = 7,
        Stall = 9,
        Union = 11,
        NPC = 13,
        Accademy = 16,
    }

    public enum ArenaType
    {
        Random = 0,
        Party = 1,
        Guild = 2,
        Job = 3,
    }

    public enum ArenaState
    {
        TenMinutesLeftToStartArena,
        FiveMinutesLeftToStartArena,
        OneMinuteLeftToStartArena,
        ArenaStarted,
        ArenaEnded,
        RegistrationClosed
    }

    public enum ArenaResult
    {
        Won,
        Lost,
        Draw,
    }

    public enum ArenaResponse
    {
        RegistrationSuccess = 0x00,
        YouAreAlreadyRegistered = 0x02,
        YouMayNotRegisterAtThisTime = 0x04,
        MatchHasBeenCanceledDueToNotEnoughPlayers = 0x06,
        UnableToRegisterYouAreNoInparty = 0x0B,
        YouAreNotWearingJobSuit = 0x0D,
        TeamGotFlag = 0x80,
        EnemyTeamGotFlag = 0x81,
        FlagChannelling = 0x82,
        FlagDropped = 0x83,
        FlagHasBeenPutAtFlagpole = 0x84,
        Unknown,
    }

    public enum CharMotionState
    {
        None = 0,
        Walking = 2,
        Running = 3,
        Sitting = 4,
    }

    public enum CharStatus
    {
        None = 0,
        Hwan = 1,
        Untouchable = 2,
        Invincible = 3,
        GMInvisible = 5,
        Sealth = 6,
        Invisible = 7,
    }

    /// <summary>
    /// DaxterSoul
    /// </summary>
    public enum FRPVPMode
    {
        None = 0,

        /// <summary>
        /// Team Red Hawk
        /// </summary>
        Red = 1,

        /// <summary>
        /// Team Black Turtle
        /// </summary>
        Black = 2,

        /// <summary>
        /// Team Blue Dragon
        /// </summary>
        Blue = 3,

        /// <summary>
        /// Team White Tiger
        /// </summary>
        White = 4,

        /// <summary>
        /// Team Giraffe
        /// </summary>
        Yellow = 5,
    }

    public enum PartyRace
    {
        Chinese,
        Europe,
    }

    public enum PartyObjective
    {
        Hunting,
        Quest,
        Trade,
        Thief,
    }

    public enum PartyType
    {
        ExpShare_ItemShare = 0x03,
        ExpFreeForAll_ItemFreeForAll = 0x04,
        ExpShare_ItemFreeForAll = 0x05,
        ExpFreeForAll_ItemShare = 0x06,
    }

    public enum ItemBlues
    {
        MATTR_DEC_MAXDUR,
        MATTR_INT,
        MATTR_STR,
        MATTR_RESIST_FROSTBITE,
        MATTR_RESIST_ESHOCK,
        MATTR_RESIST_BURN,
        MATTR_RESIST_POISON,
        MATTR_RESIST_ZOMBIE,
        MATTR_ATHANASIA,
        MATTR_SOLID,
        MATTR_LUCK,
        MATTR_NOT_REPARABLE,
        MATTR_REINFORCE_ITEM,
        MATTR_DUR,
        MATTR_HR,
        MATTR_EVADE_BLOCK,
        MATTR_EVADE_CRITICAL,
        MATTR_ER,
        MATTR_HP,
        MATTR_MP,
        MATTR_ASTRAL,
        MATTR_CRITICAL,
        MATTR_BLOCKRATE,
        MATTR_AVATAR_MPRG,
        MATTR_AVATAR_HPRG,
        MATTR_AVATAR_MDIA,
        MATTR_AVATAR_DARA,
        MATTR_AVATAR_DRUA,
        MATTR_STR_3JOB,
        MATTR_INT_3JOB,
        MATTR_INT_AVATAR,
        MATTR_STR_AVATAR,
        MATTR_REPAIR,
        MATTR_AVATAR_INT,
        MATTR_AVATAR_STR,
        MATTR_AVATAR_HR,
        MATTR_AVATAR_ER,
        MATTR_AVATAR_HP,
        MATTR_AVATAR_MP,
        MATTR_RESIST_STUN,
        MATTR_REGENHPMP,
        MATTR_RESIST_CSMP,
        MATTR_RESIST_DISEASE,
        MATTR_RESIST_SLEEP,
        MATTR_RESIST_FEAR,
        MATTR_NASRUN_UMDU,
        MATTR_NASRUN_MPNA,
        MATTR_NASRUN_HPNA,
        MATTR_NASRUN_BLOCKRATE,
        MATTR_STR_SET,
        MATTR_INT_SET,
        MATTR_ER_SET,
        MATTR_HP_SET,
        MATTR_MP_SET,
        MATTR_DUR_SET,
        MATTR_LUCK_SET,
        MATTR_AVATAR_STR_2,
        MATTR_AVATAR_STR_3,
        MATTR_AVATAR_STR_4,
        MATTR_AVATAR_INT_2,
        MATTR_AVATAR_INT_3,
        MATTR_AVATAR_INT_4,
        MATTR_AVATAR_MDIA_2,
        MATTR_AVATAR_MDIA_3,
        MATTR_AVATAR_MDIA_4,
        MATTR_AVATAR_LUCK,
        MATTR_AVATAR_LUCK_2,
        MATTR_AVATAR_LUCK_3,
        MATTR_AVATAR_LUCK_4,
        MATTR_APE,
        MATTR_REINFORCE_ITEM_SET,
        MATTR_RESIST_ALL_SET,

    }

    public enum LoginErrorType
    {
        WrongPassword = 1,
        Banned = 101,
        ServerInspection = 102,
        UserAlreadyConnected = 3,
        C5Error = 4,
        ServerFull = 5,
        C7Error = 6,
        C8Error = 7,
        IPLimitExceeded = 8,
        DamagedDLLs = 10,
    }
}
