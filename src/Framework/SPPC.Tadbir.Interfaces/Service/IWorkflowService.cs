using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گردش های کاری را تعریف می کند.
    /// </summary>
    public interface IWorkflowService
    {
        /// <summary>
        /// مجموعه ای از اطلاعات گردش های کاری در حال اجرا در برنامه را برمی گرداند
        /// </summary>
        /// <returns>مجموعه اطلاعات گردش های کاری در حال اجرا</returns>
        IEnumerable<WorkflowInstanceViewModel> GetRunningWorkflows();
    }
}
