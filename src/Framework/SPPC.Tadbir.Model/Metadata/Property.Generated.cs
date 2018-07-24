// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.349
//     Template Version: 1.0
//     Generation Date: 2018-07-21 8:20:37 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یک ویژگی از یک موجودیت را نگهداری می کند
    /// </summary>
    public partial class Property : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Property()
        {
            Name = String.Empty;
            DotNetType = String.Empty;
            StorageType = String.Empty;
            ScriptType = String.Empty;
            Settings = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// نام ویژگی به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// نوع کاربردی ویژگی در برنامه مانند مقدار، مبلغ یا رفرنس
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در دات نت
        /// </summary>
        public virtual string DotNetType { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در دیتابیس
        /// </summary>
        public virtual string StorageType { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در جاواسکریپت
        /// </summary>
        public virtual string ScriptType { get; set; }

        /// <summary>
        /// تعداد کاراکتر در ویژگی متنی - به طور پیش فرض مقدار صفر دارد و برای ویژگی های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual int Length { get; set; }

        /// <summary>
        /// حداقل تعداد کاراکتر در ویژگی متنی - به طور پیش فرض مقدار صفر دارد و برای ویژگی های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual int MinLength { get; set; }

        /// <summary>
        /// مشخص می کند که تعداد کاراکترها در یک ویژگی متنی ثابت است یا نه - به طور پیش فرض مقدار نادرست دارد و برای ویژگی های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual bool IsFixedLength { get; set; }

        /// <summary>
        /// مشخص می کند که وارد کردن مقدار برای ویژگی اجباری است یا نه
        /// </summary>
        public virtual bool IsNullable { get; set; }

        /// <summary>
        /// مشخص می کند که آیا عمل مرتب سازی بر حسب مقادیر این ویژگی فعال هست یا نه
        /// </summary>
        public virtual bool AllowSorting { get; set; }

        /// <summary>
        /// مشخص می کند که آیا عمل فیلتر کردن بر حسب مقادیر این ویژگی فعال هست یا نه
        /// </summary>
        public virtual bool AllowFiltering { get; set; }

        /// <summary>
        /// عبارت مورد نیاز برای دسترسی به ویژگی در عبارات فیلتر
        /// </summary>
        public virtual string Expression { get; set; }

        /// <summary>
        /// تنظیمات پیش فرض برای نمایش ویژگی به صورت یک ستون از نمای لیستی
        /// </summary>
        public virtual string Settings { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// موجودیتی که این ویژگی در آن تعریف شده است
        /// </summary>
        public virtual Entity Entity { get; set; }

        private void InitReferences()
        {
            Entity = new Entity();
        }
    }
}
