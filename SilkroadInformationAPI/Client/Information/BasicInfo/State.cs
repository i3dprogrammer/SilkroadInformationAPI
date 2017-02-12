using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.BasicInfo
{
    public class State
    {
        public bool LifeState;
        public CharMotionState MotionState;
        public CharStatus Status;
        public float WalkSpeed;
        public float RunSpeed;
        public float HwanSpeed;
        public byte BuffCount;
        public bool Returning = false;

        public Dictionary<uint, Spells.Skill> Buffs = new Dictionary<uint, Spells.Skill>();
    }
}
