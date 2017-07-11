using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گردش های کاری را پیاده سازی می کند.
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public WorkflowService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// مجموعه ای از اطلاعات گردش های کاری در حال اجرا در برنامه را برمی گرداند
        /// </summary>
        /// <returns>مجموعه اطلاعات گردش های کاری در حال اجرا</returns>
        public IEnumerable<WorkflowInstanceViewModel> GetRunningWorkflows()
        {
            var runningWorkflows = _apiClient.Get<IEnumerable<WorkflowInstanceViewModel>>(
                WorkflowApi.RunningWorkflows);
            return runningWorkflows;
        }

        private IApiClient _apiClient;
    }
}
