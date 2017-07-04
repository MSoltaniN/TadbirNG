using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    public class WorkflowEditionViewModel
    {
        public WorkflowEditionViewModel()
        {
        }

        public string Name { get; set; }

        public string LocalName { get; set; }

        public string Provider { get; set; }

        public bool IsDefault { get; set; }
    }
}
