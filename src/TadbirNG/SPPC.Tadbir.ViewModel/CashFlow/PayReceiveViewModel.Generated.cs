// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1513
//     Template Version: 1.0
//     Generation Date: 5/8/2023 2:45:33 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// اطلاعات دریافت یا پرداخت وجوه نقد یا چک را نگهداری می کند
    /// </summary>
    public partial class PayReceiveViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveViewModel()
        {
            Date = DateTime.Now;
            PayReceiveNo = String.Empty;
            Reference = String.Empty;
            Description = String.Empty;
            IssuedByName = String.Empty;
            ModifiedByName = String.Empty;
            ConfirmedByName = String.Empty;
            ApprovedByName = String.Empty;
        }

        /// <summary>
        /// شناسه کاربر صادر کننده
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int IssuedById { get; set; }

        /// <summary>
        /// شناسه آخرین کاربر تغییر دهنده اطلاعات
        /// </summary>
        public int ModifiedById { get; set; }

        /// <summary>
        /// شناسه کاربر تأییدکننده
        /// </summary>
        public int? ConfirmedById { get; set; }

        /// <summary>
        /// شناسه کاربر تصویب‌کننده
        /// </summary>
        public int? ApprovedById { get; set; }

        /// <summary>
        /// نوع فرم؛ 0 برای دریافت و 1 برای پرداخت
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short Type { get; set; }

        /// <summary>
        /// شماره فرم دریافت/پرداخت
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string PayReceiveNo { get; set; }

        /// <summary>
        /// شماره رفرنس فرم
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Reference { get; set; }

        /// <summary>
        /// تاریخ فرم
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime Date { get; set; }

        /// <summary>
        /// نرخ ارز
        /// </summary>
        public decimal CurrencyRate { get; set; }

        /// <summary>
        /// شرح
        /// </summary>
        [StringLength(1024, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// تاریخ ایجاد فرم
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// نام کامل کاربر صادرکننده
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string IssuedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تغییردهنده
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ModifiedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تأییدکننده
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ConfirmedByName { get; set; }

        /// <summary>
        /// نام کامل کاربر تصویب‌کننده
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ApprovedByName { get; set; }
    }
}
