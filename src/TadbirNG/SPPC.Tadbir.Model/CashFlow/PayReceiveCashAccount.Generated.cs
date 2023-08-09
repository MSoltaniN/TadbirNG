// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1537
//     Template Version: 1.0
//     Generation Date: 6/26/2023 12:58:01 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model.CashFlow
{
    /// <summary>
    /// اطلاعات مربوط به حساب‌های نقدی دریافت/پرداخت را نگهداری می کند
    /// </summary>
    public partial class PayReceiveCashAccount : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveCashAccount()
        {
            BankOrderNo = String.Empty;
            Remarks = String.Empty;
            ModifiedDate = DateTime.Now;
            SourceAppId = null;
        }

        /// <summary>
        /// حساب از نوع بانک است یا صندوق؟
        /// </summary>
        public virtual bool IsBank { get; set; }

        /// <summary>
        /// مبلغ آرتیکل حساب نقد
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// شماره حواله بانکی
        /// </summary>
        public virtual string BankOrderNo { get; set; }

        /// <summary>
        ///  ملاحظات آرتیکل حساب نقد
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// شناسه فرم دریافت/پرداخت اصلی
        /// </summary>
        public virtual PayReceive PayReceive { get; set; }

        /// <summary>
        /// مولفه سرفصل حسابداری در بردار حساب
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// مولفه تفصیلی شناور در بردار حساب
        /// </summary>
        public virtual DetailAccount DetailAccount { get; set; }

        /// <summary>
        /// مولفه مرکز هزینه در بردار حساب
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }

        /// <summary>
        /// مولفه پروژه در بردار حساب
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// مولفه منبع یا مصرف مورد استفاده
        /// </summary>
        public virtual SourceApp SourceApp { get; set; }
    }
}