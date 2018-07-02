using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ExceptionHandling
{
    public class ServiceExceptionContext
    {
        public ServiceExceptionContext()
        {
            ErrorCode = ErrorCode.UnknownRuntimeError;
            Source = ErrorSource.DotNetRuntime;
            Message = ErrorMessage.UnknownRuntimeError;
        }

        public ServiceExceptionContext(ErrorCode errorCode, string source, string message)
        {
            ErrorCode = errorCode;
            Source = source;
            Message = message;
        }

        public ErrorCode ErrorCode { get; private set; }
        public string Source { get; private set; }
        public string Message { get; private set; }
        public ErrorDetail ErrorDetail { get; set; }
    }
}
