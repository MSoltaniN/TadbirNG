using System;
using System.Collections.Generic;

namespace SPPC.Framework.Tools.ProjectCLI
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
