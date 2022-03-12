using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت کنترل سیستم را تعریف می کند.
    /// </summary>
    public class SystemIssueRepository : LoggingRepositoryBase, ISystemIssueRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="relationRepository">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public SystemIssueRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relationRepository)
            : base(context, system.Logger)
        {
            _system = system;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی موضوعات سیستم قابل دسترسی توسط کاربر مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از موضوعات سیستم قابل دسترسی توسط کاربر</returns>
        public async Task<IList<SystemIssueViewModel>> GetUserSystemIssuesAsync(int userId)
        {
            UnitOfWork.UseSystemContext();
            var issues = await FilterInaccessibleIssues();
            UnitOfWork.UseCompanyContext();

            return issues
                .Select(issue => Mapper.Map<SystemIssueViewModel>(issue))
                .ToList();
        }

        /// <summary>
        /// اطلاعات خلاصه ای از اشکالات موجود در سیستم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueId">شناسه دیتابیسی یکی از اشکالات تعریف شده که به همراه اشکالات زیرمجموعه در ساختار درختی
        /// برای گزارشگیری استفاده می شود</param>
        /// <param name="gridOptions"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>فهرست اطلاعات خلاصه برای اشکالات موجود</returns>
        public async Task<IList<SystemIssueSummaryViewModel>> GetSystemIssueSummaries(
            int issueId, GridOptions gridOptions, DateTime from, DateTime to)
        {
            var summaries = new List<SystemIssueSummaryViewModel>();
            UnitOfWork.UseSystemContext();
            var children = await GetChildIssuesAsync(issueId);
            var repository = UnitOfWork.GetAsyncRepository<SystemIssue>();
            var issues = await repository
                .GetEntityQuery()
                .Where(issue => children.Contains(issue.Id))
                .ToListAsync();
            UnitOfWork.UseCompanyContext();
            foreach (var issue in issues)
            {
                summaries.Add(await GetSummaryAsync(issue, from, to, gridOptions));
            }

            return summaries;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد دارای نا تراز را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد نا تراز</returns>
        public async Task<PagedList<VoucherViewModel>> GetUnbalancedVouchersAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = await GetUnbalancedVouchersAsync(from, to);
            var pagedList = new PagedList<VoucherViewModel>(
                vouchers.Select(v => Mapper.Map<VoucherViewModel>(v)), gridOptions);
            SortPagedListItems(pagedList);
            await OnSourceActionAsync(gridOptions, SourceListId.UnbalancedVouchers);
            return pagedList;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد فاقد آرتیکل را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد فاقد آرتیکل</returns>p
        public async Task<PagedList<VoucherViewModel>> GetVouchersWithNoArticleAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = await GetNoArticleVouchersAsync(from, to);
            var pagedList = new PagedList<VoucherViewModel>(
                vouchers.Select(v => Mapper.Map<VoucherViewModel>(v)), gridOptions);
            SortPagedListItems(pagedList);
            await OnSourceActionAsync(gridOptions, SourceListId.VouchersWithNoArticle);
            return pagedList;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد شماره اسناد جا افتاده را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد شماره اسناد جا افتاده</returns>
        public async Task<PagedList<NumberListViewModel>> GetMissingVoucherNumbersAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var missingNumbers = await GetMissingVoucherNumbersAsync(from, to);
            await OnSourceActionAsync(gridOptions, SourceListId.MissingVoucherNumbers);
            return new PagedList<NumberListViewModel>(missingNumbers, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد آرتیکل ها را بر اساس نوع کنترل سیستم برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="issueType">نوع کنترل سیستم</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد آرتیکل ها</returns>
        public async Task<PagedList<VoucherLineDetailViewModel>> GetSystemIssueArticlesAsync(
            GridOptions gridOptions, string issueType, DateTime from, DateTime to)
        {
            PagedList<VoucherLineDetailViewModel> result;
            SourceListId sourceList;
            switch (issueType)
            {
                case "miss-acc":
                    {
                        var lines = await GetMissingAccountArticlesAsync(from, to);
                        result = new PagedList<VoucherLineDetailViewModel>(
                            lines.Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl)), gridOptions);
                        sourceList = SourceListId.ArticlesWithMissingAccount;
                        break;
                    }

                case "zero-amount":
                    {
                        var lines = await GetZeroAmountArticlesAsync(from, to);
                        result = new PagedList<VoucherLineDetailViewModel>(
                            lines.Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl)), gridOptions);
                        sourceList = SourceListId.ArticlesHavingZeroAmount;
                        break;
                    }

                case "invalid-acc":
                    {
                        var lines = await GetInvalidAccountArticlesAsync(from, to);
                        result = new PagedList<VoucherLineDetailViewModel>(
                            lines.Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl)), gridOptions);
                        sourceList = SourceListId.ArticlesWithInvalidAccountItems;
                        break;
                    }

                case "invalid-acc-balance":
                    {
                        var lines = await GetInvalidBalanceArticlesAsync(to);
                        result = new PagedList<VoucherLineDetailViewModel>(
                            lines.Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl)), gridOptions);
                        sourceList = SourceListId.AccountsWithInvalidBalance;
                        break;
                    }

                case "invalid-acc-turnover":
                    {
                        var lines = await GetInvalidTurnoverArticlesAsync(from, to);
                        result = new PagedList<VoucherLineDetailViewModel>(
                            lines.Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl)), gridOptions);
                        sourceList = SourceListId.AccountsWithInvalidPeriodTurnover;
                        break;
                    }

                default:
                    {
                        result = new PagedList<VoucherLineDetailViewModel>(null);
                        sourceList = SourceListId.None;
                        break;
                    }
            }

            await OnSourceActionAsync(gridOptions, sourceList);
            SortPagedListItems(result);
            return result;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.SystemIssue; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private static void SortPagedListItems(PagedList<VoucherViewModel> pagedList)
        {
            var sortedItems = pagedList.Items
                .OrderBy(item => item.Date.Date)
                .ThenBy(item => item.No)
                .ToList();
            pagedList.Items.Clear();
            pagedList.Items.AddRange(sortedItems);
        }

        private static void SortPagedListItems(PagedList<VoucherLineDetailViewModel> pagedList)
        {
            var sortedItems = pagedList.Items
                .OrderBy(item => item.VoucherDate.Date)
                .ThenBy(item => item.VoucherNo)
                .ToList();
            pagedList.Items.Clear();
            pagedList.Items.AddRange(sortedItems);
        }

        private async Task<IList<SystemIssue>> FilterInaccessibleIssues()
        {
            var repository = UnitOfWork.GetAsyncRepository<SystemIssue>();
            var userPermissions = await GetUserPermissionIdsAsync();
            bool isAdmin = UserContext.Roles.Contains(AppConstants.AdminRoleId);

            if (isAdmin)
            {
                return await repository.GetAllAsync();
            }
            else
            {
                return await repository.GetByCriteriaAsync(
                    issue => !issue.PermissionID.HasValue
                    || userPermissions.Contains(issue.PermissionID.Value));
            }
        }

        private async Task<IList<int>> GetUserPermissionIdsAsync()
        {
            var permissionIds = new List<int>();
            var roles = await GetUserRolesAsync();
            Array.ForEach(roles.ToArray(),
                role => permissionIds.AddRange(role.RolePermissions.Select(rp => rp.PermissionId)));

            return permissionIds
                .Distinct()
                .ToList();
        }

        private async Task<IList<Role>> GetUserRolesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var userRolesIds = UserContext.Roles;

            var roles = await repository.GetByCriteriaAsync(
                role => userRolesIds.Contains(role.Id),
                role => role.RolePermissions);

            return roles;
        }

        private IQueryable<VoucherLine> GetArticlesQueryAsync(DateTime from, DateTime to)
        {
            var lines = Repository.GetAllOperationQuery<VoucherLine>(
                ViewId.VoucherLine,
                line => line.Voucher,
                line => line.Account,
                line => line.DetailAccount,
                line => line.CostCenter,
                line => line.Project,
                line => line.Currency)
                .Where(line => line.Voucher.SubjectType != (short)SubjectType.Draft
                    && line.Voucher.Date.Date >= from.Date
                    && line.Voucher.Date.Date <= to.Date);
            return lines;
        }

        private async Task<SystemIssueSummaryViewModel> GetSummaryAsync(
            SystemIssue issue, DateTime from, DateTime to, GridOptions gridOptions)
        {
            IList<Voucher> vouchers = null;
            IList<VoucherLine> lines = null;
            var summary = new SystemIssueSummaryViewModel()
            {
                Id = issue.Id,
                ParentId = issue.ParentId
            };

            // NOTE: Switch statement was replaced by if-else due to variable scoping limitations...
            if (issue.TitleKey == AppStrings.UnbalancedVouchers)
            {
                vouchers = await GetUnbalancedVouchersAsync(from, to);
                summary.ItemCount = vouchers
                    .Select(v => Mapper.Map<VoucherViewModel>(v))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.VouchersWithNoArticle)
            {
                vouchers = await GetNoArticleVouchersAsync(from, to);
                summary.ItemCount = vouchers
                    .Select(v => Mapper.Map<VoucherViewModel>(v))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.MissingVoucherNumbers)
            {
                var items = await GetMissingVoucherNumbersAsync(from, to);
                summary.ItemCount = items
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.ArticlesWithMissingAccount)
            {
                lines = await GetMissingAccountArticlesAsync(from, to);
                summary.ItemCount = lines
                    .Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.ArticlesHavingZeroAmount)
            {
                lines = await GetZeroAmountArticlesAsync(from, to);
                summary.ItemCount = lines
                    .Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.ArticlesWithInvalidAccountItems)
            {
                lines = await GetInvalidAccountArticlesAsync(from, to);
                summary.ItemCount = lines
                    .Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.AccountsWithInvalidBalance)
            {
                lines = await GetInvalidBalanceArticlesAsync(to);
                summary.ItemCount = lines
                    .Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl))
                    .Apply(gridOptions, false)
                    .Count();
            }
            else if (issue.TitleKey == AppStrings.AccountsWithInvalidPeriodTurnover)
            {
                lines = await GetInvalidTurnoverArticlesAsync(from, to);
                summary.ItemCount = lines
                    .Select(vl => Mapper.Map<VoucherLineDetailViewModel>(vl))
                    .Apply(gridOptions, false)
                    .Count();
            }

            return summary;
        }

        private async Task<IList<Voucher>> GetUnbalancedVouchersAsync(DateTime from, DateTime to)
        {
            return await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin, v => v.Branch)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && !voucher.IsBalanced
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .ToListAsync();
        }

        private async Task<IList<Voucher>> GetNoArticleVouchersAsync(DateTime from, DateTime to)
        {
            return await Repository
                .GetAllOperationQuery<Voucher>(ViewId.Voucher, v => v.Status, v => v.Origin, v => v.Branch)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && voucher.Lines.Count == 0
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .ToListAsync();
        }

        private async Task<IList<NumberListViewModel>> GetMissingVoucherNumbersAsync(DateTime from, DateTime to)
        {
            var allMissingNumbers = new List<NumberListViewModel>();
            var existingNumbers = await Repository.GetAllOperationQuery<Voucher>(ViewId.Voucher)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(voucher => voucher.No)
                .ToListAsync();

            if (existingNumbers.Count > 0)
            {
                var maxNumber = existingNumbers.Max();
                var numRange = Enumerable.Range(1, maxNumber);
                allMissingNumbers = numRange
                    .Where(num => !existingNumbers.Contains(num))
                    .OrderBy(num => num)
                    .Select(num => new NumberListViewModel() { Number = num })
                    .ToList();
            }

            return allMissingNumbers;
        }

        private async Task<IList<VoucherLine>> GetMissingAccountArticlesAsync(DateTime from, DateTime to)
        {
            return await GetArticlesQueryAsync(from, to)
                .Where(line => line.Account == null)
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetZeroAmountArticlesAsync(DateTime from, DateTime to)
        {
            return await GetArticlesQueryAsync(from, to)
                .Where(line => line.Debit == 0 && line.Credit == 0)
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetInvalidBalanceArticlesAsync(DateTime to)
        {
            var result = new List<VoucherLine>();
            var lines = await Repository.GetAllOperationQuery<VoucherLine>(
                ViewId.VoucherLine,
                line => line.Voucher,
                line => line.Account,
                line => line.DetailAccount,
                line => line.CostCenter,
                line => line.Project,
                line => line.Currency)
                .Where(line => line.Voucher.Date.Date <= to.Date
                    && (line.Account.TurnoverMode == (short)TurnoverMode.CreditorEndPeriod
                    || line.Account.TurnoverMode == (short)TurnoverMode.DebtorEndPeriod))
                .OrderBy(line => line.Voucher.Date)
                .ThenBy(line => line.Voucher.No)
                .ToListAsync();
            var accountGroups = lines.GroupBy(line => line.Account);
            foreach (var group in accountGroups)
            {
                decimal balance = 0;
                foreach (var line in group)
                {
                    balance += line.Debit - line.Credit;
                    if (line.Account.TurnoverMode == (short)TurnoverMode.CreditorEndPeriod && balance > 0)
                    {
                        result.Add(line);
                        break;
                    }

                    if (line.Account.TurnoverMode == (short)TurnoverMode.DebtorEndPeriod && balance < 0)
                    {
                        result.Add(line);
                        break;
                    }
                }
            }

            return result;
        }

        private async Task<IList<VoucherLine>> GetInvalidAccountArticlesAsync(DateTime from, DateTime to)
        {
            var lines = await GetArticlesQueryAsync(from, to)
                .ToListAsync();
            return lines
                .Where(line => !_relationRepository.LookupFullAccount(
                    Mapper.Map<AccountItemBriefViewModel>(line.Account),
                    Mapper.Map<AccountItemBriefViewModel>(line.DetailAccount),
                    Mapper.Map<AccountItemBriefViewModel>(line.CostCenter),
                    Mapper.Map<AccountItemBriefViewModel>(line.Project)))
                .ToList();
        }

        private async Task<IList<VoucherLine>> GetInvalidTurnoverArticlesAsync(DateTime from, DateTime to)
        {
            return await GetArticlesQueryAsync(from, to)
                .Where(line =>
                    (line.Account.TurnoverMode == (short)TurnoverMode.CreditorDuringPeriod && line.Debit != 0)
                    || (line.Account.TurnoverMode == (short)TurnoverMode.DebtorDuringPeriod && line.Credit != 0))
                .ToListAsync();
        }

        private async Task<IEnumerable<int>> GetChildIssuesAsync(int issueId)
        {
            var children = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<SystemIssue>();
            var issue = await repository.GetByIDAsync(issueId, iss => iss.Children);
            if (issue != null)
            {
                children.Add(issueId);
                AddChildren(issue, children);
            }

            return children;
        }

        private void AddChildren(SystemIssue issue, IList<int> children)
        {
            var repository = UnitOfWork.GetRepository<SystemIssue>();
            foreach (var child in issue.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, iss => iss.Children);
                AddChildren(item, children);
            }
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
    }
}
