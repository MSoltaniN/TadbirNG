using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    /// <summary>
    /// اطلاعات نمایشی کامل یک درخواست کالا را به همراه اطلاعات سطرهای آن نشان می دهد
    /// </summary>
    public class RequisitionFullViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RequisitionFullViewModel()
        {
            Lines = new List<VoucherLineSummaryViewModel>();
        }

        /// <summary>
        /// اطلاعات کامل درخواست کالا
        /// </summary>
        public RequisitionVoucherViewModel Voucher { get; set; }

        /// <summary>
        /// مجموعه ای از اطلاعات سطرها (آرتیکل ها) در درخواست کالا
        /// </summary>
        public IList<VoucherLineSummaryViewModel> Lines { get; private set; }
    }
}
