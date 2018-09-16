using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class LoggingRepository<TEntity, TEntityView> : RepositoryBase, ILoggingRepository<TEntity, TEntityView>
        where TEntity : FiscalEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public LoggingRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository logRepository)
            : base(unitOfWork, mapper, metadata)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی جدید را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید ذخیره شود</param>
        public async Task InsertAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnAction("Create", null, entity);
            repository.Insert(entity);
            await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی اصلاح شده را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که تغییرات آن باید ذخیره شود</param>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات سطر اطلاعاتی</param>
        public async Task UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Edit", clone, null);
            UpdateExisting(entityView, entity);
            Log.AfterState = GetState(entity);
            repository.Update(entity);
            await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        public async Task DeleteAsync(IRepository<TEntity> repository, TEntity entity)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Delete", clone, null);
            DisconnectEntity(entity);
            repository.Delete(entity);
            await FinalizeActionAsync();
        }

        /// <summary>
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        protected OperationLogViewModel Log { get; private set; }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected abstract string GetState(TEntity entity);

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="entity">سطر اطلاعاتی موجود</param>
        protected abstract void UpdateExisting(TEntityView entityView, TEntity entity);

        private static OperationLogViewModel GetOperationLog(string action, TEntity entity)
        {
            var log = new OperationLogViewModel()
            {
                Action = action,
                FiscalPeriodId = entity.FiscalPeriodId,
                BranchId = entity.BranchId,
                CompanyId = entity.Branch.CompanyId,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                Result = "Succeeded",
                Entity = typeof(TEntity).Name,
                UserId = 1
            };
            return log;
        }

        private static void DisconnectEntity(TEntity entity)
        {
            var relations = Reflector
                .GetPropertyNames(entity)
                .Where(prop => Reflector.GetPropertyType(entity, prop)
                    .Namespace.StartsWith(ModelNamespace))
                .ToArray();
            Array.ForEach(relations, prop => Reflector.SetProperty(entity, prop, null));
        }

        private void OnAction(string action, TEntity before, TEntity after)
        {
            var entity = before ?? after;
            Log = GetOperationLog(action, entity);
            Log.BeforeState = GetState(before);
            Log.AfterState = GetState(after);
        }

        private async Task FinalizeActionAsync()
        {
            try
            {
                await UnitOfWork.CommitAsync();
                await TrySaveLogAsync();
            }
            catch (Exception ex)
            {
                Log.Result = "Failed";
                Log.ErrorMessage = ex.Message;
                await TrySaveLogAsync();
                throw;
            }
        }

        private async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(Environment.NewLine);
                Debug.WriteLine("WARNING: Could not create operation log.");
                Debug.WriteLine("    More Info : {0}", ex);

                // Ignored (logging should not throw exception)
            }
        }

        private const string ModelNamespace = "SPPC.Tadbir.Model";
        private readonly IOperationLogRepository _logRepository;
    }
}
