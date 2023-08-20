using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارزها را تعریف میکند
    /// </summary>
    public interface ICurrencyRepository : IRepositoryBase
    {
        /// <summary>
        /// به روش آسنکرون، کلیه ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="activeState">وضعیت مورد نظر برای نمایش اطلاعات بر اساس وضعیت فعال و غیر فعال</param>
        /// <returns>مجموعه ای از ارزها تعریف شده</returns>
        Task<PagedList<CurrencyViewModel>> GetCurrenciesAsync(
            GridOptions gridOptions, int activeState = (int)ActiveState.Active);

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
        /// به روش آسنکرون، ارز پیش فرض برای یک بردار حساب با حساب و شناور مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب در بردار حساب مورد نظر</param>
        /// <param name="faccountId">شناسه دیتابیسی مولفه تفصیلی شناور در بردار حساب مورد نظر</param>
        /// <returns>اطلاعات ارز پیش فرض برای بردار حساب مشخص شده</returns>
        Task<CurrencyInfoViewModel> GetDefaultCurrencyAsync(int accountId, int faccountId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ارزهای مالیاتی تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <returns>ارزهای مالیاتی تعریف شده در دیتابیس شرکت جاری</returns>
        Task<IList<TaxCurrencyViewModel>> GetTaxCurrenciesAsync();

        /// <summary>
        /// به روش آسنکرون، ارزهای مالیاتی تعریف شده در شرکت جاری را به روزرسانی می کند
        /// </summary>
        /// <param name="mdbPath">مسیر فایل بانک اطلاعاتی اکسس مرتبط با ارزهای مالیاتی</param>
        Task UpdateTaxCurrenciesAsync(string mdbPath);

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
        /// به روش آسنکرون، تکراری بودن ارز مشخص شده توسط نمایه بین المللی را بررسی می کند
        /// </summary>
        /// <param name="code">نمایه بین المللی ارز مورد نظر</param>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اگر ارز مشخص شده قبلاً تعریف شده باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsDuplicateCurrencyAsync(string code, int currencyId = 0);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک ارز پایه را ایجاد می کند
        /// </summary>
        /// <param name="currency">ارز مورد نظر برای ایجاد</param>
        /// <returns>اطلاعات نمایشی ارز ایجاد شده</returns>
        Task<CurrencyViewModel> InsertDefaultCurrencyAsync(CurrencyViewModel currency);
    }
}
