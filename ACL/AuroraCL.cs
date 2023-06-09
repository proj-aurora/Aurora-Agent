using ACL.Rest;
using ACL.WebSocket;
using Serilog;
using Serilog.Core;

namespace ACL
{
    public class AuroraCL
    {
        private readonly string _auroraKey;
        private readonly ACLRest _aclRest;
        private readonly ACLWs _aclWs;
        private readonly ILogger _logger;

        public AuroraCL(string auroraKey)
        {
            _auroraKey = auroraKey;
            _aclRest = new ACLRest(auroraKey);
            _aclWs = new ACLWs(auroraKey);
            _logger = Log.Logger.ForContext<AuroraCL>();
        }

        public ACLRest ACLRest => _aclRest;
        public ACLWs ACLWs => _aclWs;

    }
}