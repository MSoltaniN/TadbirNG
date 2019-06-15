// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.398
//     Template Version: 1.0
//     Generation Date: 2018-09-18 5:10:19 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یک ستون در یک نمای اطلاعاتی را نگهداری می کند
    /// </summary>
    public partial class Column : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Column()
        {
            Name = String.Empty;
            Type = String.Empty;
            DotNetType = String.Empty;
            StorageType = String.Empty;
            ScriptType = String.Empty;
            Expression = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// نام ستون به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// نوع کاربردی ستون در برنامه مانند مقدار، مبلغ یا رفرنس
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
        /// تعداد کاراکتر در ستون متنی - به طور پیش فرض مقدار صفر دارد و برای ستون های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual int Length { get; set; }

        /// <summary>
        /// حداقل تعداد کاراکتر در ستون متنی - به طور پیش فرض مقدار صفر دارد و برای ستون های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual int MinLength { get; set; }

        /// <summary>
        /// مشخص می کند که تعداد کاراکترها در یک ستون متنی ثابت است یا نه - به طور پیش فرض مقدار نادرست دارد و برای ستون های غیرمتنی کاربردی ندارد
        /// </summary>
        public virtual bool IsFixedLength { get; set; }

        /// <summary>
        /// مشخص می کند که وارد کردن مقدار برای ستون اجباری است یا نه
        /// </summary>
        public virtual bool IsNullable { get; set; }

        /// <summary>
        /// مشخص می کند که آیا عمل مرتب سازی بر حسب مقادیر این ستون فعال هست یا نه
        /// </summary>
        public virtual bool AllowSorting { get; set; }

        /// <summary>
        /// مشخص می کند که آیا عمل فیلتر کردن بر حسب مقادیر این ستون فعال هست یا نه
        /// </summary>
        public virtual bool AllowFiltering { get; set; }

        /// <summary>
        /// وضعیت نمایشی ستون در نمای لیستی که می تواند یکی از مقادیر
        /// عدم نمایش دائمی، نمایش دائمی، نمایش یا عدم نمایش را داشته باشد
        /// </summary>
        public virtual string Visibility { get; set; }

        /// <summary>
        /// ایندکس نمایشی ستون که از صفر شروع می شود
        /// </summary>
        public virtual short DisplayIndex { get; set; }

        /// <summary>
        /// عبارت مورد نیاز برای دسترسی به ستون در عبارات فیلتر
        /// </summary>
        public virtual string Expression { get; set; }

        /// <summary>
        /// موجودیتی که این ستون در آن تعریف شده است
        /// </summary>
        public virtual View View { get; set; }

        private void InitReferences()
        {
            View = new View();
        }
    }
}
