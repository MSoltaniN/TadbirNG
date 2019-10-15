using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public class CompanyRepository : LoggingRepository<CompanyDb, CompanyDbViewModel>, ICompanyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="sqlConsole">امکان ارتباط مستقیم با بانک اطلاعاتی</param>
        public CompanyRepository(IRepositoryContext context, IOperationLogRepository log, ISqlConsole sqlConsole)
            : base(context, log)
        {
            _sqlConsole = sqlConsole;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        public async Task<IList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var companies = await repository.GetAllAsync();
            return companies
                .Select(c => Mapper.Map<CompanyDbViewModel>(c))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد شرکت های تعریف شده در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شرکت های تعریف شده در شرکت مشخص شده</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var items = await repository.GetAllAsync();
            return items
                .Select(comp => Mapper.Map<CompanyDbViewModel>(comp))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        public async Task<CompanyDbViewModel> GetCompanyAsync(int companyId)
        {
            CompanyDbViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                item = Mapper.Map<CompanyDbViewModel>(company);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="companyView">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="webHostPath">مسیر ریشه نرم افزار</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        public async Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel companyView, string webHostPath)
        {
            _webRootPath = webHostPath;
            Verify.ArgumentNotNull(companyView, "companyView");
            var company = default(CompanyDb);
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            if (companyView.Id == 0)
            {
                CreateDatabase(companyView);
                company = Mapper.Map<CompanyDb>(companyView);
                await InsertAsync(repository, company);
            }
            else
            {
                company = await repository.GetByIDAsync(
                    companyView.Id);
                if (company != null)
                {
                    await UpdateAsync(repository, company, companyView);
                }
            }

            return Mapper.Map<CompanyDbViewModel>(company);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        public async Task DeleteCompanyAsync(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                await DeleteAsync(repository, company);
            }
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteCompaniesAsync(IEnumerable<int> items)
        {
            foreach (int item in items)
            {
                await DeleteCompanyAsync(item);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که نام وارد شده برای دیتابیس تکرای میباشد یا خیر
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>اگر نام دیتابیس تکراری بود مقدار درست در غیر اینصورت مقدار نادرست را برمیگرداند</returns>
        public async Task<bool> IsDuplicateCompanyAsync(CompanyDbViewModel company)
        {
            Verify.ArgumentNotNull(company, "company");

            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var items = await repository
                .GetByCriteriaAsync(comp => comp.DbName == company.DbName);

            if (items.Count == 0)
            {
                return IsDuplicateDatabaseName(company.DbName);
            }

            return (items.SingleOrDefault(comp => comp.Id != company.Id) != null);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت، شعبه و دوره مالی را ایجاد میکند
        /// </summary>
        /// <param name="initialCompany">اطلاعات شرکت، شعبه و دوره مالی برای ایجاد شرکت</param>
        /// <param name="webHostPath">مسیر ریشه نرم افزار</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد شده</returns>
        public async Task<CompanyDbViewModel> SaveInitialCompanyAsync(InitialCompanyViewModel initialCompany, string webHostPath)
        {
            _webRootPath = webHostPath;
            Verify.ArgumentNotNull(initialCompany, "initialCompany");
            var company = default(CompanyDb);
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();

            CreateDatabase(initialCompany.Company);
            company = Mapper.Map<CompanyDb>(initialCompany.Company);
            await InsertAsync(repository, company);

            UnitOfWork.UseCompanyContext();
            CompanyConnection = BuildConnectionString(company);

            SaveBranch(initialCompany.Branch, company.Id);
            SaveFiscalPeriod(initialCompany.FiscalPeriod, company.Id);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<CompanyDbViewModel>(company);
        }

        /// <summary>
        /// مشخص میکند که نام کاربری وارد شده تکرای میباشد یا خیر
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>اگر نام کاربری تکراری بود مقدار درست در غیر اینصورت مقدار نادرست را برمیگرداند</returns>
        public bool IsDuplicateCompanyUserNameAsync(CompanyDbViewModel company)
        {
            Verify.ArgumentNotNull(company, "company");

            string userScript = @"SELECT name FROM sys.sql_logins";
            DataTable dt = _sqlConsole.ExecuteQuery(userScript);
            List<DataRow> drList = dt.AsEnumerable().ToList();
            return drList.Any(dataRow => dataRow["name"].Equals(company.UserName));
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="companyViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="company">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CompanyDbViewModel companyViewModel, CompanyDb company)
        {
            company.Name = companyViewModel.Name;
            company.Server = companyViewModel.Server;
            company.UserName = companyViewModel.UserName;
            company.Password = companyViewModel.Password;
            company.Description = companyViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CompanyDb entity)
        {
            return (entity != null)
                ? String.Format(
                    "Name : {1}{0}Server : {2}{0}UserName : {3}{0}Password : {4}{0}Description : {5}",
                    Environment.NewLine, entity.Name, entity.Server, entity.UserName, entity.Password, entity.Description)
                : null;
        }

        private void CreateDatabase(CompanyDbViewModel companyViewModel)
        {
            var scriptPath = Path.Combine(_webRootPath, @"static\Tadbir_CreateDbObjects.sql");
            if (!File.Exists(scriptPath))
            {
                throw ExceptionBuilder.NewGenericException<FileNotFoundException>();
            }

            string companyScript = string.Format(@"USE master
                                                  GO
                                                  CREATE DATABASE {0}
                                                  GO
                                                  USE {0}
                                                  GO",
                                                  companyViewModel.DbName);

            companyScript += Environment.NewLine;
            companyScript += File.ReadAllText(scriptPath);

            _sqlConsole.ExecuteNonQuery(companyScript);

            CreateDatabaseLogin(companyViewModel);

            string serverInfoScript = @"SELECT 
                                        @@servername AS 'ServerName',
                                        @@servicename AS 'InstanceName',
                                        DB_NAME() AS 'DatabaseName',
                                        HOST_NAME() AS 'HostName'";

            var serverInfo = _sqlConsole.ExecuteQuery(serverInfoScript);
            string serverName = serverInfo.Rows[0].ItemArray[0].ToString();
            companyViewModel.Server = serverName;
        }

        private void CreateDatabaseLogin(CompanyDbViewModel companyViewModel)
        {
            string loginScript = string.Format(@"CREATE LOGIN [{0}] WITH PASSWORD = '{1}', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;
                                                 GO
                                                 Use [{2}];
                                                 GO
                                                 IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'{0}')
                                                 BEGIN
                                                    CREATE USER [{0}] FOR LOGIN [{0}]
                                                    EXEC sp_addrolemember N'db_owner', N'{0}'    
                                                 END;
                                                 GO",
                                                 companyViewModel.UserName,
                                                 companyViewModel.Password,
                                                 companyViewModel.DbName);

            _sqlConsole.ExecuteNonQuery(loginScript);
        }

        private bool IsDuplicateDatabaseName(string dbName)
        {
            string dbNameScript = @"SELECT [name] FROM sys.databases";
            DataTable dt = _sqlConsole.ExecuteQuery(dbNameScript);
            List<DataRow> drList = dt.AsEnumerable().ToList();
            return drList.Any(dataRow => dataRow["name"].Equals(dbName));
        }

        private void SaveBranch(BranchViewModel branchView, int companyId)
        {
            Verify.ArgumentNotNull(branchView, "branchView");
            Branch branch = default(Branch);

            var repository = UnitOfWork.GetAsyncRepository<Branch>();

            branchView.CompanyId = companyId;
            branch = Mapper.Map<Branch>(branchView);
            repository.Insert(branch);
            //await InsertAsync(repository, branch);
        }

        private void SaveFiscalPeriod(FiscalPeriodViewModel fiscalPeriodView, int companyId)
        {
            Verify.ArgumentNotNull(fiscalPeriodView, "fiscalPeriodView");
            FiscalPeriod fiscalPeriod = default(FiscalPeriod);

            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();

            fiscalPeriodView.CompanyId = companyId;
            fiscalPeriod = Mapper.Map<FiscalPeriod>(fiscalPeriodView);
            repository.Insert(fiscalPeriod);
            //await InsertAsync(repository, fiscalPeriod);
        }

        private string BuildConnectionString(CompanyDb company)
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

        private readonly ISqlConsole _sqlConsole;
        private string _webRootPath;
    }
}
