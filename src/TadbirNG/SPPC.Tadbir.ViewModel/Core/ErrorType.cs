using System;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// انواع کلی خطاهای ایجاد شده سمت سرویس را تعریف می کند
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// عدم بروز خطا
        /// </summary>
        NoError = 0,

        /// <summary>
        /// بروز خطای اعتبارسنجی
        /// </summary>
        ValidationError = 1,

        /// <summary>
        /// بروز خطای زمان اجرا
        /// </summary>
        RuntimeException = 2,

        /// <summary>
        /// منقضی شدن تیکت امنیتی
        /// </summary>
        ExpiredSession = 3,

        /// <summary>
        /// کد خطا برای حالتی که فعالسازی برنامه هنوز انجام نشده است
        /// </summary>
        NotActivated = 4,

        /// <summary>
        /// بروز خطا هنگام اعتبارسنجی مجوز
        /// </summary>
        BadLicense = 5,

        /// <summary>
        /// کد خطا برای حالتی که مجوز برنامه باید از سرور آنلاین گرفته شود
        /// </summary>
        RequiresOnlineLicense = 6,

        /// <summary>
        /// کد خطا برای رسیدن به سقف تعداد کاربران همزمان در برنامه
        /// </summary>
        TooManySessions = 7,

        /// <summary>
        /// کد خطا برای نادرست بودن اطلاعات حساب کاربری سرور اصلی
        /// </summary>
        InvalidUserPass = 8
    }
}
