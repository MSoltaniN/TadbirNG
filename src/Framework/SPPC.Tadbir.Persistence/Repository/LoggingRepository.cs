using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Config;

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
        /// <param name="config">امکان خواندن تنظیمات جاری ایجاد لاگ را فراهم می کند</param>
        public LoggingRepository(IRepositoryContext context, ILogConfigRepository config,
            IOperationLogRepository logRepository)
            : base(context, config)
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
        /// <param name="status">وضعیت ثبتی جدید برای موجودیت عملیاتی</param>
        protected void OnDocumentStatus(DocumentStatusValue status)
        {
            OperationId operation = OperationId.None;
            switch (status)
            {
                case DocumentStatusValue.Draft:
                    operation = OperationId.UndoCheck;
                    break;
                case DocumentStatusValue.Checked:
                    operation = OperationId.Check;
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
        /// به روش آسنکرون، تنظیمات لاگ را برای موجودیت و عملیات داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="operation">عملیات مورد نظر برای خواندن تنظیمات لاگ</param>
        /// <param name="entity">موجودیت مورد نظر برای تنظیمات لاگ</param>
        /// <returns>تنظیمات لاگ برای موجودیت و عملیات مورد نظر</returns>
        protected override async Task<LogSettingViewModel> GetEntityLogConfigByOperationAsync(
            int operation, int entity)
        {
            return await GetLogConfigAsync(
                cfg => cfg.Operation.Id == operation && cfg.EntityType.Id == entity);
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات لاگ را برای فرم و عملیات داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="operation">عملیات مورد نظر برای خواندن تنظیمات لاگ</param>
        /// <param name="source">فرم عملیاتی مورد نظر برای تنظیمات لاگ</param>
        /// <returns>تنظیمات لاگ برای فرم و عملیات مورد نظر</returns>
        protected override async Task<LogSettingViewModel> GetSourceLogConfigByOperationAsync(
            int operation, int source)
        {
            return await GetLogConfigAsync(
                cfg => cfg.Operation.Id == operation && cfg.Source.Id == source);
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

        private async Task<LogSettingViewModel> GetLogConfigAsync(Expression<Func<LogSetting, bool>> criteria)
        {
            var configResult = default(LogSettingViewModel);
            var repository = UnitOfWork.GetAsyncRepository<LogSetting>();
            var config = await repository.GetSingleByCriteriaAsync(criteria);
            if (config != null)
            {
                configResult = Mapper.Map<LogSettingViewModel>(config);
            }

            return configResult;
        }

        private readonly IOperationLogRepository _logRepository;
    }
}
