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
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Reporting
{
    /// <summary>
    /// اطلاعات اصلی یک گزارش سیستمی را نگهداری می کند
    /// </summary>
    public partial class CoreReport : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CoreReport()
        {
            Code = String.Empty;
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        public virtual int? ParentId { get; set; }

        /// <summary>
        /// کد شناسایی گزارش سیستمی در زیرساخت گزارشات
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// نام گزارش سیستمی به صورت کلید متن چندزبانه
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// مشخص می کند که آیا شاخه مورد نظر مربوط به گروه بندی گزارشات است یا نه؟
        /// </summary>
        public virtual bool IsGroup { get; set; }

        /// <summary>
        /// مجموعه کلیدهای متن های چند زبانه مورد نیاز در گزارش که با کاراکتر جداکننده از یکدیگر تفکیک شده اند
        /// </summary>
        public virtual string ResourceKeys { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// گروه بندی اصلی این گزارش در ساختار درختی گزارشات
        /// </summary>
        public virtual CoreReport Parent { get; set; }

        private void InitReferences()
        {
        }
    }
}
