using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    public class WorkflowSettingsViewModel
    {
        public WorkflowSettingsViewModel()
        {
            Workflows = new List<WorkflowViewModel>();
        }

        public IList<WorkflowViewModel> Workflows { get; private set; }
    }
}
