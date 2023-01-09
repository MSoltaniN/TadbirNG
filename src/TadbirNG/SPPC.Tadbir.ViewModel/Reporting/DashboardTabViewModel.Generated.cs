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
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از برگه های داشبورد را نگهداری می کند
    /// </summary>
    public partial class DashboardTabViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DashboardTabViewModel()
        {
            Title = String.Empty;
            Widgets = new List<TabWidgetViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره ترتیبی زبانه در داشبورد مورد نظر
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int Index { get; set; }

        /// <summary>
        /// عنوانی که روی زبانه صفحه داشبورد نمایش داده می شود
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Title { get; set; }

        /// <summary>
        /// مجموعه ویجت های اضافه شده به این برگه داشبورد
        /// </summary>
        public List<TabWidgetViewModel> Widgets { get; set; }
    }
}