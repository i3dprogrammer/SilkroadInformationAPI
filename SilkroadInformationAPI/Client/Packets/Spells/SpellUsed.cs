using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Spells
{
    public class SpellUsed
    {

        public delegate void EntitySpellUsedHandler(EntitySpellUsedEventArgs e);
        public static event EntitySpellUsedHandler OnEntitySpellUsed;

        public static void Parse(Packet p)
        {
            if(p.ReadUInt8() == 0x01)
            {
                if(p.ReadUInt8() == 0x02)
                {
                    if(p.ReadUInt8() == 0x30)
                    {
                        uint spell_id = p.ReadUInt32();
                        uint attacker_id = p.ReadUInt32();
                        p.ReadUInt32(); //Spell temp id
                        uint victim_id = p.ReadUInt32();
                        OnEntitySpellUsed?.Invoke(new EntitySpellUsedEventArgs() { SpellID = spell_id, AttackerID = attacker_id, VictimID = victim_id });
                    }
                }
                else
                {
                    if(p.ReadUInt8() == 0x30)
                    {
                        //??
                    }
                }
            }
        }
    }

    public class EntitySpellUsedEventArgs : EventArgs
    {
        public uint SpellID;
        public uint AttackerID;
        public uint VictimID;
    }
}
