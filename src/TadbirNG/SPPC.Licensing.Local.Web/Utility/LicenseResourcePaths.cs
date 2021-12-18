using System.IO;
using Microsoft.AspNetCore.Hosting;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Common;

namespace SPPC.Licensing.Local.Web
{
    /// <summary>
    /// مسیرهای فایل های کاربردی مورد نیاز در سرویس مجوزدهی را با توجه به سیستم عامل جاری مشخص می کند
    /// </summary>
    public class LicenseResourcePaths : ILicensePathProvider
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="webHost">اطلاعات مورد نیاز درباره محیط اجرای سرویس وب را فراهم می کند</param>
        public LicenseResourcePaths(IWebHostEnvironment webHost)
        {
            InitPaths(webHost.WebRootPath);
        }

        private void InitPaths(string webRootPath)
        {
            BinLicense = Path.Combine(webRootPath, Constants.LicenseFile);
            Certificate = Path.Combine(webRootPath, Constants.CertificateFile);
        }

        /// <summary>
        /// مسیر فایل باینری و رمزنگاری شده مجوز برنامه
        /// </summary>
        public string BinLicense { get; private set; }

        /// <summary>
        /// مسیر گواهینامه خودامضای مورد استفاده برای مجوزدهی به برنامه
        /// </summary>
        public string Certificate { get; private set; }
    }
}
