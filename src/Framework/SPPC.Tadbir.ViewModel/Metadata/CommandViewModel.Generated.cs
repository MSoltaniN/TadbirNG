// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-05-15 10:23:05 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// اطلاعات متادیتای دستورات برنامه را نگهداری می کند
    /// </summary>
    public partial class CommandViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public CommandViewModel()
        {
            Title = String.Empty;
            Children = new List<CommandViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی این دستور
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان محلی شده این دستور در زبان جاری برنامه
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// آدرس وب مسیر اجرای دستور در برنامه
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// نام علامت تصویری یا آیکون مورد استفاده در واسط کاربری دستور
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// کلید ترکیبی میان بُر برای فراخوانی دستور از طریق صفحه کلید
        /// </summary>
        public string HotKey { get; set; }
    }
}
