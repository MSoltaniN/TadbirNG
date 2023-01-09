// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1435
//     Template Version: 1.0
//     Generation Date: 2022-09-13 10:32:14 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    /// <summary>
    /// اطلاعات یکی از ویجت (ابزارک) های کاربری قابل استفاده در داشبورد را نگهداری می کند
    /// </summary>
    public partial class Widget : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Widget()
        {
            Title = String.Empty;
            DefaultSettings = String.Empty;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
            Accounts = new List<WidgetAccount>();
        }

        /// <summary>
        /// شناسه دیتابیسی کاربری که این ویجت را ایجاد کرده است
        /// </summary>
        public virtual int CreatedById { get; set; }

        /// <summary>
        /// عنوان کاربری مورد استفاده برای ویجت که در محل مورد نظر کاربر در ویجت نمایش داده می شود
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// تنظیمات نمایشی و محاسباتی پیش فرض برای ویجت که در هر زمان توسط کاربر قابل اعمال شدن است
        /// </summary>
        public virtual string DefaultSettings { get; set; }

        /// <summary>
        /// شرح ویجت که برای اطلاعات تکمیلی قابل استفاده است
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// تابع محاسباتی استفاده شده در این ویجت
        /// </summary>
        public virtual WidgetFunction Function { get; set; }

        /// <summary>
        /// نوع انتخاب شده برای ویجت
        /// </summary>
        public virtual WidgetType Type { get; set; }

        /// <summary>
        /// مجموعه ای از بردارهای حساب اضافه شده به این ویجت
        /// </summary>
        public virtual IList<WidgetAccount> Accounts { get; }
    }
}