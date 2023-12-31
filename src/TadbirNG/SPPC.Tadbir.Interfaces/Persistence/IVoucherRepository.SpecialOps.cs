﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// به روش آسنکرون، مشخص می کند که سند ویژه مشخص شده در دوره مالی جاری - در صورت وجود - ثبت شده است یا نه
        /// </summary>
        /// <param name="type">مأخذ سند ویژه مورد نظر</param>
        /// <returns>در صورتی که سند ویژه صادر و ثبت شده باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsCurrentSpecialVoucherCheckedAsync(VoucherOriginId type);

        /// <summary>
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="isQuery">مشخص می کند که در صورت وجود نداشتن، باید
        /// از کاربر تأیید گرفته شود یا نه</param>
        /// <param name="isDefault">مشخص می کند که اولین سند افتتاحیه باید به صورت پیش فرض
        /// و با مبالغ صفر ایجاد شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetOpeningVoucherAsync(bool isQuery = false, bool isDefault = true);

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
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingTempAccountsVoucherAsync(bool mustIssue = true);

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت ادواری خوانده و برمی گرداند
        /// </summary>
        /// <param name="balanceItems">مجموعه مقادیر مانده موجودی انبار - برای سیستم ثبت ادواری</param>
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetPeriodicClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems, bool mustIssue = true);

        /// <summary>
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingVoucherAsync(bool mustIssue = true);
    }
}
