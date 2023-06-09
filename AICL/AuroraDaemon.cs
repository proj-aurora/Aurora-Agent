using System.Diagnostics;
using System.Net;
using AICL.Client;
using AICL.Server;
using CLIOptions;

namespace AICL
{
    public class AuroraDaemon
    {
        private readonly DaemonOptions _options;

        private readonly DaemonClient _client;

        private readonly DaemonServer _server;

        private readonly AICLWs _WsServer;

        //todo get available daemons
        public AuroraDaemon(DaemonOptions options)
        {
            _options = options;

            _client = new(_options.Address, _options.DaemonPort);
            _client.ConnectAsync();

            _server = new (options.Address, options.DaemonPort);
            _WsServer = new(IPAddress.Loopback, 29100);
        }

        private bool IsDaemonPresent()
        {
            return _client.IsConnected;
        }

        public bool Run()
        {
            if (IsDaemonPresent())
            {
                Console.WriteLine("Running as child");
                _server.Dispose();
                return false;
            }
            else
            {
                _server.Start();
                _client.Dispose();
                return true;
            }
        }

        public void Start()
        {

        }
    }
}