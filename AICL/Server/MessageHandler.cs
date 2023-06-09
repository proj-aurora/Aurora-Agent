using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AICL.Models;

namespace AICL.Server
{
    internal class MessageHandler
    {
        private bool _authorized = false;
        private DaemonSession _session;

        public MessageHandler(DaemonSession session)
        {
            _session = session;
        }

        public void Process(byte[] data)
        {
            try
            {
                if (!_authorized)
                {
                    AICLHello hello = data.Deserialize<AICLHello>();
                    if (hello.SIG != "AICL")
                    {
                        _session.Disconnect();
                    }

                    _authorized = true;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _session.Disconnect();
            }
        }
    }
}
