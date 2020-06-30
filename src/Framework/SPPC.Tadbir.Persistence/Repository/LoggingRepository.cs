using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class LoggingRepository<TEntity, TEntityView> : LoggingRepositoryBase<TEntity, TEntityView>
        where TEntity : class, IEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public LoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// به روش آسنکرون، رکورد مشخص شده با شناسه دیتابیسی داده شده را
        /// به همراه کلیه اطلاعات وابسته به آن از دیتابیس حذف می کند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی رکورد مورد نظر برای حذف</param>
        protected async Task DeleteWithCascadeAsync(int id)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var dependentTypes = ModelCatalogue.GetAllDependentsOfType(typeof(TEntity));
            foreach (var dependentType in dependentTypes)
            {
                var items = GetReferencedItems(id, typeof(TEntity), dependentType);
                DeleteWithCascade(typeof(TEntity), id, dependentType, items);
            }

            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var entity = await repository.GetByIDAsync(id);
            await DeleteAsync(repository, entity);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تغییر وضعیت موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="newStatus">وضعیت ثبتی جدید برای موجودیت عملیاتی</param>
        /// <param name="oldStatus"></param>
        protected void OnDocumentStatus(DocumentStatusValue newStatus, DocumentStatusValue oldStatus)
        {
            OperationId operation = OperationId.None;
            switch (newStatus)
            {
                case DocumentStatusValue.Draft:
                    operation = OperationId.UndoCheck;
                    break;
                case DocumentStatusValue.Checked:
                    if (oldStatus == DocumentStatusValue.Finalized)
                    {
                        operation = OperationId.UndoFinalize;
                    }
                    else
                    {
                        operation = OperationId.Check;
                    }

                    break;
                case DocumentStatusValue.Finalized:
                    operation = OperationId.Finalize;
                    break;
                default:
                    break;
            }

            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تایید یا برگشت از تایید موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="isConfirmed">مشخص می کند که وضعیت تایید جدید، تایید شده است یا نه؟</param>
        protected void OnDocumentConfirmation(bool isConfirmed)
        {
            OperationId operation = isConfirmed ? OperationId.Confirm : OperationId.UndoConfirm;
            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تصویب یا برگشت از تصویب موجودیت عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="isApproved">مشخص می کند که وضعیت تصویب جدید، تصویب شده است یا نه؟</param>
        protected void OnDocumentApproval(bool isApproved)
        {
            OperationId operation = isApproved ? OperationId.Approve : OperationId.UndoApprove;
            OnEntityAction(operation);
        }

        /// <summary>
        /// یک رکورد لاگ عملیاتی برای عملیات تغییر وضعیت های گروهی موجودیت های عملیاتی ایجاد می کند
        /// </summary>
        /// <param name="itemIds">شناسه اسناد</param>
        /// <param name="operation">شناسه عملیات گروهی</param>
        protected async Task OnEntityGroupChangeStatus(IEnumerable<int> itemIds, OperationId operation)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(String.Format(
                "{0} : {1}", AppStrings.VoucherCount, itemIds.Count()));
            await TrySaveLogAsync();
        }

        /// <summary>
        /// رکورد لاگ عملیاتی را در جدول مرتبط ایجاد می کند.
        /// </summary>
        /// <remarks>توجه : هر گونه خطای زمان اجرا حین عملیات، نادیده گرفته می‌شود</remarks>
        protected override async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
                ReportLoggingError(ex);

                // Ignored (logging should not throw exception)
            }
        }

        /// <summary>
        /// کد عملیات گروهی اسناد را با توجه به وضعیت قدیم و جدید آنها به دست می آورد
        /// </summary>
        /// <param name="newStatus">وضعیت  جدید سند حسابداری</param>
        /// <param name="oldStatus">وضعیت قبلی سند حسابداری</param>
        protected OperationId GetGroupOperationCode(DocumentStatusValue newStatus, DocumentStatusValue oldStatus)
        {
            OperationId operation = OperationId.None;
            switch (newStatus)
            {
                case DocumentStatusValue.Draft:
                    operation = OperationId.UndoGroupCheck;
                    break;
                case DocumentStatusValue.Checked:
                    if (oldStatus == DocumentStatusValue.Finalized)
                    {
                        operation = OperationId.UndoGroupFinalize;
                    }
                    else
                    {
                        operation = OperationId.GroupCheck;
                    }

                    break;
                case DocumentStatusValue.Finalized:
                    operation = OperationId.GroupFinalize;
                    break;
                default:
                    break;
            }

            return operation;
        }

        private void DeleteWithCascade(Type parentType, int parentId, Type type, IEnumerable<int> ids)
        {
            if (ids.Count() == 0)
            {
                return;
            }

            var dependentTypes = ModelCatalogue.GetAllDependentsOfType(type);
            foreach (var dependentType in dependentTypes)
            {
                foreach (int id in ids)
                {
                    var items = GetReferencedItems(id, type, dependentType);
                    DeleteWithCascade(type, id, dependentType, items);
                }
            }

            string keyName = parentType == typeof(DetailAccount)
                ? "Detail"
                : parentType.Name;
            var idItems = ModelCatalogue.GetModelTypeItems(type);
            string command = String.Format("DELETE FROM [{0}].[{1}] WHERE {2}ID = {3}",
                idItems[0], idItems[1], keyName, parentId);
            DbConsole.ExecuteNonQuery(command);
        }

        private readonly IOperationLogRepository _logRepository;
    }
}
