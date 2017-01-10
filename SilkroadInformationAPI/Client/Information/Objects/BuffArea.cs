using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Information.Objects
{
    public class BuffArea : Base
    {
        /// <summary>
        /// Reference skill ID being used from (skilldata),
        /// you cannot get it from ModelID.
        /// </summary>
        public uint ReferenceSkillID { get; set; }
    }
}