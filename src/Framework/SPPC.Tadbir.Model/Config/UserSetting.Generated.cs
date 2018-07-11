// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.330
//     Template Version: 1.0
//     Generation Date: 2018-06-27 1:19:43 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات یکی از تنظیمات قابل کنترل توسط کاربر را نگهداری می کند
    /// </summary>
    public partial class UserSetting : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UserSetting()
        {
            ModelType = String.Empty;
            Values = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تنظیمات پیش فرض برنامه
        /// </summary>
        public virtual int SettingId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی متادیتای موجودیت قابل کنترل برای تنظیماتی که در سطح موجودیت هستند
        /// </summary>
        public virtual int? EntityViewId { get; set; }

        /// <summary>
        /// نوع کلاس مدل مورد استفاده برای نگهداری مقادیر تنظیمات
        /// </summary>
        public virtual string ModelType { get; set; }

        /// <summary>
        /// ریز اطلاعات تنظیمات که با فرمت مشخصی ذخیره و بازیابی می شود
        /// </summary>
        public virtual string Values { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// اطلاعات عمومی تنظیمات برای این تنظیمات کاربری
        /// </summary>
        public virtual Setting Setting { get; set; }

        /// <summary>
        /// اطلاعات کاربری برای تنظیماتی که به تفکیک کاربران قابل تعریف هستند
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// اطلاعات نقش برای تنظیماتی که به تفکیک نقش ها قابل تعریف هستند
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// اطلاعات متادیتای نمای موجودیت مرتبط با این تنظیمات
        /// </summary>
        public virtual Entity EntityView { get; set; }

        private void InitReferences()
        {
            Setting = new Setting();
        }
    }
}