using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// Provides definitions for special-purpose constant values.
    /// </summary>
    public sealed class AppConstants
    {
        private AppConstants()
        {
        }

        /// <summary>
        /// Special identifier for System Administrator user (admin) in security system
        /// </summary>
        public const int AdminUserId = 1;

        /// <summary>
        /// Special-purpose name of the system administrator user
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// Dummy text to display in password boxes
        /// </summary>
        public const string DummyPassword = "************";

        /// <summary>
        /// Special identifier for System Administrator role in security system
        /// </summary>
        public const int AdminRoleId = 1;

        /// <summary>
        /// Name of security context cookie used for authorization in application level
        /// </summary>
        public const string ContextCookieName = "TadbirContext";

        /// <summary>
        /// Name of authorization context HTTP header used for authorization in Web service level
        /// </summary>
        public const string ContextHeaderName = "X-Tadbir-AuthTicket";

        /// <summary>
        /// Name of an optional HTTP header used for paging, sorting and filtering data in a grid display.
        /// </summary>
        public const string GridOptionsHeaderName = "X-Tadbir-GridOptions";

        /// <summary>
        /// Name of response header used for returning total item count of collections
        /// </summary>
        public const string TotalCountHeaderName = "X-Total-Count";

        /// <summary>
        /// Name of request header used for license checking
        /// </summary>
        public const string LicenseHeaderName = "X-Tadbir-License";

        /// <summary>
        /// نام هدر برای گرفتن پارامترها از درخواست
        /// </summary>
        public const string ParametersHeaderName = "X-Tadbir-Parameters";

        /// <summary>
        /// اندازه پیش فرض صفحه در فهرست های اطلاعاتی
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// نام تنظیمات مربوط به مسیر صفحه اصلی برنامه روی سرور وب
        /// </summary>
        public const string AppRootKey = "AppRoot";

        /// <summary>
        /// آدرس وب برنامه تحت وب
        /// </summary>
        public const string AppRoot = "http://localhost:8802/";

        /// <summary>
        /// دقت پیش فرض در مقایسه مقادیر اعشاری - برای پیشگیری از خطاهای روند سازی
        /// </summary>
        public const decimal RoundingPrecision = 0.000001M;

        /// <summary>
        /// نام پوشه مورد استفاده برای دریافت فایل های بارگذاری شده توسط کاربر
        /// </summary>
        public const string UserUploadFolderName = "Upload";

        /// <summary>
        /// اندازه کلید سری مورد استفاده در عملیات رمزنگاری اطلاعات، بر حسب بایت
        /// </summary>
        public const int CryptoKeySize = 32;

        /// <summary>
        /// اندازه بردار اولیه مورد استفاده در عملیات رمزنگاری اطلاعات، بر حسب بایت
        /// </summary>
        public const int CryptoIvSize = 16;

        /// <summary>
        /// نام لاگین سیستمی مورد استفاده برای اتصال به دیتابیس سیستمی و شرکت های بدون لاگین
        /// </summary>
        public const string SystemLoginName = "NgTadbirUser";

        /// <summary>
        /// شکل کد شده رمز عبور برای لاگین سیستمی
        /// </summary>
        public const string SystemLoginPassword = "TmdUYWRiaXJVc2Vy";

        /// <summary>
        /// حداکثر مجاز برای تعداد اقلام در گزارش های مقایسه ای
        /// </summary>
        public const int MaxCompareItems = 5;
    }
}
