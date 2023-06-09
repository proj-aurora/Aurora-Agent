using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace AICL.Models
{
    [ProtoContract]
    internal class AICLHello
    {
        public string SIG { get; set; }
        public string Version { get; set; }
    }
}
