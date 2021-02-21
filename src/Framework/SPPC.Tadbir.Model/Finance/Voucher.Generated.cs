// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.584
//     Template Version: 1.0
//     Generation Date: 02/28/1398 04:20:49 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// سند مالی که اطلاعات پولی مرتبط با یک پیشامد مالی را در سازمان نگهداری می کند
    /// </summary>
    public partial class Voucher : OperationalEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Voucher()
        {
            Date = DateTime.Now;
            Reference = String.Empty;
            Association = String.Empty;
            IssuerName = String.Empty;
            ModifierName = String.Empty;
            ConfirmerName = String.Empty;
            ApproverName = String.Empty;
            OriginId = (int)Domain.VoucherOriginId.NormalVoucher;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
            Lines = new List<VoucherLine>();
        }

        /// <summary>
        /// شماره روزانه سند مالی که مقداری عددی است
        /// </summary>
        public virtual int DailyNo { get; set; }

        /// <summary>
        /// شماره عطف برای ایجاد ازتباط محتوایی بین اسناد
        /// </summary>
        public virtual string Association { get; set; }

        /// <summary>
        /// مشخص می کند که آیا سند مورد نظر تراز است یا نه؟
        /// </summary>
        public virtual bool IsBalanced { get; set; }

        /// <summary>
        /// نوع سیستمی سند، مانند سند عادی
        /// </summary>
        public virtual short Type { get; set; }

        /// <summary>
        /// نوع مفهومی سند، مانند سند حسابداری، سند پیش نویس، سند بودجه و ...
        /// </summary>
        public virtual short SubjectType { get; set; }

        /// <summary>
        /// شماره نسخه سند که نعداد دفعات ثبت را نشان می دهد
        /// </summary>
        public virtual int SaveCount { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر صادرکننده سند مالی
        /// </summary>
        public virtual int IssuedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که آخرین تغییرات را روی سند مالی داده است
        /// </summary>
        public virtual int ModifiedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تأییدکننده سند مالی
        /// </summary>
        public virtual int? ConfirmedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تصویب کننده سند مالی
        /// </summary>
        public virtual int? ApprovedById { get; set; }

        /// <summary>
        /// نام کامل کاربر صادر کننده سند
        /// </summary>
        public virtual string IssuerName { get; set; }

        /// <summary>
        /// نام کامل کاربر تغییردهنده سند
        /// </summary>
        public virtual string ModifierName { get; set; }

        /// <summary>
        /// نام کامل کاربر تأیید کننده سند
        /// </summary>
        public virtual string ConfirmerName { get; set; }

        /// <summary>
        /// نام کامل کاربر تصویب کننده سند
        /// </summary>
        public virtual string ApproverName { get; set; }

        /// <summary>
        /// شرح سند مالی که جزئیات بیشتری را در مورد پیشامد مالی ارائه می دهد
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// مجموعه ای از آرتیکل های موجود در سند مالی
        /// </summary>
        public virtual List<VoucherLine> Lines { get; protected set; }

        /// <summary>
        /// مستند اداری مرتبط با این سند مالی
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// وضعیت ثبتی این سند مالی
        /// </summary>
        public virtual DocumentStatus Status { get; set; }

        /// <summary>
        /// مأخذ این سند مالی
        /// </summary>
        public virtual VoucherOrigin Origin { get; set; }
    }
}
