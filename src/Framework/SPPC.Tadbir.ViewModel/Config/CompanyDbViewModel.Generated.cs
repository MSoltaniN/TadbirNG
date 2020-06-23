// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.930
//     Template Version: 1.0
//     Generation Date: 2018-09-23 8:51:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Config
{
    /// <summary>
    /// اطلاعات مربوط به بانک اطلاعاتی یک شرکت را نگهداری می کند
    /// </summary>
    public partial class CompanyDbViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CompanyDbViewModel()
        {
            Name = String.Empty;
            DbName = String.Empty;
            Server = String.Empty;
            UserName = String.Empty;
            Password = String.Empty;
            IsActive = true;
            Description = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام شرکت به صورتی که در لیست شرکت های موجود نمایش داده می شود
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// نام بانک اطلاعاتی شرکت به صورتی که در لیست بانک های اطلاعاتی سرور نمایش داده می شود
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string DbName { get; set; }

        /// <summary>
        /// نام یا آدرس آی پی سرور دیتابیس
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Server { get; set; }

        /// <summary>
        /// نام کاربری برای اتصال به سرور دیتابیس
        /// </summary>
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string UserName { get; set; }

        /// <summary>
        /// رمز ورود برای اتصال به سرور دیتابیس
        /// </summary>
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Password { get; set; }

        /// <summary>
        /// وضعیت فعال بودن یا نبودن شرکت - شرکت های حذف شده غیرفعال می شوند
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// اطلاعات تکمیلی در مورد این شرکت
        /// </summary>
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
