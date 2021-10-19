using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات پولی یک آرتیکل مالی را نگهداری می کند
    /// </summary>
    public class VoucherLineAmountsViewModel
    {
        /// <summary>
        /// مبلغ بدهکار در آرتیکل مالی
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار در آرتیکل مالی
        /// </summary>
        public decimal Credit { get; set; }
    }
}
