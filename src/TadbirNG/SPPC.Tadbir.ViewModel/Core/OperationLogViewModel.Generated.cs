// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.844
//     Template Version: 1.0
//     Generation Date: 2020-03-10 12:30:01 PM
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
            EntityCode = String.Empty;
            EntityName = String.Empty;
            EntityDescription = String.Empty;
            EntityReference = String.Empty;
            EntityAssociation = String.Empty;
            Description = String.Empty;
            EntityTypeName = String.Empty;
            SourceName = String.Empty;
            SourceListName = String.Empty;
            UserName = String.Empty;
            CompanyName = String.Empty;
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
        public int? UserId { get; set; }

        /// <summary>
        /// شناسه شرکتی که عملیات روی دیتابیس آن انجام شده
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? CompanyId { get; set; }

        /// <summary>
        /// شناسه موجودیت تغییر یافته در عملیات
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// کد موجودیت تغییر یافته در عملیات
        /// </summary>
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EntityCode { get; set; }

        /// <summary>
        /// نام موجودیت تغییر یافته در عملیات
        /// </summary>
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EntityName { get; set; }

        /// <summary>
        /// شرح موجودیت تغییر یافته در عملیات
        /// </summary>
        [StringLength(1024, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EntityDescription { get; set; }

        /// <summary>
        /// شماره موجودیت تغییر یافته در عملیات
        /// </summary>
        public int EntityNo { get; set; }

        /// <summary>
        /// تاریخ موجودیت تغییر یافته در عملیات
        /// </summary>
        public DateTime? EntityDate { get; set; }

        /// <summary>
        /// رفرنس موجودیت تغییر یافته در عملیات
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EntityReference { get; set; }

        /// <summary>
        /// عطف موجودیت تغییر یافته در عملیات
        /// </summary>
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EntityAssociation { get; set; }

        /// <summary>
        /// جزئیات تکمیلی درباره لاگ عملیاتی
        /// </summary>
        public string Description { get; set; }
    }
}
