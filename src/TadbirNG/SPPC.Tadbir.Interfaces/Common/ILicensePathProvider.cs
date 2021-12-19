using System;

namespace SPPC.Tadbir.Common
{
    /// <summary>
    /// مسیرهای فایل های کاربردی مورد نیاز در سرویس مجوزدهی را با توجه به سیستم عامل جاری مشخص می کند
    /// </summary>
    public interface ILicensePathProvider
    {
        /// <summary>
        /// مسیر فایل باینری و رمزنگاری شده مجوز برنامه
        /// </summary>
        string BinLicense { get; }

        /// <summary>
        /// مسیر گواهینامه خودامضای مورد استفاده برای مجوزدهی به برنامه
        /// </summary>
        string Certificate { get; }
    }
}
