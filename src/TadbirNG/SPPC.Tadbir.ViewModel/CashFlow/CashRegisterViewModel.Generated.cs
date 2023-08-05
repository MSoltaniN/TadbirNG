// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1557
//     Template Version: 1.0
//     Generation Date: 7/31/2023 6:01:12 PM
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
    /// اطلاعات صندوق های مخصوص طبقه بندی چک ها را نگهداری می کند
    /// </summary>
    public partial class CashRegisterViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CashRegisterViewModel()
        {
            Name = String.Empty;
            Description = String.Empty;
            CreatedByName = String.Empty;
            ModifiedByName = String.Empty;
        }

        /// <summary>
        /// نام صندوق
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// محدوده دسترسی به حساب را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public short BranchScope { get; set; }

        /// <summary>
        /// شرح صندوق
        /// </summary>
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// شناسه کاربر ایجاد کننده
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// نام و نام خانوادگی کاربر ایجاد کننده
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
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
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ModifiedByName { get; set; }
    }
}
