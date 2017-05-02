using System;
using System.Collections.Generic;
using System.Linq;
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
                // Step 1 : Update transaction status with latest values...
                transaction.Status = workItem.Status;
                transaction.OperationalStatus = workItem.OperationalStatus;
                transactionRepository.Update(transaction);

                // Step 2 : Create a new pending work item using source and target identities...
                var itemRepository = _unitOfWork.GetRepository<WorkItem>();
                var newWorkItem = _mapper.Map<WorkItem>(workItem);
                itemRepository.Insert(newWorkItem);

                // Step 3 : Create new document record for transaction...
                var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
                var document = new WorkItemDocumentViewModel()
                {
                    WorkItemId = newWorkItem.Id,
                    DocumentId = workItem.DocumentId
                };
                var newDocument = _mapper.Map<WorkItemDocument>(document);
                documentRepository.Insert(newDocument);

                // Step 4 : Create history (log) item for this status change...
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
            Verify.ArgumentNotNull(workItem, "workItem");
            var transactionRepository = _unitOfWork.GetRepository<Transaction>();
            var transaction = transactionRepository.GetByID(workItem.DocumentId);
            if (transaction != null)
            {
                // Step 1 : Update transaction status with latest values...
                transaction.Status = workItem.Status;
                transaction.OperationalStatus = workItem.OperationalStatus;
                transactionRepository.Update(transaction);

                // Step 2 : Create a new pending work item using source and target identities...
                var itemRepository = _unitOfWork.GetRepository<WorkItem>();
                var newWorkItem = _mapper.Map<WorkItem>(workItem);
                itemRepository.Insert(newWorkItem);

                // Step 3 : Correlate existing document record for transaction with new work item...
                var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
                var document = documentRepository
                    .GetByCriteria(item => item.DocumentId == workItem.DocumentId)
                    .First();
                int pendingId = document.WorkItem.Id;
                document.WorkItem.Id = newWorkItem.Id;
                documentRepository.Update(document);

                // Step 4 : Delete previous (pending) work item...
                var pendingItem = itemRepository.GetByID(pendingId);
                itemRepository.Delete(pendingItem);

                // Step 5 : Create history (log) item for this status change...
                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var history = _mapper.Map<WorkItemHistory>(workItem);
                historyRepository.Insert(history);
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// رکورد کار مورد نیاز در انتهای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        public void CreateFinalWorkItem(WorkItemViewModel workItem)
        {
            Verify.ArgumentNotNull(workItem, "workItem");
            var transactionRepository = _unitOfWork.GetRepository<Transaction>();
            var transaction = transactionRepository.GetByID(workItem.DocumentId);
            if (transaction != null)
            {
                // Step 1 : Update transaction status with latest values...
                transaction.Status = workItem.Status;
                transaction.OperationalStatus = workItem.OperationalStatus;
                transactionRepository.Update(transaction);

                // Step 2 : Delete workflow document record, since workflow is about to be completed...
                var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
                var document = documentRepository
                    .GetByCriteria(item => item.DocumentId == workItem.DocumentId)
                    .First();
                int pendingId = document.WorkItem.Id;
                documentRepository.Delete(document);

                // Step 3 : Delete pending work item, since workflow is about to be completed...
                var itemRepository = _unitOfWork.GetRepository<WorkItem>();
                var pendingItem = itemRepository.GetByID(pendingId);
                itemRepository.Delete(pendingItem);

                // Step 4 : Create history (log) item for final status change...
                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var history = _mapper.Map<WorkItemHistory>(workItem);
                historyRepository.Insert(history);
            }

            _unitOfWork.Commit();
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
