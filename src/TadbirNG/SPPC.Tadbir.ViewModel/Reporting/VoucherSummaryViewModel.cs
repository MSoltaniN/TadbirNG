using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SPPC.Tadbir.ViewModel.Reporting
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
        public int No { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// جمع مبالغ بدهکار در آرتیکل های سند مالی
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار در آرتیکل های سند مالی
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// مبلغ اختلاف میان جمع بدهکار و جمع بستانکار
        /// </summary>
        public decimal Difference { get; set; }

        /// <summary>
        /// تنظیم کننده سند مالی شامل نام و نام خانوادگی با قالب نمایشی پیش فرض سیستم
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// وضعیت تراز سند مالی که یکی از مقادیر تراز یا ناتراز را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string BalanceStatus { get; set; }

        /// <summary>
        /// وضعیت ثبت سند مالی که یکی از مقادیر ثبت نشده، ثبت عادی یا ثبت قطعی
        /// را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// ماخذ سند را در زبان جاری برنامه نمایش می دهد
        /// </summary>
        public string OriginName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر صادرکننده سند مالی
        /// </summary>
        public int IssuedById { get; set; }

        /// <summary>
        /// شرح سند مالی که جزئیات بیشتری را در مورد پیشامد مالی ارائه می دهد
        /// </summary>
        public string Description { get; set; }
    }
}
