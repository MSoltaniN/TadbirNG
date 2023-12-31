// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.326
//     Template Version: 1.0
//     Generation Date: 2018-06-20 11:15:07 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات متادیتای دستورات برنامه را نگهداری می کند
    /// </summary>
    public partial class Command : PCoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Command()
        {
            TitleKey = String.Empty;
            //ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// شناسه عنوان این دستور در متن های چند زبانه
        /// </summary>
        public virtual string TitleKey { get; set; }

        /// <summary>
        /// آدرس وب مسیر اجرای دستور در برنامه
        /// </summary>
        public virtual string RouteUrl { get; set; }

        /// <summary>
        /// نام علامت تصویری یا آیکون مورد استفاده در واسط کاربری دستور
        /// </summary>
        public virtual string IconName { get; set; }

        /// <summary>
        /// کلید ترکیبی میان بُر برای فراخوانی دستور از طریق صفحه کلید
        /// </summary>
        public virtual string HotKey { get; set; }

        /// <summary>
        /// شماره ترتیبی اختیاری برای سازماندهی بهتر منوها که از یک شروع می شود - فعلاً مورد استفاده برای اولین سطح منوها
        /// </summary>
        public virtual int? Index { get; set; }

        /// <summary>
        /// دستور والد این دستور در ساختار درختی دستورات
        /// </summary>
        public virtual Command Parent { get; set; }

        /// <summary>
        /// دسترسی امنیتی مورد نیاز کاربر برای فعال کردن این دستور در برنامه
        /// </summary>
        public virtual Permission Permission { get; set; }
    }
}
