using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;

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
        /// مجموعه کارهای موجود در کارتابل دریافتی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل دریافتی کاربر تعیین شده نمایش داده می شود</returns>
        public IList<InboxItemViewModel> GetUserInbox(int userId)
        {
            IList<InboxItemViewModel> workItems = new List<InboxItemViewModel>();
            var userRepository = _unitOfWork.GetRepository<User>();
            var user = userRepository.GetByID(userId);
            if (user != null)
            {
                var roleIds = user.Roles
                    .Select(role => role.Id)
                    .ToArray();
                var repository = _unitOfWork.GetRepository<WorkItem>();
                workItems = repository
                    .GetByCriteria(GetInboxCriteria(roleIds))
                    .Select(wi => _mapper.Map<InboxItemViewModel>(wi))
                    .ToList();

                var documentRepository = _unitOfWork.GetRepository<Transaction>();
                foreach (var workItem in workItems)
                {
                    var transaction = documentRepository
                        .GetByCriteria(doc => doc.Document.Id == workItem.DocumentId)
                        .First();
                    workItem.EntityNo = transaction.No;
                    workItem.DocumentStatus = transaction.Document.OperationalStatus;
                }
            }

            return workItems;
        }

        /// <summary>
        /// مجموعه کارهای موجود در کارتابل ارسالی کاربر تعیین شده را از دیتابیس می خواند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یک کاربر موجود</param>
        /// <returns>مجموعه ای از کارها که در کارتابل ارسالی کاربر تعیین شده نمایش داده می شود</returns>
        public IList<OutboxItemViewModel> GetUserOutbox(int userId)
        {
            var repository = _unitOfWork.GetRepository<WorkItemHistory>();
            var workItems = repository
                .GetByCriteria(wih => wih.User.Id == userId)
                .Select(wih => _mapper.Map<OutboxItemViewModel>(wih))
                .ToList();

            var documentRepository = _unitOfWork.GetRepository<Transaction>();
            foreach (var workItem in workItems)
            {
                var transaction = documentRepository
                    .GetByCriteria(doc => doc.Document.Id == workItem.DocumentId)
                    .First();
                workItem.DocumentNo = transaction.No;
            }

            return workItems;
        }

        /// <summary>
        /// رکورد کار مورد نیاز در ابتدای گردش کار را در دیتابیس ایجاد می کند.
        /// </summary>
        /// <param name="workItem">اطلاعات کار جدید با فرمت مدل نمایشی</param>
        public void CreateInitialWorkItem(WorkItemViewModel workItem)
        {
            Verify.ArgumentNotNull(workItem, "workItem");

            // Step 1 : Update transaction status with latest values...
            if (DidUpdateDocument(workItem))
            {
                // Step 2 : Create a new pending work item (and its attached document)
                // using source user and target role for the operation...
                CreateNewWorkItem(workItem);

                // Step 3 : Create history (log) item for this status change...
                CreateHistoryItem(workItem);
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

            // Step 1 : Update transaction status with latest values...
            if (DidUpdateDocument(workItem))
            {
                // Step 2 : Update existing work item for this transaction...
                UpdatePendingWorkItem(workItem);

                // Step 3 : Create history (log) item for this status change...
                CreateHistoryItem(workItem);
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

            // Step 1 : Update transaction status with latest values...
            if (DidUpdateDocument(workItem))
            {
                // Step 2 : Delete existing work item for this transaction...
                DeletePendingWorkItem(workItem);

                // Step 3 : Create history (log) item for this status change...
                CreateHistoryItem(workItem);
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// یک کار موجود را به کارتابل ورودی نقش سازمانی جدید منتقل می کند.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی مستند مرتبط با کار موجود</param>
        /// <param name="documentType">نوع مستند مرتبط با کار موجود</param>
        /// <param name="newTargetId">شناسه دیتابیسی نقش جدید دریافت کننده کار موجود</param>
        public void UpdateWorkItemTarget(int documentId, string documentType, int newTargetId)
        {
            var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = documentRepository
                .GetByCriteria(wid => wid.Document.Id == documentId && wid.DocumentType == documentType)
                .FirstOrDefault();
            if (document != null)
            {
                var workItemRepository = _unitOfWork.GetRepository<WorkItem>();
                var workItem = workItemRepository.GetByID(document.WorkItem.Id);
                workItem.Target = new Role() { Id = newTargetId };
                workItemRepository.Update(workItem);
            }

            _unitOfWork.Commit();
        }

        // NOTE: This function should later be implemented in a generic manner (using dynamic lambda expressions,
        // instead of a hard-coded switch statement)
        private static Expression<Func<WorkItem, bool>> GetInboxCriteria(int[] roles)
        {
            Expression<Func<WorkItem, bool>> criteria = null;
            switch (roles.Length)
            {
                case 1:
                    criteria = (wi => wi.Target.Id == roles[0]);
                    break;
                case 2:
                    criteria = (wi => wi.Target.Id == roles[0] || wi.Target.Id == roles[1]);
                    break;
                case 3:
                    criteria = (wi => wi.Target.Id == roles[0] || wi.Target.Id == roles[1]
                        || wi.Target.Id == roles[2]);
                    break;
                case 4:
                    criteria = (wi => wi.Target.Id == roles[0] || wi.Target.Id == roles[1]
                        || wi.Target.Id == roles[2] || wi.Target.Id == roles[3]);
                    break;
                case 0:
                default:
                    criteria = (wi => false);
                    break;
            }

            return criteria;
        }

        private bool DidUpdateDocument(WorkItemViewModel workItem)
        {
            bool didUpdate = false;
            var repository = _unitOfWork.GetRepository<Document>();
            var document = repository.GetByID(workItem.DocumentId);
            if (document != null)
            {
                var action = document.Actions.First();
                document.Status = new DocumentStatus() { Id = workItem.StatusId };
                document.OperationalStatus = workItem.OperationalStatus;
                if (workItem.OperationalStatus == DocumentStatusName.Confirmed)
                {
                    action.ConfirmedBy = new User() { Id = workItem.CreatedById };
                    action.ConfirmedDate = DateTime.Now;
                }

                if (workItem.OperationalStatus == DocumentStatusName.Approved)
                {
                    action.ApprovedBy = new User() { Id = workItem.CreatedById };
                    action.ApprovedDate = DateTime.Now;
                }

                repository.Update(document);
                didUpdate = true;
            }

            return didUpdate;
        }

        private void CreateNewWorkItem(WorkItemViewModel workItem)
        {
            // Step 1 : Insert work item record...
            var itemRepository = _unitOfWork.GetRepository<WorkItem>();
            var newWorkItem = _mapper.Map<WorkItem>(workItem);
            itemRepository.Insert(newWorkItem);

            // Step 2 : Insert related document (transaction) record...
            var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = new WorkItemDocumentViewModel()
            {
                WorkItemId = newWorkItem.Id,
                EntityId = workItem.EntityId,
                DocumentId = workItem.DocumentId,
                DocumentType = workItem.DocumentType
            };
            var newDocument = _mapper.Map<WorkItemDocument>(document);
            documentRepository.Insert(newDocument);
        }

        private void UpdatePendingWorkItem(WorkItemViewModel workItem)
        {
            // Step 2 : Create a new pending work item using source and target identities...
            var itemRepository = _unitOfWork.GetRepository<WorkItem>();
            var newWorkItem = _mapper.Map<WorkItem>(workItem);
            itemRepository.Insert(newWorkItem);

            // Step 3 : Correlate existing document record for transaction with new work item...
            var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = documentRepository
                .GetByCriteria(item => item.Document.Id == workItem.DocumentId)
                .First();
            int pendingId = document.WorkItem.Id;
            document.WorkItem = new WorkItem() { Id = newWorkItem.Id };
            documentRepository.Update(document);

            // Step 4 : Delete previous (pending) work item...
            var pendingItem = itemRepository.GetByID(pendingId);
            itemRepository.Delete(pendingItem);
        }

        private void DeletePendingWorkItem(WorkItemViewModel workItem)
        {
            // Step 1 : Delete workflow document record, since workflow is about to be completed...
            var documentRepository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = documentRepository
                .GetByCriteria(item => item.Document.Id == workItem.DocumentId)
                .First();
            int pendingId = document.WorkItem.Id;
            documentRepository.Delete(document);

            // Step 2 : Delete pending work item, since workflow is about to be completed...
            var itemRepository = _unitOfWork.GetRepository<WorkItem>();
            var pendingItem = itemRepository.GetByID(pendingId);
            itemRepository.Delete(pendingItem);
        }

        private void CreateHistoryItem(WorkItemViewModel workItem)
        {
            var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
            var history = _mapper.Map<WorkItemHistory>(workItem);
            historyRepository.Insert(history);
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
