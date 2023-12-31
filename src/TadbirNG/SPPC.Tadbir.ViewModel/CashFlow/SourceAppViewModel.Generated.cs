// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1557
//     Template Version: 1.0
//     Generation Date: 8/1/2023 4:24:17 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// اطلاعات یک منبع یا مصرف را نگهداری می کند.
    /// </summary>
    public partial class SourceAppViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public SourceAppViewModel()
        {
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            CreatedByName = String.Empty;
            ModifiedByName = String.Empty;
            State = AppStrings.Active;
        }

        /// <summary>
        /// محدوده دسترسی به مرکز هزینه را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short BranchScope { get; set; }

        /// <summary>
        /// کد منبع یا مصرف
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// نام منبع یا مصرف
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// شرح منبع یا مصرف
        /// </summary>
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// نوع (منابع: صفر ، مصارف: یک) 
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short Type { get; set; }

        /// <summary>
        /// شناسه کاربر ایجاد کننده
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// نام و نام خانوادگی کاربر ایجاد کننده
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// شناسه آخرین کاربر تغییر دهنده
        /// </summary>
        public int ModifiedById { get; set; }

        /// <summary>
        /// نام و نام خانوادگی آخرین کاربر تغییر دهنده
        /// </summary>
        public string ModifiedByName { get; set; }
    }
}
