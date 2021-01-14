using System;
using System.Collections.Generic;

namespace SPPC.Tools.ProjectCLI
{
    public class CliCommand
    {
        public CliCommand()
        {
            Parameters = new List<CliParameter>();
        }

        public string Name { get; set; }

        public string HandlerType { get; set; }

        public IList<CliParameter> Parameters { get; set; }
    }
}
