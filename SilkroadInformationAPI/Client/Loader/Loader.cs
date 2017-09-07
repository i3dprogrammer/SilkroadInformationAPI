using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Client.Loader
{
    /// <summary>
    /// Credits to Vitalka for this awesome loader
    /// </summary>
    class Loader
    {
        #region Import

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string dllToLoad);
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        static extern uint ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, uint lpbuffer, uint nSize, uint lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        static extern uint WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, int nSize, uint lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern uint VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32")]
        static extern uint GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll")]
        static extern uint WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        uint ByteArray = 0;

        #region Addresses
        uint BaseAddress = 0x400000;

        uint AlreadyProgramExe = 0;
        uint RedirectIPAddress = 0;
        uint MultiClientAddress = 0;
        uint CallForwardAddress = 0;
        uint MultiClientError = 0;
        //uint NudePatch = 0;
        uint SwearFilter1 = 0;
        uint SwearFilter2 = 0;
        uint SwearFilter3 = 0;
        uint SwearFilter4 = 0;
        uint EnglishPatch = 0;
        uint RussianHack = 0;
        uint Zoomhack = 0;
        uint SeedPatchAdress = 0;
        uint TextDataName = 0;
        uint StartingMSG = 0;
        uint ChangeVersion = 0;

        #endregion

        #region Patches
        string StartingMessageText = "Thanks to DaxterSoul.";
        string VersionText = "\t\t\t\t\t\t Loader was written by vitalka";
        byte[] HexColorArray = { 0x00, 0xDD, 0x00, 0x00 };

        byte[] JMP = { 0xEB };
        byte[] RETN = { 0xC3 };
        byte[] NOPNOP = { 0x90, 0x90 };
        byte[] LanguageTab = { 0xBF, 0x08, 0x00, 0x00, 0x00, 0x90 };
        byte[] SeedPatch = { 0xB9, 0x33, 0x00, 0x00, 0x00, 0x90, 0x90, 0x90, 0x90, 0x90 };

        byte PUSH = 0x68;
        #endregion

        #region BytePattern

        byte[] RedirectIPAddressPattern = { 0x89, 0x86, 0x2C, 0x01, 0x00, 0x00, 0x8B, 0x17, 0x89, 0x56, 0x50, 0x8B, 0x47, 0x04, 0x89, 0x46, 0x54, 0x8B, 0x4F, 0x08, 0x89, 0x4E, 0x58, 0x8B, 0x57, 0x0C, 0x89, 0x56, 0x5C, 0x5E, 0xB8, 0x01, 0x00, 0x00, 0x00, 0x5D, 0xC3 };
        byte[] SeedPatchPattern = { 0x8B, 0x4C, 0x24, 0x04, 0x81, 0xE1, 0xFF, 0xFF, 0xFF, 0x7F };
        byte[] NudePatchPattern = { 0x8B, 0x84, 0xEE, 0x1C, 0x01, 0x00, 0x00, 0x3B, 0x44, 0x24, 0x14 };
        byte[] ZoomhackPattern = { 0xDF, 0xE0, 0xF6, 0xC4, 0x41, 0x7A, 0x08, 0xD9, 0x9E };
        byte[] MulticlientPattern = { 0x6A, 0x06, 0x8D, 0x44, 0x24, 0x48, 0x50, 0x8B, 0xCF };
        byte[] CallForwardPattern = { 0x56, 0x8B, 0xF1, 0x0F, 0xB7, 0x86, 0x3E, 0x10, 0x00, 0x00, 0x57, 0x66, 0x8B, 0x7C, 0x24, 0x10, 0x0F, 0xB7, 0xCF, 0x8D, 0x14, 0x01, 0x3B, 0x96, 0x4C, 0x10, 0x00, 0x00 };
        byte[] MultiClientErrorStringPattern = Encoding.Default.GetBytes("½ÇÅ©·Îµå°¡ ÀÌ¹Ì ½ÇÇà Áß ÀÔ´Ï´Ù.");
        byte[] SwearFilterStringPattern = Encoding.Unicode.GetBytes("UIIT_MSG_CHATWND_MESSAGE_FILTER");
        byte[] ServerStatusFULLStringPattern = Encoding.Unicode.GetBytes("UIO_STT_SERVER_MAX_FULL");
        byte[] ChangeVersionStringPattern = Encoding.Unicode.GetBytes("Ver %d.%03d");
        byte[] StartingMSGStringPattern = Encoding.Unicode.GetBytes("UIIT_STT_STARTING_MSG");
        byte[] AlreadyProgramExeStringPattern = Encoding.ASCII.GetBytes("//////////////////////////////////////////////////////////////////");
        byte[] NoGameGuardStringPattern = Encoding.ASCII.GetBytes(@"config\n_protect.dat");
        byte[] TextDataNameStringPattern = Encoding.ASCII.GetBytes(@"%stextdata\textdataname.txt");
        byte[] EnglishStringPattern = Encoding.ASCII.GetBytes("English");
        byte[] RussiaStringPattern = Encoding.ASCII.GetBytes("Russia");

        #endregion

        #endregion

        public Loader()
        {
            LoadLibrary("WS2_32.dll");
            LoadLibrary("Kernel32.dll");
        }

        public Process StartClient(string path, ushort port, byte local)
        {
            Console.WriteLine("Starting process.");
            byte[] FileArray = File.ReadAllBytes(path + "\\sro_client.exe");
            #region FIND ADDRESSES
            //AlreadyProgramExeSearch
            AlreadyProgramExe = FindStringPattern(AlreadyProgramExeStringPattern, FileArray, BaseAddress, PUSH, 1) - 2;
            //SeedPatchSearch
            SeedPatchAdress = BaseAddress + FindPattern(SeedPatchPattern, FileArray, 1);
            //ReplaceText
            StartingMSG = FindStringPattern(StartingMSGStringPattern, FileArray, BaseAddress, PUSH, 1) + 24;
            ChangeVersion = FindStringPattern(ChangeVersionStringPattern, FileArray, BaseAddress, PUSH, 1);

            //MulticlientSearch
            MultiClientAddress = BaseAddress + FindPattern(MulticlientPattern, FileArray, 1) + 9;
            //CallForwardSearch
            CallForwardAddress = BaseAddress + FindPattern(CallForwardPattern, FileArray, 1);
            //MultiClientErrorSearch
            MultiClientError = FindStringPattern(MultiClientErrorStringPattern, FileArray, BaseAddress, PUSH, 1) - 8;

            //if (checkBox_NudePatch.Checked)
            //{
            //    //NudePatchSearch
            //    NudePatch = BaseAddress + FindPattern(NudePatchPattern, FileArray, 1) + 11;
            //}

            //ZoomhackSearch
            Zoomhack = BaseAddress + FindPattern(ZoomhackPattern, FileArray, 2) + 5;

            //SwearFilterSearch
            SwearFilter1 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 1) - 2;
            SwearFilter2 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 2) - 2;
            SwearFilter3 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 3) - 2;
            SwearFilter4 = FindStringPattern(SwearFilterStringPattern, FileArray, BaseAddress, PUSH, 4) - 2;

            RedirectIPAddress = BaseAddress + FindPattern(RedirectIPAddressPattern, FileArray, 1) - 50;

            #endregion
            return StartLoader(path, port, local);
        }

        private Process StartLoader(string path, ushort port, byte local)
        {

            CreateMutex(IntPtr.Zero, false, "Silkroad Online Launcher");
            CreateMutex(IntPtr.Zero, false, "Ready");

            Process pSilkroad;
            pSilkroad = new Process();
            pSilkroad.StartInfo.FileName = path + "\\sro_client.exe";
            pSilkroad.StartInfo.Arguments = "0/" + local + " 0 0";
            pSilkroad.Start();

            IntPtr SroProcessHandle = OpenProcess((uint)(0x000F0000L | 0x00100000L | 0xFFF), 0, pSilkroad.Id);

            Quickpatches(SroProcessHandle);

            RedirectVsroIP(SroProcessHandle, port);

            MultiClient(SroProcessHandle);

            StartingTextMSG(SroProcessHandle, StartingMessageText, HexColorArray);

            return pSilkroad;
        }

        private void Quickpatches(IntPtr SroProcessHandle)
        {
            //Already Program Exe
            WriteProcessMemory(SroProcessHandle, AlreadyProgramExe, JMP, JMP.Length, ByteArray);

            //Multiclient Error MessageBox
            WriteProcessMemory(SroProcessHandle, MultiClientError, JMP, JMP.Length, ByteArray);

            //Nude Patch
            //WriteProcessMemory(SroProcessHandle, NudePatch, NOPNOP, NOPNOP.Length, ByteArray);

            //Swear Filter
            WriteProcessMemory(SroProcessHandle, SwearFilter1, JMP, JMP.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, SwearFilter2, JMP, JMP.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, SwearFilter3, JMP, JMP.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, SwearFilter4, JMP, JMP.Length, ByteArray);

            //Zoomhack
            WriteProcessMemory(SroProcessHandle, Zoomhack, JMP, JMP.Length, ByteArray);

            //English Patch
            WriteProcessMemory(SroProcessHandle, EnglishPatch, NOPNOP, NOPNOP.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, RussianHack, JMP, JMP.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, TextDataName, LanguageTab, LanguageTab.Length, ByteArray);

            //Seed Patch
            WriteProcessMemory(SroProcessHandle, SeedPatchAdress, SeedPatch, SeedPatch.Length, ByteArray);

        }
        private void RedirectIP(IntPtr SroProcessHandle, ushort RedirectPort)
        {
            Console.WriteLine("Redirecting IP!");
            uint RedirectIPCodeCave = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 27, 0x1000, 0x4);
            uint SockAddrStruct = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 8, 0x1000, 0x4);
            uint WS2Connect = GetProcAddress(GetModuleHandle("WS2_32.dll"), "connect");

            byte[] WS2Array = BitConverter.GetBytes(WS2Connect - RedirectIPCodeCave - 26);
            byte[] SockAddr = BitConverter.GetBytes(SockAddrStruct);
            byte[] CallRedirectIp = BitConverter.GetBytes(RedirectIPCodeCave - RedirectIPAddress - 5);
            byte[] Port = BitConverter.GetBytes(Convert.ToUInt32(RedirectPort));
            byte[] IP1 = BitConverter.GetBytes(Convert.ToUInt16("127"));
            byte[] IP2 = BitConverter.GetBytes(Convert.ToUInt16("0"));
            byte[] IP3 = BitConverter.GetBytes(Convert.ToUInt16("0"));
            byte[] IP4 = BitConverter.GetBytes(Convert.ToUInt16("1"));

            byte[] Connection = { 0x02, 0x00, Port[1], Port[0], IP1[0], IP2[0], IP3[0], IP4[0] };
            byte[] CallAddress = { 0xE8, CallRedirectIp[0], CallRedirectIp[1], CallRedirectIp[2], CallRedirectIp[3] };
            byte[] RedirectCode = {       0x50, //PUSH EAX
                                          0x66, 0x8B, 0x47, 0x02, //MOV AX,WORD PTR DS:[EDI+2]
                                          0x66, 0x3D, 0x3D, 0xA3, //CMP AX,0A33D
                                          0x75, 0x05, //JNZ SHORT xxxxxxxx
                                          0xBF, SockAddr[0], SockAddr[1], SockAddr[2], SockAddr[3], //MOV EDI,xxxxxxxx
                                          0x58, //POP EAX
                                          0x6A, 0x10, //PUSH 10
                                          0x57, //PUSH EDI
                                          0x51, //PUSH ECX
                                          0xE8, WS2Array[0], WS2Array[1], WS2Array[2], WS2Array[3], //CALL WS2_32.connect
                                          0xC3 //RETN
                                      };

            WriteProcessMemory(SroProcessHandle, RedirectIPCodeCave, RedirectCode, RedirectCode.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, SockAddrStruct, Connection, Connection.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, RedirectIPAddress, CallAddress, CallAddress.Length, ByteArray);
        }

        private void RedirectVsroIP(IntPtr Handle, ushort RedirectPort)
        {
            uint count = 0;
            byte[] Port = BitConverter.GetBytes(RedirectPort);
            uint ConnectionStack = VirtualAllocEx(Handle, IntPtr.Zero, 8, 0x1000, 0x4);
            byte[] ConnectionStackArray = BitConverter.GetBytes(ConnectionStack);
            byte[] Connection = {
                                    0x02,0x00,
                                    Port[1], Port[0],   //0x4A, 0x38, // PORT (15778)
                                    0x7F,0x00,0x00,0x01 // IP (127.0.0.1)
                                };
            uint Codecave = VirtualAllocEx(Handle, IntPtr.Zero, 16, 0x1000, 0x4);
            byte[] CodecaveArray = BitConverter.GetBytes(Codecave - 0x004B08A1 - 5);
            byte[] CodeCaveFunc = {
                                      0xBF,ConnectionStackArray[0],ConnectionStackArray[1],ConnectionStackArray[2],ConnectionStackArray[3],
                                      0x8B,0x4E,0x04,
                                      0x6A,0x10,
                                      0x68,0xA6,0x08,0x4B,0x00,
                                      0xC3
                                  };
            byte[] JMPCodeCave = { 0xE9, CodecaveArray[0], CodecaveArray[1], CodecaveArray[2], CodecaveArray[3] };
            WriteProcessMemory(Handle, ConnectionStack, Connection, Connection.Length, count);
            WriteProcessMemory(Handle, Codecave, CodeCaveFunc, CodeCaveFunc.Length, count);
            WriteProcessMemory(Handle, 0x004B08A1, JMPCodeCave, JMPCodeCave.Length, count);
        }

        private void MultiClient(IntPtr SroProcessHandle)
        {
            uint MultiClientCodeCave = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 45, 0x1000, 0x4);
            uint MACCodeCave = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, 4, 0x1000, 0x4);
            uint GTC = GetProcAddress(GetModuleHandle("kernel32.dll"), "GetTickCount");

            byte[] CallBack = BitConverter.GetBytes(MultiClientCodeCave + 41);
            byte[] CALLForward = BitConverter.GetBytes(CallForwardAddress - MultiClientCodeCave - 34);
            byte[] MACAddress = BitConverter.GetBytes(MACCodeCave);
            byte[] GTCAddress = BitConverter.GetBytes(GTC - MultiClientCodeCave - 18);

            byte[] MultiClientArray = BitConverter.GetBytes(MultiClientCodeCave - MultiClientAddress - 5);
            byte[] MultiClientCodeArray = { 0xE8, MultiClientArray[0], MultiClientArray[1], MultiClientArray[2], MultiClientArray[3] };

            byte[] MultiClientCode = {   0x8F, 0x05, CallBack[0], CallBack[1], CallBack[2], CallBack[3], //POP DWORD PTR DS:[xxxxxxxx]
                                         0xA3, MACAddress[0], MACAddress[1], MACAddress[2], MACAddress[3], //MOV DWORD PTR DS:[xxxxxxxx],EAX
                                         0x60, //PUSHAD
                                         0x9C, //PUSHFD
                                         0xE8, GTCAddress[0], GTCAddress[1], GTCAddress[2], GTCAddress[3], // Call KERNEL32.gettickcount
                                         0x8B, 0x0D, MACAddress[0], MACAddress[1], MACAddress[2], MACAddress[3], //MOV ECX,DWORD PTR DS:[xxxxxxxx]
                                         0x89, 0x41, 0x02, // MOV DWORD PTR DS:[ECX+2],EAX
                                         0x9D, //POPFD
                                         0x61, //POPAD
                                         0xE8, CALLForward[0], CALLForward[1], CALLForward[2], CALLForward[3], //CALL xxxxxxxx
                                         0xFF, 0x35, CallBack[0], CallBack[1], CallBack[2], CallBack[3], // PUSH DWORD PTR DS:[xxxxxxxx]
                                         0xC3 //RETN
                                       };

            WriteProcessMemory(SroProcessHandle, MultiClientCodeCave, MultiClientCode, MultiClientCode.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, MultiClientAddress, MultiClientCodeArray, MultiClientCodeArray.Length, ByteArray);
        }
        private void StartingTextMSG(IntPtr SroProcessHandle, string StartingText, byte[] HexColor)
        {
            string ChangeVersionString = VersionText;
            uint StartingMSGStringCodeCave = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, StartingText.Length, 0x1000, 0x4);
            uint ChangeVersionStringCodeCave = VirtualAllocEx(SroProcessHandle, IntPtr.Zero, StartingText.Length, 0x1000, 0x4);
            byte[] StartingMSGByteArray = Encoding.Unicode.GetBytes(StartingText);
            byte[] ChangeVersionByteArray = Encoding.Unicode.GetBytes(ChangeVersionString);
            byte[] CallStartingMSG = BitConverter.GetBytes(StartingMSGStringCodeCave);
            byte[] CallChangeVersion = BitConverter.GetBytes(ChangeVersionStringCodeCave);
            byte[] StartingMSGCodeArray = { 0xB8, CallStartingMSG[0], CallStartingMSG[1], CallStartingMSG[2], CallStartingMSG[3] };
            byte[] ChangeVersionCodeArray = { 0x68, CallChangeVersion[0], CallChangeVersion[1], CallChangeVersion[2], CallChangeVersion[3] };
            WriteProcessMemory(SroProcessHandle, ChangeVersionStringCodeCave, ChangeVersionByteArray, ChangeVersionByteArray.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, ChangeVersion, ChangeVersionCodeArray, ChangeVersionCodeArray.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, ChangeVersion - 59, HexColor, HexColor.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, StartingMSGStringCodeCave, StartingMSGByteArray, StartingMSGByteArray.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, StartingMSG, StartingMSGCodeArray, StartingMSGCodeArray.Length, ByteArray);
            WriteProcessMemory(SroProcessHandle, StartingMSG + 9, HexColor, HexColor.Length, ByteArray);
        }
        private uint FindPattern(byte[] Pattern, byte[] FileByteArray, uint Result)
        {
            uint MyPosition = 0;
            uint ResultCounter = 0;
            for (uint PositionFileByteArray = 0; PositionFileByteArray < FileByteArray.Length - Pattern.Length; PositionFileByteArray++)
            {
                bool found = true;
                for (uint PositionPattern = 0; PositionPattern < Pattern.Length; PositionPattern++)
                {
                    if (FileByteArray[PositionFileByteArray + PositionPattern] != Pattern[PositionPattern])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    ResultCounter += 1;
                    if (Result == ResultCounter)
                    {
                        MyPosition = PositionFileByteArray;
                        break;
                    }
                }
            }
            return MyPosition;
        }
        private uint FindStringPattern(byte[] StringByteArray, byte[] FileArray, uint BaseAddress, byte StringWorker, uint Result)
        {
            uint MyPosition = 0;
            byte[] StringWorkerAddress = { StringWorker, 0x00, 0x00, 0x00, 0x00 };
            byte[] StringAddress = new byte[4];
            StringAddress = BitConverter.GetBytes(BaseAddress + FindPattern(StringByteArray, FileArray, 1));
            StringWorkerAddress[1] = StringAddress[0];
            StringWorkerAddress[2] = StringAddress[1];
            StringWorkerAddress[3] = StringAddress[2];
            StringWorkerAddress[4] = StringAddress[3];

            MyPosition = BaseAddress + FindPattern(StringWorkerAddress, FileArray, Result);
            return MyPosition;
        }
    }
}
