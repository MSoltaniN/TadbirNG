using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات مرتبط با خواندن اطلاعات گردش های کاری را تعریف می کند.
    /// </summary>
    public interface IWorkflowRepository
    {
        /// <summary>
        /// اطلاعات گردش های کاری در حال اجرا در برنامه را از محل ذخیره می خواند
        /// </summary>
        /// <returns>گردش های کاری در حال اجرا</returns>
        IList<WorkflowInstanceViewModel> GetRunningWorkflows();
    }
}
