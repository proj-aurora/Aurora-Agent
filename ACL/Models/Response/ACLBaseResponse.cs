using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL.Models.Response
{
    public class ACLBaseResponse<T> where T : class
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string? Error { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
