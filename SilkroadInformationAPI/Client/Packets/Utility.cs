using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets
{
    class Utility
    {
        public static void LogPacket(Packet p)
        {
            string data = string.Format("[{0}][{1:X4}][{2} bytes]{3}{4}{6}{5}{6}", "S->C", p.Opcode, p.GetBytes().Length, p.Encrypted ? "[Encrypted]" : "", p.Massive ? "[Massive]" : "", SilkroadSecurityApi.Utility.HexDump(p.GetBytes()), Environment.NewLine);
            File.AppendAllText(Environment.CurrentDirectory + "\\" + "Packets.txt", Environment.NewLine + data);
        }
        public static void LogPacket(string data)
        {
            File.AppendAllText(Environment.CurrentDirectory + "\\" + "PacketLog.txt", Environment.NewLine + data);
        }
    }
}
