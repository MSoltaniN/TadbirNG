using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعاتی نمایشی یک آرتیکل مالی را جهت استفاده در گزارش دفتر روزنامه نگهداری می کند
    /// </summary>
    public class JournalViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی آرتیکل مالی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// شماره سند مالی، ستون شماره در نمای لیستی
        /// </summary>
        public string VoucherNo { get; set; }

        /// <summary>
        /// کد کامل سرفصل حسابداری مورد استفاده در ردیف سند، ستون شماره حساب در نمای لیستی
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری مورد استفاده در ردیف سند، ستون عنوان حساب در نمای لیستی
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// شرح آرتیکل مالی
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مبلغ بدهکار در آرتیکل مالی
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار در آرتیکل مالی
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که آرتیکل برای آن ایجاد شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی سند مالی
        /// </summary>
        public int VoucherStatusId { get; set; }
    }
}
