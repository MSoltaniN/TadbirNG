using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.NHibernate
{
    public interface IWorkflowRepository
    {
        IList<WorkflowInstanceViewModel> GetRunningWorkflows();
    }
}
