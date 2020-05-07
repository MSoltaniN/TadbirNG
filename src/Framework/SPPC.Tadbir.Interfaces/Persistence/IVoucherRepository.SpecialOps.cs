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
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetOpeningVoucherAsync();

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
            IList<InventoryBalanceViewModel> balanceItems);

        /// <summary>
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingVoucherAsync();
    }
}
