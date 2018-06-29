using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Config
{
    /// <summary>
    /// اطلاعات نمایشی خلاصه شده برای یکی از تنظیمات برنامه را نگهداری می کند
    /// </summary>
    public class SettingBriefViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی این تنظیمات
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان محلی شده تنظیمات با زبان جاری برنامه
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// شرح یا توضیحات محلی شده تنظیمات با زبان جاری برنامه
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مقادیر جاری برای تنظیمات
        /// </summary>
        public object Values { get; set; }
    }
}
