using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Report
{
    /// <summary>
    /// اطلاعات نمایشی مورد نیاز در گزارش خلاصه اسناد حسابداری را نگهداری می کند
    /// </summary>
    public class VoucherSummaryViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VoucherSummaryViewModel()
        {
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
        /// جمع مبالغ بدهکار در آرتیکل های سند مالی
        /// </summary>
        public string DebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار در آرتیکل های سند مالی
        /// </summary>
        public string CreditSum { get; set; }

        /// <summary>
        /// مبلغ اختلاف میان جمع بدهکار و جمع بستانکار
        /// </summary>
        public string Difference { get; set; }

        /// <summary>
        /// تنظیم کننده سند مالی شامل نام و نام خانوادگی با قالب نمایشی پیش فرض سیستم
        /// </summary>
        public string PreparedBy { get; set; }

        /// <summary>
        /// وضعیت تراز سند مالی که یکی از مقادیر تراز یا ناتراز
        /// را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string BalanceStatus { get; set; }

        /// <summary>
        /// وضعیت ثبت سند مالی که یکی از مقادیر یادداشت، ثبت عادی یا ثبت قطعی
        /// را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string CheckStatus { get; set; }

        /// <summary>
        /// ماخذ سند که در حال حاضر فقط مقدار سند عادی را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تنظیم کننده سند مالی
        /// </summary>
        public int PreparedById { get; set; }
    }
}
