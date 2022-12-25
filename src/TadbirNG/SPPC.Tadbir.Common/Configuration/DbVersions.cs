using System.Reflection;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// کلاس کمکی برای خواندن اطلاعات نسخه دیتابیس های برنامه از اسمبلی مشترک تدبیر
    /// </summary>
    public sealed class DbVersions
    {
        private DbVersions()
        {
        }

        /// <summary>
        /// نسخه جاری دیتابیس سیستمی برنامه را برمی گرداند
        /// </summary>
        public static string SystemDbVersion
        {
            get { return GetDbVersionAttribute()?.System; }
        }

        /// <summary>
        /// نسخه جاری دیتابیس شرکتی برنامه را برمی گرداند
        /// </summary>
        public static string CompanyDbVersion
        {
            get { return GetDbVersionAttribute()?.Company; }
        }

        private static DbVersionAttribute GetDbVersionAttribute()
        {
            var attribute = default(DbVersionAttribute);
            var commonAssembly = Assembly.GetAssembly(typeof(DbVersionAttribute));
            if (commonAssembly != null)
            {
                attribute = commonAssembly.GetCustomAttribute<DbVersionAttribute>();
            }

            return attribute;
        }
    }
}
