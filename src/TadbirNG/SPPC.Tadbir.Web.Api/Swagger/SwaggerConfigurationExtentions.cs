using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Web.Api.Swagger
{
    /// <summary>
    ///
    /// </summary>
    public static class SwaggerConfigurationExtentions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            var info = new OpenApiInfo()
            {
                Title = "Tadbir Api",
                Version = "v1.0",
                Description = "Allows programmatic access to Tadbir functionality.",
                Contact = new OpenApiContact
                {
                    Email = "software@sppc.co.ir",
                    Name = "Software Department",
                    Url = new Uri("https://www.sppcco.com"),
                },
                License = new OpenApiLicense
                {
                    Name = "SPPC License",
                    Url = new Uri("https://www.sppcco.com"),
                },
            };
            var scheme = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the X-Tadbir-AuthTicket scheme. Example: \"X-Tadbir-AuthTicket:{token}\"",
                Name = AppConstants.ContextHeaderName,
                In = ParameterLocation.Header
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", info);

                ////c.DescribeAllEnumsAsStrings();
                ////c.DescribeStringEnumsInCamelCase();
                c.DescribeAllParametersInCamelCase();

                c.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "X-Tadbir-AuthTicket");
                c.AddSecurityDefinition("X-Tadbir-AuthTicket", scheme);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
