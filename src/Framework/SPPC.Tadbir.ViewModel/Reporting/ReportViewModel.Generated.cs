// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.469
//     Template Version: 1.0
//     Generation Date: 2018-12-12 4:40:33 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یک گزارش در برنامه را نگهداری می کند
    /// </summary>
    public partial class ReportViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ReportViewModel()
        {
            Template = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// محتوای الگوی طراحی شده برای نمایش گزارش در برنامه
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر سیستمی است یا توسط کاربر ذخیره شده است
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public bool IsSystem { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر حالت پیش فرض دارد یا نه؟
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// شناسه زیرسیستم گزارش
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int SubsystemId { get; set; }
    }
}
