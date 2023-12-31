﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه
        /// </summary>
        public UserContextViewModel UserContext
        {
            get { return Context.UserContext; }
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
        /// شعبه های زیرمجموعه شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های زیرمجموعه</returns>
        public IEnumerable<int> GetChildTree(int branchId)
        {
            return GetChildTreeAsync(branchId).Result;
        }

        /// <summary>
        /// شعبه های والد شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه زیرمجموعه مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های والد</returns>
        public IEnumerable<int> GetParentTree(int branchId)
        {
            return GetParentTreeAsync(branchId).Result;
        }

        /// <summary>
        /// نام و نام خانوادگی کاربر جاری را با قالب پیش فرض برمی گرداند
        /// </summary>
        /// <returns>نام و نام خانوادگی کاربر جاری</returns>
        protected string GetCurrentUserFullName()
        {
            return $"{UserContext.PersonFullName}";
        }

        /// <summary>
        /// به روش آسنکرون، شرکت جاری در برنامه را به شرکت مشخص شده تغییر می دهد
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر</param>
        protected async Task SetCurrentCompanyAsync(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                UnitOfWork.SwitchCompany(BuildConnectionString(company));
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک موجودیت با شناسه دیتابیسی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع کلاس مدل برای موجودیت</typeparam>
        /// <typeparam name="TEntityView">نوع کلاس مدل نمایشی برای موجودیت</typeparam>
        /// <param name="id">شناسه دیتابیسی سطر اطلاعاتی مورد نظر</param>
        /// <returns>در صورت وجود سطر اطلاعاتی با شناسه داده شده، اطلاعات نمایشی موجودیت و در غیر این صورت
        /// رفرنس بدون مقدار را برمی گرداند</returns>
        protected async Task<TEntityView> GetByIdAsync<TEntity, TEntityView>(int id)
            where TEntity : class, IEntity
            where TEntityView : class, new()
        {
            TEntityView itemView = null;
            var repository = UnitOfWork.GetAsyncRepository<TEntity>();
            var item = await repository.GetByIDAsync(id);
            if (item != null)
            {
                itemView = Mapper.Map<TEntityView>(item);
            }

            return itemView;
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های زیرمجموعه شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های زیرمجموعه</returns>
        protected async Task<IEnumerable<int>> GetChildTreeAsync(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId, br => br.Children);
            AddChildren(branch, tree);
            return tree;
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های والد شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه زیرمجموعه مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های والد</returns>
        protected async Task<IEnumerable<int>> GetParentTreeAsync(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDWithTrackingAsync(branchId);
            var currentBranch = branch;
            while (currentBranch != null)
            {
                tree.Add(currentBranch.Id);
                repository.LoadReference(currentBranch, br => br.Parent);
                currentBranch = currentBranch.Parent;
            }

            return tree;
        }

        /// <summary>
        /// رشته اتصال شرکت با اطلاعات داده شده را ساخته و برمی گرداند
        /// </summary>
        /// <param name="company">اطلاعات شرکت مورد نظر برای ساختن رشته اتصال</param>
        /// <returns>رشته اتصال ساخته شده با اطلاعات شرکت</returns>
        protected string BuildConnectionString(CompanyDb company)
        {
            if (company == null)
            {
                return null;
            }

            var csBuilder = new SqlConnectionStringBuilder(Context.SystemConnection);
            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};Database={1};Connect Timeout=600;", csBuilder.DataSource, company.DbName);
            if (!String.IsNullOrEmpty(company.UserName) && !String.IsNullOrEmpty(company.Password))
            {
                builder.AppendFormat("User ID={0};Password={1};Trusted_Connection=False;MultipleActiveResultSets=False",
                    company.UserName, company.Password);
            }
            else
            {
                builder.AppendFormat("User ID={0};Password={1};Trusted_Connection=False;MultipleActiveResultSets=False",
                    csBuilder.UserID, csBuilder.Password);
            }

            return builder.ToString();
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
            var idItems = ModelCatalogue.GetModelTypeItems(dependentType);
            string command = String.Format("SELECT {0}ID FROM [{1}].[{2}] WHERE {3}ID = {4}",
                dependentType.Name, idItems[0], idItems[1], type.Name, id);
            var result = DbConsole.ExecuteQuery(command);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => Int32.Parse(row.ItemArray[0].ToString()))
                .ToArray();
        }

        /// <summary>
        /// به روش آسنکرون، عبارت موجود برای فیلتر شعبه را از تنظیمات لیست اطلاعاتی خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">تنظیمات لیست اطلاعاتی</param>
        /// <returns>عبارت مورد نیاز برای فیلتر شعبه</returns>
        protected async Task<string> GetBranchFilterAsync(GridOptions gridOptions)
        {
            string branchFilter = String.Empty;
            if (gridOptions.QuickFilter != null)
            {
                var gridFilter = gridOptions.QuickFilter
                    .GetAllFilters()
                    .Where(filter => filter.FieldName == "BranchId")
                    .FirstOrDefault();
                if (gridFilter == null)
                {
                    var childBranches = new List<int>(await GetChildTreeAsync(UserContext.BranchId));
                    childBranches.Insert(0, UserContext.BranchId);
                    branchFilter = String.Format("BranchID IN({0})", String.Join(",", childBranches));
                }
                else
                {
                    branchFilter = gridFilter.ToString();
                }
            }

            return branchFilter;
        }

        /// <summary>
        /// کامل بودن بردار حساب ورودی را بررسی می کند
        /// </summary>
        /// <param name="fullAccount">بردار حساب داده شده برای اعتبارسنجی</param>
        /// <param name="repository">امکان خواندن اطلاعات را با توجه به دسترسی های سطری و شعب فراهم می کند</param>
        /// <returns> در صورت کامل بودن بردار حساب ورودی مقدار درست
        /// و در غیر اینصورت نادرست برمی گرداند</returns>
        protected async Task<bool> IsValidFullAccountAsync(FullAccountViewModel fullAccount, ISecureRepository repository)
        {
            Verify.ArgumentNotNull(fullAccount, nameof(fullAccount));
            var accountId = fullAccount.Account.Id;
            var detailAccountId = fullAccount.DetailAccount.Id;
            var costCenterId = fullAccount.CostCenter.Id;
            var projectId = fullAccount.Project.Id;
            return await repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children,
                    acc => acc.AccountDetailAccounts, acc => acc.AccountCostCenters, acc => acc.AccountProjects)
                .AnyAsync(acc => acc.Id == accountId
                    && acc.Children.Count() == 0
                    && ((detailAccountId > 0 && acc.AccountDetailAccounts.Any(da => da.DetailAccountId == detailAccountId))
                    || (detailAccountId <= 0 && acc.AccountDetailAccounts.Count == 0))
                    && ((costCenterId > 0 && acc.AccountCostCenters.Any(ac => ac.CostCenterId == costCenterId))
                    || (costCenterId <= 0 && acc.AccountCostCenters.Count == 0))
                    && ((projectId > 0 && acc.AccountProjects.Any(ap => ap.ProjectId == projectId))
                    || (projectId <= 0 && acc.AccountProjects.Count == 0)));
        }

        /// <summary>
        /// فیلدها راهبری موجودیت پایه را مقداردهی می‌کند 
        /// </summary>
        /// <param name="baseEntity">موجودیت پایه مورد نظر</param>
        protected void SetBaseEntityInfo(IBaseEntity baseEntity)
        {
            Verify.ArgumentNotNull(baseEntity, nameof(baseEntity));

            if (baseEntity.Id == 0) 
            {
                baseEntity.CreatedById = UserContext.Id;
                baseEntity.CreatedByName = UserContext.PersonFullName;
                baseEntity.CreatedDate = DateTime.Now;
            }

            baseEntity.ModifiedById = UserContext.Id;
            baseEntity.ModifiedByName = UserContext.PersonFullName;
            baseEntity.ModifiedDate = DateTime.Now;
        }

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }

        private const string _branchReferenceScript = @"
SELECT COUNT(*) FROM [{0}].[{1}] WHERE BranchID = {2}";
        private const string _fiscalPeriodReferenceScript = @"
SELECT COUNT(*) FROM [{0}].[{1}] WHERE FiscalPeriodID = {2}";
    }
}
