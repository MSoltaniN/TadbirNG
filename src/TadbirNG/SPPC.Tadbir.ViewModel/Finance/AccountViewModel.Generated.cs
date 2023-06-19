// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.510
//     Template Version: 1.0
//     Generation Date: 2019-02-06 3:25:20 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات یک سرفصل حسابداری مورد استفاده برای ثبت پیشامدهای مالی سازمان را نگهداری می کند
    /// </summary>
    public partial class AccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountViewModel()
        {
            Code = String.Empty;
            FullCode = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            IsActive = true;
            IsCurrencyAdjustable = true;
            TurnoverMode = "Unlimited";
        }

        /// <summary>
        /// محدوده دسترسی به حساب را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public short BranchScope { get; set; }

        /// <summary>
        /// کد شناسایی برای سطح جاری سرفصل حسابداری در ساختار درختی
        /// </summary>
        [Display(Name = FieldNames.CodeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل سرفصل حسابداری متشکل از کدهای تمام سطوح قبلی در ساختار درختی
        /// </summary>
        [Display(Name = FieldNames.FullCodeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string FullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// شماره سطح که عمق این سرفصل حسابداری را در ساختار درختی مشخص می کند
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// مشخص می کند که آیا حساب مورد نظر فعال است یا نه؟
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// مشخص می کند که آیا تسعیر ارزی برای حساب مورد نظر قابل انجام است یا نه؟
        /// </summary>
        public bool IsCurrencyAdjustable { get; set; }

        /// <summary>
        /// محدودیت ثبت های مالی حساب را مشخص می کند
        /// </summary>
        public string TurnoverMode { get; set; }

        /// <summary>
        /// شرحی که اطلاعات تکمیلی برای این سرفصل حسابداری را مشخص می کند
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
