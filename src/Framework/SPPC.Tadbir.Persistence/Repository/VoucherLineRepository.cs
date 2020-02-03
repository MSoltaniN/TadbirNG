using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Extensions;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را پیاده سازی می کند.
    /// </summary>
    public class VoucherLineRepository : LoggingRepository<VoucherLine, VoucherLineViewModel>, IVoucherLineRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public VoucherLineRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relations, ILogConfigRepository config)
            : base(context, config, system?.Logger)
        {
            _system = system;
            _relationRepository = relations;
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<IList<VoucherLineViewModel>> GetArticlesAsync(int voucherId, GridOptions gridOptions = null)
        {
            var query = GetVoucherLinesQuery(voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewName.VoucherLine);
            var lines = await query
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .Apply(gridOptions)
                .ToListAsync();
            return lines;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<VoucherLineViewModel> GetArticleAsync(int articleId)
        {
            VoucherLineViewModel articleViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var article = await GetVoucherLineQuery(articleId)
                .SingleOrDefaultAsync();
            if (article != null)
            {
                articleViewModel = Mapper.Map<VoucherLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync<TViewModel>(int voucherId, GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery()
                .Where(line => line.Voucher.Id == voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewName.VoucherLine);
            return await query
                .Select(line => Mapper.Map<TViewModel>(line))
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی سرفصل حسابداری مشخص شده
        /// را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مدل نمایشی سرفصل حسابداری مورد استفاده در آرتیکل</returns>
        public async Task<AccountViewModel> GetArticleAccountAsync(int accountId)
        {
            var articleAccount = default(AccountViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                articleAccount = Mapper.Map<AccountViewModel>(account);
            }

            return articleAccount;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی تفصیلی شناور مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی شناور مورد استفاده در آرتیکل</returns>
        public async Task<DetailAccountViewModel> GetArticleDetailAccountAsync(int faccountId)
        {
            var articleDetailAccount = default(DetailAccountViewModel);
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, acc => acc.Children);
            if (detailAccount != null)
            {
                articleDetailAccount = Mapper.Map<DetailAccountViewModel>(detailAccount);
            }

            return articleDetailAccount;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی مرکز هزینه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مرکز هزینه مورد استفاده در آرتیکل</returns>
        public async Task<CostCenterViewModel> GetArticleCostCenterAsync(int costCenterId)
        {
            var articleCostCenter = default(CostCenterViewModel);
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, acc => acc.Children);
            if (costCenter != null)
            {
                articleCostCenter = Mapper.Map<CostCenterViewModel>(costCenter);
            }

            return articleCostCenter;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی پروژه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه مورد استفاده در آرتیکل</returns>
        public async Task<ProjectViewModel> GetArticleProjectAsync(int projectId)
        {
            var articleProject = default(ProjectViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, acc => acc.Children);
            if (project != null)
            {
                articleProject = Mapper.Map<ProjectViewModel>(project);
            }

            return articleProject;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="lineView">آرتیکل برای ایجاد یا اصلاح</param>
        public async Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel lineView)
        {
            Verify.ArgumentNotNull(lineView, "lineView");
            VoucherLine line = default(VoucherLine);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            if (lineView.Id == 0)
            {
                line = Mapper.Map<VoucherLine>(lineView);
                line.CreatedById = UserContext.Id;
                await InsertAsync(repository, line);
                await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
            }
            else
            {
                line = await repository.GetByIDAsync(lineView.Id);
                if (line != null)
                {
                    await UpdateAsync(repository, line, lineView);
                    await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
                }
            }

            return Mapper.Map<VoucherLineViewModel>(line);
        }

        /// <summary>
        /// به روش آسنکرون، علامتگذاری مشخص شده را روی آرتیکل سند اعمال می کند
        /// </summary>
        /// <param name="mark">اطلاعات علامتکذاری آرتیکل</param>
        public async Task SaveArticleMarkAsync(VoucherLineMarkViewModel mark)
        {
            Verify.ArgumentNotNull(mark, nameof(mark));
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var line = await repository.GetByIDAsync(mark.Id);
            if (line != null)
            {
                line.Mark = mark.Mark;
                repository.Update(line);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل برای حذف</param>
        public async Task DeleteArticleAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var article = await repository.GetByIDAsync(articleId);
            if (article != null)
            {
                await DeleteAsync(repository, article);
                await UpdateVoucherBalanceStatusAsync(article.VoucherId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteArticlesAsync(IEnumerable<int> items)
        {
            int voucherId = 0;
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            foreach (int item in items)
            {
                var article = await repository.GetByIDAsync(item);
                if (article != null)
                {
                    voucherId = article.VoucherId;
                    await DeleteNoLogAsync(repository, article);
                }
            }

            if (voucherId > 0)
            {
                await UpdateVoucherBalanceStatusAsync(voucherId);
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های ثبت شده را برمیگرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل ها</returns>
        public async Task<int> GetAllArticlesCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery();
            return await query.CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد آرتیکل ها را بر اساس نوع کنترل سیستم برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="issueType">نوع کنترل سیستم</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد آرتیکل ها</returns>
        public async Task<(IList<VoucherLineDetailViewModel>, int)> GetSystemIssueArticlesAsync(GridOptions gridOptions, string issueType, DateTime from, DateTime to)
        {
            (IList<VoucherLineDetailViewModel>, int) result;

            switch (issueType)
            {
                case "miss-acc":
                    {
                        var lines = GetArticlesAsync(from, to);
                        lines = GetArticlesWithMissingAccount(lines);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        break;
                    }

                case "zero-amount":
                    {
                        var lines = GetArticlesAsync(from, to);
                        lines = GetArticleHavingZeroAmount(lines);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        break;
                    }

                case "invalid-acc":
                    {
                        var lines = GetArticlesAsync(from, to);
                        var enumerableLines = GetArticleWithInvalidAccount(lines);
                        result = GetListAndCount(gridOptions, enumerableLines);
                        break;
                    }

                case "invalid-acc-balance":
                    {
                        result = await GetArticleWithInvalidBalance(gridOptions, to);
                        break;
                    }

                case "invalid-acc-turnover":
                    {
                        var lines = GetArticleWithInvalidTurnover(from, to);
                        result = await GetListAndCountAsync(gridOptions, lines);
                        break;
                    }

                default:
                    {
                        result = (new List<VoucherLineDetailViewModel>(), 0);
                        break;
                    }
            }

            return result;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.VoucherLine; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="lineView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="line">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(VoucherLineViewModel lineView, VoucherLine line)
        {
            line.AccountId = lineView.FullAccount.Account.Id;
            line.DetailId = lineView.FullAccount.DetailAccount.Id > 0 ? lineView.FullAccount.DetailAccount.Id : (int?)null;
            line.CostCenterId = lineView.FullAccount.CostCenter.Id > 0 ? lineView.FullAccount.CostCenter.Id : (int?)null;
            line.ProjectId = lineView.FullAccount.Project.Id > 0 ? lineView.FullAccount.Project.Id : (int?)null;
            line.CurrencyId = lineView.CurrencyId;
            line.Debit = lineView.Debit;
            line.Credit = lineView.Credit;
            line.Description = lineView.Description;
            line.CurrencyValue = lineView.CurrencyValue;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(VoucherLine entity)
        {
            return (entity != null)
                ? String.Format(
                    @"Account : {1}{0}Detail Account : {2}{0}Cost Center : {3}{0}Project : {4}
Currency : {5}{0}Debit : {6}{0}Credit : {7}{0}Description : {8}",
                    Environment.NewLine, entity.AccountId, entity.DetailId, entity.CostCenterId, entity.ProjectId,
                    entity.CurrencyId, entity.Debit, entity.Credit, entity.Description)
                : null;
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

        private IQueryable<VoucherLine> GetArticleWithInvalidTurnover(DateTime from, DateTime to)
        {
            var lines = GetArticlesAsync(from, to);

            lines = lines
                .Where(line =>
                (line.Account.TurnoverMode == (short)TurnoverMode.CreditorDuringPeriod && line.Debit != 0)
                || (line.Account.TurnoverMode == (short)TurnoverMode.DebtorDuringPeriod && line.Credit != 0));

            return lines;
        }

        private async Task<(IList<VoucherLineDetailViewModel>, int)> GetArticleWithInvalidBalance(GridOptions gridOptions, DateTime to)
        {
            List<VoucherLine> result = new List<VoucherLine>();

            var lines = await Repository.GetAllOperationQuery<VoucherLine>(
                ViewName.VoucherLine,
                line => line.Voucher,
                line => line.Account,
                line => line.DetailAccount,
                line => line.CostCenter,
                line => line.Project,
                line => line.Currency)
                .Where(line => line.Voucher.Date.Date <= to.Date
                && (line.Account.TurnoverMode == (short)TurnoverMode.CreditorEndPeriod || line.Account.TurnoverMode == (short)TurnoverMode.DebtorEndPeriod))
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

            return (voucherLines.ToList(), result.Count());
        }

        private IQueryable<VoucherLine> GetArticlesAsync(DateTime from, DateTime to)
        {
            var lines = Repository.GetAllOperationQuery<VoucherLine>(
                ViewName.VoucherLine,
                line => line.Voucher,
                line => line.Account,
                line => line.DetailAccount,
                line => line.CostCenter,
                line => line.Project,
                line => line.Currency)
                .Where(line => line.Voucher.Date.Date >= from.Date && line.Voucher.Date.Date <= to.Date);
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

        private async Task<(IList<VoucherLineDetailViewModel>, int)> GetListAndCountAsync(GridOptions gridOptions, IQueryable<VoucherLine> lines)
        {
            var voucherLines = lines.Select(item => Mapper.Map<VoucherLineDetailViewModel>(item));

            var filteredList = voucherLines
                .Apply(gridOptions, false);

            var vouchersList = await filteredList
                .OrderBy(line => line.VoucherDate)
                .ThenBy(line => line.VoucherNo)
                .ApplyPaging(gridOptions)
                .ToListAsync();

            return (vouchersList, await filteredList.CountAsync());
        }

        private (IList<VoucherLineDetailViewModel>, int) GetListAndCount(GridOptions gridOptions, IEnumerable<VoucherLine> lines)
        {
            var voucherLines = lines.Select(item => Mapper.Map<VoucherLineDetailViewModel>(item));

            var filteredList = voucherLines
               .Apply(gridOptions, false);

            var vouchersList = filteredList
                .OrderBy(line => line.VoucherDate)
                .ThenBy(line => line.VoucherNo)
                .ApplyPaging(gridOptions)
                .ToList();

            return (vouchersList, filteredList.Count());
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private async Task UpdateVoucherBalanceStatusAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetByIDAsync(voucherId, v => v.Lines);
            var debitSum = voucher.Lines.Sum(vl => vl.Debit);
            var creditSum = voucher.Lines.Sum(vl => vl.Credit);
            voucher.IsBalanced = debitSum.AlmostEquals(creditSum);
            repository.Update(voucher);
            await UnitOfWork.CommitAsync();
        }

        private IQueryable<VoucherLine> GetVoucherLinesQuery(int voucherId)
        {
            var repository = UnitOfWork.GetRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency)
                .Where(line => line.Voucher.Id == voucherId);
            return linesQuery;
        }

        private IQueryable<VoucherLine> GetVoucherLineQuery(int articleId)
        {
            var repository = UnitOfWork.GetRepository<VoucherLine>();
            var lineQuery = repository
                .GetEntityQuery(
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency)
                .Where(line => line.Id == articleId);
            return lineQuery;
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
    }
}
