using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبه گردش و مانده برای یک مولفه حساب را تعریف می کند
    /// </summary>
    public interface ITestBalanceUtility : IAccountItemUtility
    {
        /// <summary>
        /// اطلاعات سطوح درختی مورد استفاده را در یک مولفه حساب خوانده و برمی گرداند
        /// </summary>
        /// <returns>سطوح درختی مورد استفاده</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync();

        /// <summary>
        /// اطلاعات سطوح زیرمجموعه موجود را برای یک مولفه حساب خوانده و برمی گرداند
        /// </summary>
        /// <returns>سطوح زیرمجموعه موجود</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync();

        /// <summary>
        /// مولفه حساب مورد استفاده در آرتیکل سند را به پرس و جوی داده شده اضافه می کند
        /// </summary>
        /// <param name="query">پرس و جوی داده شده</param>
        /// <returns>پرس و جوی داده شده که مشتمل بر مولفه حساب مورد نظر در آرتیکل سند است</returns>
        IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query);

        /// <summary>
        /// فیلتر مورد نیاز برای به دست آوردن آرتیکل های سطح درختی داده شده و پایین تر را برمی گرداند
        /// </summary>
        /// <param name="level">سطح درختی مورد نظر</param>
        /// <returns>عبارت لامدای مورد نیاز برای اعمال فیلتر</returns>
        Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level);

        /// <summary>
        /// فیلتر مورد نیاز برای به دست آوردن آرتیکل های سطح درختی داده شده را برمی گرداند
        /// </summary>
        /// <param name="level">سطح درختی مورد نظر</param>
        /// <returns>عبارت لامدای مورد نیاز برای اعمال فیلتر</returns>
        Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level);

        /// <summary>
        /// سطر داده شده از گزارش گردش و مانده را تبدیل به سطر تجمیع شده برای حساب با کد کامل داده شده می کند
        /// </summary>
        /// <param name="line">سطر داده شده از گزارش</param>
        /// <param name="fullCode">کد کامل حساب در سطر تجمیع شده</param>
        /// <returns>اطلاعات نمایشی سطر تجمیع شده</returns>
        Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode);

        /// <summary>
        /// مانده اولیه برای یک مولفه حساب را با توجه به گزینه های گزارش گردش و مانده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="parameters">گزینه های داده شده برای محاسبه گردش و مانده</param>
        /// <returns>مانده اولیه مولفه حساب مورد نظر</returns>
        Task<decimal> GetInitialBalanceAsync(int itemId, TestBalanceParameters parameters);

        /// <summary>
        /// سطرهای بدون مانده و گردش را با توجه به سطرهای داده شده برای گزارش خوانده و برمی گرداند
        /// </summary>
        /// <param name="items">سطرهای داده شده برای گزارش</param>
        /// <param name="mode">نوع گزارش مورد نظر</param>
        /// <returns>سطرهای بدون مانده و گردش</returns>
        Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceMode mode);

        /// <summary>
        /// سطرهای داده شده برای گزارش را با توجه به اطلاعات مولفه حساب داده شده فیلتر کرده و برمی گرداند
        /// </summary>
        /// <param name="lines">سطرهای داده شده برای گزارش</param>
        /// <param name="accountItem">اطلاعات مولفه حساب مورد نظر برای فیلتر</param>
        /// <returns>سطرهای فیلتر شده گزارش</returns>
        IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem);

        /// <summary>
        /// شناسه دیتابیسی مولفه حساب استفاده شده در سطر داده شده از گزارش را برمی گرداند
        /// </summary>
        /// <param name="item">یکی از سطرهای گزارش گردش و مانده</param>
        /// <returns>شناسه مولفه حساب مورد نظر</returns>
        int GetItemId(TestBalanceItemViewModel item);

        /// <summary>
        /// سطرهای داده شده برای گزارش را بر حسب کد کامل یک مولفه حساب مرتب سازی کرده و برمی گرداند
        /// </summary>
        /// <param name="items">سطرهای داده شده برای گزارش</param>
        /// <returns>سطرهای مرتب شده بر اساس کد کامل مولفه حساب</returns>
        IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items);

        /// <summary>
        /// شناسه دیتابیسی نمای لیستی گزارش تراز یا گردش و مانده را با توجه به تعداد ستون های گزارش مشخص می کند
        /// </summary>
        /// <param name="format">قالب گزارش تراز یا گردش و مانده مورد نظر</param>
        /// <returns>شناسه نمای لیستی گزارش</returns>
        int GetSourceList(TestBalanceFormat format);
    }
}
