using System;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// کلاس پایه که امکانات اولیه عملیات دیتابیسی را در اختیار کلاس های مشتق شده قرار می دهد
    /// </summary>
    public abstract class RepositoryBase : IRepositoryBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        protected RepositoryBase(IRepositoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// رشته اتصال مرتبط با شرکت جاری
        /// </summary>
        public string CompanyConnection
        {
            get
            {
                return UnitOfWork.CompanyConnection;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    UnitOfWork.SwitchCompany(value);
                    UnitOfWork.UseCompanyContext();
                }
            }
        }

        /// <summary>
        /// پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی
        /// </summary>
        protected IAppUnitOfWork UnitOfWork
        {
            get { return _context.UnitOfWork; }
        }

        /// <summary>
        /// نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی
        /// </summary>
        protected IDomainMapper Mapper
        {
            get { return _context.Mapper; }
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه
        /// </summary>
        protected UserContextViewModel UserContext
        {
            get { return _context.UserContext; }
        }

        /// <summary>
        /// به روش آسنکرون، شرکت جاری در برنامه را به شرکت مشخص شده تغییر می دهد
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر</param>
        public async Task SetCurrentCompanyAsync(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                UnitOfWork.SwitchCompany(BuildConnectionString(company));
            }
        }

        /// <summary>
        /// به روش آسنکرون، رشته اتصال شرکت را ایجاد میکند
        /// </summary>
        /// <param name="companyId">شناسه یکتای شرکت</param>
        /// <returns>رشته اتصال</returns>
        public async Task<string> BuildConnectionString(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            return BuildConnectionString(company);
        }

        private static string BuildConnectionString(CompanyDb company)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};Database={1};", company.Server, company.DbName);
            if (!String.IsNullOrEmpty(company.UserName) && !String.IsNullOrEmpty(company.Password))
            {
                builder.AppendFormat("User ID={0};Password={1};Trusted_Connection=False;MultipleActiveResultSets=True",
                    company.UserName, company.Password);
            }
            else
            {
                builder.Append("Trusted_Connection=True;MultipleActiveResultSets=True");
            }

            return builder.ToString();
        }

        private readonly IRepositoryContext _context;
    }
}
