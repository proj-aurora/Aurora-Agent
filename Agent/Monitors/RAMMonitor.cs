﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace Agent.Monitors
{
    internal class RAMMonitor : IMonitor
    {
        public const string Name = "RAM";
        private readonly Computer _computer;
        private readonly ILogger _logger;

        public RAMMonitor(Computer computer)
        {
            _computer = computer;
            _logger = Log.Logger.ForContext<RAMMonitor>();
            Initialize();
        }

        public void Initialize()
        {
            _logger.Debug("Initialized {0} Monitor", Name);
        }

        public void Enable()
        {
            _computer.IsMemoryEnabled = true;
        }

        public void Dispose()
        {
        }
    }
}