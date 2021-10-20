using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class EntityActionsModel
    {
        public EntityActionsModel()
        {
            CustomActions = new List<string>();
            HasView =
                HasCreate =
                HasModify =
                HasDelete =
                HasFilter =
                HasPrint =
                HasExport = true;
        }

        public bool HasView { get; set; }

        public bool HasCreate { get; set; }

        public bool HasModify { get; set; }

        public bool HasDelete { get; set; }

        public bool HasFilter { get; set; }

        public bool HasPrint { get; set; }

        public bool HasExport { get; set; }

        public List<string> CustomActions { get; }
    }
}
