using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت گواهینامه های امنیتی را تعریف می کند
    /// </summary>
    public interface ICertificateManager
    {
        /// <summary>
        /// یک گواهینامه امنیتی جدید خودامضا با مشخصات داده شده را ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="issuerName">نام مورد نظر برای صادرکننده گواهینامه</param>
        /// <param name="subjectName">نام مورد نظر برای موضوع گواهینامه</param>
        /// <returns>گواهینامه خودامضای جدید</returns>
        X509Certificate2 GenerateSelfSigned(string issuerName, string subjectName);

        /// <summary>
        /// گواهینامه امنیتی را در انباره با مشخصات داده شده اضافه می کند
        /// </summary>
        /// <param name="certificate">گواهینامه مورد نظر برای اضافه کردن به انباره</param>
        /// <param name="name">نام انباره گواهینامه مورد نظر</param>
        /// <param name="location">موقعیت سیستمی انباره گواهینامه مورد نظر</param>
        void AddToStore(X509Certificate2 certificate, StoreName name, StoreLocation location);

        /// <summary>
        /// اولین گواهینامه امنیتی موجود با نام صادرکننده داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issuerName">نام صادرکننده مورد نظر برای خواندن گواهینامه</param>
        /// <returns>گواهینامه امنیتی خوانده شده یا رفرنس بدون مقدار در صورت پیدا نشدن گواهینامه</returns>
        X509Certificate2 GetFromStore(string issuerName);

        /// <summary>
        /// گواهینامه امنیتی را از روی فایل مشخص شده بارگذاری کرده و برمی گرداند
        /// </summary>
        /// <param name="path">مسیر فیزیکی فایل گواهینامه</param>
        /// <param name="password">رمز مورد نیاز برای خواندن اطلاعات گواهینامه از روی فایل</param>
        /// <returns>گواهینامه بارگذاری شده از روی فایل</returns>
        /// <remarks>در صورت نادرست بودن رمز داده شده، خطا ایجاد می شود</remarks>
        X509Certificate2 GetFromFile(string path, string password);
    }
}
