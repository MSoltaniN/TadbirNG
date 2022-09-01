// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1425
//     Template Version: 1.0
//     Generation Date: 2022-08-31 11:04:12 PM
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
    /// اطلاعات یکی از پارامترهای قابل استفاده در ویجت های داشبورد را نگهداری می کند
    /// </summary>
    public partial class WidgetParameter : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public WidgetParameter()
        {
            Name = String.Empty;
            Alias = String.Empty;
            Type = String.Empty;
            DefaultValue = String.Empty;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام نمایشی چندزبانه برای این پارامتر
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// نام کوتاه مورد استفاده در متد سرویسی که برای محاسبه کمیت ها استفاده می شود
        /// </summary>
        public virtual string Alias { get; set; }

        /// <summary>
        /// نوع داده ای مورد نظر برای مقادیر پارامتر
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// شناسه مورد نیاز برای مقدار پیش فرض پارامتر، مانند تاریخ ابتدای دوره مالی، تاریخ انتهای دوره مالی و غیره
        /// </summary>
        public virtual string DefaultValue { get; set; }

        /// <summary>
        /// شرح پارامتر که برای توضیحات تکمیلی قابل استفاده است
        /// </summary>
        public virtual string Description { get; set; }
    }
}
