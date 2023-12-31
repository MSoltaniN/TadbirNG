// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1522
//     Template Version: 1.0
//     Generation Date: 5/29/2023 6:34:21 PM
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
    /// اطلاعات مربوط به طرف حساب فرم دریافت/پرداخت را نگهداری می کند
    /// </summary>
    public partial class PayReceiveAccount : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveAccount()
        {
            Remarks = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// مبلغ آرتیکل طرف حساب
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// ملاحظات آرتیکل طرف حساب
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// مولفه سرفصل حسابداری در بردار حساب
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// مولفه مرکز هزینه در بردار حساب
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }

        /// <summary>
        /// مولفه پروژه در بردار حساب
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// فرم دریافت/پرداخت اصلی
        /// </summary>
        public virtual PayReceive PayReceive { get; set; }

        /// <summary>
        /// مولفه تفصیلی شناور در بردار حساب 
        /// </summary>
        public virtual DetailAccount DetailAccount { get; set; }
    }
}
