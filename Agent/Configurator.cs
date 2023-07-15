using ACL;
using Agent.Monitors;
using AICL;
using LibreHardwareMonitor.Hardware;
using Serilog;
using Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace Agent
{
    internal class Configurator
    {
        private readonly AgentOptions _options;
        private readonly ILogger _logger;

        public Configurator(AgentOptions options)
        {
            _options = options;
            _logger = Log.Logger.ForContext<Configurator>();
        }

        public async Task RunAsync()
        {
            _logger.Debug("Running on Configuration Mode");
            
            Console.Write("Enter agent key: ");
            string key = Console.ReadLine();
            ClearCurrentConsoleLine();
            _logger.Information("New agent key: {0}", key);
            _options.AuroraKey = key;

            var dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "aurora");
            var config = Path.Combine(dir, "config.json");

            await File.WriteAllTextAsync(config, JsonConvert.SerializeObject(_options, Formatting.Indented));
            _logger.Information("Updated configuration. restarting application.");
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
