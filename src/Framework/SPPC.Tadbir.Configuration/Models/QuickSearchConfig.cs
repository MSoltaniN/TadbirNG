using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    public class QuickSearchConfig
    {
        public QuickSearchConfig()
        {
            Columns = new List<QuickSearchColumnConfig>();
        }

        public int ViewId { get; set; }

        public string SearchMode { get; set; }

        public List<QuickSearchColumnConfig> Columns { get; }
    }
}
