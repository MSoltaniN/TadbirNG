using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;

namespace SPPC.Licensing.Local.Web
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
            services.AddControllers()
                .AddNewtonsoftJson();

            var container = new TypeContainer(services, Configuration);
            container.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Some neutral change to trigger build server
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureLocalization(app);
            app.UseCors(
                options => options
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .WithHeaders("Content-Type", "Accept-Language", LicenseConstants.InstanceHeaderName,
                        AppConstants.LicenseHeaderName, AppConstants.ContextHeaderName));
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
