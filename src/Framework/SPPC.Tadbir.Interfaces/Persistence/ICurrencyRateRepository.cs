using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات نرخ های ارز را تعریف می کند
    /// </summary>
    public interface ICurrencyRateRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه نرخ های ثبت شده برای ارز مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>مجموعه نرخ های ثبت شده برای ارز مورد نظر</returns>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        Task<PagedList<CurrencyRateViewModel>> GetCurrencyRatesAsync(int currencyId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نرخ ارز با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="rateId">شناسه عددی یکی از نرخ های موجود</param>
        /// <returns>اطلاعات نرخ ارز مورد نظر</returns>
        Task<CurrencyRateViewModel> GetCurrencyRateAsync(int rateId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک نرخ ارز را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="currencyRate">نرخ ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی نرخ ارز ایجاد یا اصلاح شده</returns>
        Task<CurrencyRateViewModel> SaveCurrencyRateAsync(CurrencyRateViewModel currencyRate);

        /// <summary>
        /// به روش آسنکرون، نرخ ارز مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="rateId">شناسه عددی نرخ ارز مورد نظر برای حذف</param>
        Task DeleteCurrencyRateAsync(int rateId);

        /// <summary>
        /// به روش آسنکرون، نرخ ارزهای مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteCurrencyRatesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا برای ارز مشخص شده نرخ تعریف شده است یا خیر؟
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>اگر برای ارز مشخص شده نرخ تعریف شده باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> CurrencyHasRatesAsync(int currencyId);
    }
}
