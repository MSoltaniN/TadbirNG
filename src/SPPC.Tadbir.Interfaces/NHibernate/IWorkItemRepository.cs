using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت کارهای یک فرآیند کسب و کار را تعریف می کند.
    /// </summary>
    public interface IWorkItemRepository
    {
        /// <summary>
        /// مجموعه کارهای موجود در کارتابل دریاقتی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل دریافتی کاربر تعیین شده نمایش داده می شود</returns>
        IList<WorkItemViewModel> GetUserInbox(int userId);

        /// <summary>
        /// مجموعه کارهای موجود در کارتابل ارسالی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل ارسالی کاربر تعیین شده نمایش داده می شود</returns>
        IList<WorkItemViewModel> GetUserOutbox(int userId);

        /// <summary>
        /// یک واحد کاری را در دستابیس ذخیره می کند.
        /// </summary>
        /// <param name="workItem">کار جدید یا یک کار موجود</param>
        void SaveWorkItem(WorkItemViewModel workItem);
    }
}
