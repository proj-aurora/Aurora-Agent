using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared.Options
{
    public class AgentOptions
    {
        [JsonProperty("version")]
        public string Version { get; set; } = "v0";

        [JsonProperty("monitors")]
        public AgentMonitorOption[] Monitors { get; set; }

        [JsonProperty("auroraKey")]
        public string AuroraKey { get; set; } = "PLEASE_RUN_CONFIGURATOR";
    }
}
