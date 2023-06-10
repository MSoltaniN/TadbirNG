// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-03-07 12:46:38 PM
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
    /// اطلاعات یک تفصیلی شناور مورد استفاده برای ثبت پیشامدهای مالی سازمان را نگهداری می کند
    /// </summary>
    public partial class DetailAccountViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public DetailAccountViewModel()
        {
            this.Code = String.Empty;
            this.FullCode = String.Empty;
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        /// <summary>
        /// محدوده دسترسی به تفصیلی شناور را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public short BranchScope { get; set; }

        /// <summary>
        /// کد شناسایی برای سطح جاری تفصیلی شناور در ساختار درختی
        /// </summary>
        [Display(Name = FieldNames.CodeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل تفصیلی شناور متشکل از کدهای تمام سطوح قبلی در ساختار درختی
        /// </summary>
        [Display(Name = FieldNames.FullCodeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string FullCode { get; set; }

        /// <summary>
        /// نام تفصیلی شناور
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// شماره سطح که عمق این تفصیلی شناور را در ساختار درختی مشخص می کند
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// شرحی که اطلاعات تکمیلی برای این تفصیلی شناور را مشخص می کند
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
