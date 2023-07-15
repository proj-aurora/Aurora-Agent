using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACL;
using AICL;
using HidSharp.Reports;
using Proto;
using Serilog;
using Timer = System.Timers.Timer;

namespace Agent
{
    internal class Reporter
    {
        private readonly ILogger _logger;
        private readonly AuroraCL _acl;
        private readonly AuroraICL _aicl;
        private readonly Timer _timer;
        private readonly List<AICLFluentBase> _reports;
        
        public Reporter(AuroraCL acl, AuroraICL aicl)
        {
            _acl = acl;
            _aicl = aicl;
            _timer = new Timer(10000);
            _reports = new List<AICLFluentBase>();
            _logger = Log.Logger.ForContext<Reporter>();
        }

        public async Task InitializeAsync()
        {
            _aicl.ReportReceived += data => _reports.Add(data);
            _timer.AutoReset = true;
            _timer.Elapsed += async (sender, args) => await ReportAsync();
            _timer.Start();
        }

        public async Task ReportAsync()
        {
            List<KeyValuePair<string, object>> reports;
            lock (_reports)
            {
                reports = new List<KeyValuePair<string, object>>();
                _reports.ForEach(x => reports.Add(new(x.Tag, x)));
                _reports.Clear();

                _logger.Information("Uploading Reports...");
            }
            await _acl.ACLS.SendReports(reports);
        }
    }
}
