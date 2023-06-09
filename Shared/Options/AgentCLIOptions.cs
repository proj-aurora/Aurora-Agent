﻿using CommandLine;

namespace Shared.Options
{
    [Verb("Agent", HelpText = "Settings related with Agent")]
    public class AgentCLIOptions
    { 
        [Option('c', "config", Required = false, HelpText = "Specifies the path to the config file to use at runtime")]
        public string? ConfigPath { get; set; }
    }
}