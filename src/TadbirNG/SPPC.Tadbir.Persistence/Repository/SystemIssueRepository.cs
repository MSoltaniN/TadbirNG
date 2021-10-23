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
            var sysIssues = await FilterInaccessibleIssues();
            UnitOfWork.UseCompanyContext();

            return sysIssues
                .Select(iss => Mapper.Map<SystemIssueViewModel>(iss))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد فاقد آرتیکل را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد فاقد آرتیکل</returns>p
        public async Task<ValueTuple<IList<VoucherViewModel>, int>> GetVouchersWithNoArticleAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = Repository.GetAllOperationQuery<Voucher>(
                ViewId.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && voucher.Lines.Count == 0
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item));

            var listAndCount = await GetListAndCountAsync(gridOptions, vouchers);
            await OnSourceActionAsync(gridOptions, SourceListId.VouchersWithNoArticle);
            return listAndCount;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد دارای نا تراز را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد نا تراز</returns>
        public async Task<ValueTuple<IList<VoucherViewModel>, int>> GetUnbalancedVouchersAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var vouchers = Repository.GetAllOperationQuery<Voucher>(
                ViewId.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && !voucher.IsBalanced
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(item => Mapper.Map<VoucherViewModel>(item));

            var listAndCount = await GetListAndCountAsync(gridOptions, vouchers);
            await OnSourceActionAsync(gridOptions, SourceListId.UnbalancedVouchers);
            return listAndCount;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد شماره اسناد جا افتاده را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد شماره اسناد جا افتاده</returns>
        public async Task<ValueTuple<IList<NumberListViewModel>, int>> GetMissingVoucherNumbersAsync(
            GridOptions gridOptions, DateTime from, DateTime to)
        {
            var missingNumbers = new List<NumberListViewModel>();
            int count = 0;
            var listAndCount = (missingNumbers, count);
            var existingNumbers = await Repository.GetAllOperationQuery<Voucher>(ViewId.Voucher)
                .Where(voucher => voucher.SubjectType != (short)SubjectType.Draft
                    && voucher.Date.Date >= from.Date && voucher.Date.Date <= to.Date)
                .Select(voucher => voucher.No)
                .ToListAsync();

            if (existingNumbers.Count > 0)
            {
                var maxNumber = existingNumbers.Max();
                var numRange = Enumerable.Range(1, maxNumber);
                var allMissingNumbers = numRange
                    .Where(num => !existingNumbers.Contains(num))
                    .OrderBy(num => num)
                    .Select(num => new NumberListViewModel() { Number = num });

                count = allMissingNumbers
                    .Apply(gridOptions, false)
                    .Count();
                missingNumbers.AddRange(allMissingNumbers.Apply(gridOptions));
                listAndCount = (missingNumbers, count);
            }

            await OnSourceActionAsync(gridOptions, SourceListId.MissingVoucherNumbers);
            return listAndCount;
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد آرتیکل ها را بر اساس نوع کنترل سیستم برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="issueType">نوع کنترل سیستم</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد آرتیکل ها</returns>
        public async Task<ValueTuple<IList<VoucherLineDetailViewModel>, int>> GetSystemIssueArticlesAsync(
            GridOptions gridOptions, string issueType, DateTime from, DateTime to)
        {
            (IList<VoucherLineDetailViewModel>, int) result;
            SourceListId sourceList;
            switch (issueType)
            {
                case "miss-acc":
                    {
                        var lines = GetArticlesQueryAsync(from, to);
                        lines = GetArticlesWithMissingAccount(lines);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        sourceList = SourceListId.ArticlesWithMissingAccount;
                        break;
                    }

                case "zero-amount":
                    {
                        var lines = GetArticlesQueryAsync(from, to);
                        lines = GetArticleHavingZeroAmount(lines);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        sourceList = SourceListId.ArticlesHavingZeroAmount;
                        break;
                    }

                case "invalid-acc":
                    {
                        var lines = GetArticlesQueryAsync(from, to);
                        var enumerableLines = GetArticleWithInvalidAccount(lines);
                        result = GetListAndCount(gridOptions, enumerableLines);
                        sourceList = SourceListId.ArticlesWithInvalidAccountItems;
                        break;
                    }

                case "invalid-acc-balance":
                    {
                        result = await GetArticleWithInvalidBalance(gridOptions, to);
                        sourceList = SourceListId.AccountsWithInvalidBalance;
                        break;
                    }

                case "invalid-acc-turnover":
                    {
                        var lines = GetArticleWithInvalidTurnover(from, to);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        sourceList = SourceListId.AccountsWithInvalidPeriodTurnover;
                        break;
                    }

                default:
                    {
                        result = new ValueTuple<IList<VoucherLineDetailViewModel>, int>(
                            new List<VoucherLineDetailViewModel>(), 0);
                        sourceList = SourceListId.None;
                        break;
                    }
            }

            await OnSourceActionAsync(gridOptions, sourceList);
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

        private static async Task<ValueTuple<IList<VoucherViewModel>, int>> GetListAndCountAsync(
            GridOptions gridOptions, IQueryable<VoucherViewModel> vouchers)
        {
            var filteredList = vouchers
                .Apply(gridOptions, false);

            var vouchersList = await filteredList
                .OrderBy(voucher => voucher.Date.Date)
                .ThenBy(voucher => voucher.No)
                .ApplyPaging(gridOptions)
                .ToListAsync();

            return (vouchersList, await filteredList.CountAsync());
        }

        private static IQueryable<VoucherLine> GetArticlesWithMissingAccount(IQueryable<VoucherLine> voucherLines)
        {
            var lines = voucherLines
                 .Where(line => line.Account == null);

            return lines;
        }

        private static IQueryable<VoucherLine> GetArticleHavingZeroAmount(IQueryable<VoucherLine> voucherLines)
        {
            var lines = voucherLines
                 .Where(line => line.Debit == 0 && line.Credit == 0);

            return lines;
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

        private IQueryable<VoucherLine> GetArticleWithInvalidTurnover(DateTime from, DateTime to)
        {
            var lines = GetArticlesQueryAsync(from, to);

            lines = lines
                .Where(line =>
                (line.Account.TurnoverMode == (short)TurnoverMode.CreditorDuringPeriod && line.Debit != 0)
                || (line.Account.TurnoverMode == (short)TurnoverMode.DebtorDuringPeriod && line.Credit != 0));

            return lines;
        }

        private async Task<ValueTuple<IList<VoucherLineDetailViewModel>, int>> GetArticleWithInvalidBalance(
            GridOptions gridOptions, DateTime to)
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

            var accountGroup = lines.GroupBy(line => line.Account);

            foreach (var group in accountGroup)
            {
                decimal balancedValue = 0;

                foreach (var line in group)
                {
                    balancedValue += line.Debit - line.Credit;
                    if (line.Account.TurnoverMode == (short)TurnoverMode.CreditorEndPeriod && balancedValue > 0)
                    {
                        result.Add(line);
                        break;
                    }

                    if (line.Account.TurnoverMode == (short)TurnoverMode.DebtorEndPeriod && balancedValue < 0)
                    {
                        result.Add(line);
                        break;
                    }
                }
            }

            var voucherLines = result.Select(item => Mapper.Map<VoucherLineDetailViewModel>(item));
            voucherLines = voucherLines
                .ApplyPaging(gridOptions);

            return (voucherLines.ToList(), result.Count);
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

        private IEnumerable<VoucherLine> GetArticleWithInvalidAccount(IQueryable<VoucherLine> voucherLines)
        {
            var lines = voucherLines.ToList();

            var lineList = lines.Where(
                line => !_relationRepository.LookupFullAccount(
                    Mapper.Map<AccountItemBriefViewModel>(line.Account),
                    Mapper.Map<AccountItemBriefViewModel>(line.DetailAccount),
                    Mapper.Map<AccountItemBriefViewModel>(line.CostCenter),
                    Mapper.Map<AccountItemBriefViewModel>(line.Project)));

            return lineList;
        }

        private async Task<ValueTuple<IList<VoucherLineDetailViewModel>, int>> GetListAndCountAsync(
            GridOptions gridOptions, IQueryable<VoucherLine> lines)
        {
            var voucherLines = lines.Select(item => Mapper.Map<VoucherLineDetailViewModel>(item));

            var filteredList = voucherLines
                .Apply(gridOptions, false);

            var vouchersList = await filteredList
                .OrderBy(line => line.VoucherDate.Date)
                .ThenBy(line => line.VoucherNo)
                .ApplyPaging(gridOptions)
                .ToListAsync();

            return (vouchersList, await filteredList.CountAsync());
        }

        private ValueTuple<IList<VoucherLineDetailViewModel>, int> GetListAndCount(
            GridOptions gridOptions, IEnumerable<VoucherLine> lines)
        {
            var voucherLines = lines.Select(item => Mapper.Map<VoucherLineDetailViewModel>(item));

            var filteredList = voucherLines
               .Apply(gridOptions, false);

            var vouchersList = filteredList
                .OrderBy(line => line.VoucherDate.Date)
                .ThenBy(line => line.VoucherNo)
                .ApplyPaging(gridOptions)
                .ToList();

            return (vouchersList, filteredList.Count());
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
    }
}
