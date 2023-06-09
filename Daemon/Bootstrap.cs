using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AICL;
using CLIOptions;

namespace Daemon
{
    internal class Bootstrap
    {
        private readonly DaemonOptions _options;
        private readonly AuroraDaemon _daemon;

        public Bootstrap(DaemonOptions options)
        {
            _options = options;
            _daemon = new(options);
        }

        public void Run()
        {
            var shouldDaemon = _daemon.Run();
        }


    }
}
