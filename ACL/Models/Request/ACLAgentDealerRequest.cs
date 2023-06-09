using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL.Models.Request
{
    public class ACLAgentDealerRequest
    {
        public ACLAgentDealerRequest(string auroraKey)
        {
            AuroraKey = auroraKey;
        }

        [JsonProperty("auroraKey")]
        public string AuroraKey { get; set; }
    }
}
