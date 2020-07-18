using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
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
            bool includeUnauthorizedAndForbiddenResponses, string schemeName = "X-Tadbir-AuthTicket")
        {
            _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            _schemeName = schemeName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
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
                operation.Responses.TryAdd("401", new Response { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new Response { Description = "Forbidden" });
            }

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>> { { _schemeName, Array.Empty<string>() } }
            };
        }

        private readonly bool _includeUnauthorizedAndForbiddenResponses;
        private readonly string _schemeName;
    }
}
