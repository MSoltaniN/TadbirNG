using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ارتباط مستقیم با یک پایگاه داده ای اس کیو ال سرور را پیاده سازی می کند
    /// </summary>
    public class SqlServerConsole : ISqlConsole
    {
        /// <summary>
        /// رشته اتصال به دیتابیس که در هر زمان قابل تغییر است
        /// </summary>
        /// <remarks>این رشته به طور پیش فرض هنگام ساخت کلاس روی دیتابیس سیستمی تنظیم می شود</remarks>
        public string ConnectionString { get; set; }

        /// <summary>
        /// یک یا چند دستور دیتابیسی داده شده (که داده ای برنمی گرداند) را به سرور بانک اطلاعاتی ارسال کرده و
        /// تعداد رکوردهای تغییریافته را برمی گرداند
        /// </summary>
        /// <param name="sqlCommand">دستورات دیتابیسی مورد نظر</param>
        /// <returns>تعداد رکوردهای تغییریافته پس از اجرای دستورات</returns>
        public int ExecuteNonQuery(string sqlCommand)
        {
            var connection = GetDatabaseConnection();
            int rowsAffected = connection.ExecuteNonQuery(sqlCommand);
            connection.Disconnect();
            return rowsAffected;
        }

        /// <summary>
        /// یک دستور دیتابیسی برای خواندن اطلاعات را به سرور بانک اطلاعاتی ارسال کرده و اطلاعات آن را برمی گرداند
        /// </summary>
        /// <param name="sqlCommand">دستور دیتابیسی برای خواندن اطلاعات</param>
        /// <returns>اطلاعات به دست آمده از دستور دیتابیسی با ساختار جدولی</returns>
        public DataTable ExecuteQuery(string sqlCommand)
        {
            var connection = GetDatabaseConnection();
            var dataSet = connection.ExecuteWithResults(sqlCommand);
            connection.Disconnect();
            return dataSet.Tables.Count > 0
                ? dataSet.Tables[0]
                : null;
        }

        /// <summary>
        /// اجزا تشکیل دهنده رشته اتصال به دیتابیس را برمیگرداند
        /// </summary>
        /// <returns>دیکشنری از کلید و مقدار اجزاء تشکیل دهنده رشته اتصال دیتابیس</returns>
        public IDictionary<string, string> GetConnectionStringProperties()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);

            var csDictionary = new Dictionary<string, string>();

            foreach (var item in builder.Keys)
            {
                string key = item.ToString();
                csDictionary.Add(key, builder[key].ToString());
            }

            return csDictionary;
        }

        /// <summary>
        /// امکان اتصال به دیتابیس به کمک رشته اتصال داده شده را بررسی می کند
        /// </summary>
        /// <param name="connectionString">رشته اتصال مورد نظر برای بررسی</param>
        /// <returns>در صورت برقراری ارتباط مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool TestConnection(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// رشته اتصال جدیدی با استفاده از مشخصات اتصال جاری و نام دیتابیس داده شده ساخته و برمی گرداند
        /// </summary>
        /// <param name="dbName">نام دیتابیس مورد نظر</param>
        /// <returns>رشته اتصال ساخته شده</returns>
        public string BuildConnectionString(string dbName)
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString)
            {
                InitialCatalog = dbName
            };

            return builder.ConnectionString;
        }

        private ServerConnection GetDatabaseConnection()
        {
            var server = new Server(
                new ServerConnection(
                    new SqlConnection(ConnectionString)));
            return server.ConnectionContext;
        }
    }
}
