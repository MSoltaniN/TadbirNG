using System;
using System.Linq;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// اطلاعات فنی کامل را برای یک خطای زمان اجرا نگهداری می کند
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ErrorDetail()
        {
            Version = "1.0";
        }

        /// <summary>
        /// برچسب تاریخ و زمان برای مشخص کردن زمان دقیق وقوع خطا، در ناحیه زمانی استاندارد
        /// </summary>
        public string TimestampUtc { get; private set; }

        /// <summary>
        /// کد خطای مورد استفاده در خطای سرویسی
        /// </summary>
        public ErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// پیغام خطای اصلی که از استثناء ایجاد شده گرفته می شود
        /// </summary>
        public string OriginalMessage { get; set; }

        /// <summary>
        /// نام متدی که اجرای آن منجر به ایجاد خطا شده است
        /// </summary>
        public string FaultingMethod { get; private set; }

        /// <summary>
        /// نام کلاس استثناء ایجاد شده هنگام بروز خطا
        /// </summary>
        public string FaultType { get; private set; }

        /// <summary>
        /// رد کامل توابع در حال اجرا هنگام بروز خطا
        /// </summary>
        public string StackTrace { get; private set; }

        /// <summary>
        /// نسخه ساختار اطلاعاتی موجود در این کلاس
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// یک نمونه از این کلاس با استفاده از اطلاعات موجود در آبجکت استثناء داده شده می سازد
        /// </summary>
        /// <param name="exception">آبجکت استثناء داده شده</param>
        /// <param name="errorCode">کد خطای سرویس</param>
        /// <returns>نمونه ای از این کلاس حاوی اطلاعات آبجکت استثناء داده شده</returns>
        public static ErrorDetail CreateFromException(Exception exception, ErrorCode errorCode)
        {
            var targetSite = exception.TargetSite;
            var typeName = (targetSite.DeclaringType != null)
                ? targetSite.DeclaringType.Name
                : "[anonymous]";
            var errorDetail = new ErrorDetail()
            {
                TimestampUtc = DateTime.UtcNow.ToString(AppConstants.TimestampFormat),
                ErrorCode = errorCode,
                OriginalMessage = exception.Message,
                FaultingMethod = String.Format("{0}.{1}", typeName, exception.TargetSite.Name),
                FaultType = exception.GetType().Name,
                StackTrace = GetBriefStackTrace(exception.StackTrace)
            };

            return errorDetail;
        }

        /// <summary>
        /// نمایش متنی برای این کلاس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>نمایش متنی برای این کلاس</returns>
        public override string ToString()
        {
            var display = String.Format(DisplayTemplate, Environment.NewLine,
                TimestampUtc, FaultType, FaultingMethod, OriginalMessage, StackTrace, Version);
            return display;
        }

        private static string GetBriefStackTrace(string fullTrace)
        {
            var items = fullTrace.Split(
                new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(Environment.NewLine, items
                .Where(item => item.Contains("SPPC.Framework") || item.Contains("SPPC.Tadbir")));
        }

        private const string DisplayTemplate =
            "Timestamp (UTC) : {1}{0}An exception of type '{2}' occured while executing method '{3}'.{0}" +
            "{4}{0}{0}Stack trace :{0}{5}{0}(metadata version : {6}){0}";
    }
}
