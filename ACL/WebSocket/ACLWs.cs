using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.WebSocket
{
    public class ACLWs
    {
        private readonly string _auroraKey;
        private readonly ILogger _logger;
        public ACLWs(string auroraKey)
        {
            _auroraKey = auroraKey;
            _logger = Log.Logger.ForContext<ACLWs>();
        }
    }
}
