// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1537
//     Template Version: 1.0
//     Generation Date: 6/26/2023 1:27:46 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model.CashFlow
{
    /// <summary>
    /// اطلاعات دریافت یا پرداخت وجوه نقد یا چک را نگهداری می کند
    /// </summary>
    public partial class PayReceive : OperationalEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceive()
        {
            PayReceiveNo = String.Empty;
            Reference = String.Empty;
            Description = String.Empty;
            IssuedByName = String.Empty;
            ModifiedByName = String.Empty;
            ConfirmedByName = String.Empty;
            ApprovedByName = String.Empty;
            Date = DateTime.Now;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شناسه کاربر صادر کننده
        /// </summary>
        public virtual int IssuedById { get; set; }

        /// <summary>
        /// شناسه آخرین کاربر تغییر دهنده اطلاعات
        /// </summary>
        public virtual int ModifiedById { get; set; }

        /// <summary>
        /// شناسه کاربر تأییدکننده
        /// </summary>
        public virtual int? ConfirmedById { get; set; }

        /// <summary>
        /// شناسه کاربر تصویب‌کننده
        /// </summary>
        public virtual int? ApprovedById { get; set; }

        /// <summary>
        /// نوع فرم؛ 0 برای دریافت و 1 برای پرداخت
        /// </summary>
        public virtual short Type { get; set; }

        /// <summary>
        /// شماره فرم دریافت/پرداخت
        /// </summary>
        public virtual string PayReceiveNo { get; set; }

        /// <summary>
        /// نرخ ارز
        /// </summary>
        public virtual decimal? CurrencyRate { get; set; }

        /// <summary>
        /// شرح
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// تاریخ ایجاد فرم
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// نام کامل کاربر صادرکننده
        /// </summary>
        public virtual string IssuedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تغییردهنده
        /// </summary>
        public virtual string ModifiedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تأییدکننده
        /// </summary>
        public virtual string ConfirmedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تصویب‌کننده
        /// </summary>
        public virtual string ApprovedByName { get; set; }

        /// <summary>
        /// پول یا ارز مورد استفاده در فرم دریافت/پرداخت
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// مجموعه ای از طرف حساب‌های فرم دریافت/پرداخت
        /// </summary>
        public virtual IList<PayReceiveAccount> Accounts { get; protected set; }

        /// <summary>
        /// مجموعه ای از حساب‌های نقدی فرم دریافت/پرداخت
        /// </summary>
        public virtual IList<PayReceiveCashAccount> CashAccounts { get; protected set; }

        /// <summary>
        /// مجموعه ای از آرتیکل‌های مالی مرتبط با این فرم دریافت/پرداخت
        /// </summary>
        public virtual IList<PayReceiveVoucherLine> PayReceiveVoucherLines { get; protected set; }
    }
}
