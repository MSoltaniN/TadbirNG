using System;

namespace SPPC.Tools.SystemDesignerCli
{
    public class CliParameter
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool AllowMultiple { get; set; }
    }
}
