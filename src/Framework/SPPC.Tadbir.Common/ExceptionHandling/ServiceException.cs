using System;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.ExceptionHandling
{
    public class ServiceException : Exception
    {
        public ServiceException()
            : this(DefaultMessage, null)
        {
        }

        public ServiceException(string message)
            : this(message, null)
        {
        }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
            Source = DefaultSource;
            ErrorCode = ErrorCode.UnknownRuntimeError;
            _errorDetail = new ErrorDetail();
        }

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
