using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddDbContext<TadbirContext>();
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ILookupRepository, LookupRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<ISecurityRepository, SecurityRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IRequisitionRepository, RequisitionRepository>();
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

            app.UseCors(
                options => options
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .WithHeaders("Content-Type", "X-Tadbir-AuthTicket"));

            app.UseMvc();
        }
    }
}
