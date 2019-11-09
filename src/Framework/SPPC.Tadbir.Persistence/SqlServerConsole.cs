using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SPPC.Framework.Persistence;

namespace SPPC.Tadbir.Persistence
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
            var server = new Server(
                new ServerConnection(
                    new SqlConnection(ConnectionString)));
            return server.ConnectionContext.ExecuteNonQuery(sqlCommand);
        }

        /// <summary>
        /// یک دستور دیتابیسی برای خواندن اطلاعات را به سرور بانک اطلاعاتی ارسال کرده و اطلاعات آن را برمی گرداند
        /// </summary>
        /// <param name="sqlCommand">دستور دیتابیسی برای خواندن اطلاعات</param>
        /// <returns>اطلاعات به دست آمده از دستور دیتابیسی با ساختار جدولی</returns>
        public DataTable ExecuteQuery(string sqlCommand)
        {
            var server = new Server(
                new ServerConnection(
                    new SqlConnection(ConnectionString)));
            var dataSet = server.ConnectionContext.ExecuteWithResults(sqlCommand);
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

            Dictionary<string, string> csDictionary = new Dictionary<string, string>();

            foreach (var item in builder.Keys)
            {
                string key = item.ToString();
                csDictionary.Add(key, builder[key].ToString());
            }

            return csDictionary;
        }

        /// <summary>
        /// ارتباط رشته اتصال را با دیتابیس بررسی میکند
        /// </summary>
        /// <returns>در صورت برقراری ارتباط مقدار درست و در غیر این صورت مقدار غلط را برمیگرداند</returns>
        public bool TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// رشته اتصال را تغییر میدهد
        /// </summary>
        /// <param name="dbName">نام دیتابیس</param>
        public void BuildConnectionString(string dbName)
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(ConnectionString);

            builder.InitialCatalog = dbName;

            ConnectionString = builder.ConnectionString;
        }
    }
}
