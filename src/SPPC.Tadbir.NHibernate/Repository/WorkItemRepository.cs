using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
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
        /// رکورد کار مورد نیاز در ابتدای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        public void CreateInitialWorkItem(WorkItemViewModel workItem)
        {
            Verify.ArgumentNotNull(workItem, "workItem");
            var transactionRepository = _unitOfWork.GetRepository<Transaction>();
            var transaction = transactionRepository.GetByID(workItem.DocumentId);
            if (transaction != null)
            {
                transaction.Status = workItem.Status;
                transaction.OperationalStatus = workItem.OperationalStatus;
                transactionRepository.Update(transaction);

                var itemRepository = _unitOfWork.GetRepository<WorkItem>();
                var newWorkItem = _mapper.Map<WorkItem>(workItem);
                itemRepository.Insert(newWorkItem);

                var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
                var document = new WorkItemDocumentViewModel()
                {
                    WorkItemId = newWorkItem.Id,
                    DocumentId = workItem.DocumentId
                };
                var newDocument = _mapper.Map<WorkItemDocument>(document);
                documentRepository.Insert(newDocument);

                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var history = _mapper.Map<WorkItemHistory>(workItem);
                historyRepository.Insert(history);
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// رکورد کار مورد نیاز در گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        public void CreateWorkItem(WorkItemViewModel workItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// رکورد کار مورد نیاز در انتهای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        public void CreateFinalWorkItem(WorkItemViewModel workItem)
        {
            throw new NotImplementedException();
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
