﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:32 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Inventory
{
    /// <summary>
    /// اطلاعات نمایشی یک کالا را در سیستم کنترل موجودی نشان می دهد
    /// </summary>
    public partial class ProductViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public ProductViewModel()
        {
            this.Code = String.Empty;
            this.Name = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// کد کالا در سیستم کدگزاری سازمان
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Code { get; set; }

        /// <summary>
        /// نام کالا
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(128, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }
    }
}
