using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات پایه مورد نیاز برای ذخیره و بازیابی تنظیمات را تعریف می کند
    /// </summary>
    public interface IBaseConfigRepository
    {
        /// <summary>
        /// به روش آسنکرون، تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        Task<IList<SettingBriefViewModel>> GetAllConfigAsync();

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت داده شده برای تنظیمات را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات اصلاح شده</param>
        Task SaveConfigAsync(IList<SettingBriefViewModel> configItems);

        /// <summary>
        /// اطلاعات تنظیمات مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="settingId">شناسه دیتابیسی تنظیمات مورد نظر</param>
        /// <returns>اطلاعات نمایشی برای تنظیمات مورد نظر</returns>
        Task<SettingBriefViewModel> GetConfigByIdAsync(int settingId);

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای کلاس تنظیمات مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع تنظیمات مورد نیاز</typeparam>
        /// <returns>تنظیمات موجود برای کلاس تنظیمات مشخص شده</returns>
        Task<TConfig> GetConfigByTypeAsync<TConfig>();
    }
}
