using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Finance;
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
            Context = context;
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
        /// امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند
        /// </summary>
        protected IRepositoryContext Context { get; }

        /// <summary>
        /// امکان دسترسی به دیتابیس ها و انجام تراکنش های دیتابیسی را فراهم می کند
        /// </summary>
        protected IAppUnitOfWork UnitOfWork
        {
            get { return Context.UnitOfWork; }
        }

        /// <summary>
        /// امکان تبدیل کلاس های مختلف به یکدیگر را فراهم می کند
        /// </summary>
        protected IDomainMapper Mapper
        {
            get { return Context.Mapper; }
        }

        /// <summary>
        /// امکان اجرای مستقیم دستورات دیتابیسی را فراهم می کند
        /// </summary>
        protected ISqlConsole DbConsole
        {
            get { return Context.DbConsole; }
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه
        /// </summary>
        protected UserContextViewModel UserContext
        {
            get { return Context.UserContext; }
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
        public async Task<string> BuildConnectionStringAsync(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            return BuildConnectionString(company);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا موجودیت مالی داده شده به شعبه مورد نظر وابسته است یا نه؟
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت مورد بررسی</typeparam>
        /// <param name="branchId">شناسه دیتابیسی شعبه سازمانی مورد نظر</param>
        /// <returns>اگر وابستگی وجود داشته باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        protected async Task<bool> HasBranchReferenceAsync<TEntity>(int branchId)
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
            var idItems = ModelCatalogue.GetModelTypeItems(entityType);
            if (idItems != null)
            {
                string command = String.Format(_branchReferenceScript, idItems[0], idItems[1], branchId);
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var result = DbConsole.ExecuteQuery(command);
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
            var idItems = ModelCatalogue.GetModelTypeItems(entityType);
            if (idItems != null)
            {
                string command = String.Format(_fiscalPeriodReferenceScript, idItems[0], idItems[1], fiscalPeriodId);
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var result = DbConsole.ExecuteQuery(command);
                if (result != null && result.Rows.Count > 0 && result.Rows[0].ItemArray.Length > 0)
                {
                    referenceCount = Int32.Parse(result.Rows[0].ItemArray[0].ToString());
                }
            }

            return referenceCount > 0;
        }

        /// <summary>
        /// شناسه های دیتابیسی کلیه رکوردهای وابسته به رکورد اطلاعاتی مشخص شده
        /// با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی رکورد اطلاعاتی اصلی</param>
        /// <param name="type">نوع دات نتی موجودیت اصلی</param>
        /// <param name="dependentType">نوع دات نتی موجودیت وابسته به موجودیت اصلی</param>
        /// <returns>مجموعهای از شناسه های دیتابیسی موجودیت وابسته</returns>
        protected int[] GetReferencedItems(int id, Type type, Type dependentType)
        {
            string keyName = (type != typeof(DetailAccount))
                ? type.Name
                : "Detail";
            var idItems = ModelCatalogue.GetModelTypeItems(dependentType);
            string command = String.Format("SELECT {0}ID FROM [{1}].[{2}] WHERE {3}ID = {4}",
                dependentType.Name, idItems[0], idItems[1], keyName, id);
            var result = DbConsole.ExecuteQuery(command);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => Int32.Parse(row.ItemArray[0].ToString()))
                .ToArray();
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
    }
}
