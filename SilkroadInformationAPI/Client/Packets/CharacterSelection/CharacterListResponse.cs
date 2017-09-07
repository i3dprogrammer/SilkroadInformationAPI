using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.CharacterSelection
{
    public class CharacterListResponse
    {
        public delegate void CharacterListHandler(CharacterLsitEventArgs e);
        public static event CharacterListHandler OnCharacterListReceive;

        public static void Parse(Packet p)
        {
            p.ReadUInt8();
            if(p.ReadUInt8() == 0x01)
            {
                var Chars = new List<CharacterLsitEventArgs.SelectionCharacter>();
                byte count = p.ReadUInt8();
                for(int i = 0; i < count; i++)
                {
                    var Char = new CharacterLsitEventArgs.SelectionCharacter();
                    Char.RefObjID = p.ReadUInt32();
                    Char.Name = p.ReadAscii();
                    p.ReadUInt8(); //Scale
                    Char.CurrentLevel = p.ReadUInt8();
                    p.ReadUInt64(); //Exp
                    Char.STR = p.ReadUInt16();
                    Char.INT = p.ReadUInt16();
                    Char.StatPoints = p.ReadUInt16();
                    Char.CurrentHP= p.ReadUInt32();
                    Char.CurrentMP = p.ReadUInt32();
                    Char.BeingDeleted = (p.ReadUInt8() == 0x01);
                    if (Char.BeingDeleted)
                        Char.DeleteTime = p.ReadUInt32();
                    p.ReadUInt8(); //Guild
                    byte flag = p.ReadUInt8();
                    if (flag == 0x01)
                        p.ReadAscii();
                    p.ReadUInt8(); //Academy

                    byte count2 = p.ReadUInt8();
                    for (byte j = 0; j < count2; j++)
                        Char.Inventory.Add(new Tuple<uint, byte>(p.ReadUInt32(), p.ReadUInt8()));
                    count2 = p.ReadUInt8();
                    for (byte j = 0; j < count2; j++)
                        Char.AvatarInventory.Add(new Tuple<uint, byte>(p.ReadUInt32(), p.ReadUInt8()));
                    Chars.Add(Char);
                }
                OnCharacterListReceive?.Invoke(new CharacterLsitEventArgs() { Characters = Chars });
            } else
            {
                //Error code
            }
        }
    }

    public class CharacterLsitEventArgs : EventArgs
    {
        public List<SelectionCharacter> Characters;

        public class SelectionCharacter
        {
            public uint RefObjID;
            public string Name;
            public byte CurrentLevel;
            public ushort STR;
            public ushort INT;
            public ushort StatPoints;
            public uint CurrentHP;
            public uint CurrentMP;
            public bool BeingDeleted;
            public uint DeleteTime;
            public List<Tuple<uint, byte>> Inventory = new List<Tuple<uint, byte>>();
            public List<Tuple<uint, byte>> AvatarInventory = new List<Tuple<uint, byte>>();
        }
    }
}
