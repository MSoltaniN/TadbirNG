using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using SPPC.Tadbir.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SPPC.Tadbir.Web.Api.Swagger
{
    /// <summary>
    ///
    /// </summary>
    public class UnauthorizedResponsesOperationFilter : IOperationFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="includeUnauthorizedAndForbiddenResponses"></param>
        /// <param name="schemeName"></param>
        public UnauthorizedResponsesOperationFilter(
            bool includeUnauthorizedAndForbiddenResponses,
            string schemeName = AppConstants.ContextHeaderName)
        {
            _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            _schemeName = schemeName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter);
            if (hasAnonymous)
            {
                return;
            }

            var hasAuthorize = filters.Any(p => p.Filter is ActionFilterAttribute);
            if (!hasAuthorize)
            {
                return;
            }

            if (_includeUnauthorizedAndForbiddenResponses)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
            }

            var req = (OpenApiSecurityRequirement)new Dictionary<OpenApiSecurityScheme, IList<string>>();
            req.Add(new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the X-Tadbir-AuthTicket scheme. Example: \"X-Tadbir-AuthTicket:{token}\"",
                Name = _schemeName,
                In = ParameterLocation.Header
            },
            new List<string>());
            operation.Security = new List<OpenApiSecurityRequirement> { req };
        }

        private readonly bool _includeUnauthorizedAndForbiddenResponses;
        private readonly string _schemeName;
    }
}
