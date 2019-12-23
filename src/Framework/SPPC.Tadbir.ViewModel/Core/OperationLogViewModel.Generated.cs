// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.776
//     Template Version: 1.0
//     Generation Date: 09/30/1398 12:25:16 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// اطلاعات سوابق عملیاتی برنامه را در هر شرکت نگهداری می کند
    /// </summary>
    public partial class OperationLogViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public OperationLogViewModel()
        {
            Description = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تاریخ انجام عملیات
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime Date { get; set; }

        /// <summary>
        /// زمان انجام عملیات
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// شناسه کاربری که عملیات توسط او در برنامه انجام شده
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int UserId { get; set; }

        /// <summary>
        /// شناسه شرکتی که عملیات روی دیتابیس آن انجام شده
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int CompanyId { get; set; }

        /// <summary>
        /// شناسه نمای اطلاعاتی لیستی به کار رفته حین انجام عملیات
        /// </summary>
        public int SourceListId { get; set; }

        /// <summary>
        /// شناسه موجودیت ایجاد، اصلاح یا حذف شده
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// جزئیات تکمیلی درباره لاگ عملیاتی
        /// </summary>
        public string Description { get; set; }
    }
}
