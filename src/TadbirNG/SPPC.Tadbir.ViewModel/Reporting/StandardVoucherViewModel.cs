using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات نمایشی مورد نیاز در گزارش فرم مرسوم سند حسابداری را نگهداری می کند
    /// </summary>
    public class StandardVoucherViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public StandardVoucherViewModel()
        {
            Lines = new List<StandardVoucherLineViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی سند مالی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره سند مالی
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// شرح سند مالی
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// نام کامل کاربر صادرکننده سند
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// نوع مفهومی سند - مانند سند عادی، سند پیش نویس و غیره
        /// </summary>
        public short SubjectType { get; set; }

        /// <summary>
        /// مجموعه سطرهای مورد نیاز در فرم مرسوم که ترکیبی از اطلاعات سرفصل ها و آرتیکل ها را شامل می شود
        /// </summary>
        public List<StandardVoucherLineViewModel> Lines { get; }
    }
}
