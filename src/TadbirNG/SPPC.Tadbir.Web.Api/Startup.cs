using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Middleware;
using SPPC.Tadbir.Web.Api.Swagger;

namespace SPPC.Tadbir.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AuthorizeRequestFilter));
            });
            services.AddCors();
            services.AddSwagger();

            var container = new TypeContainer(services, Configuration);
            container.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPPC.Tadbir.Web.Api (v1.0)"));
            }

            ConfigureLocalization(app);
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors(
                options => options
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Tadbir-AuthTicket", "X-Total-Count")
                    .WithHeaders("Content-Type", "Accept-Language", AppConstants.ContextHeaderName,
                        Constants.InstanceHeaderName, AppConstants.GridOptionsHeaderName,
                        AppConstants.ParametersHeaderName, AppConstants.LicenseHeaderName));

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureLocalization(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("fa-IR"),
                new CultureInfo("fa"),
                new CultureInfo("en-US"),
                new CultureInfo("en")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa", "fa"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}
