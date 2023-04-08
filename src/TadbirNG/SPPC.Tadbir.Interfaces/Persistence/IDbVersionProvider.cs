using System;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان خواندن آخرین نسخه را برای دیتابیس های برنامه فراهم می کند
    /// </summary>
    public interface IDbVersionProvider
    {
        /// <summary>
        /// نسخه جاری دیتابیس سیستمی برنامه را برمی گرداند
        /// </summary>
        string SystemDbVersion { get; }

        /// <summary>
        /// نسخه جاری دیتابیس شرکتی برنامه را برمی گرداند
        /// </summary>
        string CompanyDbVersion { get; }
    }
}
