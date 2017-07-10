using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    public interface IWorkflowService
    {
        IEnumerable<WorkflowInstanceViewModel> GetRunningWorkflows();
    }
}
