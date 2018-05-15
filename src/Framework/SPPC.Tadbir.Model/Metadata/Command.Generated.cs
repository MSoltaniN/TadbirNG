// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-05-15 10:23:02 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات متادیتای دستورات برنامه را نگهداری می کند
    /// </summary>
    public partial class Command : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public Command()
        {
            this.TitleKey = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// شناسه عنوان این دستور در متن های چند زبانه
        /// </summary>
        public virtual string TitleKey { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// دستور والد این دستور در ساختار درختی دستورات
        /// </summary>
        public virtual Command Parent { get; set; }

        /// <summary>
        /// دسترسی امنیتی مورد نیاز کاربر برای فعال کردن این دستور در برنامه
        /// </summary>
        public virtual Permission Permission { get; set; }

        private void InitReferences()
        {
        }
    }
}
