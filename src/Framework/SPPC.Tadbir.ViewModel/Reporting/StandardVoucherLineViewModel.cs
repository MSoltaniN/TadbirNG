using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Report
{
    /// <summary>
    /// اطلاعات نمایشی یکی از سطرها را در گزارش فرم مرسوم سند حسابداری را نگهداری می کند
    /// </summary>
    public class StandardVoucherLineViewModel
    {
        /// <summary>
        /// کد کامل سرفصل حسابداری در یکی از سطوح
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری یا شرح آرتیکل
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مبلغ جزء که برای سطر اصلی آرتیکل فقط مقدار غیر صفر خواهد داشت
        /// </summary>
        public decimal PartialAmount { get; set; }

        /// <summary>
        /// مبلغ بدهکار در آرتیکل سند
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار در آرتیکل سند
        /// </summary>
        public decimal Credit { get; set; }
    }
}
