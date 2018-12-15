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
    /// اطلاعات اصلی یک گزارش سیستمی را نگهداری می کند
    /// </summary>
    public partial class CoreReportViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CoreReportViewModel()
        {
            Code = String.Empty;
            Name = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// کد شناسایی گزارش سیستمی در زیرساخت گزارشات
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Code { get; set; }

        /// <summary>
        /// نام گزارش سیستمی به صورت کلید متن چندزبانه
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// مشخص می کند که آیا شاخه مورد نظر مربوط به گروه بندی گزارشات است یا نه؟
        /// </summary>
        public bool IsGroup { get; set; }
    }
}
