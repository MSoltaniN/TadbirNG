﻿using System;
using System.Collections.Generic;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class CliParameter
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool AllowMultiple { get; set; }
    }
}
