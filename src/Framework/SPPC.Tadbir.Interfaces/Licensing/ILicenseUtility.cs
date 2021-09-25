using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Tadbir.Licensing
{
    /// <summary>
    /// امکانات مورد نیاز برای گرفتن یا کنترل درستی مجوز برنامه را تعریف می کند
    /// </summary>
    public interface ILicenseUtility
    {
        /// <summary>
        /// مسیر فایل مجوز ایجاد شده پس از فعال سازی برنامه
        /// </summary>
        string LicensePath { get; set; }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        LicenseStatus ValidateLicense(string instance);

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور خلاصه بررسی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        LicenseStatus QuickValidateLicense(string instance);

        /// <summary>
        /// درستی اطلاعات متنی مجوز مورد استفاده سرویس را با امضای دیجیتالی داده شده بررسی می کند
        /// </summary>
        /// <param name="apiLicense">اطلاعات متنی فایل مجوز سرویس</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای بررسی درستی مجوز</param>
        /// <returns>در صورت درستی مجوز مقدار بولی "درست" و در صورت
        /// عدم مطابقت اطلاعات متنی با اطلاعات فعال سازی شده مقدار بولی "نادرست" را برمی گرداند</returns>
        bool ValidateSignature(string apiLicense, string signature);

        /// <summary>
        /// اطلاعات مجوز فعال سازی شده موجود را خوانده و به صورت امضای دیجیتالی برمی گرداند
        /// </summary>
        /// <returns>امضای دیجیتالی به دست آمده از مجوز فعال سازی شده</returns>
        string GetActiveLicense();

        /// <summary>
        /// اطلاعات رمزنگاری شده مجوز را خوانده و به صورت مدل اطلاعاتی مجوز برمی گرداند
        /// </summary>
        /// <param name="licenseData">اطلاعات رمزنگاری مجوز</param>
        /// <returns>اطلاعات رمزگشایی شده مجوز به صورت مدل اطلاعاتی مجوز</returns>
        LicenseFileModel LoadLicense(string licenseData);

        /// <summary>
        ///
        /// </summary>
        /// <param name="activation"></param>
        /// <returns></returns>
        string GetActivatedLicense(ActivationModel activation);

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseCheck"></param>
        /// <returns></returns>
        string GetLicense(LicenseCheckModel licenseCheck);
    }
}
