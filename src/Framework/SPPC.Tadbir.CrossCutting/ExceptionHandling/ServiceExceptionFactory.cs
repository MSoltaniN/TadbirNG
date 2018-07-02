using System;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.ExceptionHandling
{
    public static class ServiceExceptionFactory
    {
        public static ServiceException FromException(Exception exception)
        {
            Verify.ArgumentNotNull(exception, "exception");
            var context = ContextFromException(exception);
            context.ErrorDetail = ErrorDetail.CreateFromException(exception);
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
                context = GetOtherRuntimeContext(exception);
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

        private static ServiceExceptionContext GetOtherRuntimeContext(Exception exception)
        {
            return new ServiceExceptionContext(
                ErrorCode.UnknownRuntimeError, ErrorSource.DotNetRuntime, ErrorMessage.UnknownRuntimeError);
        }

        private static ServiceExceptionContext GetWebApiContext(Exception exception)
        {
            var context = default(ServiceExceptionContext);
            //if (exception is HttpResponseException)
            //{
            //    context = new ServiceExceptionContext(
            //        ErrorCode.WebApiRuntimeError, ErrorSource.AspNetWebApiRuntime, ErrorMessage.WebApiRuntimeError);
            //}

            return context;
        }
    }
}
