using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مرتبط با علامتگذاری آرتیکل سند را نگهداری می کند
    /// </summary>
    public class VoucherLineMarkViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherLineMarkViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی آرتیکل سند
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// علامتگذاری آرتیکل
        /// </summary>
        public string Mark { get; set; }
    }
}
