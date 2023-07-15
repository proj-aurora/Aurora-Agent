using CommandLine;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Shared.Options;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System.Text;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace Agent
{
    internal class Program
    {
        private static ILogger _logger;
        private static AgentCLIOptions _options;
        static async Task Main(string[] args)
        {
            Parser.Default.ParseArguments<AgentCLIOptions>(args)
                .WithParsed(o => { _options = o; });

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[Aurora [{Timestamp:HH:mm:ss}] [{SourceContext}] {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();

            _logger = Log.Logger.ForContext<Program>();

            var dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "aurora");

            var config = Path.Combine(dir, "config.json");
            ENTRY:
            if (!File.Exists(config))
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                await File.WriteAllTextAsync(config, JsonConvert.SerializeObject(new AgentOptions()));
                _logger.Information("Created new config file under {0}", config);
            }

            _logger.Information("Loaded config from {0}", config);

            AgentOptions opt = JsonConvert.DeserializeObject<AgentOptions>(await File.ReadAllTextAsync(config));

            if (_options.ConfigurationMode)
            {
                _options.ConfigurationMode = false;
                Configurator configurator = new(opt);
                await configurator.RunAsync();
                goto ENTRY;
            }

            Bootstrap bootstrap = new(opt);
            await bootstrap.RunAsync();
            await Task.Delay(-1);
        }
    }
}