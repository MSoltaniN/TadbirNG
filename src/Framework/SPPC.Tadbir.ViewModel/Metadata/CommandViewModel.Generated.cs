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
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

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
        public virtual string RouteUrl { get; set; }
    }
}
