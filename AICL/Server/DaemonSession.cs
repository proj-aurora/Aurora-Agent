using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AICL.Models;
using NetCoreServer;
using ProtoBuf;

namespace AICL.Server
{
    public class DaemonSession : TcpSession
    {
        private MessageHandler _handler;

        public DaemonSession(DaemonServer server) : base(server)
        {
            _handler = new(this);
        }

        protected override void OnConnected()
        {
            AICLHello hello = new() { SIG = "AIPC", Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(2) };
            SendAsync(hello.Serialize());
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            _handler.Process(buffer);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }
}
