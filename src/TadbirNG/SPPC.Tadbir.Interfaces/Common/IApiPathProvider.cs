using System;

namespace SPPC.Tadbir.Common
{
    /// <summary>
    /// مسیرهای فایل های کاربردی مورد نیاز در سرویس وب را با توجه به سیستم عامل جاری مشخص می کند
    /// </summary>
    public interface IApiPathProvider
    {
        /// <summary>
        /// مسیر فایل داده ای استان های ایران
        /// </summary>
        string IranStates { get; }

        /// <summary>
        /// مسیر فایل داده ای شهرهای ایران
        /// </summary>
        string IranCities { get; }

        /// <summary>
        /// مسیر فایل داده ای ارزهای استاندارد
        /// </summary>
        string Currencies { get; }

        /// <summary>
        /// مسیر فایل داده ای حساب های پیش فرض
        /// </summary>
        string Accounts { get; }

        /// <summary>
        /// مسیر فایل اسکریپت مورد نیاز برای ایجاد حسابهای پیش فرض مجموعه حساب
        /// </summary>
        string AccountScript { get; }

        /// <summary>
        /// مسیر فایل اسکریپت مورد نیاز برای ایجاد شرکت جدید
        /// </summary>
        string CompanyScript { get; }

        /// <summary>
        /// مسیر فایل متنی مجوز برنامه
        /// </summary>
        string License { get; }

        /// <summary>
        /// مسیر فایل محدودیت های ویرایش برنامه
        /// </summary>
        string Edition { get; }
    }
}
