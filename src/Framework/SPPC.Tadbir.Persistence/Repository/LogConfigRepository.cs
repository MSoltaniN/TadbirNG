using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت تنظیمات لاگ های عملیاتی را پیاده سازی می کند
    /// </summary>
    public class LogConfigRepository : RepositoryBase, ILogConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public LogConfigRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تنظیمات لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تنظیمات تعریف شده برای لاگ های عملیاتی</returns>
        public Task<IList<LogSettingViewModel>> GetAllConfigAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات لاگ را برای موجودیت و عملیات داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="operation">عملیات مورد نظر برای خواندن تنظیمات لاگ</param>
        /// <param name="entity">موجودیت مورد نظر برای تنظیمات لاگ</param>
        /// <returns>تنظیمات لاگ برای موجودیت و عملیات مورد نظر</returns>
        public async Task<LogSettingViewModel> GetEntityLogConfigByOperationAsync(int operation, int entity)
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
        public async Task<LogSettingViewModel> GetSourceLogConfigByOperationAsync(int operation, int source)
        {
            return await GetLogConfigAsync(
                cfg => cfg.Operation.Id == operation && cfg.Source.Id == source);
        }

        /// <summary>
        /// به روش آسنکرون، تغییرات داده شده برای تنظیمات لاگ های عملیاتی را ذخیره می کند
        /// </summary>
        /// <param name="modified">تنظیمات تغییر یافته مورد نظر برای ذخیره</param>
        public Task SaveModifiedLogConfigAsync(IList<LogSettingViewModel> modified)
        {
            throw new NotImplementedException();
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
    }
}
