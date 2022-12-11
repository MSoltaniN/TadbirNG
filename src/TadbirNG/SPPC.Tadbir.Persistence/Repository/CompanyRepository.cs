using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public class CompanyRepository
        : SystemEntityLoggingRepository<CompanyDb, CompanyDbViewModel>, ICompanyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="appConfig">امکان خواندن تنظیمات کلی برنامه را فراهم می کند</param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز در سرویس وب را فراهم می کند</param>
        public CompanyRepository(IRepositoryContext context, IOperationLogRepository log,
            IConfiguration appConfig, IApiPathProvider pathProvider)
            : base(context, log)
        {
            _appConfig = appConfig;
            _pathProvider = pathProvider;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        public async Task<PagedList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var companies = new List<CompanyDb>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
                companies.AddRange(await repository.GetByCriteriaAsync(await GetSecurityFilterAsync()));
            }

            await ReadAsync(gridOptions);
            return new PagedList<CompanyDbViewModel>(
                companies.Select(c => Mapper.Map<CompanyDbViewModel>(c)), gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        public async Task<CompanyDbViewModel> GetCompanyAsync(int companyId)
        {
            return await GetByIdAsync<CompanyDb, CompanyDbViewModel>(companyId);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="companyView">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        public async Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel companyView)
        {
            Verify.ArgumentNotNull(companyView, nameof(companyView));
            if (await IsActivatedCompanyAsync(companyView))
            {
                return companyView;
            }

            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            CompanyDb company;
            if (companyView.Id == 0)
            {
                companyView.Server = GetServerName();
                await PrepareNewCompanyAsync(companyView);
                company = Mapper.Map<CompanyDb>(companyView);
                await InsertAsync(repository, company);
            }
            else
            {
                company = await repository.GetByIDAsync(companyView.Id);
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
            var company = await repository.GetByIDWithTrackingAsync(companyId, c => c.RoleCompanies);
            if (company != null)
            {
                company.RoleCompanies.Clear();
                company.IsActive = false;
                repository.Update(company);
                OnEntityAction(OperationId.Delete);
                await FinalizeActionAsync(company);
            }
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteCompaniesAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            foreach (int item in items)
            {
                var company = await repository.GetByIDWithTrackingAsync(item, c => c.RoleCompanies);
                if (company != null)
                {
                    company.RoleCompanies.Clear();
                    company.IsActive = false;
                    repository.Update(company);
                }
            }

            await UnitOfWork.CommitAsync();
            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از شرکت های موجود را برمی گرداند که نام شرکت یا نام دیتابیس آنها
        /// مشابه شرکت داده شده است
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>مجموعه ای از شرکت های موجود با نام شرکت یا نام دیتابیس تکراری</returns>
        public async Task<IEnumerable<CompanyDbViewModel>> GetDuplicateCompaniesAsync(CompanyDbViewModel company)
        {
            Verify.ArgumentNotNull(company, nameof(company));
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            return await repository
                .GetEntityQuery()
                .Where(comp => comp.Id != company.Id &&
                    (comp.DbName == company.DbName || comp.Name == company.Name))
                .Select(comp => Mapper.Map<CompanyDbViewModel>(comp))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک شرکت را خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه یکی از شرکت های موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetCompanyRolesAsync(int companyId)
        {
            RelatedItemsViewModel companyRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var existing = await repository.GetByIDAsync(companyId, co => co.RoleCompanies);
            if (existing != null)
            {
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var enabledRoleIds = existing.RoleCompanies.Select(rc => rc.RoleId);
                var enabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                var disabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => !enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);

                companyRoles = new RelatedItemsViewModel() { Id = companyId };
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => companyRoles.RelatedItems.Add(item));
            }

            return companyRoles;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک شرکت را ذخیره می کند
        /// </summary>
        /// <param name="companyRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        public async Task SaveCompanyRolesAsync(RelatedItemsViewModel companyRoles)
        {
            Verify.ArgumentNotNull(companyRoles, nameof(companyRoles));
            var repository = UnitOfWork.GetAsyncRepository<RoleCompany>();
            var existing = await repository.GetByCriteriaAsync(rc => rc.CompanyId == companyRoles.Id);
            if (AreRolesModified(existing, companyRoles))
            {
                if (existing.Count > 0)
                {
                    RemoveUnassignedRoles(repository, existing, companyRoles);
                }

                AddNewRoles(repository, existing, companyRoles);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.RoleAccess);
                Log.Description = await GetCompanyRoleDescriptionAsync(companyRoles.Id);
                await TrySaveLogAsync();
            }
        }

        internal override int? EntityType
        {
            get { return (int)SysEntityTypeId.CompanyDb; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="companyViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="company">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CompanyDbViewModel companyViewModel, CompanyDb company)
        {
            company.Name = companyViewModel.Name;
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
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Name, entity.Name,
                    AppStrings.UserName, entity.UserName, AppStrings.Password, entity.Password,
                    AppStrings.Description, entity.Description)
                : null;
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static bool AreRolesModified(IList<RoleCompany> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(rc => rc.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return !AreEqual(existingItems, enabledItems);
        }

        private static void RemoveUnassignedRoles(
            IRepository<RoleCompany> repository, IList<RoleCompany> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(rc => rc.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rc => rc.RoleId == id)
                    .Single();
                repository.Delete(removed);
            }
        }

        private static void AddNewRoles(
            IRepository<RoleCompany> repository, IList<RoleCompany> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.Select(rc => rc.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var roleCompany = new RoleCompany()
                {
                    RoleId = item.Id,
                    CompanyId = roleItems.Id
                };
                repository.Insert(roleCompany);
            }
        }

        // NOTE: This method compensates for Docker's inability to retain current database context between
        // consecutive script executions. USE NGTadbir is required by Docker, but is invalid while creating
        // a new company.
        private static string GetCompanyScript(string path)
        {
            var lines = File.ReadAllLines(path);
            return String.Join(Environment.NewLine, lines.Skip(3));
        }

        private void CreateDatabase(CompanyDbViewModel company)
        {
            var sqlBuilder = new StringBuilder();
            var scriptPath = _pathProvider.CompanyScript;
            if (!File.Exists(scriptPath))
            {
                throw ExceptionBuilder.NewGenericException<FileNotFoundException>();
            }

            sqlBuilder.AppendFormat(@"
                CREATE DATABASE [{0}]
                GO
                USE [{0}]
                GO
                ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
                GO",
                company.DbName);
            sqlBuilder.AppendLine();
            sqlBuilder.Append(GetCompanyScript(scriptPath));
            DbConsole.ExecuteNonQuery(sqlBuilder.ToString());
        }

        private void CreateDatabaseLogin(CompanyDbViewModel company)
        {
            string loginScript = String.Format(@"
                CREATE LOGIN [{0}] WITH PASSWORD = '{1}', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;
                GO
                ALTER SERVER ROLE securityadmin ADD MEMBER {0};
                GO
                ALTER SERVER ROLE dbcreator ADD MEMBER {0};
                GO
                ALTER SERVER ROLE sysadmin ADD MEMBER {0};
                GO
                ALTER AUTHORIZATION ON DATABASE::{2} TO {0}
                GO",
                company.UserName, company.Password, company.DbName);
            DbConsole.ExecuteNonQuery(loginScript);
        }

        private string GetServerName()
        {
            string connection = _appConfig.GetConnectionString("TadbirSysApi");
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            return sqlBuilder.DataSource;
        }

        // NOTE: This method is called immediately after a company database is created. Because current login
        // automatically owns a new database (i.e. owner of a new database becomes current SQL Server login),
        // we need to connect to the new database using current login credentials...
        private async Task ImportLookupsAsync(string dbName)
        {
            var csBuilder = new SqlConnectionStringBuilder(UnitOfWork.CompanyConnection)
            {
                InitialCatalog = dbName
            };
            UnitOfWork.SwitchCompany(csBuilder.ConnectionString);
            UnitOfWork.UseCompanyContext();

            await ImportLookupAsync<Province, ProvinceViewModel>(_pathProvider.IranStates);
            await ImportLookupAsync<City, CityViewModel>(_pathProvider.IranCities);
            await ImportLookupAsync<TaxCurrency, TaxCurrency>(_pathProvider.TaxCurrencies);

            UnitOfWork.UseSystemContext();
            await SetCurrentCompanyAsync(UserContext.CompanyId);
        }

        private async Task ImportLookupAsync<TModel, TViewModel>(string dataFilePath)
            where TModel : class, IEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TModel>();
            var items = JsonHelper.To<IEnumerable<TViewModel>>(File.ReadAllText(dataFilePath));
            foreach (var item in items)
            {
                Reflector.SetProperty(item, "Id", 0);
                repository.Insert(Mapper.Map<TModel>(item));
            }

            await UnitOfWork.CommitAsync();
        }

        private bool IsDuplicateDatabaseName(string dbName)
        {
            string dbNameScript = String.Format(
                @"SELECT [name] FROM sys.databases
                    WHERE LOWER([name]) = LOWER('{0}')", dbName);
            DataTable table = DbConsole.ExecuteQuery(dbNameScript);
            return table.Rows.Count > 0;
        }

        private bool IsDuplicateCompanyUserName(string userName)
        {
            string userScript = String.Format(
                $"SELECT name FROM sys.sql_logins WHERE LOWER(name) = '{userName?.ToLower()}'");
            var table = DbConsole.ExecuteQuery(userScript);
            return table.Rows.Count > 0;
        }

        private async Task<string> GetCompanyRoleDescriptionAsync(int companyId)
        {
            string description = String.Empty;
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                string template = Context.Localize(AppStrings.RolesWithAccessToResource);
                string entity = Context.Localize(AppStrings.Company).ToLower();
                description = String.Format(template, entity, company.Name);
            }

            return description;
        }

        private async Task<Expression<Func<CompanyDb, bool>>> GetSecurityFilterAsync()
        {
            if (!UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleCompany>();
                var companyIds = await repository
                    .GetEntityQuery()
                    .Where(rc => rc.Company.IsActive && UserContext.Roles.Contains(rc.RoleId))
                    .Select(rc => rc.CompanyId)
                    .Distinct()
                    .ToListAsync();
                return company => companyIds.Contains(company.Id);
            }
            else
            {
                return company => company.IsActive;
            }
        }

        private async Task<bool> IsActivatedCompanyAsync(CompanyDbViewModel company)
        {
            bool activated = false;
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var inactive = await repository.GetSingleByCriteriaAsync(
                c => c.Id != company.Id && c.DbName == company.DbName && !c.IsActive);
            if (inactive != null)
            {
                await PrepareNewCompanyAsync(company);
                OnEntityAction(OperationId.Create);
                UpdateExisting(company, inactive);
                inactive.IsActive = true;
                company.Id = inactive.Id;
                Log.Description = Context.Localize(GetState(inactive));
                repository.Update(inactive);
                await FinalizeActionAsync(inactive);
                activated = true;
            }

            return activated;
        }

        private async Task PrepareNewCompanyAsync(CompanyDbViewModel company)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            if (!IsDuplicateDatabaseName(company.DbName))
            {
                CreateDatabase(company);
                await ImportLookupsAsync(company.DbName);
            }

            var csBuilder = new SqlConnectionStringBuilder(Context.SystemConnection);
            if (String.IsNullOrEmpty(company.UserName))
            {
                string script = String.Format(@"
                    ALTER AUTHORIZATION ON DATABASE::{0} TO {1}
                    GO", company.DbName, csBuilder.UserID);
                DbConsole.ExecuteNonQuery(script);
            }
            else if (!IsDuplicateCompanyUserName(company.UserName))
            {
                CreateDatabaseLogin(company);
            }
            else
            {
                // User may have typed wrong password for existing login; Overwrite it with correct password...
                // NOTE: If user enters wrong password for an existing login NOT assigned to a company, we can't
                // correct it, because login passwords are inaccessible.
                string existingPass = await repository
                    .GetEntityQuery()
                    .Where(c => c.UserName == company.UserName)
                    .Select(c => c.Password)
                    .FirstOrDefaultAsync();
                company.Password = existingPass ?? company.Password;
                string script = String.Format(@"
                    USE [{0}]
                    GO
                    ALTER AUTHORIZATION ON DATABASE::{0} TO {1}
                    GO", company.DbName, company.UserName);
                DbConsole.ExecuteNonQuery(script);
            }
        }

        private readonly IConfiguration _appConfig;
        private readonly IApiPathProvider _pathProvider;
    }
}
