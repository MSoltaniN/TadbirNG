using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// فیلدهای اطلاعاتی مورد نیاز آرتیکل سند را که برای محاسبات سود و زیان
    /// مورد نیاز هستند نگهداری می کند
    /// </summary>
    public class ProfitLossLineViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب مورد استفاده در آرتیکل سند
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// نام حساب مورد استفاده در آرتیکل سند
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// مبلغ بدهکار در آرتیکل سند
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار در آرتیکل سند
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ایجادکننده آرتیکل سند
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// رفرنس سند مالی
        /// </summary>
        public string VoucherReference { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی سند مالی
        /// </summary>
        public int VoucherStatusId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تاییدکننده سند مالی
        /// </summary>
        public int? VoucherConfirmedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تصویب کننده سند مالی
        /// </summary>
        public int? VoucherApprovedById { get; set; }
    }
}
