using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Agent
{
    public static class Extension
    {
        public static byte[] ToBytes(this object obj)
        {
            using MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
}
