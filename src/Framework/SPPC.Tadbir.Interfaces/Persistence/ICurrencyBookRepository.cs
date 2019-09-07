using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    public interface ICurrencyBookRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "ساده : مطابق ردیف های سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookByRowAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ هر سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookVoucherSumAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookDailySumAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookMonthlySumAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "ساده : مطابق ردیف های سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookByRowByBranchAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ هر سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookVoucherSumByBranchAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookDailySumByBranchAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookMonthlySumByBranchAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، تمامی ارزهای استفاده شده در آرتیکل های سند را به همراه مجموع بدهکار و بستانکار برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns></returns>
        Task<CurrencyBookViewModel> GetCurrencyBookAllCurrenciesAsync(
            CurrencyBookParamViewModel bookParam,
            GridOptions gridOptions);
    }
}
