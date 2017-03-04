using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized text for generic application contents (in Persian).
    /// </summary>
    public sealed class Strings
    {
        private Strings()
        {
        }

        /// <summary>
        /// Localized text for the application name
        /// </summary>
        public const string AppName = "سیستم جامع مالی و اداری تدبیر";

        /// <summary>
        /// Localized text for the application brand name
        /// </summary>
        public const string AppBrandName = "تدبیر";

        /// <summary>
        /// Localized text for the main copyright content
        /// </summary>
        public const string CopyrightText = "کلیه حقوق برای شرکت پردازش مواری سامان (با مسئولیت محدود) محفوظ است.";

        /// <summary>
        /// Localized text for home page title
        /// </summary>
        public const string HomePage = "صفحه اصلی";

        /// <summary>
        /// Localized text for the application lead information in home page
        /// </summary>
        public const string AppLeadInfo = "سازمان خود را هر کجا که هستید با قدرت و اطمینان اداره کنید.";

        /// <summary>
        /// Localized text for Manage Accounts navigation link
        /// </summary>
        public const string ManageAccounts = "مدیریت حساب ها";

        /// <summary>
        /// Localized text for Manage Transactions navigation link
        /// </summary>
        public const string ManageTransactions = "مدیریت اسناد";

        /// <summary>
        /// Localized text for About page title
        /// </summary>
        public const string About = "درباره";

        /// <summary>
        /// Localized text for the application type name
        /// </summary>
        public const string WebVersion = "نسخه وب";

        /// <summary>
        /// Localized text for the main content in the About page
        /// </summary>
        public const string AboutMainContent = "سیستم جامع مالی و اداری تدبیر یک برنامه نرم افزاری قدرتمند است که در راستای برآورده کردن نیاز های اطلاعاتی سازمان ها و مراکز تجاری پیاده سازی شده است. نسخه وب فعلی برای پشتیبانی از نیازهای خاص کاربران برای بکارگیری فناوری های مدرن امروزی پیاده سازی شده است.";

        /// <summary>
        /// Localized text for subsystem introduction section in the About page
        /// </summary>
        public const string AboutSubsystemIntro = "زیرسیستم های قابل استفاده در نسخه فعلی عبارتند از :";

        /// <summary>
        /// Localized text for the Accounting subsystem name
        /// </summary>
        public const string Accounting = "حسابداری";

        /// <summary>
        /// Localized text for the Cash Flow subsystem name
        /// </summary>
        public const string CashFlow = "خزانه داری";

        /// <summary>
        /// Localized text for the Sales/Purchase subsystem name
        /// </summary>
        public const string SalesPurchase = "خرید و فروش";

        /// <summary>
        /// Localized text for the Inventory and Warehousing subsystem name
        /// </summary>
        public const string WarehousingInventory = "کنترل موجودی و انبار";

        /// <summary>
        /// Localized text for intro content related to the Accounting subsystem
        /// </summary>
        public const string AccountingIntro = "بخش حسابداری با پشتیبانی از آخرین استانداردهای حسابداری دوبل و گزارشات متنوع و گسترده، بخش بسیار وسیعی از نیازهای مالی سازمان ها را پوشش می دهد.";

        /// <summary>
        /// Localized text for intro content related to the Cash Flow subsystem
        /// </summary>
        public const string CashFlowIntro = "برای ثبت و کنترل کلیه عملیات مرتبط با جریان نقدینگی (Cash Flow) می توان از امکانات متنوع بخش خزانه داری استفاده کرد.";

        /// <summary>
        /// Localized text for intro content related to the Sales/Purchase subsystem
        /// </summary>
        public const string SalesPurchaseIntro = "بخش خرید و فروش با پشتیبانی از کلیه قابلیت های کلیدی برای ثبت و کنترل اطلاعات و عملیات مرتبط با خرید و فروش، امکانات وسیعی را در اختیار سازمان ها قرار می دهد.";

        /// <summary>
        /// Localized text for intro content related to the Inventory and Warehousing subsystem
        /// </summary>
        public const string WarehousingIntro = "بخش کنترل موجودی و انبار با امکانات گسترده و انعطاف پذیری در تعریف اطلاعات پایه و عملیاتی، پاسخگوی نیازهای سازمانی طیف بسیار وسیعی از کاربران خواهد بود.";

        /// <summary>
        /// Localized text for Edit caption 
        /// </summary>
        public const string Edit = "ویرایش";

        /// <summary>
        /// Localized text for Details caption
        /// </summary>
        public const string Details = "جزئیات";

        /// <summary>
        /// Localized text for Delete caption
        /// </summary>
        public const string Delete = "حذف";

        /// <summary>
        /// Localized text for Save caption 
        /// </summary>
        public const string Save = "ذخیره";

        /// <summary>
        /// Localized text for Cancel caption 
        /// </summary>
        public const string Cancel = "انصراف";

        /// <summary>
        /// Localized text for Total caption 
        /// </summary>
        public const string Total = "جمع کل";

        /// <summary>
        /// Localized text for 'Login to app' caption
        /// </summary>
        public const string AppLogin = "ورود به برنامه";

        /// <summary>
        /// Localized text for a message indicating that the value entered for security code is incorrect.
        /// </summary>
        public const string InvalidSecurityCode = "کد امنیتی وارد شده نادرست است.";

        /// <summary>
        /// Localized text for a validation message indicating that entering security code is required.
        /// </summary>
        public const string SecurityCodeIsRequired = "وارد کردن کد امنیتی اجباری است.";

        /// <summary>
        /// Localized text for a validation message indicating that the value entered for user name or password
        /// is incorrect.
        /// </summary>
        public const string InvalidUserOrPassword = "نام کاربری یا رمز ورود نادرست است.";

        /// <summary>
        /// Localized text for a message indicating that the request verification token is invalid.
        /// </summary>
        public const string InvalidBrowserRequest = "درخواست دریافت شده از مرورگر معتبر نیست. لطفا دوباره تلاش کنید.";

        /// <summary>
        /// Localized text for Not Found title
        /// </summary>
        public const string PageNotFoundTitle = "صفحه مورد نظر یافت نشد";

        /// <summary>
        /// Localized text for user hint applicable to a Not Found (404) error
        /// </summary>
        public const string PageNotFoundHint = "لطفا در مورد درستی آدرس وب مورد نظرتان اطمینان حاصل نموده و دوباره تلاش کنید.";

        /// <summary>
        /// Localized text for a short message displayed in case of a Not Found (404) error
        /// </summary>
        public const string PageNotFound = "صفحه مورد نظر شما در این برنامه وجود ندارد.";

        /// <summary>
        /// Localized text for Error Occured title
        /// </summary>
        public const string ErrorOccurredTitle = "بروز خطا";

        /// <summary>
        /// Localized text for common courtesy message displayed in case of a server error
        /// (i.e. We appologize for the inconvenience)
        /// </summary>
        public const string ErrorOccurredHint = "پوزش ما را بابت وقفه احتمالی در کار خود بپذیرید.";

        /// <summary>
        /// Localized text for a short message displayed in case of a server error
        /// </summary>
        public const string ErrorOccurred = "متاسفانه بدلیل بروز خطای سمت سرور، درخواست شما در این زمان قابل اجرا نیست.";

        /// <summary>
        /// Localized text for 'New security code' caption
        /// </summary>
        public static readonly string NewSecurityCode = "کد امنیتی جدید";
    }
}
