using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Auth;
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
        Task<IList<CurrencyRateViewModel>> GetCurrencyRatesAsync(int currencyId);

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
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
