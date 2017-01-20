using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Party
{
    public class PartyUpdate
    {
        /// <summary>
        /// Event is called upon party disbanding.
        /// </summary>
        public static event Action OnPartyDisband;
        
        /// <summary>
        /// The event is called AFTER member joins the party, with the object being the new member.
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMemberJoin;

        /// <summary>
        /// This is called BEFORE member leaving the party, with the object being member "leaving" the party.
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMemberLeave;

        /// <summary>
        /// This is called BEFORE member get kicked from party.
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMemberKicked;


        /// <summary>
        /// This is called after member hp or mp update, with the object being the member getting the change.
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMemberHPMPUpdate;

        /// <summary>
        /// This is called after member position ON MAP update.
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMemberPositionUpdate;

        /// <summary>
        /// This event is called when the party master changes
        /// </summary>
        public static event Action<Information.Party.Party.PartyMember> OnPartyMasterUpdate;

        public static void Parse(Packet p)
        {
            uint uid;
            byte flag = p.ReadUInt8();
            switch (flag) //TODO: Parse other cases
            {
                case 0x01: //party disbanded
                    Client.Party.PartyMembers.Clear();
                    Client.Party.MembersCount = 1;
                    OnPartyDisband?.Invoke();
                    break;
                case 0x02: //Someone joined
                    var member = PartyUtility.ParseMember(p);
                    Client.Party.PartyMembers.Add(member.UniqueID, member);
                    Client.Party.MembersCount += 1;
                    OnPartyMemberJoin?.Invoke(member);
                    break;
                case 0x03: //Party member left
                    uid = p.ReadUInt32();
                    byte reason = p.ReadUInt8();
                    if (Client.Party.PartyMembers.ContainsKey(uid))
                    {
                        if (reason == 0x02)
                            OnPartyMemberLeave?.Invoke(Client.Party.PartyMembers[uid]);
                        else if (reason == 0x04)
                            OnPartyMemberKicked?.Invoke(Client.Party.PartyMembers[uid]);
                        Client.Party.PartyMembers.Remove(uid);
                    }
                    Client.Party.MembersCount -= 1;
                    break;
                case 0x06: //Party member info update
                    uid = p.ReadUInt32();
                    byte action = p.ReadUInt8();
                    if (action == 0x04) //HP MP Update
                    {
                        byte MPHP = p.ReadUInt8();
                        byte HP = Convert.ToByte(MPHP.ToString("X2")[1].ToString(), 16);
                        byte MP = Convert.ToByte(MPHP.ToString("X2")[0].ToString(), 16);
                        Client.Party.PartyMembers[uid].HPPercentage = HP;
                        Client.Party.PartyMembers[uid].MPPercentage = MP;
                        OnPartyMemberHPMPUpdate?.Invoke(Client.Party.PartyMembers[uid]);
                    }
                    else if (action == 0x08)
                    {
                        p.ReadUInt32(); //Mastery Tree 1
                        p.ReadUInt32(); //Mastery Tree 2
                    }
                    else if (action == 0x20) //Position Update
                    {
                        Information.BasicInfo.Position pos = new Information.BasicInfo.Position();
                        pos.RegionID = p.ReadUInt16();
                        if (pos.RegionID < short.MaxValue)
                        {   //World
                            pos.X = p.ReadUInt16();
                            pos.Y = p.ReadUInt16();
                            pos.Z = p.ReadUInt16();
                        }
                        else
                        {   //Dungeon
                            pos.X = p.ReadUInt32(); // Probably 16bit a mistake.
                            pos.Y = p.ReadUInt32();
                            pos.Z = p.ReadUInt32();
                        }
                        Client.Party.PartyMembers[uid].Position = pos;
                        OnPartyMemberPositionUpdate?.Invoke(Client.Party.PartyMembers[uid]);
                    }
                    break;
                case 0x09: //Party master updated
                    uid = p.ReadUInt32();
                    if (Client.Party.PartyMembers.ContainsKey(uid))
                    {
                        Client.Party.MasterUniqueID = uid;
                        Client.Party.PartyMaster = Client.Party.PartyMembers[uid].Name;
                    }
                    OnPartyMasterUpdate?.Invoke(Client.Party.PartyMembers[uid]);
                    break;
            }
        }
    }
}
