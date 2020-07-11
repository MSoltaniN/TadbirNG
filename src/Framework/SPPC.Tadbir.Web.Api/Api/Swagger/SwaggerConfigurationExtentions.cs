using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace SPPC.Tadbir.Web.Api.Api.Swagger
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                   "v1",
                   new Info()
                   {
                       Title = "SPPCCO Library Api",
                       Version = "v1",
                       Description = "Through this Api you can access Apis",
                       Contact = new Contact
                       {
                           Email = "ms.pishbin2@gmail.com",
                           Name = "nazdar pishbin",
                           Url = "https://www.sppcco.com/",
                       },
                       License = new License
                       {
                           Name = "License",
                           Url = "https://www.sppcco.com/",
                       },
                   });

                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllParametersInCamelCase();

                c.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "X-Tadbir-AuthTicket");
                c.AddSecurityDefinition("X-Tadbir-AuthTicket", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the X-Tadbir-AuthTicket scheme. Example: \"X-Tadbir-AuthTicket:{token}\"",
                    Name = "X-Tadbir-AuthTicket",
                    In = "header"
                });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1");
            });
        }
    }

}
