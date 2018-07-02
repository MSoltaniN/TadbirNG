using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// اطلاعات تکمیلی یک خطای سرویسی را نگهداری می کند
    /// </summary>
    public class ServiceExceptionContext
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ServiceExceptionContext()
        {
            ErrorCode = ErrorCode.UnknownRuntimeError;
            Source = ErrorSource.DotNetRuntime;
            Message = ErrorMessage.UnknownRuntimeError;
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با کد خطا، منبع خطا و پیغام خطای داده شده می سازد
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public ServiceExceptionContext(ErrorCode errorCode, string source, string message)
        {
            ErrorCode = errorCode;
            Source = source;
            Message = message;
        }

        /// <summary>
        /// کد خطای مورد استفاده در خطای سرویسی
        /// </summary>
        public ErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// منبع خطای سرویسی
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// پیغام خطا
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// جزییات فنی خطا
        /// </summary>
        public ErrorDetail ErrorDetail { get; set; }
    }
}
