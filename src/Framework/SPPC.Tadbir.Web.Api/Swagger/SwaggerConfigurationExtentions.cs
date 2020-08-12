﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
            var info = new Info()
            {
                Title = "Tadbir Api",
                Version = "v1",
                Description = "Allows programmatic access to Tadbir functionality.",
                Contact = new Contact
                {
                    Email = "software@sppc.co.ir",
                    Name = "Software Department",
                    Url = "https://www.sppcco.com/",
                },
                License = new License
                {
                    Name = "License",
                    Url = "https://www.sppcco.com/",
                },
            };
            var scheme = new ApiKeyScheme
            {
                Description = "JWT Authorization header using the X-Tadbir-AuthTicket scheme. Example: \"X-Tadbir-AuthTicket:{token}\"",
                Name = "X-Tadbir-AuthTicket",
                In = "header"
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);

                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllParametersInCamelCase();

                c.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "X-Tadbir-AuthTicket");
                c.AddSecurityDefinition("X-Tadbir-AuthTicket", scheme);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tadbir Api (v1)");
            });
        }
    }
}