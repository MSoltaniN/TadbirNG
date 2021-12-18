using System.IO;
using Microsoft.AspNetCore.Hosting;
using SPPC.Tadbir.Common;

namespace SPPC.Tadbir.Web.Api
{
    /// <summary>
    /// مسیرهای فایل های کاربردی مورد نیاز سرویس وب را با توجه به سیستم عامل جاری مشخص می کند
    /// </summary>
    public class ApiResourcePaths : IApiPathProvider
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="webHost">اطلاعات مورد نیاز درباره محیط اجرای سرویس وب را فراهم می کند</param>
        public ApiResourcePaths(IWebHostEnvironment webHost)
        {
            InitPaths(webHost.WebRootPath);
        }

        private void InitPaths(string webRootPath)
        {
            IranStates = Path.Combine(webRootPath, "static", "ir-states.json");
            IranCities = Path.Combine(webRootPath, "static", "ir-cities.json");
            Currencies = Path.Combine(webRootPath, "static", "currencies.json");
            Accounts = Path.Combine(webRootPath, "static", "DefaultAccounts.json");
            AccountScript = Path.Combine(webRootPath, "static", "CollectionAccounts.sql");
            CompanyScript = Path.Combine(webRootPath, "static", "Tadbir_CreateDbObjects.sql");
#if DEBUG
            License = Path.Combine(webRootPath, "license.Development.json");
            Edition = "SPPC.Tadbir.Web.Api.wwwroot.edition.Development.json";
#else
            License = Path.Combine(webRootPath, "license");
            Edition = "SPPC.Tadbir.Web.Api.wwwroot.edition";
#endif
        }

        /// <summary>
        /// مسیر فایل داده ای استان های ایران
        /// </summary>
        public string IranStates { get; private set; }

        /// <summary>
        /// مسیر فایل داده ای شهرهای ایران
        /// </summary>
        public string IranCities { get; private set; }

        /// <summary>
        /// مسیر فایل داده ای ارزهای استاندارد
        /// </summary>
        public string Currencies { get; private set; }

        /// <summary>
        /// مسیر فایل داده ای حساب های پیش فرض
        /// </summary>
        public string Accounts { get; private set; }

        /// <summary>
        /// مسیر فایل اسکریپت مورد نیاز برای ایجاد حسابهای پیش فرض مجموعه حساب
        /// </summary>
        public string AccountScript { get; private set; }

        /// <summary>
        /// مسیر فایل اسکریپت مورد نیاز برای ایجاد شرکت جدید
        /// </summary>
        public string CompanyScript { get; private set; }

        /// <summary>
        /// مسیر فایل متنی مجوز برنامه
        /// </summary>
        public string License { get; private set; }

        /// <summary>
        /// مسیر فایل محدودیت های ویرایش برنامه
        /// </summary>
        public string Edition { get; private set; }
    }
}
