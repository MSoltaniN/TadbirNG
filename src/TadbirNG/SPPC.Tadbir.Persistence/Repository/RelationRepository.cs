﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارتباطات بین مولفه های مختلف بردار حساب را پیاده سازی می کند
    /// </summary>
    public class RelationRepository : LoggingRepositoryBase, IRelationRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="itemRepository">پیاده سازی اینترفیس مربوط به عملیات بردار حساب</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public RelationRepository(
            IRepositoryContext context, ISystemRepository system, IAccountItemRepository itemRepository,
            IOperationLogRepository log)
            : base(context, log)
        {
            _system = system;
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(GridOptions gridOptions = null)
        {
            var accounts = await _itemRepository.GetLeafAccountsAsync(gridOptions);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var detailAccounts = useLeafItems
                ? await _itemRepository.GetLeafDetailAccountsAsync(gridOptions)
                : await _itemRepository.GetRootDetailAccountsAsync(gridOptions);
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var costCenters = useLeafItems
                ? await _itemRepository.GetLeafCostCentersAsync(gridOptions)
                : await _itemRepository.GetRootCostCentersAsync(gridOptions);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var projects = useLeafItems
                ? await _itemRepository.GetLeafProjectsAsync(gridOptions)
                : await _itemRepository.GetRootProjectsAsync(gridOptions);
            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableAccountsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafAccountsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableDetailAccountsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafDetailAccountsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableCostCentersLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafCostCentersAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableProjectsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafProjectsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی بردار حساب داده شده را در ارتباطات موجود جستجو کرده
        /// و در صورت پیدا نکردن بردار حساب، وضعیت اشکال موجود را به صورت شناسه متن چندزبانه خطا برمی گرداند
        /// </summary>
        /// <param name="fullAccount">مدل نمایشی بردار حساب مورد جستجو</param>
        /// <returns>در صورت پیدا نکردن بردار حساب، شناسه متن چندزبانه خطا و در صورت پیدا کردن
        /// رشته خالی را برمی گرداند</returns>
        public async Task<string> LookupFullAccountAsync(FullAccountViewModel fullAccount)
        {
            Verify.ArgumentNotNull(fullAccount, nameof(fullAccount));
            var account = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children,
                    acc => acc.AccountDetailAccounts, acc => acc.AccountCostCenters, acc => acc.AccountProjects)
                .Where(acc => acc.Id == fullAccount.Account.Id)
                .SingleOrDefaultAsync();
            string errorKey = EnsureValidAccountInFullAccount(account);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            var criteria = GetFullAccountCriteria(account);
            errorKey = EnsureNoMissingItemInFullAccount(criteria, fullAccount);
             if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            errorKey = await EnsureValidItemsInFullAccountAsync(fullAccount);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            errorKey = EnsureValidRelationsInFullAccount(account, fullAccount);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            return String.Empty;
        }

        /// <summary>
        /// معتبر بودن بردار حساب را با توجه به سرفصل های حسابداری داده شده بررسی میکند
        /// </summary>
        /// <param name="account">اطلاعات نمایشی خلاصه حساب</param>
        /// <param name="detailAccount">اطلاعات نمایشی خلاصه تفصیلی شناور</param>
        /// <param name="costCenter">اطلاعات نمایشی خلاصه مرکز هزینه</param>
        /// <param name="project">اطلاعات نمایشی خلاصه پروژه</param>
        /// <returns>در صورت معتبر بودن بردارحساب مقدار درست در غیر اینصورت مقدار نادرست برمیگرداند</returns>
        public bool LookupFullAccount(
            AccountItemBriefViewModel account,
            AccountItemBriefViewModel detailAccount,
            AccountItemBriefViewModel costCenter,
            AccountItemBriefViewModel project)
        {
            var fullAccount = new FullAccountViewModel()
            {
                Account = account,
                DetailAccount = detailAccount,
                CostCenter = costCenter,
                Project = project
            };

            return String.IsNullOrEmpty(LookupFullAccountAsync(fullAccount).Result);
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ada => ada.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ada => ada.DetailAccount.Children.Count == 0);
            }

            var relatedDetailIds = await existingItemsQuery
                .Select(ada => ada.DetailAccountId)
                .ToListAsync();
            var detailAccounts = await Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount)
                .Where(facc => relatedDetailIds.Contains(facc.Id))
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            detailAccounts = detailAccounts
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(detailAccounts.ToArray(), facc => facc.IsSelected = true);
            await LogAssociationOperationAsync<Account>(
                OperationId.View, AppStrings.Account, accountId, AppStrings.DetailAccount, gridOptions);
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ac => ac.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ac => ac.CostCenter.Children.Count == 0);
            }

            var relatedCenterIds = await existingItemsQuery
                .Select(ac => ac.CostCenterId)
                .ToListAsync();
            var costCenters = await Repository
                .GetAllQuery<CostCenter>(ViewId.CostCenter)
                .Where(cc => relatedCenterIds.Contains(cc.Id))
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            costCenters = costCenters
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(costCenters.ToArray(), cc => cc.IsSelected = true);
            await LogAssociationOperationAsync<Account>(
                OperationId.View, AppStrings.Account, accountId, AppStrings.CostCenter, gridOptions);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ap => ap.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ap => ap.Project.Children.Count == 0);
            }

            var relatedProjectIds = await existingItemsQuery
                .Select(ap => ap.ProjectId)
                .ToListAsync();
            var projects = await Repository
                .GetAllQuery<Project>(ViewId.Project)
                .Where(prj => relatedProjectIds.Contains(prj.Id))
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            projects = projects
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(projects.ToArray(), prj => prj.IsSelected = true);
            await LogAssociationOperationAsync<Account>(
                OperationId.View, AppStrings.Account, accountId, AppStrings.Project, gridOptions);
            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountAccountsAsync(
            int detailId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.DetailAccountId == detailId
                    && ada.Account.Children.Count == 0)
                .Select(ada => ada.AccountId)
                .ToListAsync();
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            accounts = accounts
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            await LogAssociationOperationAsync<DetailAccount>(
                OperationId.View, AppStrings.DetailAccount, detailId, AppStrings.Account, gridOptions);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterAccountsAsync(
            int costCenterId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.CostCenterId == costCenterId
                    && ac.Account.Children.Count == 0)
                .Select(ac => ac.AccountId)
                .ToListAsync();
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            accounts = accounts
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            await LogAssociationOperationAsync<CostCenter>(
                OperationId.View, AppStrings.CostCenter, costCenterId, AppStrings.Account, gridOptions);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با پروژه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectAccountsAsync(
            int projectId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.ProjectId == projectId
                    && ap.Account.Children.Count == 0)
                .Select(ap => ap.AccountId)
                .ToListAsync();
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            accounts = accounts
                .Apply(gridOptions)
                .ToList();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            await LogAssociationOperationAsync<Project>(
                OperationId.View, AppStrings.Project, projectId, AppStrings.Account, gridOptions);
            return accounts;
        }

        #region AccountDetailAccount Methods

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        public async Task AddAccountDetailAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountDetailAccounts);
            if (existing != null)
            {
                await AddNewAccountDetailAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.DetailAccount);
            }
        }

        private async Task AddNewAccountDetailAccountsAsync(
            Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int detailId in relations.RelatedItemIds)
            {
                await AddNewAccountDetailAccount(existing, detailId);
            }
        }

        private async Task AddNewAccountDetailAccount(Account existing, int detailId)
        {
            var existingRelation = existing.AccountDetailAccounts
                .Where(ada => ada.AccountId == existing.Id && ada.DetailAccountId == detailId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(detailId, facc => facc.Children);
            var accountDetailAccount = new AccountDetailAccount()
            {
                AccountId = existing.Id,
                DetailAccountId = detailAccount.Id
            };
            existing.AccountDetailAccounts.Add(accountDetailAccount);
            foreach (var child in detailAccount.Children)
            {
                await AddNewAccountDetailAccount(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        public async Task RemoveAccountDetailAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountDetailAccounts);
            if (existing != null)
            {
                await RemoveDisconnectedDetailAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.DetailAccount);
            }
        }

        private async Task RemoveDisconnectedDetailAccountsAsync(
            Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveAccountDetailAccountAsync(existing, id);
            }
        }

        private async Task RemoveAccountDetailAccountAsync(Account existing, int detailId)
        {
            var existingRelation = existing.AccountDetailAccounts
                .Where(ada => ada.DetailAccountId == detailId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            existing.AccountDetailAccounts.Remove(existingRelation);
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDWithTrackingAsync(detailId, facc => facc.Children);
            foreach (var child in detailAccount.Children)
            {
                await RemoveAccountDetailAccountAsync(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک تفصیلی شناور را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        public async Task AddDetailAccountAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, facc => facc.AccountDetailAccounts, facc => facc.Children);
            if (existing != null)
            {
                await AddConnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<DetailAccount>(
                    OperationId.Save, AppStrings.DetailAccount, relations.Id, AppStrings.Account);
            }
        }

        private async Task AddConnectedAccountsAsync(
            DetailAccount existing, AccountItemRelationsViewModel relations)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            foreach (int id in relations.RelatedItemIds)
            {
                var account = await repository.GetByIDWithTrackingAsync(id, acc => acc.AccountDetailAccounts);
                await AddConnectedAccountAsync(existing, account);
            }
        }

        private async Task AddConnectedAccountAsync(DetailAccount existing, Account account)
        {
            var existingRelation = existing.AccountDetailAccounts
                .Where(ada => ada.AccountId == account.Id && ada.DetailAccountId == existing.Id)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var accountDetailAccount = new AccountDetailAccount()
            {
                AccountId = account.Id,
                DetailAccountId = existing.Id
            };
            existing.AccountDetailAccounts.Add(accountDetailAccount);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, facc => facc.AccountDetailAccounts);
                await repository.LoadCollectionAsync(child, facc => facc.Children);
                await AddConnectedAccountAsync(child, account);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک تفصیلی شناور را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        public async Task RemoveDetailAccountAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, facc => facc.AccountDetailAccounts, facc => facc.Children);
            if (existing != null)
            {
                await RemoveDisconnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<DetailAccount>(
                    OperationId.Save, AppStrings.DetailAccount, relations.Id, AppStrings.Account);
            }
        }

        private async Task RemoveDisconnectedAccountsAsync(
            DetailAccount existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveDisconnectedAccountAsync(existing, id);
            }
        }

        private async Task RemoveDisconnectedAccountAsync(DetailAccount existing, int accountId)
        {
            var existingRelation = existing.AccountDetailAccounts
                .Where(ada => ada.AccountId == accountId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            existing.AccountDetailAccounts.Remove(existingRelation);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, facc => facc.AccountDetailAccounts);
                await repository.LoadCollectionAsync(child, facc => facc.Children);
                await RemoveDisconnectedAccountAsync(child, accountId);
            }
        }

        #endregion

        #region AccountCostCenter Methods

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        public async Task AddAccountCostCentersAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountCostCenters);
            if (existing != null)
            {
                await AddNewAccountCostCentersAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.CostCenter);
            }
        }

        private async Task AddNewAccountCostCentersAsync(
            Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int centerId in relations.RelatedItemIds)
            {
                await AddNewAccountCostCenterAsync(existing, centerId);
            }
        }

        private async Task AddNewAccountCostCenterAsync(Account existing, int centerId)
        {
            var existingRelation = existing.AccountCostCenters
                .Where(ac => ac.AccountId == existing.Id && ac.CostCenterId == centerId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(centerId, cc => cc.Children);
            var accountCostCenter = new AccountCostCenter()
            {
                AccountId = existing.Id,
                CostCenterId = costCenter.Id
            };
            existing.AccountCostCenters.Add(accountCostCenter);
            foreach (var child in costCenter.Children)
            {
                await AddNewAccountCostCenterAsync(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        public async Task RemoveAccountCostCentersAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountCostCenters);
            if (existing != null)
            {
                await RemoveDisconnectedCostCentersAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.CostCenter);
            }
        }

        private async Task RemoveDisconnectedCostCentersAsync(
            Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveAccountCostCenterAsync(existing, id);
            }
        }

        private async Task RemoveAccountCostCenterAsync(Account existing, int centerId)
        {
            var existingRelation = existing.AccountCostCenters
                .Where(ac => ac.CostCenterId == centerId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            existing.AccountCostCenters.Remove(existingRelation);
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDWithTrackingAsync(centerId, cc => cc.Children);
            foreach (var child in costCenter.Children)
            {
                await RemoveAccountCostCenterAsync(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک مرکز هزینه را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        public async Task AddCostCenterAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, cc => cc.AccountCostCenters, cc => cc.Children);
            if (existing != null)
            {
                await AddConnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<CostCenter>(
                    OperationId.Save, AppStrings.CostCenter, relations.Id, AppStrings.Account);
            }
        }

        private async Task AddConnectedAccountsAsync(
            CostCenter existing, AccountItemRelationsViewModel relations)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            foreach (int id in relations.RelatedItemIds)
            {
                var account = await repository.GetByIDWithTrackingAsync(id);
                await AddConnectedAccountAsync(existing, account);
            }
        }

        private async Task AddConnectedAccountAsync(CostCenter existing, Account account)
        {
            var existingRelation = existing.AccountCostCenters
                .Where(ac => ac.AccountId == account.Id && ac.CostCenterId == existing.Id)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var accountCostCenter = new AccountCostCenter()
            {
                AccountId = account.Id,
                CostCenterId = existing.Id
            };
            existing.AccountCostCenters.Add(accountCostCenter);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, cc => cc.AccountCostCenters);
                await repository.LoadCollectionAsync(child, cc => cc.Children);
                await AddConnectedAccountAsync(child, account);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک مرکز هزینه را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        public async Task RemoveCostCenterAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, cc => cc.AccountCostCenters, cc => cc.Children);
            if (existing != null)
            {
                await RemoveDisconnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<CostCenter>(
                    OperationId.Save, AppStrings.CostCenter, relations.Id, AppStrings.Account);
            }
        }

        private async Task RemoveDisconnectedAccountsAsync(
            CostCenter existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveDisconnectedAccountAsync(existing, id);
            }
        }

        private async Task RemoveDisconnectedAccountAsync(CostCenter existing, int accountId)
        {
            var existingRelation = existing.AccountCostCenters
                .Where(ac => ac.AccountId == accountId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            existing.AccountCostCenters.Remove(existingRelation);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, cc => cc.AccountCostCenters);
                await repository.LoadCollectionAsync(child, cc => cc.Children);
                await RemoveDisconnectedAccountAsync(child, accountId);
            }
        }

        #endregion

        #region AccountProject Methods

        /// <summary>
        /// به روش آسنکرون، پروژه های مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        public async Task AddAccountProjectsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountProjects);
            if (existing != null)
            {
                await AddNewAccountProjectsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.Project);
            }
        }

        private async Task AddNewAccountProjectsAsync(Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int projectId in relations.RelatedItemIds)
            {
                await AddNewAccountProjectAsync(existing, projectId);
            }
        }

        private async Task AddNewAccountProjectAsync(Account existing, int projectId)
        {
            var existingRelation = existing.AccountProjects
                .Where(ap => ap.AccountId == existing.Id && ap.ProjectId == projectId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            var accountProject = new AccountProject()
            {
                AccountId = existing.Id,
                ProjectId = project.Id
            };
            existing.AccountProjects.Add(accountProject);
            foreach (var child in project.Children)
            {
                await AddNewAccountProjectAsync(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        public async Task RemoveAccountProjectsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, nameof(relations));
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountProjects);
            if (existing != null)
            {
                await RemoveDisconnectedProjectsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Account>(
                    OperationId.Save, AppStrings.Account, relations.Id, AppStrings.Project);
            }
        }

        private async Task RemoveDisconnectedProjectsAsync(
            Account existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveAccountProjectAsync(existing, id);
            }
        }

        private async Task RemoveAccountProjectAsync(Account existing, int projectId)
        {
            var existingRelation = existing.AccountProjects
                .Where(ap => ap.ProjectId == projectId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            existing.AccountProjects.Remove(existingRelation);
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDWithTrackingAsync(projectId, prj => prj.Children);
            foreach (var child in project.Children)
            {
                await RemoveAccountProjectAsync(existing, child.Id);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک پروژه را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        public async Task AddProjectAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, prj => prj.AccountProjects, prj => prj.Children);
            if (existing != null)
            {
                await AddConnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Project>(
                    OperationId.Save, AppStrings.Project, relations.Id, AppStrings.Account);
            }
        }

        private async Task AddConnectedAccountsAsync(
            Project existing, AccountItemRelationsViewModel relations)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            foreach (int id in relations.RelatedItemIds)
            {
                var account = await repository.GetByIDWithTrackingAsync(id);
                await AddConnectedAccountAsync(existing, account);
            }
        }

        private async Task AddConnectedAccountAsync(Project existing, Account account)
        {
            var existingRelation = existing.AccountProjects
                .Where(ap => ap.AccountId == account.Id && ap.ProjectId == existing.Id)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var accountProject = new AccountProject()
            {
                AccountId = account.Id,
                ProjectId = existing.Id
            };
            existing.AccountProjects.Add(accountProject);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, prj => prj.AccountProjects);
                await repository.LoadCollectionAsync(child, prj => prj.Children);
                await AddConnectedAccountAsync(child, account);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک پروژه را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        public async Task RemoveProjectAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, prj => prj.AccountProjects, prj => prj.Children);
            if (existing != null)
            {
                await RemoveDisconnectedAccountsAsync(existing, relations);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                await LogAssociationOperationAsync<Project>(
                    OperationId.Save, AppStrings.Project, relations.Id, AppStrings.Account);
            }
        }

        private async Task RemoveDisconnectedAccountsAsync(
            Project existing, AccountItemRelationsViewModel relations)
        {
            foreach (int id in relations.RelatedItemIds)
            {
                await RemoveDisconnectedAccountAsync(existing, id);
            }
        }

        private async Task RemoveDisconnectedAccountAsync(Project existing, int accountId)
        {
            var existingRelation = existing.AccountProjects
                .Where(ap => ap.AccountId == accountId)
                .SingleOrDefault();
            if (existingRelation == null)
            {
                return;
            }

            var repository = UnitOfWork.GetAsyncRepository<Project>();
            existing.AccountProjects.Remove(existingRelation);
            foreach (var child in existing.Children)
            {
                await repository.LoadCollectionAsync(child, prj => prj.AccountProjects);
                await repository.LoadCollectionAsync(child, prj => prj.Children);
                await RemoveDisconnectedAccountAsync(child, accountId);
            }
        }

        #endregion

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedDetailIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.AccountId == accountId)
                .Select(ada => ada.DetailAccountId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => !relatedDetailIds.Contains(facc.Id));
            if (useLeafItems)
            {
                query = query.Where(facc => facc.Children.Count == 0);
            }

            var detailAccounts = await query
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return detailAccounts
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedCenterIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.AccountId == accountId)
                .Select(ac => ac.CostCenterId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<CostCenter>(ViewId.CostCenter, cc => cc.Children)
                .Where(cc => !relatedCenterIds.Contains(cc.Id));
            if (useLeafItems)
            {
                query = query.Where(cc => cc.Children.Count == 0);
            }

            var costCenters = await query
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            return costCenters
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            var relatedProjectIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.AccountId == accountId)
                .Select(ap => ap.ProjectId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<Project>(ViewId.Project, prj => prj.Children)
                .Where(prj => !relatedProjectIds.Contains(prj.Id));
            if (useLeafItems)
            {
                query = query.Where(prj => prj.Children.Count == 0);
            }

            var projects = await query
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            return projects
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForDetailAccountAsync(
            int detailId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.DetailAccountId == detailId)
                .Select(ada => ada.AccountId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            var accounts = await query
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForCostCenterAsync(
            int costCenterId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.CostCenterId == costCenterId)
                .Select(ac => ac.AccountId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            var accounts = await query
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForProjectAsync(
            int projectId, GridOptions gridOptions = null)
        {
            var relationRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.ProjectId == projectId)
                .Select(ap => ap.AccountId)
                .ToListAsync();
            var query = Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            var accounts = await query
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک تفصیلی شناور جدید انجام می دهد
        /// </summary>
        /// <param name="insertedDetailId">شناسه دیتابیسی تفصیلی شناور ایجاد شده</param>
        public async Task OnDetailAccountInsertedAsync(int insertedDetailId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDWithTrackingAsync(
                insertedDetailId, facc => facc.AccountDetailAccounts);
            if (detailAccount != null && detailAccount.ParentId.HasValue)
            {
                var relationRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
                int count = detailAccount.AccountDetailAccounts.Count;
                var relatedAccounts = await relationRepository
                    .GetEntityQuery(ada => ada.Account)
                    .Where(ada => ada.DetailAccountId == detailAccount.ParentId.Value
                        && ada.Account.FiscalPeriodId <= UserContext.FiscalPeriodId)
                    .Select(ada => ada.Account)
                    .ToListAsync();
                foreach (var account in relatedAccounts)
                {
                    var accountDetailAccount = new AccountDetailAccount()
                    {
                        AccountId = account.Id,
                        DetailAccountId = detailAccount.Id
                    };
                    detailAccount.AccountDetailAccounts.Add(accountDetailAccount);
                }

                if (detailAccount.AccountDetailAccounts.Count != count)
                {
                    repository.Update(detailAccount);
                    await UnitOfWork.CommitAsync();
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک مرکز هزینه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedCenterId">شناسه دیتابیسی مرکز هزینه ایجاد شده</param>
        public async Task OnCostCenterInsertedAsync(int insertedCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDWithTrackingAsync(
                insertedCenterId, cc => cc.AccountCostCenters);
            if (costCenter != null && costCenter.ParentId.HasValue)
            {
                var relationRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
                int count = costCenter.AccountCostCenters.Count;
                var relatedAccounts = await relationRepository
                    .GetEntityQuery(ac => ac.Account)
                    .Where(ac => ac.CostCenterId == costCenter.ParentId.Value
                        && ac.Account.FiscalPeriodId <= UserContext.FiscalPeriodId)
                    .Select(ac => ac.Account)
                    .ToListAsync();
                foreach (var account in relatedAccounts)
                {
                    var accountCostCenter = new AccountCostCenter()
                    {
                        AccountId = account.Id,
                        CostCenterId = costCenter.Id
                    };
                    costCenter.AccountCostCenters.Add(accountCostCenter);
                }

                if (costCenter.AccountCostCenters.Count != count)
                {
                    repository.Update(costCenter);
                    await UnitOfWork.CommitAsync();
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک پروژه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedProjectId">شناسه دیتابیسی پروژه ایجاد شده</param>
        public async Task OnProjectInsertedAsync(int insertedProjectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDWithTrackingAsync(
                insertedProjectId, prj => prj.AccountProjects);
            if (project != null && project.ParentId.HasValue)
            {
                var relationRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
                int count = project.AccountProjects.Count;
                var relatedAccounts = await relationRepository
                    .GetEntityQuery(ap => ap.Account)
                    .Where(ap => ap.ProjectId == project.ParentId.Value
                        && ap.Account.FiscalPeriodId <= UserContext.FiscalPeriodId)
                    .Select(ap => ap.Account)
                    .ToListAsync();
                foreach (var account in relatedAccounts)
                {
                    var accountProject = new AccountProject()
                    {
                        AccountId = account.Id,
                        ProjectId = project.Id
                    };
                    project.AccountProjects.Add(accountProject);
                }

                if (project.AccountProjects.Count != count)
                {
                    repository.Update(project);
                    await UnitOfWork.CommitAsync();
                }
            }
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.AccountRelations; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        #region Account Vector Validation Methods

        private static FullAccountCriteriaViewModel GetFullAccountCriteria(Account account)
        {
            return new FullAccountCriteriaViewModel()
            {
                AccountId = account.Id,
                RequiresDetailAccount = account.AccountDetailAccounts.Count > 0,
                RequiresCostCenter = account.AccountCostCenters.Count > 0,
                RequiresProject = account.AccountProjects.Count > 0
            };
        }

        private static string EnsureValidAccountInFullAccount(Account account)
        {
            string errorKey = String.Empty;
            if (account == null)
            {
                errorKey = AppStrings.InvalidAccountInFullAccount;
            }
            else if (account.Children.Count > 0)
            {
                errorKey = AppStrings.NonLeafAccountInFullAccount;
            }

            return errorKey;
        }

        private static string EnsureNoMissingItemInFullAccount(
            FullAccountCriteriaViewModel criteria, FullAccountViewModel fullAccount)
        {
            string errorKey = String.Empty;
            if (criteria.RequiresDetailAccount && fullAccount.DetailAccount == null)
            {
                errorKey = AppStrings.MissingDetailAccountInFullAccount;
            }
            else if (criteria.RequiresCostCenter && fullAccount.CostCenter == null)
            {
                errorKey = AppStrings.MissingCostCenterInFullAccount;
            }
            else if (criteria.RequiresProject && fullAccount.Project == null)
            {
                errorKey = AppStrings.MissingProjectInFullAccount;
            }

            return errorKey;
        }

        private static string EnsureValidRelationsInFullAccount(
            Account account, FullAccountViewModel fullAccount)
        {
            if (fullAccount.DetailAccount != null)
            {
                bool isValidRelation = account.AccountDetailAccounts
                    .Select(ada => ada.DetailAccountId)
                    .Contains(fullAccount.DetailAccount.Id);
                if (!isValidRelation)
                {
                    return AppStrings.UnrelatedDetailAccountInFullAccount;
                }
            }

            if (fullAccount.CostCenter != null)
            {
                bool isValidRelation = account.AccountCostCenters
                    .Select(ac => ac.CostCenterId)
                    .Contains(fullAccount.CostCenter.Id);
                if (!isValidRelation)
                {
                    return AppStrings.UnrelatedCostCenterInFullAccount;
                }
            }

            if (fullAccount.Project != null)
            {
                bool isValidRelation = account.AccountProjects
                    .Select(ap => ap.ProjectId)
                    .Contains(fullAccount.Project.Id);
                if (!isValidRelation)
                {
                    return AppStrings.UnrelatedProjectInFullAccount;
                }
            }

            return String.Empty;
        }

        private async Task<string> EnsureValidItemsInFullAccountAsync(FullAccountViewModel fullAccount)
        {
            string errorKey = String.Empty;
            if (fullAccount.DetailAccount != null)
            {
                errorKey = await EnsureValidDetailAccountInFullAccountAsync(fullAccount.DetailAccount.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            if (fullAccount.CostCenter != null)
            {
                errorKey = await EnsureValidCostCenterInFullAccountAsync(fullAccount.CostCenter.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            if (fullAccount.Project != null)
            {
                errorKey = await EnsureValidProjectInFullAccountAsync(fullAccount.Project.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            return errorKey;
        }

        private async Task<string> EnsureValidDetailAccountInFullAccountAsync(int detailId)
        {
            string errorKey = String.Empty;
            var detailAccount = await Repository
                .GetAllQuery<DetailAccount>(
                    ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => facc.Id == detailId)
                .SingleOrDefaultAsync();
            if (detailAccount == null)
            {
                errorKey = AppStrings.InvalidDetailAccountInFullAccount;
            }
            else if (detailAccount.Children.Count > 0)
            {
                errorKey = AppStrings.NonLeafDetailAccountInFullAccount;
            }

            return errorKey;
        }

        private async Task<string> EnsureValidCostCenterInFullAccountAsync(int costCenterId)
        {
            string errorKey = String.Empty;
            var costCenter = await Repository
                .GetAllQuery<CostCenter>(ViewId.CostCenter, cc => cc.Children)
                .Where(cc => cc.Id == costCenterId)
                .SingleOrDefaultAsync();
            if (costCenter == null)
            {
                errorKey = AppStrings.InvalidCostCenterInFullAccount;
            }
            else if (costCenter.Children.Count > 0)
            {
                errorKey = AppStrings.NonLeafCostCenterInFullAccount;
            }

            return errorKey;
        }

        private async Task<string> EnsureValidProjectInFullAccountAsync(int projectId)
        {
            string errorKey = String.Empty;
            var project = await Repository
                .GetAllQuery<Project>(ViewId.Project, prj => prj.Children)
                .Where(prj => prj.Id == projectId)
                .SingleOrDefaultAsync();
            if (project == null)
            {
                errorKey = AppStrings.InvalidProjectInFullAccount;
            }
            else if (project.Children.Count > 0)
            {
                errorKey = AppStrings.NonLeafProjectInFullAccount;
            }

            return errorKey;
        }

        #endregion

        private async Task LogAssociationOperationAsync<TEntity>(
            OperationId operation, string fromItem, int fromId, string toItem, GridOptions gridOptions = null)
            where TEntity : class, ITreeEntity
        {
            bool needsLog = (gridOptions != null && gridOptions.ListChanged)
                || operation == OperationId.Save;
            if (needsLog)
            {
                var repository = UnitOfWork.GetAsyncRepository<TEntity>();
                var accountItem = await repository.GetByIDAsync(fromId);
                if (accountItem != null)
                {
                    var operationId = gridOptions != null
                        ? (OperationId)gridOptions.Operation
                        : operation;
                    string description = GetRelationLogDescription(accountItem, fromItem, toItem);
                    await OnSourceActionAsync(operationId, description,
                        accountItem.Name, accountItem.FullCode, accountItem.Description ?? String.Empty);
                }
            }
        }

        private string GetRelationLogDescription<TEntity>(
            TEntity accountItem, string fromItem, string toItem)
            where TEntity : class, ITreeEntity
        {
            string template = Context.Localize(AppStrings.AccountItemRelations);
            string description = String.Format(template,
                fromItem, accountItem.Name, accountItem.FullCode, toItem);
            return Context.Localize(description);
        }

        private readonly ISystemRepository _system;
        private readonly IAccountItemRepository _itemRepository;
    }
}
