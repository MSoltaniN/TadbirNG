using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// اطلاعات پارامترهای سیستمی مرتبط با دیتابیس های برنامه را نگهداری می کند
    /// </summary>
    public class DbParameters
    {
        /// <summary>
        /// نام لاگین پیش فرض برای اتصال به دیتابیس سیستمی و دیتابیس شرکت ها
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// رمز عبور لاگین پیش فرض
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// نام انتخاب شده برای دیتابیس سیستمی برنامه
        /// </summary>
        public string SysDbName { get; set; }

        /// <summary>
        /// نام دیتابیس شرکت نمونه که هنگام نصب برنامه روی سرور اصلی ساخته می شود
        /// </summary>
        public string FirstDbName { get; set; }

        /// <summary>
        /// عنوان شرکت نمونه که هنگام نصب برنامه روی سرور اصلی ساخته می شود
        /// </summary>
        public string FirstCompanyName { get; set; }

        /// <summary>
        /// نام کاربری پیش فرض مورد استفاده برای راهبر سیستم
        /// </summary>
        public string AdminUserName { get; set; }

        /// <summary>
        /// رمز عبور پیش فرض مورد استفاده برای راهبر سیستم
        /// </summary>
        public string AdminPassword { get; set; }

        /// <summary>
        /// شکل درهم شده برای رمز عبور پیش فرض مورد استفاده برای راهبر سیستم
        /// </summary>
        public string AdminPasswordHash { get; set; }

        /// <summary>
        /// شکل درهم شده برای رمز عبور پیش فرض مورد استفاده برای عملیات حساس در برنامه
        /// </summary>
        public string SuperPasswordHash { get; set; }

        /// <summary>
        /// نام پیش فرض برای راهبر سیستم
        /// </summary>
        public string AdminFirstName { get; set; }

        /// <summary>
        /// نام خانوادگی پیش فرض برای راهبر سیستم
        /// </summary>
        public string AdminLastName { get; set; }
    }
}
