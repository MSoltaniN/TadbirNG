using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را پیاده سازی می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public class LookupRepository : RepositoryBase, ILookupRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public LookupRepository(IRepositoryContext context)
            : base(context)
        {
        }

        #region Finance Subsystem Lookup

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetAccountsAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriod.Id <= fpId
                    && acc.Branch.Id == branchId);
            return accounts
                .Select(acc => Mapper.Map<KeyValue>(acc))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(det => det.FiscalPeriod.Id <= fpId
                    && det.Branch.Id == branchId);
            return detailAccounts
                .Select(det => Mapper.Map<KeyValue>(det))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetCostCentersAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriod.Id <= fpId
                    && cc.Branch.Id == branchId);
            return costCenters
                .Select(cc => Mapper.Map<KeyValue>(cc))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetProjectsAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriod.Id <= fpId
                    && prj.Branch.Id == branchId);
            return projects
                .Select(prj => Mapper.Map<KeyValue>(prj))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه اسناد مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetVouchersAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var vouchers = await repository
                .GetByCriteriaAsync(voucher => voucher.FiscalPeriod.Id == fpId
                    && voucher.Branch.Id == branchId);
            return vouchers
                .Select(voucher => Mapper.Map<KeyValue>(voucher))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه آرتیکل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(GridOptions gridOptions = null)
        {
            int fpId = _currentContext.FiscalPeriodId;
            int branchId = _currentContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetByCriteriaAsync(line => line.FiscalPeriod.Id == fpId
                    && line.Branch.Id == branchId);
            return lines
                .Select(line => Mapper.Map<KeyValue>(line))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        public async Task<IEnumerable<KeyValue>> GetCurrenciesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            return await repository
                .GetEntityQuery()
                .Select(curr => Mapper.Map<KeyValue>(curr))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلی ارزهای تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        public async Task<IEnumerable<CurrencyInfoViewModel>> GetCurrenciesInfoAsync(bool withRate)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currencies = await repository
                .GetEntityWithTrackingQuery()
                .ToListAsync();
            var lookup = new List<CurrencyInfoViewModel>();
            foreach (var currency in currencies)
            {
                var lookupItem = Mapper.Map<CurrencyInfoViewModel>(currency);
                if (withRate)
                {
                    lookupItem.LastRate = await GetLastCurrencyRateAsync(repository, currency);
                }

                lookup.Add(lookupItem);
            }

            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های تعریف شده و قابل دسترسی توسط کاربر مشخص شده را به صورت مجموعه ای
        /// از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از شرکت های قابل دسترسی</returns>
        public async Task<IList<KeyValue>> GetUserAccessibleCompaniesAsync(int userId)
        {
            UnitOfWork.UseSystemContext();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            var companies = new List<int>();
            if (user != null)
            {
                var relatedRepository = UnitOfWork.GetAsyncRepository<RoleCompany>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.RoleId)
                        .ToArray(),
                    roleId =>
                    {
                        var roleCompanies = relatedRepository.GetByCriteria(
                            rc => rc.RoleId == roleId);
                        companies.AddRange(
                            roleCompanies.Select(rc => rc.CompanyId));
                    });
            }

            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var userCompanies = await repository
                .GetEntityQuery()
                .Where(c => companies
                    .Distinct()
                    .Contains(c.Id))
                .Select(c => Mapper.Map<KeyValue>(c))
                .ToListAsync();
            UnitOfWork.UseCompanyContext();
            return userCompanies;
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetUserAccessibleFiscalPeriodsAsync(int companyId, int userId)
        {
            var fiscalPeriods = new List<FiscalPeriod>();
            UnitOfWork.UseSystemContext();
            await SetCurrentCompanyAsync(companyId);
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                UnitOfWork.UseCompanyContext();
                var relatedRepository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.RoleId)
                        .ToArray(),
                    roleId =>
                    {
                        var rolePeriods = relatedRepository.GetByCriteria(
                            rfp => rfp.RoleId == roleId, rfp => rfp.FiscalPeriod);
                        fiscalPeriods.AddRange(
                            rolePeriods
                                .Select(rfp => rfp.FiscalPeriod)
                                .Where(fp => fp.CompanyId == companyId));
                    });
                fiscalPeriods = fiscalPeriods
                    .Distinct(new EntityEqualityComparer())
                    .Cast<FiscalPeriod>()
                    .ToList();
            }

            return fiscalPeriods
                .OrderBy(fp => fp.Name)
                .Select(fp => Mapper.Map<KeyValue>(fp));
        }

        /// <summary>
        /// به روش آسنکرون، شعب سازمانی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه شعب سازمانی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetUserAccessibleBranchesAsync(int companyId, int userId)
        {
            var branches = new List<Branch>();
            UnitOfWork.UseSystemContext();
            await SetCurrentCompanyAsync(companyId);
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                UnitOfWork.UseCompanyContext();
                var relatedRepository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.RoleId)
                        .ToArray(),
                    roleId =>
                    {
                        var roleBranches = relatedRepository.GetByCriteria(
                            rb => rb.RoleId == roleId, rb => rb.Branch);
                        branches.AddRange(
                            roleBranches
                                .Select(rb => rb.Branch)
                                .Where(br => br.CompanyId == companyId));
                    });
                branches = branches
                    .Distinct(new EntityEqualityComparer())
                    .Cast<Branch>()
                    .ToList();
            }

            return branches
                .OrderBy(br => br.Name)
                .Select(br => Mapper.Map<KeyValue>(br));
        }

        /// <summary>
        /// ماهیت های قابل استفاده در تعریف گروه های حساب را
        /// به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>مجموعه ماهیت های قابل استفاده در تعریف گروه های حساب</returns>
        public IList<KeyValue> GetAccountGroupCategories()
        {
            var categories = new List<KeyValue>();
            var items = new string[]
            {
                "CategoryAsset", "CategoryAssociation", "CategoryCapital",
                "CategoryCoordination", "CategoryExpense", "CategoryIncome",
                "CategoryLiability", "CategoryPurchase", "CategorySales"
            };
            Array.ForEach(items, item => categories.Add(new KeyValue(item, item)));
            return categories;
        }

        /// <summary>
        /// به روش آسنکرون، گروه های حساب تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه گروه های حساب تعریف شده</returns>
        public async Task<IEnumerable<KeyValue>> GetAccountGroupsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            return await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<KeyValue>(grp))
                .ToListAsync();
        }

        /// <summary>
        /// انواع سیستمی تعریف شده برای سند را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>انواع سیستمی تعریف شده برای سند</returns>
        public IEnumerable<KeyValue> GetVoucherTypes()
        {
            var types = new List<KeyValue>()
            {
                new KeyValue { Key = "0", Value = "NormalVoucher" }
            };

            return types;
        }

        /// <summary>
        /// انواع تعریف شده برای آرتیکل سند را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>انواع تعریف شده برای آرتیکل سند</returns>
        public IEnumerable<KeyValue> GetVoucherLineTypes()
        {
            var types = new List<KeyValue>()
            {
                new KeyValue { Key = "0", Value = "NormalLine" },
                new KeyValue { Key = "1", Value = "TaxAndToll" },
                new KeyValue { Key = "2", Value = "Revised" }
            };

            return types;
        }

        /// <summary>
        /// محدودیت های ثبت قابل استفاده در تعریف حساب را به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>محدودیت های ثبت قابل استفاده در تعریف حساب</returns>
        public IList<KeyValue> GetAccountTurnoverModes()
        {
            var turnovers = new List<KeyValue>()
            {
                new KeyValue { Key = "-1", Value = "Unlimited" },
                new KeyValue { Key = "0", Value = "DebtorDuringPeriod" },
                new KeyValue { Key = "1", Value = "CreditorDuringPeriod" },
                new KeyValue { Key = "2", Value = "DebtorEndPeriod" },
                new KeyValue { Key = "3", Value = "CreditorEndPeriod" },
            };

            return turnovers;
        }

        #endregion

        #region Security Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، اطلاعات اشخاص تعریف شده در برنامه را به صورت یک دیکشنری
        /// که بر حسب شناسه دیتابیسی کاربر ایندکس شده برمی گرداند
        /// </summary>
        /// <returns>مجموعه اطلاعات کاربران موجود به صورت دیکشنری</returns>
        public async Task<IDictionary<int, string>> GetUserPersonsAsync()
        {
            var userPersons = new Dictionary<int, string>();
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var all = await repository
                .GetEntityQuery(user => user.Person)
                .Select(user => new KeyValuePair<int, string>(
                    user.Id, String.Format("{0}، {1}", user.Person.LastName, user.Person.FirstName)))
                .ToArrayAsync();
            Array.ForEach(all, kv => userPersons.Add(kv.Key, kv.Value));
            UnitOfWork.UseCompanyContext();
            return userPersons;
        }

        /// <summary>
        /// به روش آسنکرون، نقش های امنیتی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه نقش های امنیتی تعریف شده</returns>
        public async Task<IList<KeyValue>> GetRolesAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var roles = await repository
                .GetAllAsync();
            var lookup = roles
                .OrderBy(role => role.Name)
                .Select(role => Mapper.Map<KeyValue>(role))
                .Apply(gridOptions)
                .ToList();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }

        #endregion

        #region Metadata Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، موجودیت های پایه تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های پایه تعریف شده</returns>
        public async Task<IList<ViewSummaryViewModel>> GetBaseEntityViewsAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetByCriteriaAsync(view => view.EntityType == "Base");
            var lookup = views
                .Select(view => Mapper.Map<ViewSummaryViewModel>(view))
                .Apply(gridOptions)
                .ToList();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت های تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های تعریف شده</returns>
        public async Task<IList<KeyValue>> GetEntityViewsAsync(GridOptions gridOptions = null)
        {
            return await GetViewsByCriteriaAsync(view => !String.IsNullOrEmpty(view.FetchUrl), gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت های سلسله مراتبی (درختی) را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های درختی</returns>
        public async Task<IList<KeyValue>> GetTreeViewsAsync(GridOptions gridOptions = null)
        {
            return await GetViewsByCriteriaAsync(view => view.IsHierarchy, gridOptions);
        }

        #endregion

        private static async Task<double> GetLastCurrencyRateAsync(
            IAsyncRepository<Currency> repository, Currency currency)
        {
            await repository.LoadCollectionAsync(currency, curr => curr.Rates);
            var lastRate = currency.Rates
                .OrderByDescending(rate => rate.Date)
                .ThenByDescending(rate => rate.Time)
                .FirstOrDefault();
            return lastRate != null
                ? lastRate.Multiplier
                : 0.0F;
        }

        private IQueryable<User> GetUserQuery(int userId)
        {
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var query = repository
                .GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles);
            return query;
        }

        private async Task<IList<KeyValue>> GetViewsByCriteriaAsync(
            Expression<Func<View, bool>> criteria, GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetByCriteriaAsync(criteria);
            var lookup = views
                .Select(view => Mapper.Map<KeyValue>(view))
                .Apply(gridOptions)
                .ToList();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }
    }
}
