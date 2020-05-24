using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Persistence
{
    public partial interface IVoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، مشخص می کند که شعبه داده شده امکان صدور سندهای ویژه را دارد یا نه
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه داده شده</param>
        /// <returns>اگر شعبه داده شده امکان صدور سندهای ویژه را داشته باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> CanIssueSpecialVoucherAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetOpeningVoucherAsync(bool isQuery = false);

        /// <summary>
        /// به روش آسنکرون مشخص می کند که برای دوره مالی قبل سند اختتامیه صادر شده یا نه
        /// </summary>
        /// <returns>در صورتی که دوره مالی قبل سند اختتامیه داشته باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> HasPreviousClosingVoucherAsync();

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت دائمی خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingTempAccountsVoucherAsync();

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت ادواری خوانده و برمی گرداند
        /// </summary>
        /// <param name="balanceItems">مجموعه مقادیر مانده موجودی انبار - برای سیستم ثبت ادواری</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetPeriodicClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems);

        /// <summary>
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingVoucherAsync();
    }
}
