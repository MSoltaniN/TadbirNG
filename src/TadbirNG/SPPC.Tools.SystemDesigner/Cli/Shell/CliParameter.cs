using System;
using System.Collections.Generic;

namespace SPPC.Tools.SystemDesigner.Cli
{
    public class CliParameter
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool AllowMultiple { get; set; }
    }
}
