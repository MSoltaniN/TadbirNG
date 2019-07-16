using System;
using System.Collections.Generic;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class AppSettingsModel
    {
        public ConnectionStringsModel ConnectionStrings { get; set; }

        public LoggingModel Logging { get; set; }
    }
}
