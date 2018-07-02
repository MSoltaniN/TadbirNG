using System;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// خطای سرویسی را با استفاده از آبجکت های خطای مختلف می سازد
    /// </summary>
    public static class ServiceExceptionFactory
    {
        /// <summary>
        /// آبجکت خطای عمومی داده شده را به یک نمونه خطای سرویسی حاوی اطلاعات فنی خطا تبدیل می کند
        /// </summary>
        /// <param name="exception">آبجکت خطای عمومی</param>
        /// <returns>خطای سرویسی تبدیل شده از خطای عمومی</returns>
        public static ServiceException FromException(Exception exception)
        {
            Verify.ArgumentNotNull(exception, "exception");
            var context = ContextFromException(exception);
            context.ErrorDetail = ErrorDetail.CreateFromException(exception, context.ErrorCode);
            return new ServiceException(context, exception);
        }

        private static ServiceExceptionContext ContextFromException(Exception exception)
        {
            var context = new ServiceExceptionContext();
            if (exception is JsonException)
            {
                context = GetJsonSerializerContext(exception);
            }
            else if (exception is AutoMapperConfigurationException || exception is AutoMapperMappingException)
            {
                context = GetAutoMapperContext(exception);
            }
            else if (exception is InvalidOperationException)
            {
                context = GetIocContainerContext(exception);
            }
            else if (exception is DbUpdateException)
            {
                context = GetOrmMappingContext(exception);
            }
            else if (exception is SqlException)
            {
                context = GetDataProviderContext(exception);
            }
            else
            {
                context = GetOtherRuntimeContext();
            }

            return context;
        }

        private static ServiceExceptionContext GetJsonSerializerContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            if (exception is JsonException)
            {
                context = new ServiceExceptionContext(
                    ErrorCode.JsonSerializerError, ErrorSource.JsonSerializer, ErrorMessage.JsonSerializerError);
            }

            return context;
        }

        private static ServiceExceptionContext GetAutoMapperContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            if (exception is AutoMapperConfigurationException)
            {
                context = new ServiceExceptionContext(
                    ErrorCode.AutoMapperConfigurationError, ErrorSource.AutoMapper, ErrorMessage.AutoMapperConfigurationError);
            }
            else if (exception is AutoMapperMappingException)
            {
                context = new ServiceExceptionContext(
                    ErrorCode.AutoMapperMappingError, ErrorSource.AutoMapper, ErrorMessage.AutoMapperMappingError);
            }

            return context;
        }

        private static ServiceExceptionContext GetOrmMappingContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            if (exception is DbUpdateException)
            {
                context = new ServiceExceptionContext(
                    ErrorCode.OrmMappingError, ErrorSource.EntityFramework, ErrorMessage.OrmMappingError);
            }

            return context;
        }

        private static ServiceExceptionContext GetDataProviderContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            if (exception is SqlException)
            {
                context = new ServiceExceptionContext(
                    ErrorCode.DataProviderError, ErrorSource.SqlServerAdoNet, ErrorMessage.DataProviderError);
            }

            return context;
        }

        private static ServiceExceptionContext GetIocContainerContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            if (exception is InvalidOperationException && exception.Source.Contains("DependencyInjection"))
            {
                context = new ServiceExceptionContext(
                    ErrorCode.TypeResolutionError, ErrorSource.IocContainer, ErrorMessage.TypeResolutionError);
            }
            else
            {
                context = new ServiceExceptionContext(
                    ErrorCode.UnknownRuntimeError, ErrorSource.DotNetRuntime, ErrorMessage.UnknownRuntimeError);
            }

            return context;
        }

        private static ServiceExceptionContext GetOtherRuntimeContext()
        {
            return new ServiceExceptionContext(
                ErrorCode.UnknownRuntimeError, ErrorSource.DotNetRuntime, ErrorMessage.UnknownRuntimeError);
        }
    }
}
