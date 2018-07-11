// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.327
//     Template Version: 1.0
//     Generation Date: 2018-06-24 1:29:55 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات یکی از تنظیمات برنامه را نگهداری می کند
    /// </summary>
    public partial class Setting : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Setting()
        {
            TitleKey = String.Empty;
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
        /// نام زیرسیستم مورد نیاز برای تنظیمات - مورد استفاده در تنظیمات خاص یک زیرسیستم
        /// </summary>
        public virtual string Subsystem { get; set; }

        /// <summary>
        /// شناسه متن چندزبانه برای عنوان تنظیمات در واسط کاربری
        /// </summary>
        public virtual string TitleKey { get; set; }

        /// <summary>
        /// نوع تنظیمات که مشخص کننده سطح دسترسی برای مدیریت و تغییر تنظیمات است
        /// </summary>
        public virtual short Type { get; set; }

        /// <summary>
        /// سطح اعمال تنظیمات در برنامه که می تواند سراسری، در سطح زیرسیستم و یا در سطح موجودیت تعریف شود
        /// </summary>
        public virtual short ScopeType { get; set; }

        /// <summary>
        /// نوع کلاس مدل مورد استفاده برای نگهداری مقادیر تنظیمات
        /// </summary>
        public virtual string ModelType { get; set; }

        /// <summary>
        /// ریز اطلاعات تنظیمات که با فرمت مشخصی ذخیره و بازیابی می شود
        /// </summary>
        public virtual string Values { get; set; }

        /// <summary>
        /// شناسه متن چندزبانه برای شرح تنظیمات در واسط کاربری
        /// </summary>
        public virtual string DescriptionKey { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// تنظیمات والد برای این تنظیمات در ساختار درختی
        /// </summary>
        public virtual Setting Parent { get; set; }

        private void InitReferences()
        {
        }
    }
}