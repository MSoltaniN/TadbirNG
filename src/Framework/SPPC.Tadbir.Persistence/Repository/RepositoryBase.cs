using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
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
            _dbConsole = new SqlServerConsole
            {
                ConnectionString = context.UnitOfWork.CompanyConnection
            };        // TODO: !! CARDINAL SIN !! Inject this later
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
        /// به روش آسنکرون، مشخص می کند که آیا موجودیت مالی داده شده به شعبه مورد نظر وابسته است یا نه؟
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت مورد بررسی</typeparam>
        /// <param name="branchId">شناسه دیتابیسی شعبه سازمانی مورد نظر</param>
        /// <returns>اگر وابستگی وجود داشته باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        protected async Task<bool> HasBranchReference<TEntity>(int branchId)
            where TEntity : FiscalEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            int count = await repository
                .GetEntityQuery()
                .Where(item => item.BranchId == branchId)
                .CountAsync();
            return count > 0;
        }

        /// <summary>
        /// مشخص می کند که آیا موجودیت مالی داده شده به شعبه مورد نظر وابسته است یا نه؟
        /// </summary>
        /// <param name="entityType">نوع دات نتی موجودیت مالی مورد بررسی</param>
        /// <param name="branchId">شناسه دیتابیسی شعبه سازمانی مورد نظر</param>
        /// <returns>اگر وابستگی وجود داشته باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        protected bool HasBranchReference(Type entityType, int branchId)
        {
            int referenceCount = 0;
            var idItems = GetModelTypeItems(entityType);
            if (idItems != null)
            {
                string command = String.Format(_branchReferenceScript, idItems[0], idItems[1], branchId);
                var result = _dbConsole.ExecuteQuery(command);
                if (result != null && result.Rows.Count > 0 && result.Rows[0].ItemArray.Length > 0)
                {
                    referenceCount = Int32.Parse(result.Rows[0].ItemArray[0].ToString());
                }
            }

            return referenceCount > 0;
        }

        /// <summary>
        /// مشخص می کند که آیا موجودیت مالی داده شده به دوره مالی مورد نظر وابسته است یا نه؟
        /// </summary>
        /// <param name="entityType">نوع دات نتی موجودیت مالی مورد بررسی</param>
        /// <param name="fiscalPeriodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر وابستگی وجود داشته باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        protected bool HasFiscalPeriodReference(Type entityType, int fiscalPeriodId)
        {
            int referenceCount = 0;
            var idItems = GetModelTypeItems(entityType);
            if (idItems != null)
            {
                string command = String.Format(_fiscalPeriodReferenceScript, idItems[0], idItems[1], fiscalPeriodId);
                var result = _dbConsole.ExecuteQuery(command);
                if (result != null && result.Rows.Count > 0 && result.Rows[0].ItemArray.Length > 0)
                {
                    referenceCount = Int32.Parse(result.Rows[0].ItemArray[0].ToString());
                }
            }

            return referenceCount > 0;
        }

        private static string[] GetModelTypeItems(Type entityType)
        {
            Verify.ArgumentNotNull(entityType, nameof(entityType));
            var idItems = entityType.FullName.Split('.');

            // Subsystem-specific model types are expected to have full type name like below :
            // SPPC.Tadbir.Model.[Schema].[Table]
            if (idItems.Count() != 5)
            {
                return null;
            }

            return idItems.Skip(3).ToArray();
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

        private const string _branchReferenceScript = @"
SELECT COUNT(*) FROM [{0}].[{1}] WHERE BranchID = {2}";
        private const string _fiscalPeriodReferenceScript = @"
SELECT COUNT(*) FROM [{0}].[{1}] WHERE FiscalPeriodID = {2}";
        private readonly IRepositoryContext _context;
        private readonly ISqlConsole _dbConsole;
    }
}
