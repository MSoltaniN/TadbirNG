using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی  سیستمی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class SystemLoggingRepository<TEntity, TEntityView> : LoggingRepositoryBase<TEntity, TEntityView>
        where TEntity : class, IEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات جاری ایجاد لاگ را فراهم می کند</param>
        public SystemLoggingRepository(IRepositoryContext context, ILogConfigRepository config,
            IOperationLogRepository logRepository)
            : base(context, config)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات لاگ را برای موجودیت و عملیات داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="operation">عملیات مورد نظر برای خواندن تنظیمات لاگ</param>
        /// <param name="entity">موجودیت مورد نظر برای تنظیمات لاگ</param>
        /// <returns>تنظیمات لاگ برای موجودیت و عملیات مورد نظر</returns>
        protected override async Task<LogSettingViewModel> GetEntityLogConfigByOperationAsync(int operation, int entity)
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
        protected override async Task<LogSettingViewModel> GetSourceLogConfigByOperationAsync(int operation, int source)
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
                await _logRepository.SaveSystemLogAsync(Log);
            }
            catch (Exception ex)
            {
                ReportLoggingError(ex);

                // Ignored (logging should not throw exception)
            }
        }

        private async Task<LogSettingViewModel> GetLogConfigAsync(Expression<Func<SysLogSetting, bool>> criteria)
        {
            var configResult = default(LogSettingViewModel);
            var repository = UnitOfWork.GetAsyncRepository<SysLogSetting>();
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
