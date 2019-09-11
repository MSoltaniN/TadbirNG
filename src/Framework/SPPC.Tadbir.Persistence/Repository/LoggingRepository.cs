using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class LoggingRepository<TEntity, TEntityView> : RepositoryBase, ILoggingRepository<TEntity, TEntityView>
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
        /// به روش آسنکرون، سطر اطلاعاتی جدید را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید ذخیره شود</param>
        public async Task<bool> InsertAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnAction("Create", null, entity);
            repository.Insert(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی اصلاح شده را در دیتابیس جاری برنامه و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که تغییرات آن باید ذخیره شود</param>
        /// <param name="entityView">مدل نمایشی شامل آخرین تغییرات سطر اطلاعاتی</param>
        public async Task<bool> UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Edit", clone, null);
            UpdateExisting(entityView, entity);
            Log.AfterState = GetState(entity);
            repository.Update(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// به روش آسنکرون، سطر اطلاعاتی قابل حذف را از دیتابیس جاری برنامه حذف و سطر لاگ عملیاتی را
        /// در دیتابیس سیستمی ذخیره می کند
        /// </summary>
        /// <param name="repository">اتصال دیتابیسی به دیتابیس شرکت جاری در برنامه</param>
        /// <param name="entity">سطر اطلاعاتی که باید حذف شود</param>
        public async Task<bool> DeleteAsync(IRepository<TEntity> repository, TEntity entity)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Delete", clone, null);
            DisconnectEntity(entity);
            repository.Delete(entity);
            return await FinalizeActionAsync();
        }

        /// <summary>
        /// مدل نمایشی لاگ عملیاتی برای عملیات جاری
        /// </summary>
        protected OperationLogViewModel Log { get; private set; }

        /// <summary>
        /// تغییرات انجام شده را اعمال کرده و در صورت امکان لاگ عملیاتی را ایجاد می کند
        /// </summary>
        protected async Task<bool> FinalizeActionAsync()
        {
            bool succeeded = false;
            try
            {
                await UnitOfWork.CommitAsync();
                succeeded = true;
                await TrySaveLogAsync();
            }
            catch (Exception ex)
            {
                Log.Result = "Failed";
                Log.ErrorMessage = ex.Message;
                await TrySaveLogAsync();
                throw;
            }

            return succeeded;
        }

        /// <summary>
        /// رکورد لاگ عملیاتی را با توجه به وضعیت قدیم و جدید سطر اطلاعاتی آماده می کند
        /// </summary>
        /// <param name="action">نوع عملیات مورد استفاده در لاگ عملیاتی</param>
        /// <param name="before">وضعیت قدیم سطر اطلاعاتی</param>
        /// <param name="after">وضعیت جدید سطر اطلاعاتی</param>
        protected void OnAction(string action, TEntity before, TEntity after)
        {
            Log = GetOperationLog(action);
            Log.BeforeState = GetState(before);
            Log.AfterState = GetState(after);
        }

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

        private static void DisconnectEntity(TEntity entity)
        {
            var relations = Reflector
                .GetPropertyNames(entity)
                .Where(prop => Reflector.GetPropertyType(entity, prop)
                    .Namespace.StartsWith(ModelNamespace))
                .ToArray();
            Array.ForEach(relations, prop => Reflector.SetProperty(entity, prop, null));
        }

        private OperationLogViewModel GetOperationLog(string action)
        {
            var log = new OperationLogViewModel()
            {
                Action = action,
                FiscalPeriodId = _currentContext.FiscalPeriodId,
                BranchId = _currentContext.BranchId,
                CompanyId = _currentContext.CompanyId,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                Result = "Succeeded",
                Entity = typeof(TEntity).Name,
                UserId = _currentContext.Id
            };
            return log;
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
