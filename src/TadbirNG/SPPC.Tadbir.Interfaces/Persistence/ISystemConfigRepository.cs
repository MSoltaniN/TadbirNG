using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Configuration.Models;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات سیستمی و مستقل از محتوا را تعریف می کند
    /// </summary>
    public interface ISystemConfigRepository : IBaseConfigRepository
    {
        /// <summary>
        /// به روش آسنکرون، تمام تنظیمات کاربری موجود برای فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>تنظیمات کاربری موجود برای فرم های لیستی</returns>
        Task<IList<ListFormViewConfig>> GetListViewConfigByUserAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود برای یکی از فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای یکی از فرم های لیستی</returns>
        Task<ListFormViewConfig> GetListViewConfigByUserAsync(int userId, int viewId);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای فرم لیستی</param>
        Task SaveUserListConfigAsync(int userId, ListFormViewConfig userConfig);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود برای گزارش فوری برای یکی از فرم های لیستی را
        /// برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای گزارش فوری</returns>
        Task<QuickReportConfig> GetQuickReportConfigAsync(int userId, int viewId);

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای گزارش فوری برای یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای گزارش فوری</param>
        Task SaveQuickReportConfigAsync(int userId, QuickReportConfig userConfig);

        /// <summary>
        /// به روش آسنکرون، شناسه دیتابیسی متناظر با کد دو حرفی استاندارد یک زبان را خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeCode">کد دو حرفی استاندارد زبان مورد نظر</param>
        /// <returns>شناسه دیتابیسی متناظر با کد زبانی داده شده</returns>
        Task<int> GetLocaleIdAsync(string localeCode);
    }
}
