using CommandLine;

namespace CLIOptions
{
    [Verb("Daemon", HelpText = "Settings related with Daemon")]
    public class DaemonOptions : IOptions
    {
        public string Version { get; } = "v0";

        [Option('p', "daemon-port", Required = false, HelpText = "Set daemon listening port.")]
        public int DaemonPort { get; set; } = 22291;

        [Option('a', "address", Required = false, HelpText = "Set api address which daemon uses")]
        public string Address { get; set; } = "https://api.aurora.local";
    }
}