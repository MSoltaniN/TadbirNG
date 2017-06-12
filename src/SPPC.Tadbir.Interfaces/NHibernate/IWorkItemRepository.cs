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
        /// مجموعه کارهای موجود در کارتابل دریافتی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل دریافتی کاربر تعیین شده نمایش داده می شود</returns>
        IList<InboxItemViewModel> GetUserInbox(int userId);

        /// <summary>
        /// یک کار موجود را به کارتابل ورودی نقش سازمانی جدید منتقل می کند.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با کار موجود</param>
        /// <param name="documentType">نوع مستند مرتبط با کار موجود</param>
        /// <param name="newTargetId">شناسه دیتابیسی نقش جدید دریافت کننده کار موجود</param>
        void UpdateWorkItemTarget(int documentId, string documentType, int newTargetId);

        /// <summary>
        /// مجموعه کارهای موجود در کارتابل ارسالی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل ارسالی کاربر تعیین شده نمایش داده می شود</returns>
        IList<OutboxItemViewModel> GetUserOutbox(int userId);

        /// <summary>
        /// رکورد کار مورد نیاز در ابتدای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        void CreateInitialWorkItem(WorkItemViewModel workItem);

        /// <summary>
        /// رکورد کار مورد نیاز در گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        void CreateWorkItem(WorkItemViewModel workItem);

        /// <summary>
        /// رکورد کار مورد نیاز در انتهای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        void CreateFinalWorkItem(WorkItemViewModel workItem);
    }
}
