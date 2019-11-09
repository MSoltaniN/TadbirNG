using System;
using System.Collections.Generic;
using System.Data;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ارتباط مستقیم با یک بانک اطلاعاتی را تعریف می کند
    /// </summary>
    public interface ISqlConsole
    {
        /// <summary>
        /// رشته اتصال به دیتابیس که در هر زمان قابل تغییر است
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// یک یا چند دستور دیتابیسی داده شده (که داده ای برنمی گرداند) را به سرور بانک اطلاعاتی ارسال کرده و
        /// تعداد رکوردهای تغییریافته را برمی گرداند
        /// </summary>
        /// <param name="sqlCommand">دستورات دیتابیسی مورد نظر</param>
        /// <returns>تعداد رکوردهای تغییریافته پس از اجرای دستورات</returns>
        int ExecuteNonQuery(string sqlCommand);

        /// <summary>
        /// یک دستور دیتابیسی برای خواندن اطلاعات را به سرور بانک اطلاعاتی ارسال کرده و اطلاعات آن را برمی گرداند
        /// </summary>
        /// <param name="sqlCommand">دستور دیتابیسی برای خواندن اطلاعات</param>
        /// <returns>اطلاعات به دست آمده از دستور دیتابیسی با ساختار جدولی</returns>
        DataTable ExecuteQuery(string sqlCommand);

        /// <summary>
        /// اجزا تشکیل دهنده رشته اتصال به دیتابیس را برمیگرداند
        /// </summary>
        /// <returns>دیکشنری از کلید و مقدار اجزاء تشکیل دهنده رشته اتصال دیتابیس</returns>
        IDictionary<string, string> GetConnectionStringProperties();

        /// <summary>
        /// ارتباط رشته اتصال را با دیتابیس بررسی میکند
        /// </summary>
        /// <returns>در صورت برقراری ارتباط مقدار درست و در غیر این صورت مقدار غلط را برمیگرداند</returns>
        bool TestConnection();

        /// <summary>
        /// نام دیتابیس رشته اتصال را تغییر میدهد
        /// </summary>
        /// <param name="dbName">نام دیتابیس</param>
        /// <returns>رشته اتصال جدید</returns>
        string BuildConnectionString(string dbName);
    }
}
