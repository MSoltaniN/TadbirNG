// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.792
//     Template Version: 1.0
//     Generation Date: 10/30/1398 04:06:57 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// اطلاعات بایگانی سوابق عملیاتی برنامه را در هر شرکت نگهداری می کند
    /// </summary>
    public partial class OperationLogArchive : FiscalEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public OperationLogArchive()
        {
            Description = String.Empty;
            Date = DateTime.Now;
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
        /// شناسه کاربری که عملیات توسط او در برنامه انجام شده
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// شناسه شرکتی که عملیات روی دیتابیس آن انجام شده
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// شناسه موجودیت ایجاد، اصلاح یا حذف شده
        /// </summary>
        public virtual int? EntityId { get; set; }

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
        /// اطلاعات فراداده ای نمای لیستی مورد استفاده
        /// </summary>
        public virtual OperationSourceList SourceList { get; set; }
    }
}
