﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;

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
        public async Task<IEnumerable<KeyValue>> GetAccountsAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriodId <= fpId);
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
        public async Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(det => det.FiscalPeriodId <= fpId);
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
        public async Task<IEnumerable<KeyValue>> GetCostCentersAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriodId <= fpId);
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
        public async Task<IEnumerable<KeyValue>> GetProjectsAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriodId <= fpId);
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
        public async Task<IEnumerable<KeyValue>> GetVouchersAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var vouchers = await repository
                .GetByCriteriaAsync(voucher => voucher.FiscalPeriodId == fpId);
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
        public async Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(GridOptions gridOptions)
        {
            int fpId = UserContext.FiscalPeriodId;
            int branchId = UserContext.BranchId;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetByCriteriaAsync(line => line.FiscalPeriodId == fpId);
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
            return await Repository
                .GetAllQuery<Currency>(ViewId.Currency)
                .Where(curr => !curr.IsDefaultCurrency)
                .Select(curr => Mapper.Map<KeyValue>(curr))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلی ارزهای تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        public async Task<IEnumerable<CurrencyInfoViewModel>> GetCurrenciesInfoAsync(bool withRate)
        {
            Expression<Func<Currency, bool>> filter = curr => !curr.IsDefaultCurrency;
            if (withRate)
            {
                filter = curr => !curr.IsDefaultCurrency;
            }

            var currencies = await Repository
                .GetAllQuery<Currency>(ViewId.Currency)
                .Where(filter)
                .ToListAsync();
            var lookup = new List<CurrencyInfoViewModel>();
            foreach (var currency in currencies)
            {
                var lookupItem = Mapper.Map<CurrencyInfoViewModel>(currency);
                if (withRate)
                {
                    lookupItem.LastRate = await GetLastCurrencyRateAsync(currency);
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
                            fiscalPeriods.AddRange(rolePeriods.Select(rfp => rfp.FiscalPeriod));
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
                            branches.AddRange(roleBranches.Select(rb => rb.Branch));
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
                new KeyValue { Key = "-1", Value = "AllVouchers" },
                new KeyValue { Key = "0", Value = "NormalVouchers" },
                new KeyValue { Key = "1", Value = "DraftVouchers" }
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
        /// به روش آسنکرون، مولفه های بردار حساب را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه مولفه های بردار حساب</returns>
        public async Task<IEnumerable<ViewSummaryViewModel>> GetFullAccountPartsLookupAsync()
        {
            var partViewIds = new int[]
            {
                ViewId.Account, ViewId.DetailAccount, ViewId.CostCenter, ViewId.Project
            };
            return await GetViewsByCriteriaAsync<ViewSummaryViewModel>(
                view => partViewIds.Contains(view.Id), null);
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
                    aca.CollectionId == (int)AccountCollectionId.Inventory)
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

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از رفرنس های استفاده شده در سندهای مالی را برمی گرداند
        /// </summary>
        /// <returns>مجموعه رفرنس های استفاده شده در سندهای مالی</returns>
        public async Task<IEnumerable<string>> GetVoucherReferencesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository
                .GetEntityQuery()
                .Where(v => v.FiscalPeriodId == UserContext.FiscalPeriodId
                    && !String.IsNullOrEmpty(v.Reference))
                .Select(v => v.Reference)
                .Distinct()
                .ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<PagedList<VoucherSummaryViewModel>> GetVouchersByOperationalDateAsync(
            DateTime date, GridOptions gridOptions)
        {
            var vouchers = await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher)
                .Where(v => v.Date.Date == date.Date
                    && v.StatusId == (int)DocumentStatusId.NotChecked)
                .Select(v => Mapper.Map<VoucherSummaryViewModel>(v))
                .ToListAsync();

            return new PagedList<VoucherSummaryViewModel>(vouchers, gridOptions);
        }

        #endregion

        #region Security Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، نقش های امنیتی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه نقش های امنیتی تعریف شده</returns>
        public async Task<IList<KeyValue>> GetRolesAsync(GridOptions gridOptions)
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
        public async Task<IList<ViewSummaryViewModel>> GetBaseEntityViewsAsync(GridOptions gridOptions)
        {
            return await GetViewsByCriteriaAsync<ViewSummaryViewModel>(
                view => view.EntityType == "Base", gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت تعریف شده را به صورت کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns> موجودیت پایه تعریف شده</returns>
        public async Task<IList<ViewSummaryViewModel>> GetEntityViewAsync(int viewId)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetByCriteriaAsync(view => view.Id == viewId);
            var lookup = views
                .Select(view => Mapper.Map<ViewSummaryViewModel>(view))
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
            return await GetViewsByCriteriaAsync<KeyValue>(
                view => !String.IsNullOrEmpty(view.FetchUrl), gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت های سلسله مراتبی (درختی) را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های درختی</returns>
        public async Task<IList<KeyValue>> GetTreeViewsAsync(GridOptions gridOptions = null)
        {
            return await GetViewsByCriteriaAsync<KeyValue>(view => view.IsHierarchy, gridOptions);
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
                .Select(item => Mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، لیست شهرهای یک استان را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="provinceCode">کد یکتای استان</param>
        /// <returns>لیست شهرهای یک استان</returns>
        public async Task<IList<KeyValue>> GetCitiesAsync(string provinceCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<City>();
            var cities = await repository.GetByCriteriaAsync(city => city.Province.Code == provinceCode 
            && !city.Code.Contains("00000"));
            return cities
                .OrderBy(item => item.Name)
                .Select(item => Mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، لیست دسترسی های سطری مجاز برای یک موجودیت را به صورت مجموعه ای از
        /// کلیدهای متنی چندزبانه برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی موجودیت مورد نظر</param>
        /// <returns>لیست دسترسی های سطری مجاز برای یک موجودیت</returns>
        public async Task<IList<string>> GetValidRowPermissionsAsync(int viewId)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<ValidRowPermission>();
            var permissions = await repository
                .GetEntityQuery()
                .Where(perm => perm.View.Id == viewId)
                .Select(perm => perm.AccessMode)
                .ToListAsync();
            UnitOfWork.UseCompanyContext();
            return permissions;
        }

        #endregion

        #region Treasury Subsystem lookup

        /// <inheritdoc/>
        public async Task<IList<KeyValue>> GetSourceApps(int sourceAppType)
        {
            List<KeyValue> result;
            if (sourceAppType != (int)SourceAppType.Both)
            {
                result = await Repository
                    .GetAllQuery<SourceApp>(ViewId.SourceApp)
                    .Where(sa => sa.Type == sourceAppType)
                    .Select(sa => Mapper.Map<KeyValue>(sa))
                    .ToListAsync();
            }
            else
            {
                result = await Repository
                    .GetAllQuery<SourceApp>(ViewId.SourceApp)
                    .Select(sa => Mapper.Map<KeyValue>(sa))
                    .ToListAsync();
            }

            var noneItem = new KeyValue
            {
                Key = null,
                Value = Context.Localize(AppStrings.None)
            };

            result.Add(noneItem);
            result = result
                .OrderBy(sa => Convert.ToInt32(sa.Key ?? "0"))
                .ToList();
            return result;
        }

        #endregion Treasury Subsystem lookup

        private static string GetFullName(User user)
        {
            return user.Person.FullName;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get
            {
                return _system.Config;
            }
        }

        private async Task<double> GetLastCurrencyRateAsync(Currency currency)
        {
            var lastRate = await Repository
                .GetAllQuery<CurrencyRate>(ViewId.CurrencyRate)
                .Where(rate => rate.CurrencyId == currency.Id)
                .OrderByDescending(rate => rate.Date)
                .ThenByDescending(rate => rate.Time)
                .Select(rate => rate.Multiplier)
                .FirstOrDefaultAsync();
            return lastRate;
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

        private async Task<IList<TModel>> GetViewsByCriteriaAsync<TModel>(
            Expression<Func<View, bool>> criteria, GridOptions gridOptions)
            where TModel : class, new()
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetByCriteriaAsync(criteria);
            var lookup = views
                .Select(view => Mapper.Map<TModel>(view))
                .Apply(gridOptions)
                .ToList();
            UnitOfWork.UseCompanyContext();
            return lookup;
        }

        private readonly ISystemRepository _system;
    }
}
