using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    /// <summary>
    /// این مدل نمایشی تنظیمات مربوط به یکی از ویرایش ها یا پیاده سازی های یک گردش کار را نمایش می دهد.
    /// </summary>
    public class WorkflowEditionViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public WorkflowEditionViewModel()
        {
        }

        /// <summary>
        /// نام سیستمی این ویرایش
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام محلی شده (فارسی) این ویرایش
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// نام مورد استفاده برای تبدیل اینترفیس اصلی گردش کار به پیاده سازی انجام شده توسط این ویرایش
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// مقداری که مشخص می کند آیا این ویرایش پیش فرض است یا نه
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
