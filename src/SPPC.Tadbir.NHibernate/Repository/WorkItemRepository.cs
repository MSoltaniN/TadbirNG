using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Workflow;
using SwForAll.Platform.Common;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت کارهای یک فرآیند کسب و کار را پیاده سازی می کند.
    /// </summary>
    public class WorkItemRepository : IWorkItemRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند.
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public WorkItemRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// مجموعه کارهای موجود در کارتابل دریاقتی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل دریافتی کاربر تعیین شده نمایش داده می شود</returns>
        public IList<WorkItemViewModel> GetUserInbox(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// مجموعه کارهای موجود در کارتابل ارسالی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل ارسالی کاربر تعیین شده نمایش داده می شود</returns>
        public IList<WorkItemViewModel> GetUserOutbox(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// یک واحد کاری را در دستابیس ذخیره می کند.
        /// </summary>
        /// <param name="workItem">کار جدید یا یک کار موجود</param>
        public void SaveWorkItem(WorkItemViewModel workItem)
        {
            Verify.ArgumentNotNull(workItem, "workItem");
            Debug.WriteLine(
                "New Work Item: ['CreatedByID' = {0}, 'Target' = {1}, 'Date' = {2}, " +
                "'Time' = {3}, 'Title' = {4}, 'DocumentType' = Transaction, 'DocumentID' = {5}, " +
                "'Remarks' = {6}]",
                workItem.CreatedById, workItem.TargetRole, workItem.Date.ToShortDateString(), workItem.Time.ToString(),
                workItem.Title, workItem.DocumentId, workItem.Remarks);
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
