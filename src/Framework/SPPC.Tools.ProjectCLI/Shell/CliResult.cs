using System;
using System.Collections.Generic;

namespace SPPC.Tools.ProjectCLI
{
    public sealed class CliResult
    {
        private CliResult()
        {
        }

        public const int Done = 0;
        public const int MissingConfiguration = -1;
        public const int NoCommand = -2;
        public const int UnsupportedCommand = -3;
        public const int MissingParameter = -4;
        public const int TypeLoadError = -5;
    }
}
