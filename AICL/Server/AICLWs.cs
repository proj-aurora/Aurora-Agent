using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace AICL.Server
{
    public class AICLWs : WsServer
    {
        public AICLWs(IPAddress address, int port) : base(address, port)
        {
        }

        protected override TcpSession CreateSession() { return new WebSocketSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
