using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL.Rest
{
    public class ACLEndpoints
    {
        public const string API = "http://aurora.bluearchive.network";

        [JsonProperty("gateway")]
        public string Gateway { get; set; }
        
        [JsonProperty("dealer")]
        public ACLDealerEndpoint DealerEndpoint { get; set; }

        [JsonProperty("agent")]
        public ACLAgentEndpoint AgentEndpoint { get; set; }
    }

    public class ACLEndpointPaths
    {
        public const string Endpoints = "/endpoints.json";
    }

    #region Dealer Endpoints

    public class ACLDealerEndpoint
    {
        [JsonProperty("basePath")]
        public string BasePath { get; set; }

        [JsonProperty("identify")]
        public string Identify { get; set; }
    }
#endregion

    #region Agent Endpoints
    public class ACLAgentEndpoint
    {
        [JsonProperty("basePath")]
        public string BasePath { get; set; }

        [JsonProperty("identify")]
        public string Identify { get; set; }
    }
#endregion
}
