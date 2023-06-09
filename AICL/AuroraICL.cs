using System.Net;
using AICL.Server;
using Serilog;
using Serilog.Core;

namespace AICL
{
    public class AuroraICL
    {
        private readonly ILogger _logger;
        private readonly AICLWs _aiclWs;

        public AuroraICL()
        {
            _logger = Log.Logger.ForContext<AuroraICL>();
            _aiclWs = new AICLWs(IPAddress.Loopback, 29100);
        }

        public AICLWs AICLWs => _aiclWs;
    }
}