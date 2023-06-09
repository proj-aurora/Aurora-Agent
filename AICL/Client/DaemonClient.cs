using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace AICL.Client
{
    public class DaemonClient : TcpClient
    {
        public DaemonClient(string address, int port) : base(address, port)
        {

        }
    }
}
