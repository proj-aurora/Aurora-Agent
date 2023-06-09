using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace AICL.Server
{
    public class DaemonServer : TcpServer
    {
        public DaemonServer(string bind, int port) : base(bind, port)
        {
        }

        protected override TcpSession CreateSession() { return new DaemonSession(this); }

        protected override void OnError(SocketError error)
        {
        }

    }
}
