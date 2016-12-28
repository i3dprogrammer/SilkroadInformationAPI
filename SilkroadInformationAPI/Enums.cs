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
        AttackAndRidePet, //0	1	1	2	3	3
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
        Stones100,
        MagicCube,
        MonsterMask,
        Tablets,
        None
    }
}
