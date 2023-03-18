using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ارتقاء ساختار یک دیتابیس را تعریف می کند
    /// </summary>
    public interface IDbUpgrade
    {
        /// <summary>
        /// دیتابیس مشخص شده با رشته اتصال را از نظر نیاز به ارتقاء بررسی می کند
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>در صورت نیاز دیتابیس به ارتقاء مقدار بولی "درست" و در غیر این صورت مقدار
        /// بولی "نادرست" را برمی گرداند</returns>
        bool NeedsUpgrade(string connection, string scriptPath);

        /// <summary>
        /// دیتابیس مشخص شده با رشته اتصال را با استفاده از یک اسکریپت ارتقاء می دهد
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>تعداد تغییرات اعمال شده روی دیتابیس مورد نظر برای ارتقاء</returns>
        int UpgradeDatabase(string connection, string scriptPath);

        /// <summary>
        /// با توجه به دستورات موجود برای به روزرسانی دیتابیس مشخص شده در رشته اتصال، آخرین نسخه دیتابیس را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="connection">رشته اتصال به دیتابیس مورد نظر برای ارتقاء</param>
        /// <param name="scriptPath">مسیر کامل پوشه ای که فایل اسکریپت به روزرسانی در آن قرار دارد</param>
        /// <returns>آخرین نسخه دیتابیس مورد نظر با توجه به فایل دستورات به روزرسانی</returns>
        Version GetLatestDbVersion(string connection, string scriptPath);

        /// <summary>
        /// رشته های اتصال به دیتابیس را برای کلیه شرکت های فعال در برنامه تدبیر خوانده و برمی گرداند
        /// </summary>
        /// <param name="sysConnection">رشته اتصال به دیتابیس سیستمی تدبیر</param>
        /// <returns>مجموعه ای از رشته های اتصال به شرکت های فعال</returns>
        IEnumerable<string> GetCompanyConnections(string sysConnection);
    }
}
