﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public class CompanyRepository : SystemLoggingRepository<CompanyDb, CompanyDbViewModel>, ICompanyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="appConfig">امکان خواندن تنظیمات کلی برنامه را فراهم می کند</param>
        public CompanyRepository(IRepositoryContext context, IOperationLogRepository log,
            IConfiguration appConfig)
            : base(context, log)
        {
            _appConfig = appConfig;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        public async Task<PagedList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var companies = await repository.GetByCriteriaAsync(await GetSecurityFilterAsync());
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
        /// <param name="webRoot">مسیر ریشه نرم افزار</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        public async Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel companyView, string webRoot)
        {
            Verify.ArgumentNotNull(companyView, "companyView");
            var company = default(CompanyDb);
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            if (companyView.Id == 0)
            {
                CreateDatabase(companyView, webRoot);
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
        /// به روش آسنکرون، مشخص میکند که نام وارد شده برای دیتابیس تکراری میباشد یا خیر
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>اگر نام دیتابیس تکراری بود مقدار درست در غیر اینصورت مقدار نادرست را برمی گرداند</returns>
        public async Task<bool> IsDuplicateCompanyAsync(CompanyDbViewModel company)
        {
            Verify.ArgumentNotNull(company, nameof(company));
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var existing = await repository.GetSingleByCriteriaAsync(
                comp => comp.Id != company.Id && comp.DbName == company.DbName);
            if (existing != null)
            {
                return true;
            }

            var isDuplicateDb = IsDuplicateDatabaseName(company.DbName);
            if (isDuplicateDb)
            {
                return !IsTadbirDatabase(company.DbName);
            }

            return false;
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
            DataTable table = DbConsole.ExecuteQuery(userScript);
            List<DataRow> rows = table.AsEnumerable().ToList();
            string userName = !String.IsNullOrEmpty(company.UserName)
                ? company.UserName.ToLower()
                : String.Empty;
            return rows.Any(row =>
                row["name"]
                    .ToString()
                    .ToLower()
                    .Equals(userName));
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
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9}",
                    AppStrings.Name, entity.Name, AppStrings.Server, entity.Server,
                    AppStrings.UserName, entity.UserName, AppStrings.Password, entity.Password,
                    AppStrings.Description, entity.Description)
                : null;
        }

        private void CreateDatabase(CompanyDbViewModel company, string webRoot)
        {
            if (!IsDuplicateDatabaseName(company.DbName))
            {
                var scriptPath = Path.Combine(webRoot, @"static\Tadbir_CreateDbObjects.sql");
                if (!File.Exists(scriptPath))
                {
                    throw ExceptionBuilder.NewGenericException<FileNotFoundException>();
                }

                string companyScript = String.Format(
                    @"CREATE DATABASE {0}
                    GO
                    USE {0}
                    GO",
                    company.DbName);

                companyScript += Environment.NewLine;
                companyScript += File.ReadAllText(scriptPath);
                DbConsole.ExecuteNonQuery(companyScript);
            }

            CreateDatabaseLogin(company);
            SetServerName(company);
        }

        private void CreateDatabaseLogin(CompanyDbViewModel company)
        {
            if (String.IsNullOrEmpty(company.UserName))
            {
                return;
            }

            string loginScript = string.Format(
                @"CREATE LOGIN [{0}] WITH PASSWORD = '{1}', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;
                    GO
                    Use [{2}];
                    GO
                    IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'{0}')
                    BEGIN
                    CREATE USER [{0}] FOR LOGIN [{0}]
                    EXEC sp_addrolemember N'db_owner', N'{0}'    
                    END;
                    GO",
                company.UserName, company.Password, company.DbName);
            DbConsole.ExecuteNonQuery(loginScript);
        }

        private void SetServerName(CompanyDbViewModel company)
        {
            string connection = _appConfig.GetConnectionString("TadbirSysApi");
            var sqlBuilder = new SqlConnectionStringBuilder(connection);
            company.Server = sqlBuilder.DataSource;
        }

        private bool IsDuplicateDatabaseName(string dbName)
        {
            string dbNameScript = String.Format(
                @"SELECT [name] FROM sys.databases
                    WHERE LOWER([name]) = LOWER('{0}')", dbName);
            DataTable table = DbConsole.ExecuteQuery(dbNameScript);
            return table.Rows.Count > 0;
        }

        private bool IsTadbirDatabase(string dbName)
        {
            string connectionString = DbConsole.BuildConnectionString(dbName);
            if (DbConsole.TestConnection(connectionString))
            {
                string tableScript = String.Format(
                    @"SELECT * FROM {0}.INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME='Account' AND TABLE_SCHEMA='Finance'",
                        dbName);
                DataTable table = DbConsole.ExecuteQuery(tableScript);
                return table.Rows.Count == 1;
            }

            return false;
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

        private readonly IConfiguration _appConfig;
    }
}
