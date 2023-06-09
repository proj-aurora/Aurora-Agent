using CommandLine;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Shared.Options;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Agent
{
    internal class Program
    {
        private static ILogger _logger;
        
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[Aurora [{Timestamp:HH:mm:ss}] [{SourceContext}] {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();

            _logger = Log.Logger.ForContext<Program>();

            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "aurora");

            var config = Path.Combine(dir, "config.json");

            if (!File.Exists(config))
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                await File.WriteAllTextAsync(config, JsonConvert.SerializeObject(new AgentOptions()));
                _logger.Information("Created new config file under {0}", config);
            }

            _logger.Information("Loaded config from {0}", config);  

            Parser.Default.ParseArguments<AgentCLIOptions>(args)
                .WithParsed(o =>
                {
                    if (o.ConfigPath != null)
                    {
                        config = o.ConfigPath;
                    }
                });

            AgentOptions opt = JsonConvert.DeserializeObject<AgentOptions>(await File.ReadAllTextAsync(config));

            Bootstrap bootstrap = new(opt);
            await bootstrap.RunAsync();
            await Task.Delay(-1);
        }
    }
}