using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت تنظیمات لاگ های عملیاتی را تعریف می کند
    /// </summary>
    public interface ILogConfigRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه تنظیمات لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تنظیمات تعریف شده برای لاگ های عملیاتی</returns>
        Task<IList<LogSettingNodeViewModel>> GetAllConfigAsync();

        /// <summary>
        /// به روش آسنکرون، کلیه تنظیمات لاگ های سیستمی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تنظیمات تعریف شده برای لاگ های سیستمی</returns>
        Task<IList<LogSettingNodeViewModel>> GetAllSystemConfigAsync();

        /// <summary>
        /// به روش آسنکرون، تغییرات داده شده برای تنظیمات لاگ های عملیاتی را ذخیره می کند
        /// </summary>
        /// <param name="modified">تنظیمات تغییر یافته مورد نظر برای ذخیره</param>
        Task SaveModifiedConfigAsync(IList<LogSettingItemViewModel> modified);

        /// <summary>
        /// به روش آسنکرون، تغییرات داده شده برای تنظیمات لاگ های سیستمی را ذخیره می کند
        /// </summary>
        /// <param name="modified">تنظیمات تغییر یافته مورد نظر برای ذخیره</param>
        Task SaveModifiedSystemConfigAsync(IList<LogSettingItemViewModel> modified);
    }
}
