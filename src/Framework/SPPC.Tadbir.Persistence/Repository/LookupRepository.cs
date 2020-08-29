using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
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
        public LookupRepository(IRepositoryContext context, ISystemRepository system)
            : base(context)
        {
            _system = system;
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriod.Id <= fpId);
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(det => det.FiscalPeriod.Id <= fpId);
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriod.Id <= fpId);
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriod.Id <= fpId);
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var vouchers = await repository
                .GetByCriteriaAsync(voucher => voucher.FiscalPeriod.Id == fpId);
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
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetByCriteriaAsync(line => line.FiscalPeriod.Id == fpId);
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
            var userCompanies = new List<CompanyDb>();
            UnitOfWork.UseSystemContext();
            var companies = new List<int>();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
                var roleIds = user.UserRoles.Select(ur => ur.RoleId);
                if (roleIds.Contains(AppConstants.AdminRoleId))
                {
                    userCompanies.AddRange(await repository.GetByCriteriaAsync(c => c.IsActive));
                }
                else
                {
                    var relatedRepository = UnitOfWork.GetAsyncRepository<RoleCompany>();
                    Array.ForEach(roleIds.ToArray(),
                        roleId =>
                        {
                            var roleCompanies = relatedRepository.GetByCriteria(
                                rc => rc.RoleId == roleId);
                            companies.AddRange(
                                roleCompanies.Select(rc => rc.CompanyId));
                        });

                    userCompanies = await repository
                        .GetEntityQuery()
                        .Where(c => c.IsActive)
                        .Where(c => companies
                            .Distinct()
                            .Contains(c.Id))
                        .ToListAsync();
                }
            }

            UnitOfWork.UseCompanyContext();
            return userCompanies
                .OrderBy(c => c.Name)
                .Select(c => Mapper.Map<KeyValue>(c))
                .ToList();
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
                var roleIds = user.UserRoles.Select(ur => ur.RoleId);
                if (roleIds.Contains(AppConstants.AdminRoleId))
                {
                    var fiscalRepository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
                    fiscalPeriods.AddRange(await fiscalRepository.GetAllAsync());
                }
                else
                {
                    var relatedRepository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                    Array.ForEach(roleIds.ToArray(),
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
            }

            UnitOfWork.UseCompanyContext();
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
                var roleIds = user.UserRoles.Select(ur => ur.RoleId);
                if (roleIds.Contains(AppConstants.AdminRoleId))
                {
                    var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
                    branches.AddRange(await branchRepository.GetAllAsync());
                }
                else
                {
                    var relatedRepository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                    Array.ForEach(roleIds.ToArray(),
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
            }

            UnitOfWork.UseCompanyContext();
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
                new KeyValue { Key = "0", Value = "NormalVoucher" },
                new KeyValue { Key = "1", Value = "OpeningVoucher" },
                new KeyValue { Key = "2", Value = "ClosingVoucher" },
                new KeyValue { Key = "3", Value = "ClosingTempAccounts" }
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
                new KeyValue { Key = AppStrings.Unlimited, Value = AppStrings.Unlimited },
                new KeyValue { Key = AppStrings.DebtorDuringPeriod, Value = AppStrings.DebtorDuringPeriod },
                new KeyValue { Key = AppStrings.CreditorDuringPeriod, Value = AppStrings.CreditorDuringPeriod },
                new KeyValue { Key = AppStrings.DebtorEndPeriod, Value = AppStrings.DebtorEndPeriod },
                new KeyValue { Key = AppStrings.CreditorEndPeriod, Value = AppStrings.CreditorEndPeriod },
            };

            return turnovers;
        }

        /// <summary>
        /// به روش آسنکرون، سطوح قابل استفاده برای دفتر حساب را از تنظیمات درختی خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns></returns>
        public async Task<IList<AccountLevelViewModel>> GetAccountBookLevelsAsync(int viewId)
        {
            var levels = new List<AccountLevelViewModel>();
            var config = await Config.GetViewTreeConfigByViewAsync(viewId);
            int key = 0;
            Array.ForEach(config.Current.Levels.Where(lvl => lvl.IsUsed).ToArray(), level =>
                    {
                        levels.Add(new AccountLevelViewModel()
                        {
                            Key = key++,
                            Level = level.No - 1,
                            Title = level.Name,
                            ViewId = viewId
                        });
                    });
            return levels;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مجموعه حساب موجودی کالا را
        /// برای کلیه شعب خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از حساب های موجودی کالا</returns>
        public async Task<IList<AccountViewModel>> GetInventoryAccountsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var collectionAccounts = await repository
                .GetEntityQuery()
                .Include(aca => aca.Account)
                .Where(aca => aca.FiscalPeriodId <= UserContext.FiscalPeriodId &&
                    aca.CollectionId == (int)AccountCollectionId.ProductInventory)
                .Select(aca => aca.Account)
                .ToListAsync();

            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            var leafAccounts = await accountRepository
                .GetEntityQuery()
                .Where(acc => acc.Children.Count == 0 &&
                    collectionAccounts.Any(item => acc.FullCode.StartsWith(item.FullCode)))
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .ToListAsync();
            return leafAccounts;
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
                .Where(role => role.Key != AppConstants.AdminRoleId.ToString())
                .Apply(gridOptions)
                .ToList();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه کاربران را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه کاربران</returns>
        public async Task<IList<KeyValue>> GetUsersAsync()
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var lookup = new List<KeyValue>();
            if (UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                lookup.Add(new KeyValue("0", "AllUsers"));
                lookup.AddRange(await repository
                    .GetEntityQuery(user => user.Person)
                    .Select(user => new KeyValue(user.Id.ToString(), GetFullName(user)))
                    .ToListAsync());
            }
            else
            {
                var user = await repository.GetByIDAsync(UserContext.Id, u => u.Person);
                if (user != null)
                {
                    lookup.Add(new KeyValue(user.Id.ToString(), GetFullName(user)));
                }
            }

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

        /// <summary>
        /// به روش آسنکرون، موجودیت های برنامه را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های برنامه</returns>
        public async Task<IList<SourceEntityViewModel>> GetEntityTypesAsync()
        {
            var lookup = new List<SourceEntityViewModel>
            {
                new SourceEntityViewModel() { Name = "AllEntities" }
            };
            var repository = UnitOfWork.GetAsyncRepository<EntityType>();
            lookup.AddRange(await repository
                .GetEntityQuery()
                .Select(entity => new SourceEntityViewModel() { EntityTypeId = entity.Id, Name = entity.Name })
                .ToListAsync());
            var sourceRepository = UnitOfWork.GetAsyncRepository<OperationSource>();
            lookup.AddRange(await sourceRepository
                .GetEntityQuery()
                .Select(source => new SourceEntityViewModel() { SourceId = source.Id, Name = source.Name })
                .ToListAsync());
            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت های سیستمی را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های برنامه</returns>
        public async Task<IList<SourceEntityViewModel>> GetSystemEntityTypesAsync()
        {
            UnitOfWork.UseSystemContext();
            var lookup = await GetEntityTypesAsync();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، لیست استان ها را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>لیست استان ها</returns>
        public async Task<IList<KeyValue>> GetProvincesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Province>();
            var provinces = await repository.GetAllAsync();
            return provinces
                .OrderBy(item => item.Name)
                .Select(item => Mapper.Map<KeyValue>(item)).ToList();
        }

        /// <summary>
        /// به روش آسنکرون، لیست شهرهای یک استان را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="provinceCode">کد یکتای استان</param>
        /// <returns>لیست شهرهای یک استان</returns>
        public async Task<IList<KeyValue>> GetCitiesAsync(string provinceCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<City>();
            var cities = await repository.GetByCriteriaAsync(city => city.Province.Code == provinceCode);
            return cities
                .OrderBy(item => item.Name)
                .Select(item => Mapper.Map<KeyValue>(item)).ToList();
        }

        #endregion

        private static string GetFullName(User user)
        {
            return String.Format("{0}, {1}", user.Person.LastName, user.Person.FirstName);
        }

        private IConfigRepository Config
        {
            get
            {
                return _system.Config;
            }
        }

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

        private readonly ISystemRepository _system;
    }
}
