using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    public class WorkflowService : IWorkflowService
    {
        public WorkflowService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<WorkflowInstanceViewModel> GetRunningWorkflows()
        {
            var runningWorkflows = _apiClient.Get<IEnumerable<WorkflowInstanceViewModel>>(
                WorkflowApi.RunningWorkflows);
            return runningWorkflows;
        }

        private IApiClient _apiClient;
    }
}
