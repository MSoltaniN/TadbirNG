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
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// اطلاعات بایگانی سوابق عملیات سیستمی را نگهداری می کند
    /// </summary>
    public partial class SysOperationLogArchive : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public SysOperationLogArchive()
        {
            EntityCode = String.Empty;
            EntityName = String.Empty;
            EntityDescription = String.Empty;
            Description = String.Empty;
            Date = DateTime.Now;
            EntityDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// تاریخ انجام عملیات
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// زمان انجام عملیات
        /// </summary>
        public virtual TimeSpan Time { get; set; }

        /// <summary>
        /// شناسه موجودیت ایجاد، اصلاح یا حذف شده
        /// </summary>
        public virtual int? EntityId { get; set; }

        /// <summary>
        /// کد موجودیت تغییر یافته در عملیات
        /// </summary>
        public virtual string EntityCode { get; set; }

        /// <summary>
        /// نام موجودیت تغییر یافته در عملیات
        /// </summary>
        public virtual string EntityName { get; set; }

        /// <summary>
        /// شرح موجودیت تغییر یافته در عملیات
        /// </summary>
        public virtual string EntityDescription { get; set; }

        /// <summary>
        /// شماره موجودیت تغییر یافته در عملیات
        /// </summary>
        public virtual int? EntityNo { get; set; }

        /// <summary>
        /// تاریخ موجودیت تغییر یافته در عملیات
        /// </summary>
        public virtual DateTime? EntityDate { get; set; }

        /// <summary>
        /// جزئیات تکمیلی درباره لاگ عملیاتی
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای عملیات انجام شده
        /// </summary>
        public virtual Operation Operation { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای فرم عملیاتی مورد استفاده
        /// </summary>
        public virtual OperationSource Source { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای موجودیت مورد استفاده در عملیات
        /// </summary>
        public virtual EntityType EntityType { get; set; }

        /// <summary>
        /// نمای اطلاعاتی لیستی به کار رفته حین انجام عملیات
        /// </summary>
        public virtual View SourceList { get; set; }

        /// <summary>
        /// کاربری که عملیات سیستمی توسط او انجام شده
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// شرکتی که عملیات سیستمی در دیتابیس آن انجام شده
        /// </summary>
        public virtual CompanyDb Company { get; set; }
    }
}
