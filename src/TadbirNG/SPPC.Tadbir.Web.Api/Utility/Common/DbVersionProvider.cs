using SPPC.Tadbir.Common;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان خواندن آخرین نسخه را برای دیتابیس های برنامه فراهم می کند
    /// </summary>
    public class DbVersionProvider : IDbVersionProvider
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="paths">مسیرهای فایل های کاربردی مورد نیاز در سرویس وب را فراهم می کند</param>
        /// <param name="versionExtractor">امکان استخراج آخرین نسخه دیتابیس های مختلف برنامه را فراهم می کند</param>
        public DbVersionProvider(IApiPathProvider paths, IDbVersionExtractor versionExtractor)
        {
            _paths = paths;
            _extractor = versionExtractor;
        }

        /// <summary>
        /// نسخه جاری دیتابیس سیستمی برنامه را برمی گرداند
        /// </summary>
        public string SystemDbVersion
        {
            get { return _extractor.GetSystemDbVersion(_paths.ScriptRoot); }
        }

        /// <summary>
        /// نسخه جاری دیتابیس شرکتی برنامه را برمی گرداند
        /// </summary>
        public string CompanyDbVersion
        {
            get { return _extractor.GetCompanyDbVersion(_paths.ScriptRoot); }
        }

        private readonly IApiPathProvider _paths;
        private readonly IDbVersionExtractor _extractor;
    }
}
