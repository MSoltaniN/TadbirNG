using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Service;

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
            services.AddDbContext<TadbirContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TadbirApi")));
            services.AddLocalization();
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IDetailAccountRepository, DetailAccountRepository>();
            services.AddTransient<ICostCenterRepository, CostCenterRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IAccountItemRepository, AccountItemRepository>();
            services.AddTransient<ILookupRepository, LookupRepository>();
            services.AddTransient<ISecurityRepository, SecurityRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();
            services.AddTransient<IFiscalPeriodRepository, FiscalPeriodRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IRelationRepository, RelationRepository>();
            services.AddTransient<IMetadataRepository, MetadataRepository>();
            services.AddTransient<IConfigRepository, ConfigRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDomainMapper, DomainMapper>();
            services.AddTransient<DbContext, TadbirContext>();
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<ISecurityContextManager, ServiceContextManager>();
            services.AddTransient<ITextEncoder<SecurityContext>, Base64Encoder<SecurityContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                    .WithExposedHeaders("X-Tadbir-AuthTicket", "X-Total-Count")
                    .WithHeaders("Content-Type", "Accept-Language", "X-Tadbir-AuthTicket", "X-Tadbir-GridOptions"));

            app.UseMvc();
        }

        private void ConfigureLocalization(IApplicationBuilder app)
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
