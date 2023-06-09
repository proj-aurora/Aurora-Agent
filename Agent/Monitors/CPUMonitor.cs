using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace Agent.Monitors
{
    internal class CPUMonitor : IMonitor
    {
        public const string Name = "CPU";
        private readonly Computer _computer;
        private readonly ILogger _logger;

        public CPUMonitor(Computer computer)
        {
            _computer = computer;
            _logger = Log.Logger.ForContext<CPUMonitor>();
            Initialize();
        }

        public void Initialize()
        {
            _logger.Debug("Initialized {0} Monitor", Name);
        }

        public void Enable()
        {
            _computer.IsCpuEnabled = true;
        }

        public void Dispose()
        {
        }
    }
}
