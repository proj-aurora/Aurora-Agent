using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL.Models.Response
{
    public class ACLAgentDealerResponse
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("report_url")]
        public string ReportURL { get; set; }
    }
}
