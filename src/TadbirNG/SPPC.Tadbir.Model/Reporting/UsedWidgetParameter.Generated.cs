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

namespace SPPC.Tadbir.Model.Reporting
{
    /// <summary>
    /// اطلاعات یکی از پارامترهای استفاده شده در یک ویجت را نگهداری می کند
    /// </summary>
    public partial class UsedWidgetParameter : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UsedWidgetParameter()
        {
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// ویجتی که این پارامتر برای نمایش آن در داشبورد مورد نیاز است
        /// </summary>
        public virtual Widget Widget { get; set; }

        /// <summary>
        /// یکی از پارامترهای ثابت که برای نمایش ویجت مورد نیاز است
        /// </summary>
        public virtual WidgetParameter Parameter { get; set; }
    }
}
