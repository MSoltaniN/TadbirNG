using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را تعریف می کند
    /// </summary>
    public interface IConfigRepository
    {
        /// <summary>
        /// تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        Task<IList<SettingBriefViewModel>> GetAllConfigAsync();

        /// <summary>
        /// تنظیمات موجود برای کلاس تنظیمات مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع تنظیمات مورد نیاز</typeparam>
        /// <returns>تنظیمات موجود برای کلاس تنظیمات مشخص شده</returns>
        Task<TConfig> GetConfigByTypeAsync<TConfig>();

        /// <summary>
        /// تمام تنظیمات کاربری موجود برای فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>تنظیمات کاربری موجود برای فرم های لیستی</returns>
        Task<IList<UserSettingViewModel>> GetListViewConfigByUserAsync(int userId);
    }
}
