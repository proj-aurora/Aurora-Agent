using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ACL;
using Agent.Monitors;
using AICL;
using LibreHardwareMonitor.Hardware;
using Newtonsoft.Json;
using Serilog;
using Shared.Options;

namespace Agent
{
    internal class Bootstrap
    {
        private List<IMonitor> _monitors;
        private readonly AgentOptions _options;
        private readonly ILogger _logger;
        private readonly Computer _computer;
        private readonly AuroraCL _acl;
        private readonly AuroraICL _aicl;

        public Bootstrap(AgentOptions options)
        {
            _options = options;
            _logger = Log.Logger.ForContext<Bootstrap>();
            _monitors = new List<IMonitor>();
            _acl = new AuroraCL(options.AuroraKey);
            _aicl = new AuroraICL();
        }

        public async Task RunAsync()
        {
            if (!IsAuroraKeyExist())
            {
                _logger.Error("No Aurora Key set. Please run configurator using -c");
                Exit();
            }

            LoadMonitorsFromConfig();

            await _acl.ACLRest.FetchAllEndpointsAsync();

            _logger.Information("Identifying Agent...");
            var identifyResponse = await _acl.ACLRest.AgentIdentifyAsync("test");

            if (!identifyResponse.Success)
            {
                _logger.Error(identifyResponse.Error);
                _logger.Error("Agent identification Failed!. Please check your configuration & Aurora Key");
                Exit(1);
            }
            _logger.Debug("Agent Id: {0}, Active: {1}", identifyResponse.Data.Id, identifyResponse.Data.Active);

            var dealResponse = await _acl.ACLRest.AgentDealAsync();
            if (!dealResponse.Success)
            {
                _logger.Error(dealResponse.Error);
                _logger.Error("Unable to get response from dealer({0})", _acl.ACLRest.Endpoints.DealerEndpoint.BasePath);
                Exit(1);
            }

            _logger.Debug("DSTRegion: {0} Report URL: {1}", dealResponse.Data.Region, dealResponse.Data.ReportURL);


            _logger.Debug("Initializing ACLS...");
            _acl.InitializeACLS(dealResponse.Data);
            await _acl.ACLS.InitializeAsync();

            _logger.Debug("Starting AICL WebSocket server...");
            var aiclWsStart = _aicl.AICLWs.Start();
            if (!aiclWsStart)
            {
                _logger.Error("Cannot start AICLWsServer. Make sure port 29100/tcp is not in use.");
                Exit(1);
            }

            Reporter reporter = new Reporter(_acl, _aicl);
            await reporter.InitializeAsync();
        }

        private void Exit(int exitCode = 0)
        {
            Environment.Exit(exitCode);
        }

        private void LoadMonitorsFromConfig()
        {
            _logger.Information("Initializing Monitors...");

            var asm = Assembly.GetEntryAssembly();
            var ns = asm.GetName().Name;
            var monitors = from a in asm.GetTypes()
                where a.Namespace == $"{ns}.Monitors" && !a.IsInterface
                select a;
            
            foreach (var monitor in monitors)
            {
                _logger.Debug("Found bundled monitor: {0}", monitor.FullName);
                var m = (IMonitor)Activator.CreateInstance(monitor, _computer)!;
                _monitors.Add(m);
            }

            _logger.Information("Initialized {0} monitors", monitors.Count());
        }

        private bool IsAuroraKeyExist()
        {
            return _options.AuroraKey is not ("PLEASE_RUN_CONFIGURATOR" or null or "");
        }
    }
}
