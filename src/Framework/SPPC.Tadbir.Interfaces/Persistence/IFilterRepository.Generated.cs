using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت فیلترها را تعریف میکند
    /// </summary>
    public interface IFilterRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه فیلترهای فرم مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی (فرم) مرتبط با فیلتر</param>
        /// <returns>مجموعه ای از فیلترها تعریف شده برای فرم مورد نظر</returns>
        Task<IList<FilterViewModel>> GetFiltersAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، فیلتر با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="filterId">شناسه عددی یکی از فیلترها موجود</param>
        /// <returns>فیلتر مشخص شده با شناسه عددی</returns>
        Task<FilterViewModel> GetFilterAsync(int filterId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک فیلتر را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="filter">فیلتر مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی فیلتر ایجاد یا اصلاح شده</returns>
        Task<FilterViewModel> SaveFilterAsync(FilterViewModel filter);

        /// <summary>
        /// به روش آسنکرون، فیلتر مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="filterId">شناسه عددی فیلتر مورد نظر برای حذف</param>
        Task DeleteFilterAsync(int filterId);

        /// <summary>
        /// به روش آسنکرون مشخص می کند که فیلتر مورد نظر قواعد موجود برای یکتا بودن فیلتر را نقض می کند یا نه
        /// </summary>
        /// <param name="filter">فیلتر مورد نظر برای بررسی</param>
        /// <returns>در صورت نقض شدن قواعد یکتایی مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsDuplicateFilterAsync(FilterViewModel filter);
    }
}
