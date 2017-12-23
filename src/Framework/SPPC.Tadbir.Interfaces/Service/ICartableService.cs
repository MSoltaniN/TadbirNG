using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مرتبط با مشاهده کارتابل ها را تعریف می کند.
    /// </summary>
    public interface ICartableService
    {
        /// <summary>
        /// کارهای موجود در کارتابل ورودی یک کاربر خاص را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از رکوردهای اطلاعاتی در کارتابل ورودی کاربر داده شده</returns>
        IEnumerable<InboxItemViewModel> GetUserInbox(int userId);

        /// <summary>
        /// کارهای موجود در کارتابل ارسالی یک کاربر خاص را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از رکوردهای اطلاعاتی در کارتابل ارسالی کاربر داده شده</returns>
        IEnumerable<OutboxItemViewModel> GetUserOutbox(int userId);
    }
}
