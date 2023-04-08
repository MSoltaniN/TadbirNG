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

        /// <summary>
        /// مسیر کامل پوشه اصلی داده ها
        /// </summary>
        public string DataRoot { get; private set; }

        /// <summary>
        /// مسیر کامل پوشه اصلی دستورات دیتابیسی
        /// </summary>
        public string ScriptRoot { get; private set; }

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
        /// مسیر فایل داده ای اطلاعات مالیاتی ارزها
        /// </summary>
        public string TaxCurrencies { get; private set; }

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

        private void InitPaths(string webRootPath)
        {
            DataRoot = Path.Combine(webRootPath, "static", "Data");
            ScriptRoot = Path.Combine(webRootPath, "static", "Script");
            IranStates = Path.Combine(DataRoot, "ir-states.json");
            IranCities = Path.Combine(DataRoot, "ir-cities.json");
            Currencies = Path.Combine(DataRoot, "currencies.json");
            TaxCurrencies = Path.Combine(DataRoot, "tax-currencies.json");
            Accounts = Path.Combine(DataRoot, "DefaultAccounts.json");
            AccountScript = Path.Combine(ScriptRoot, "CollectionAccounts.sql");
            CompanyScript = Path.Combine(ScriptRoot, ScriptConstants.DbCreateScript);
#if DEBUG
            License = Path.Combine(webRootPath, "license.Development.json");
            Edition = "SPPC.Tadbir.Web.Api.wwwroot.edition.Development.json";
#else
            License = Path.Combine(webRootPath, "license");
            Edition = "SPPC.Tadbir.Web.Api.wwwroot.edition";
#endif
        }
    }
}
