using System;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

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
            ErrorCode = ErrorCode.UnknownRuntimeError;
            _errorDetail = new ErrorDetail();
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
            ErrorCode = context.ErrorCode;
            _errorDetail = context.ErrorDetail;
        }

        /// <summary>
        /// کد خطای مربوط به این خطای سرویس
        /// </summary>
        public ErrorCode ErrorCode { get; protected set; }

        /// <summary>
        /// اطلاعات فنی کامل برای این خطای سرویس به صورت سریال شده متنی
        /// </summary>
        public string ErrorDetail
        {
            get { return JsonHelper.From(_errorDetail); }
        }

        private const string DefaultSource = "Tadbir Web API";
        private const string DefaultMessage = "Tadbir service encountered an error while processing current request.";
        private readonly ErrorDetail _errorDetail;
    }
}
