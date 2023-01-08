using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// اطلاعات نسخه دیتابیس های مورد نیاز برنامه را نگهداری می کند
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class DbVersionAttribute : Attribute
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DbVersionAttribute()
        {
        }

        /// <summary>
        /// نسخه دیتابیس سیستمی
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// نسخه دیتابیس شرکتی
        /// </summary>
        public string Company { get; set; }
    }
}
