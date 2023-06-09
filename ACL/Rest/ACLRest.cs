using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ACL.Models.Request;
using ACL.Models.Response;
using ACL.WebSocket;
using Serilog;

namespace ACL.Rest
{
    public class ACLRest
    {
        private readonly string _auroraKey;
        private readonly ILogger _logger;
        private readonly ACLClient _client;
        private ACLEndpoints _endpoints;

        private string baseUrl => ACLEndpoints.API;
        private string agentBaseUrl => string.Concat(baseUrl, _endpoints.AgentEndpoint.BasePath);
        private string dealerBaseUrl => string.Concat(baseUrl, _endpoints.DealerEndpoint.BasePath);



        public ACLEndpoints Endpoints => _endpoints;

        public ACLRest(string auroraKey)
        {
            _auroraKey = auroraKey;
            _logger = Log.Logger.ForContext<ACLRest>();
            _client = new ACLClient();
        }

        public async Task FetchAllEndpointsAsync()
        {
            _logger.Information("Fetching all endpoints...");
            var endpoints = await _client.GetAsync<ACLEndpoints>(string.Concat(ACLEndpoints.API, ACLEndpointPaths.Endpoints));
         
            _endpoints = endpoints;
            ACLUtils.PrintProperties(_logger, endpoints);
        }

        public async Task<ACLBaseResponse<ACLAgentIdentifyResponse>> AgentIdentifyAsync(string hwid)
        {
            _logger.Debug("Executing AgentIdentify");
            var body = new ACLAgentIdentifyRequest(_auroraKey, hwid);

            var response = await _client.PostAsync<ACLBaseResponse<ACLAgentIdentifyResponse>>(string.Concat(agentBaseUrl, _endpoints.AgentEndpoint.Identify), body);
            return response;
        }

        public async Task<ACLBaseResponse<ACLAgentDealerResponse>> AgentDealAsync()
        {
            _logger.Debug("Executing AgentDeal");
            var body = new ACLAgentDealerRequest(_auroraKey);

            var response = await _client.PostAsync<ACLBaseResponse<ACLAgentDealerResponse>>(string.Concat(dealerBaseUrl, _endpoints.DealerEndpoint.Identify), body);

            return response;
        }
    }
}
