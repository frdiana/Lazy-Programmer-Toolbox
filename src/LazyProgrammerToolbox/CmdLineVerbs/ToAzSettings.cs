using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace LazyProgrammerToolbox.CmdLineVerbs
{
    [Verb("toAzSettings", HelpText = "Convert a dotnet core appsettings.json to azure settings format")]
    public class ToAzSettings
    {
        [Option("input", Required = true, HelpText = "Input appsettings.json file")]
        public string Input { get; set; }


        [Option("output", Required = true, HelpText = "Output azure config file ")]
        public string Output { get; set; }
    }
}
