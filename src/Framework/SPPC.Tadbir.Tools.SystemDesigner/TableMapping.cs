using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    public class TableMapping
    {
        public TableMapping()
        {
        }

        public string Source { get; set; }

        public string Target { get; set; }

        public string[] RequiredFields { get; private set; }

        public string[] SourceFields { get; set; }
    }
}
