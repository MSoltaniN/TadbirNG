using System.Collections.Generic;

namespace SPPC.Tools.SystemDesignerCli
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
