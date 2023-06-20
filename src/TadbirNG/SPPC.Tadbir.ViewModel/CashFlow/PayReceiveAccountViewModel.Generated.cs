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
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// اطلاعات مربوط به طرف حساب دریافت/پرداخت را نگهداری می کند
    /// </summary>
    public partial class PayReceiveAccountViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveAccountViewModel()
        {
            FullAccount = new FullAccountViewModel();
            Description = String.Empty;
        }

        /// <summary>
        /// مبلغ آرتیکل طرف حساب
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// ملاحظات آرتیکل طرف حساب
        /// </summary>
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
