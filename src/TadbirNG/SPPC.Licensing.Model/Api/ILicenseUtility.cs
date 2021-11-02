using SPPC.Framework.Helpers;
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
        /// مجوز برنامه را فعالسازی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="connection"></param>
        /// <returns>نتیجه فعالسازی مجوز به صورت کدهای سیستمی تعریف شده</returns>
        ActivationResult ActivateLicense(string instance, RemoteConnection connection);

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseCheck"></param>
        /// <returns></returns>
        string GetOnlineLicense(string instance, RemoteConnection connection);

        /// <summary>
        /// اطلاعات مجوز فعال سازی شده موجود را خوانده و به صورت امضای دیجیتالی برمی گرداند
        /// </summary>
        /// <returns>امضای دیجیتالی به دست آمده از مجوز فعال سازی شده</returns>
        string GetLicense();

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="connection"></param>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        LicenseStatus ValidateLicense(string instance, RemoteConnection connection);

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
    }
}
