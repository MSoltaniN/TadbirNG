using System;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// نام موجودیت هایی را که یک یا چند دسترسی امنیتی برای آنها تعریف شده مشخص می کند
    /// </summary>
    public sealed class SecureEntity
    {
        private SecureEntity()
        {
        }

        /// <summary>
        /// نام موجودیت سرفصل حسابداری
        /// </summary>
        public const string Account = "Account";

        /// <summary>
        /// نام موجودیت تفصیلی شناور
        /// </summary>
        public const string DetailAccount = "DetailAccount";

        /// <summary>
        /// نام موجودیت مرکز هزینه
        /// </summary>
        public const string CostCenter = "CostCenter";

        /// <summary>
        /// نام موجودیت پروژه
        /// </summary>
        public const string Project = "Project";

        /// <summary>
        /// نام موجودیت گروه حساب
        /// </summary>
        public const string AccountGroup = "AccountGroup";

        /// <summary>
        /// نام موجودیت مجموعه حساب
        /// </summary>
        public const string AccountCollection = "AccountCollection";

        /// <summary>
        /// نام موجودیت مجازی ارتباطات بردار حساب
        /// </summary>
        public const string AccountRelations = "AccountRelations";

        /// <summary>
        /// نام موجودیت ارز
        /// </summary>
        public const string Currency = "Currency";

        /// <summary>
        /// نام موجودیت نرخ ارز
        /// </summary>
        public const string CurrencyRate = "CurrencyRate";

        /// <summary>
        /// نام موجودیت دوره مالی
        /// </summary>
        public const string FiscalPeriod = "FiscalPeriod";

        /// <summary>
        /// نام موجودیت شعبه سازمانی
        /// </summary>
        public const string Branch = "Branch";

        /// <summary>
        /// نام موجودیت سند مالی
        /// </summary>
        public const string Voucher = "Voucher";

        /// <summary>
        /// نام موجودیت اسناد مالی - مورد استفاده در فرم مدیریت اسناد
        /// </summary>
        public const string Vouchers = "Vouchers";

        /// <summary>
        /// نام موجودیت سند مالی پیش نویس
        /// </summary>
        public const string DraftVoucher = "DraftVoucher";

        /// <summary>
        /// نام موجودیت اسناد مالی پیش نویس - مورد استفاده در فرم مدیریت اسناد
        /// </summary>
        public const string DraftVouchers = "DraftVouchers";

        /// <summary>
        /// نام موجودیت سند عملیات ویژه
        /// </summary>
        public const string SpecialVoucher = "SpecialVoucher";

        /// <summary>
        /// نام موجودیت کاربر برنامه
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// نام موجودیت نقش سازمانی
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// نام موجودیت شرکت
        /// </summary>
        public const string Company = "Company";

        /// <summary>
        /// نام موجودیت دسترسی به سطرها
        /// </summary>
        public const string RowAccess = "RowAccess";

        /// <summary>
        /// نام موجودیت تنظیمات
        /// </summary>
        public const string Setting = "Setting";

        /// <summary>
        /// نام موجودیت تنظیمات لاگ
        /// </summary>
        public const string LogSetting = "LogSetting";

        /// <summary>
        /// نام موجودیت لاگ عملیاتی شرکتی
        /// </summary>
        public const string OperationLog = "OperationLog";

        /// <summary>
        /// نام موجودیت لاگ عملیاتی سیستمی
        /// </summary>
        public const string SysOperationLog = "SysOperationLog";

        /// <summary>
        /// نام موجودیت گزارش کاربری
        /// </summary>
        public const string UserReport = "UserReport";

        /// <summary>
        /// نام موجودیت دفتر روزنامه
        /// </summary>
        public const string Journal = "Journal";

        /// <summary>
        /// نام موجودیت دفتر حساب
        /// </summary>
        public const string AccountBook = "AccountBook";

        /// <summary>
        /// نام موجودیت تراز آزمایشی
        /// </summary>
        public const string TestBalance = "TestBalance";

        /// <summary>
        /// نام موجودیت گردش و مانده سطوح شناور
        /// </summary>
        public const string ItemBalance = "TestBalance";

        /// <summary>
        /// نام موجودیت گزارش
        /// </summary>
        public const string Report = "Report";

        /// <summary>
        /// نام موجودیت گزارش عملیات ارزی
        /// </summary>
        public const string CurrencyBook = "CurrencyBook";

        /// <summary>
        /// نام موجودیت کنترل سیستم
        /// </summary>
        public const string SystemIssue = "SystemIssue";

        /// <summary>
        /// نام موجودیت مانده به تفکیک حساب
        /// </summary>
        public const string BalanceByAccount = "BalanceByAccount";

        /// <summary>
        /// نام موجودیت سود و زیان
        /// </summary>
        public const string ProfitLoss = "ProfitLoss";

        /// <summary>
        /// نام موجودیت ترازنامه
        /// </summary>
        public const string BalanceSheet = "BalanceSheet";

        /// <summary>
        /// نام موجودیت داشبورد
        /// </summary>
        public const string Dashboard = "Dashboard";

        /// <summary>
        /// نام موجودیت دسته چک
        /// </summary>
        public const string CheckBook = "CheckBook";

        /// <summary>
        /// نام موجودیت صندوق
        /// </summary>
        public const string CashRegister = "CashRegister";

        /// <summary>
        /// نام موجودیت دفتر دسته چک
        /// </summary>
        public const string CheckBookReport = "CheckBookReport";

        /// <summary>
        /// نام موجودیت منبع و مصرف
        /// </summary>
        public const string SourceApp = "SourceApp";
    }
}
