using System;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات مورد نیاز برای استخراج آخرین نسخه دیتابیس های مختلف برنامه را تعریف می کند
    /// </summary>
    public interface IDbVersionExtractor
    {
        /// <summary>
        /// آخرین نسخه دیتابیس سیستمی را از اسکریپت ارتقاء متناظر به دست می آورد
        /// </summary>
        /// <param name="scriptRoot">مسیر کامل پوشه ای که اسکریپت های ارتقاء دیتابیس درون آن قرار دارد</param>
        /// <returns>آخرین نسخه دیتابیس سیستمی</returns>
        string GetSystemDbVersion(string scriptRoot);

        /// <summary>
        /// آخرین نسخه دیتابیس شرکتی را از اسکریپت ارتقاء متناظر به دست می آورد
        /// </summary>
        /// <param name="scriptRoot">مسیر کامل پوشه ای که اسکریپت های ارتقاء دیتابیس درون آن قرار دارد</param>
        /// <returns>آخرین نسخه دیتابیس شرکتی</returns>
        string GetCompanyDbVersion(string scriptRoot);
    }
}
