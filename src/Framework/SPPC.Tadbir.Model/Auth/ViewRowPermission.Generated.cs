// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.343
//     Template Version: 1.0
//     Generation Date: 2018-07-17 8:20:21 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// تنظیمات مورد استفاده در محدودسازی سطرهای اطلاعاتی قابل دسترسی را نگهداری می کند
    /// </summary>
    public partial class ViewRowPermission : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewRowPermission()
        {
            AccessMode = RowAccessOptions.Default;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// روش محدودسازی سطرهای اطلاعاتی قابل دسترسی
        /// </summary>
        public virtual string AccessMode { get; set; }

        /// <summary>
        /// یک مقدار عددی صحیح یا اعشاری که برای محدود کردن بر اساس تعداد یا مبلغ قابل استفاده است
        /// </summary>
        public virtual double Value { get; set; }

        /// <summary>
        /// مقدار عددی صحیح یا اعشاری اضافی برای تعیین سقف عددی
        /// </summary>
        public virtual double Value2 { get; set; }

        /// <summary>
        /// یک متن آزاد که برای محدود کردن بر اساس رفرنس های مورد استفاده در سطرهای عملیاتی قابل استفاده است
        /// </summary>
        public virtual string TextValue { get; set; }

        /// <summary>
        /// شناسه های دیتابیسی برای تعریف دقیق سطرهای قابل دسترسی، که با یک جداکننده از هم جدا شده اند
        /// </summary>
        public virtual string Items { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// نقش امنیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// موجودیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public virtual View View { get; set; }

        private void InitReferences()
        {
            Role = new Role();
            View = new View();
        }
    }
}
