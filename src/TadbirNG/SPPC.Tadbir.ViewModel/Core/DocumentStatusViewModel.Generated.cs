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

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// اطلاعات نمایشی یکی از وضعیت های ثبتی مربوط به اسناد مالی یا اداری را نشان می دهد
    /// </summary>
    public partial class DocumentStatusViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public DocumentStatusViewModel()
        {
            Name = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این وضعیت ثبتی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام وضعیت ثبتی
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(64, MinimumLength = 0, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public string Name { get; set; }
    }
}
