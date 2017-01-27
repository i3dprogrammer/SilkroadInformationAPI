using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace SilkroadInformationAPI.Client.Network
{
    public class ProxyClient
    {
        class Context
        {
            public Socket Socket { get; set; }
            public Security Security { get; set; }
            public TransferBuffer Buffer { get; set; }
            public Security RelaySecurity { get; set; }

            public Context()
            {
                Socket = null;
                Security = new Security();
                RelaySecurity = null;
                Buffer = new TransferBuffer(8192);
            }
        }

        static bool ConnectedToWorldwideServer = false;
        static bool AutoSwitchToClientless = true;

        public static void StartProxy(string remote_ip, ushort remote_port, string local_ip, ushort local_port)
        {
            Context local_context = new Context();
            local_context.Security.GenerateSecurity(true, true, true);

            Context remote_context = new Context();

            remote_context.RelaySecurity = local_context.Security;
            local_context.RelaySecurity = remote_context.Security;

            List<Context> contexts = new List<Context>();
            contexts.Add(local_context);
            contexts.Add(remote_context);

            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(new IPEndPoint(IPAddress.Parse(local_ip), local_port));
                server.Listen(1);

                local_context.Socket = server.Accept();
            }

            using (local_context.Socket)
            {
                using (remote_context.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    remote_context.Socket.Connect(remote_ip, remote_port);
                    SroClient.RemoteSecurity = remote_context.Security;
                    SroClient.LocalSecurity = local_context.Security;
                    while (true)
                    {
                        foreach (Context context in contexts) // Network input event processing
                        {
                            try
                            {
                                if (context.Socket.Poll(0, SelectMode.SelectRead))
                                {
                                    int count = context.Socket.Receive(context.Buffer.Buffer);
                                    if (count == 0)
                                    {
                                        throw new Exception("The remote connection has been lost.");
                                    }
                                    context.Security.Recv(context.Buffer.Buffer, 0, count);
                                }
                            }
                            catch
                            {
                                if (context == local_context && ConnectedToWorldwideServer == true && AutoSwitchToClientless)
                                    Clientless_Start(remote_context);
                                return;
                            }
                        }

                        foreach (Context context in contexts) // Logic event processing
                        {
                            List<Packet> packets = context.Security.TransferIncoming();
                            if (packets != null)
                            {
                                foreach (Packet packet in packets)
                                {
                                    Dispatcher.Process(packet);

                                    if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000) // ignore always
                                    {
                                    }
                                    else if (packet.Opcode == 0x2001)
                                    {
                                        if (context == remote_context) // ignore local to proxy only
                                        {
                                            context.RelaySecurity.Send(packet); // proxy to remote is handled by API
                                        }
                                    }
                                    else if (packet.Opcode == 0xA102)
                                    {
                                        byte result = packet.ReadUInt8();
                                        if (result == 1)
                                        {
                                            uint id = packet.ReadUInt32();
                                            string ip = packet.ReadAscii();
                                            ushort port = packet.ReadUInt16();

                                            var agentThread = new Thread(()=>StartProxy(ip, ushort.Parse((port).ToString()), local_ip, local_port));
                                            agentThread.Start();

                                            Thread.Sleep(500); // Should be enough time, if not, increase, but too long and C9 timeout results

                                            Packet new_packet = new Packet(0xA102, true);
                                            new_packet.WriteUInt8(result);
                                            new_packet.WriteUInt32(id);
                                            new_packet.WriteAscii(local_ip);
                                            new_packet.WriteUInt16(local_port);

                                            context.RelaySecurity.Send(new_packet);
                                            ConnectedToWorldwideServer = true;
                                        }
                                    }
                                    else
                                    {
                                        context.RelaySecurity.Send(packet);
                                    }
                                }
                            }
                        }
                        #region TransferOutgoing
                        foreach (Context context in contexts) // Network output event processing
                        {
                            if (context.Socket.Poll(0, SelectMode.SelectWrite))
                            {
                                List<KeyValuePair<TransferBuffer, Packet>> buffers = context.Security.TransferOutgoing();
                                if (buffers != null)
                                {
                                    foreach (KeyValuePair<TransferBuffer, Packet> kvp in buffers)
                                    {
                                        TransferBuffer buffer = kvp.Key;
                                        while (true)
                                        {
                                            int count = context.Socket.Send(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None);
                                            buffer.Offset += count;
                                            if (buffer.Offset == buffer.Size)
                                            {
                                                break;
                                            }
                                            Thread.Sleep(1);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        Thread.Sleep(1); // Cycle complete, prevent 100% CPU usage
                    }
                }
            }


        }

        static void Clientless_Start(Context context)
        {
            var keepalive = new Thread(() => Clientless_Ping(context));
            keepalive.Start();

            while (true)
            {
                if (context.Socket.Poll(0, SelectMode.SelectRead))
                {
                    int count = context.Socket.Receive(context.Buffer.Buffer);
                    if (count == 0)
                    {
                        throw new Exception("The remote connection has been lost.");
                    }
                    context.Security.Recv(context.Buffer.Buffer, 0, count);
                }

                List<Packet> packets = context.Security.TransferIncoming();
                if (packets != null)
                {
                    foreach (Packet packet in packets)
                    {
                        if (packet.Opcode == 0x34B5) //Character teleport successfully
                        {
                            Packet answer = new Packet(0x34B6); //Teleport confirmation packet
                            SroClient.RemoteSecurity.Send(answer);
                        }
                    }
                }

                if (context.Socket.Poll(0, SelectMode.SelectWrite))
                {
                    List<KeyValuePair<TransferBuffer, Packet>> buffers = context.Security.TransferOutgoing();
                    if (buffers != null)
                    {
                        foreach (KeyValuePair<TransferBuffer, Packet> kvp in buffers)
                        {
                            TransferBuffer buffer = kvp.Key;
                            while (true)
                            {
                                int count = context.Socket.Send(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None);
                                buffer.Offset += count;
                                if (buffer.Offset == buffer.Size)
                                {
                                    break;
                                }
                                Thread.Sleep(1);
                            }
                        }
                    }
                }

            }
        }

        static void Clientless_Ping(Context context)
        {
            while (true)
            {
                Packet p = new Packet(0x2002);
                context.Security.Send(p);
                Thread.Sleep(5000);
            }
        }

    }
}
