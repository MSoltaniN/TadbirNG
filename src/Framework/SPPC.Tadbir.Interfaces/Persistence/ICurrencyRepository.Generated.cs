using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارزها را تعریف میکند
    /// </summary>
    public interface ICurrencyRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از ارزها تعریف شده</returns>
        Task<IList<CurrencyViewModel>> GetCurrenciesAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد ارزها</returns>
        Task<int> GetCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، ارز با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currencyId">شناسه عددی یکی از ارزها موجود</param>
        /// <returns>ارز مشخص شده با شناسه عددی</returns>
        Task<CurrencyViewModel> GetCurrencyAsync(int currencyId);

        /// <summary>
        /// اطلاعات استاندارد یک ارز با نام مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="localDbPath">مسیر فیزیکی فایل استاتیک بانک اطلاعاتی ارزهای استاندارد</param>
        /// <param name="nameKey">کلید متن چندزبانه برای نام ارز مورد نظر</param>
        /// <returns>اطلاعات استاندارد ارز</returns>
        CurrencyViewModel GetCurrencyByName(string localDbPath, string nameKey);

        /// <summary>
        /// مجموعه ای از همه ارزهای معتبر شناخته شده را به صورت زوج های کلید-مقدار خوانده و برمی گرداند
        /// </summary>
        /// <param name="localDbPath">مسیر فیزیکی فایل استاتیک بانک اطلاعاتی ارزهای استاندارد</param>
        /// <returns>مجموعه ای از همه ارزهای معتبر شناخته شده</returns>
        IList<KeyValue> GetCurrencyNamesLookup(string localDbPath);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک ارز را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="currency">ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی ارز ایجاد یا اصلاح شده</returns>
        Task<CurrencyViewModel> SaveCurrencyAsync(CurrencyViewModel currency);

        /// <summary>
        /// به روش آسنکرون، ارز مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="currencyId">شناسه عددی ارز مورد نظر برای حذف</param>
        Task DeleteCurrencyAsync(int currencyId);

        /// <summary>
        /// به روش آسنکرون، ارزهای مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteCurrenciesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا ارز مشخص شده با شناسه دیتابیسی قابل حذف هست یا نه؟
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر برای حذف</param>
        /// <returns>در صورتی که ارز مورد نظر قابل حذف باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند.</returns>
        Task<bool> CanDeleteCurrencyAsync(int currencyId);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
