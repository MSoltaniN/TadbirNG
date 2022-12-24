using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// آخرین نسخه های دیتابیس های مختلف برنامه را به صورت رشته های ثابت نگهداری می کند
    /// </summary>
    public sealed class DbVersionValues
    {
        private DbVersionValues()
        {
        }

        /// <summary>
        /// آخرین نسخه دیتابیس سیستمی
        /// </summary>
        public const string SystemDbVersion = "1.2.1460";

        /// <summary>
        /// آخرین نسخه دیتابیس شرکتی
        /// </summary>
        public const string CompanyDbVersion = "1.2.1450";
    }
}
