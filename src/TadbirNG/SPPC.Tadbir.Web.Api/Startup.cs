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
    /// <summary>
    /// سرویس وب را پیش از اجرا آماده سازی و تنظیم می کند
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="configuration">اطلاعات پیکربندی موجود برای سرویس</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// اطلاعات پیکربندی موجود برای سرویس
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// خدمات مورد نیاز سرویس وب را به زیرساخت اجرایی اضافه می کند
        /// </summary>
        /// <param name="services">اطلاعا فراداده ای خدمات فعال در سرویس وب جاری</param>
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
        /// <summary>
        /// خدمات زیرساختی مورد نیاز در مسیر اجرایی سرویس وب را پیکربندی می کند
        /// </summary>
        /// <param name="app">امکان پیکربندی مسیر اجرایی سرویس وب را فراهم می کند</param>
        /// <param name="env">اطلاعات محیط میزبانی سرویس را فراهم می کند</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPPC.Tadbir.Web.Api (v1.0)"));

            ConfigureLocalization(app);
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors(
                options => options
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .WithExposedHeaders(AppConstants.ContextHeaderName, AppConstants.TotalCountHeaderName)
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
