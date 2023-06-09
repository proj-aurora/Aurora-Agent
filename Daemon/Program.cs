using CLIOptions;
using CommandLine;
using Newtonsoft.Json;

namespace Daemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Aurora");

            var config = Path.Combine(dir, "config.json");

            if (!File.Exists(config))
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.WriteAllText(config, JsonConvert.SerializeObject(new DaemonOptions()));
            }

            DaemonOptions opt = JsonConvert.DeserializeObject<DaemonOptions>(File.ReadAllText(config));
            Parser.Default.ParseArguments<DaemonOptions>(args)
                .WithParsed(o =>
                {
                    opt = o;
                    Console.WriteLine(JsonConvert.SerializeObject(o));
                });
            
            Bootstrap bootstrap = new(opt);
            bootstrap.Run();
        }
    }
}