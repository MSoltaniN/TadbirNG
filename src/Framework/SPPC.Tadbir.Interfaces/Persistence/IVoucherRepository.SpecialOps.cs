using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        Task<VoucherViewModel> GetClosingVoucherAsync();
    }
}
