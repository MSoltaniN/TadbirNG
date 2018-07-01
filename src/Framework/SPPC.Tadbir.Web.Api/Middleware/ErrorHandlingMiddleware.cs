using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Web.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonHelper.From(ErrorFromException(exception), false);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)StatusCodeFromException(exception);
            return context.Response.WriteAsync(result);
        }

        private static HttpStatusCode StatusCodeFromException(Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            return code;
        }

        private static object ErrorFromException(Exception exception)
        {
            Verify.ArgumentNotNull(exception, "exception");
            return new { error = exception.Message };
        }

        private readonly RequestDelegate _next;
    }
}
