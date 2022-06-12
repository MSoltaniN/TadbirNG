using System;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// کلاس استثناء برای پوشش خطاهای ایجاد شده در سرویس وب
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ServiceException()
            : this(DefaultMessage, null)
        {
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با پیغام خطای داده شده می سازد
        /// </summary>
        /// <param name="message"></param>
        public ServiceException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با پیغام خطا و خطای قبلی داده شده می سازد
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
            Source = DefaultSource;
            ErrorDetail = new ErrorDetail();
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با محتوای خطای سرویسی و خطای قبلی داده شده می سازد
        /// </summary>
        /// <param name="context">اطلاعات تکمیلی خطای سرویسی</param>
        /// <param name="innerException">خطای قبلی در زنجیره خطاهای ایجاد شده</param>
        public ServiceException(ServiceExceptionContext context, Exception innerException)
            : base(context?.Message, innerException)
        {
            Verify.ArgumentNotNull(context, "context");
            Source = context.Source;
            ErrorDetail = context.ErrorDetail;
        }

        /// <summary>
        /// اطلاعات فنی کامل برای این خطای سرویس
        /// </summary>
        public ErrorDetail ErrorDetail { get; protected set; }

        private const string DefaultSource = "Tadbir Web API";
        private const string DefaultMessage = "Tadbir service encountered an error while processing current request.";
    }
}
