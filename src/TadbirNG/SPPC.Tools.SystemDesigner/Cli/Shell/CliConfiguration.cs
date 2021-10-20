using System;
using System.Collections.Generic;

namespace SPPC.Tools.SystemDesigner.Cli
{
    public class CliConfiguration
    {
        public CliConfiguration()
        {
            Commands = new List<CliCommand>();
        }

        public IList<CliCommand> Commands { get; set; }
    }
}
