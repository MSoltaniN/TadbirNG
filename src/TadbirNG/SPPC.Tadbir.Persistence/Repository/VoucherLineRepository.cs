using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Extensions;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را پیاده سازی می کند.
    /// </summary>
    public class VoucherLineRepository
        : EntityLoggingRepository<VoucherLine, VoucherLineViewModel>, IVoucherLineRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public VoucherLineRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<VoucherLineViewModel>> GetArticlesAsync(
            int voucherId, GridOptions gridOptions)
        {
            var query = GetVoucherLinesQuery(voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewId.VoucherLine);
            var lines = await query
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return new PagedList<VoucherLineViewModel>(lines, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<VoucherLineViewModel> GetArticleAsync(int articleId)
        {
            VoucherLineViewModel articleViewModel = null;
            var article = await GetVoucherLineQuery(articleId)
                .SingleOrDefaultAsync();
            if (article != null)
            {
                articleViewModel = Mapper.Map<VoucherLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <inheritdoc/>
        public async Task<int> GetArticleCountAsync<TViewModel>(int voucherId, GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery()
                .Where(line => line.Voucher.Id == voucherId);
            query = Repository.ApplyRowFilter(ref query, ViewId.VoucherLine);
            var articles = await query
                .Select(line => Mapper.Map<TViewModel>(line))
                .ToListAsync();
            return articles
                .Apply(gridOptions, false)
                .Count();
        }

        /// <inheritdoc/>
        public async Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel lineView)
        {
            Verify.ArgumentNotNull(lineView, "lineView");
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            VoucherLine line;
            if (lineView.Id == 0)
            {
                line = Mapper.Map<VoucherLine>(lineView);
                line.RowNo = await GetNextRowNoAsync(line.VoucherId);
                line.CreatedById = UserContext.Id;
                await InsertAsync(repository, line, OperationId.CreateLine);
                await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
            }
            else
            {
                line = await repository.GetByIDAsync(lineView.Id);
                if (line != null)
                {
                    await UpdateAsync(repository, line, lineView, OperationId.EditLine);
                    await UpdateVoucherBalanceStatusAsync(lineView.VoucherId);
                }
            }

            return Mapper.Map<VoucherLineViewModel>(line);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task DeleteArticleAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var article = await repository.GetByIDAsync(articleId);
            if (article != null)
            {
                await DeleteAsync(repository, article, OperationId.DeleteLine);
                await UpdateRowNumbersAsync(article);
                await UpdateVoucherBalanceStatusAsync(article.VoucherId);
            }
        }

        /// <inheritdoc/>
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
                await UpdateRowNumbersAsync(voucherId);
            }

            await OnEntityGroupDeleted(items, OperationId.GroupDeleteLines);
        }

        /// <inheritdoc/>
        public async Task<int> GetAllArticlesCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = repository.GetEntityQuery();
            return await query.CountAsync();
        }

        /// <inheritdoc/>
        public async Task<int> GetLineSubjectTypeAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            return await repository
                .GetEntityQuery(line => line.Voucher)
                .Where(line => line.Id == articleId)
                .Select(line => line.Voucher.SubjectType)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<PayReceiveViewModel> GetRelatedPayReceiveAsync(int articleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveVoucherLine>();
            return await repository
                .GetEntityQuery(item => item.PayReceive)
                .Where(item => item.VoucherLineId == articleId)
                .Select(item => Mapper.Map<PayReceiveViewModel>(item.PayReceive))
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsCashOrBankAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var cashBankCollections = new int[] { (int)AccountCollectionId.Bank, (int)AccountCollectionId.CashFund };
            var userBranchId = UserContext.BranchId;
            var userFiscalPeriodId = UserContext.FiscalPeriodId;
            var isAccountInCollection = await repository.GetEntityQuery()
                .AnyAsync(aca =>
                    cashBankCollections.Contains(aca.CollectionId) &&
                    aca.AccountId == accountId &&
                    aca.BranchId == userBranchId &&
                    aca.FiscalPeriodId == userFiscalPeriodId);
            return isAccountInCollection;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا منبع یا مصرف انتخاب شده با ماهیت آرتیکل منطبق هست یا خیر
        /// برای مبالغ بدهکار باید منبع انتخاب شود و برای مبالغ بستانکار باید مصرف انتخاب شود.
        /// </summary>
        /// <param name="sourceAppId">شناسه دیتابیسی منبع یا مصرف مورد نظر</param>
        /// <param name="debit">مبلغ بدهکار برای این آرتیکل مالی</param>
        /// <param name="credit">مبلغ بستانکار برای این آرتیکل مالی</param>
        /// <returns>مقدار بولی درست در صورت منطبق بودن ماهیت آرتیکل با منبع یا مصرف انتخاب شده، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsValidSourceAppInArticleAsync(int sourceAppId, decimal debit, decimal credit)
        {
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            var sourceApp = await repository.GetByIDAsync(sourceAppId);
            if (sourceApp == null)
            {
                return false;
            }

            if ((sourceApp.Type == (short)SourceAppType.Source && debit != 0)
                || (sourceApp.Type == (short)SourceAppType.Application && credit != 0))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        protected override async Task FinalizeActionAsync(VoucherLine entity)
        {
            var voucherRepository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await voucherRepository.GetByIDAsync(entity.VoucherId);
            if (voucher != null)
            {
                await UnitOfWork.CommitAsync();
                Log.EntityId = entity.Id;
                CopyEntityDataToLog(voucher);
                await TrySaveLogAsync();
            }
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Voucher; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(VoucherLineViewModel lineView, VoucherLine line)
        {
            line.AccountId = lineView.FullAccount.Account.Id;
            line.DetailAccountId = lineView.FullAccount.DetailAccount.Id > 0 ? lineView.FullAccount.DetailAccount.Id : null;
            line.CostCenterId = lineView.FullAccount.CostCenter.Id > 0 ? lineView.FullAccount.CostCenter.Id : null;
            line.ProjectId = lineView.FullAccount.Project.Id > 0 ? lineView.FullAccount.Project.Id : null;
            line.CurrencyId = lineView.CurrencyId;
            line.Debit = lineView.Debit;
            line.Credit = lineView.Credit;
            line.Description = lineView.Description;
            line.CurrencyValue = lineView.CurrencyValue;
            line.SourceAppId = lineView.SourceAppId;
        }

        /// <inheritdoc/>
        protected override string GetState(VoucherLine entity)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            var account = repository.GetByID(entity.AccountId);
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Account, account.FullCode, AppStrings.Debit, entity.Debit,
                    AppStrings.Credit, entity.Credit, AppStrings.Description, entity.Description)
                : null;
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
            var linesQuery = Repository
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine,
                    line => line.Voucher, line => line.Account, line => line.DetailAccount, line => line.CostCenter,
                    line => line.Project, line => line.Currency, line => line.Branch, line => line.SourceApp)
                .Where(line => line.Voucher.Id == voucherId)
                .OrderBy(line => line.RowNo);
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

        private async Task<int> GetNextRowNoAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            int lastRowNo = await repository
                .GetEntityQuery()
                .Where(line => line.VoucherId == voucherId)
                .OrderByDescending(line => line.RowNo)
                .Select(line => line.RowNo)
                .FirstOrDefaultAsync();
            return lastRowNo + 1;
        }

        private async Task UpdateRowNumbersAsync(VoucherLine line)
        {
            Verify.ArgumentNotNull(line, nameof(line));
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var nextLines = await repository
                .GetEntityQuery()
                .Where(vl => vl.VoucherId == line.VoucherId &&
                    vl.RowNo > line.RowNo)
                .OrderBy(vl => vl.RowNo)
                .ToListAsync();
            foreach (var nextLine in nextLines)
            {
                nextLine.RowNo = nextLine.RowNo - 1;
                repository.Update(nextLine);
            }

            await UnitOfWork.CommitAsync();
        }

        private async Task UpdateRowNumbersAsync(int voucherId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetEntityQuery()
                .Where(vl => vl.VoucherId == voucherId)
                .OrderBy(vl => vl.RowNo)
                .ToListAsync();
            int rowNo = 1;
            foreach (var line in lines)
            {
                line.RowNo = rowNo++;
                repository.Update(line);
            }

            await UnitOfWork.CommitAsync();
        }

        private readonly ISystemRepository _system;
    }
}
