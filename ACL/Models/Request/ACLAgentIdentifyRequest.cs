using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL.Models.Request
{
    public class ACLAgentIdentifyRequest
    {
        public ACLAgentIdentifyRequest(string auroraKey, string hWID)
        {
            AuroraKey = auroraKey;
            HWID = hWID;
        }

        [JsonProperty("auroraKey")]
        public string AuroraKey { get; set; }

        [JsonProperty("hwid")]
        public string HWID { get; set; }
    }
}
