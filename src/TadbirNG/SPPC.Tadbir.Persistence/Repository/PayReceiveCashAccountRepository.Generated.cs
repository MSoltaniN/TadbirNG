using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت حساب‌های نقدی را پیاده سازی می کند
    /// </summary>
    public class PayReceiveCashAccountRepository
        : EntityLoggingRepository<PayReceiveCashAccount, PayReceiveCashAccountViewModel>, IPayReceiveCashAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public PayReceiveCashAccountRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<PayReceiveCashAccountViewModel>> GetCashAccountArticlesAsync(
            int payReceiveId, int type, GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var cashAccountArticles = new List<PayReceiveCashAccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.PrintCashAccountLines)
            {
                var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
                cashAccountArticles = await repository
                    .GetEntityQuery(ca => ca.Account,
                        ca => ca.CostCenter,
                        ca => ca.DetailAccount,
                        ca => ca.Project,
                        ca => ca.SourceApp)
                    .Where(account => account.PayReceiveId == payReceiveId)
                    .Select(account => Mapper.Map<PayReceiveCashAccountViewModel>(account))
                    .ToListAsync();
            }

            int entityTypeId = GetEntityTypeId(type);
            await ReadAsync(gridOptions, null, entityTypeId);
            return new PagedList<PayReceiveCashAccountViewModel>(cashAccountArticles, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<PayReceiveCashAccountViewModel> GetCashAccountArticleAsync(int cashAccountArticleId)
        {
            PayReceiveCashAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var cashAccountArticle = await repository.GetByIDAsync(cashAccountArticleId,
                account => account.PayReceive,
                account => account.Account,
                account => account.Project,
                account => account.DetailAccount,
                account => account.CostCenter);
            if (cashAccountArticle != null)
            {
                item = Mapper.Map<PayReceiveCashAccountViewModel>(cashAccountArticle);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<PayReceiveCashAccountSummaryViewModel> GetCashAccountArticleSummaryAsync(
            int cashAccountArticleId)
        {
            PayReceiveCashAccountSummaryViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var cashAccountArticle = await repository.GetByIDAsync(cashAccountArticleId);
            if (cashAccountArticle != null)
            {
                item = Mapper.Map<PayReceiveCashAccountSummaryViewModel>(cashAccountArticle);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<PayReceiveCashAccountViewModel> SaveCashAccountArticleAsync(
            PayReceiveCashAccountViewModel cashAccountArticle, int type)
        {
            Verify.ArgumentNotNull(cashAccountArticle, nameof(cashAccountArticle));
            PayReceiveCashAccount cashAccountArticleModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            int? entityTypeId = GetEntityTypeId(type);
            if (cashAccountArticle.Id == 0)
            {
                cashAccountArticle.SourceAppId = cashAccountArticle.SourceAppId == 0
                    ? null
                    : cashAccountArticle.SourceAppId;
                cashAccountArticle.BankOrderNo = cashAccountArticle.IsBank
                    ? cashAccountArticle.BankOrderNo.Trim()
                    : null;
                cashAccountArticleModel = Mapper.Map<PayReceiveCashAccount>(cashAccountArticle);

                await InsertAsync(repository, cashAccountArticleModel, OperationId.CreateCashAccountLine, entityTypeId);
            }
            else
            {
                cashAccountArticleModel = await repository.GetByIDAsync(cashAccountArticle.Id);
                if (cashAccountArticleModel != null)
                {
                    await UpdateAsync(repository, cashAccountArticleModel,
                        cashAccountArticle, OperationId.EditCashAccountLine, entityTypeId);
                }
            }

            return Mapper.Map<PayReceiveCashAccountViewModel>(cashAccountArticleModel);
        }

        /// <inheritdoc/>
        public async Task DeleteCashAccountArticleAsync(int cashAccountArticleId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var cashAccountArticle = await repository.GetByIDAsync(cashAccountArticleId);
            if (cashAccountArticle != null)
            {
                int? entityTypeId = GetEntityTypeId(type);
                await DeleteAsync(
                    repository, cashAccountArticle, OperationId.DeleteCashAccountLine, entityTypeId);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteCashAccountArticlesAsync(IList<int> cashAccountArticleIds, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            foreach (int cashAccountArticleId in cashAccountArticleIds)
            {
                var cashAccountArticle = await repository.GetByIDAsync(cashAccountArticleId);
                if (cashAccountArticle != null)
                {
                    await DeleteNoLogAsync(repository, cashAccountArticle);
                }
            }

            int entityTypeId = GetEntityTypeId(type);
            await OnEntityGroupDeleted(cashAccountArticleIds, OperationId.GroupDeleteCashAccountLines, entityTypeId);
        }

        /// <inheritdoc/>
        public async Task<PayReceiveViewModel> GetPayReceiveAsync(IList<int> cashAccountArticleIds)
        {
            PayReceiveViewModel item = null;
            Verify.ArgumentNotNull(cashAccountArticleIds, nameof(cashAccountArticleIds));
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository
                .GetEntityQuery()
                .Where(pr => pr.CashAccounts.Any(
                    ca => cashAccountArticleIds.Any(id => id == ca.Id)))
                .SingleOrDefaultAsync();
            if (payReceive != null)
            {
                item = Mapper.Map<PayReceiveViewModel>(payReceive);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task DeleteInvalidRowsCashAccountArticleAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var articles = await repository
                .GetEntityQuery(a => a.Account, a => a.DetailAccount, a => a.Project, a => a.CostCenter)
                .Where(article => article.PayReceiveId == payReceiveId)
                .ToArrayAsync();
            var invalidItems = articles.Where(
                article => article.Amount <= Decimal.Zero
                || article.AccountId == null)
                .ToList();

            var fullAccountArticles = articles.Where(article =>
                article.Amount > Decimal.Zero
                || article.AccountId != null);
            foreach (var article in fullAccountArticles)
            {
                var articleView = Mapper.Map<PayReceiveCashAccountViewModel>(article);
                if (!await IsValidFullAccountAsync(articleView.FullAccount, Repository))
                {
                    invalidItems.Add(article);
                }
            }

            DeleteArticlesGroup(repository, invalidItems);
            await UnitOfWork.CommitAsync();
            int entityTypeId = GetEntityTypeId(type);
            var articleIds = articles
                .Select(article => article.Id)
                .ToArray();
            await OnEntityGroupDeleted(articleIds, OperationId.RemoveInvalidCashAccountLines, entityTypeId);
        }

        /// <inheritdoc/>
        public async Task<bool> HasCashAccountArticleInvalidRowsAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var articles = await repository
               .GetEntityQuery(a => a.Account, a => a.DetailAccount, a => a.Project, a => a.CostCenter)
               .Where(article => article.PayReceiveId == payReceiveId)
               .ToArrayAsync();
            var hasInvalidItems = articles.Any(
                article => article.Amount <= Decimal.Zero
                || article.AccountId == null);
            if (hasInvalidItems)
            {
                return true;
            }
            var fullAccountArticles = articles.Where(article =>
                article.Amount > Decimal.Zero
                || article.AccountId != null);

            foreach (var article in fullAccountArticles)
            {
                var articleView = Mapper.Map<PayReceiveCashAccountViewModel>(article);
                if (!await IsValidFullAccountAsync(articleView.FullAccount, Repository))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task AggregateCashAccountArticleRowsAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var aggregatedRows = repository
                .GetEntityQuery()
                .Where(a => a.PayReceiveId == payReceiveId && a.AccountId.HasValue)
                .AsEnumerable()
                .GroupBy(acc => new
                    {
                        acc.AccountId,
                        acc.DetailAccountId,
                        acc.CostCenterId,
                        acc.ProjectId,
                        acc.SourceAppId,
                        acc.BankOrderNo
                    })
                .Where(group => group.Count() > 1)
                .Select(group => new
                {
                    Id = group.Min(article => article.Id),
                    Amount = group.Sum(article => article.Amount),
                    Remarks = group
                        .Select(article => article.Remarks?.Trim())
                        .Where(d => !String.IsNullOrEmpty(d))
                        .ToArray()
                })
                .ToArray();

            foreach (var item in aggregatedRows)
            {
                var aggregatedArticle = await repository.GetByIDAsync(item.Id);
                aggregatedArticle.Amount = item.Amount;
                aggregatedArticle.Remarks = item.Remarks.Length > 0
                    ? String.Join(" - ", item.Remarks)
                    : null;
                repository.Update(aggregatedArticle);
                var removedArticles = await GetRemovedAggregetedArticlesAsync(repository, aggregatedArticle);
                DeleteArticlesGroup(repository, removedArticles);
            }

            int entityTypeId = GetEntityTypeId(type);
            await FinalizeActionAsync(OperationId.AggregateCashAccountLines, entityTypeId);
        }

        /// <inheritdoc/>
        public async Task<bool> HasCashAccountArticlesToAggregateAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var aggregateCount = await repository
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId && article.AccountId != null)
                .GroupBy(acc => new
                    {
                        acc.AccountId,
                        acc.DetailAccountId,
                        acc.CostCenterId,
                        acc.ProjectId,
                        acc.SourceAppId,
                        acc.BankOrderNo
                    })
                .Where(articles => articles.Count() > 1)
                .CountAsync();

            return aggregateCount > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> IsBankCashAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            return await repository
                .GetEntityQuery()
                .AnyAsync(aca => aca.AccountId == accountId
                    && aca.CollectionId == (int)AccountCollectionId.Bank);
        }

        /// <inheritdoc/>
        public async Task<bool> IsCashierCashAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            return await repository
                .GetEntityQuery()
                .AnyAsync(aca => aca.AccountId == accountId
                    && aca.CollectionId == (int)AccountCollectionId.CashFund);
        }

        /// <inheritdoc/>
        public async Task<bool> IsSourceCashAccountAsync(int sourceAppId)
        {
            return await Repository
                .GetAllQuery<SourceApp>(ViewId.SourceApp)
                .AnyAsync(sa => sa.Id == sourceAppId
                    && sa.Type == (int)SourceAppType.Source);
        }

        /// <inheritdoc/>
        public async Task<bool> IsAppCashAccountAsync(int sourceAppId)
        {
            return await Repository
                .GetAllQuery<SourceApp>(ViewId.SourceApp)
                .AnyAsync(sa => sa.Id == sourceAppId
                    && sa.Type == (int)SourceAppType.Application);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receipt; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(
            PayReceiveCashAccountViewModel cashAccountArticleView, PayReceiveCashAccount cashAccountArticle)
        {
            cashAccountArticle.AccountId = GetNullableId(cashAccountArticleView.FullAccount.Account);
            cashAccountArticle.DetailAccountId = GetNullableId(cashAccountArticleView.FullAccount.DetailAccount);
            cashAccountArticle.CostCenterId = GetNullableId(cashAccountArticleView.FullAccount.CostCenter);
            cashAccountArticle.ProjectId = GetNullableId(cashAccountArticleView.FullAccount.Project);
            cashAccountArticle.Amount = cashAccountArticleView.Amount;
            cashAccountArticle.IsBank = cashAccountArticleView.IsBank;
            cashAccountArticle.Remarks = cashAccountArticleView.Remarks;
            cashAccountArticle.SourceAppId = cashAccountArticleView.SourceAppId == 0
                ? null
                : cashAccountArticleView.SourceAppId;
            cashAccountArticle.BankOrderNo = cashAccountArticleView.IsBank
                ? cashAccountArticleView.BankOrderNo.Trim()
                : null;
        }

        /// <inheritdoc/>
        protected override string GetState(PayReceiveCashAccount entity)
        {
            string accountFullCode = String.Empty;
            if (entity.AccountId.HasValue)
            {
                var repository = UnitOfWork.GetRepository<Account>();
                var account = repository.GetByID((int)entity.AccountId);
                accountFullCode = $"{AppStrings.Account} : {account.FullCode}, ";
            }

            string sourceAppName = String.Empty;
            if (entity.SourceAppId.HasValue)
            {
                var repository = UnitOfWork.GetRepository<SourceApp>();
                var sourceApp = repository.GetByID((int)entity.SourceAppId);
                if (sourceApp.Type == (int)SourceAppType.Source)
                {
                    sourceAppName = $"{AppStrings.Source} : {sourceApp.Name}, ";
                }
                else
                {
                    sourceAppName = $"{AppStrings.Application} : {sourceApp.Name}, ";
                }
            }

            return entity != null
                ? accountFullCode + sourceAppName +
                    $"{AppStrings.Amount} : {entity.Amount}, {AppStrings.Remarks} : {entity.Remarks}"
                : String.Empty;
        }

        private static int? GetNullableId(AccountItemBriefViewModel item)
        {
            return (item != null && item.Id > 0)
                ? item.Id
                : null;
        }

        private int GetEntityTypeId(int type)
        {
            return (int)(type == (int)PayReceiveType.Receipt
                ? EntityTypeId.Receipt
                : EntityTypeId.Payment);
        }

        private async Task<IList<PayReceiveCashAccount>> GetRemovedAggregetedArticlesAsync(
            IAsyncRepository<PayReceiveCashAccount> repository, PayReceiveCashAccount aggregatedArticle)
        {
            var removedArticles = await repository.GetByCriteriaAsync(
                a => a.PayReceiveId == aggregatedArticle.PayReceiveId
                && a.Id != aggregatedArticle.Id
                && a.AccountId == aggregatedArticle.AccountId
                && a.DetailAccountId == aggregatedArticle.DetailAccountId
                && a.CostCenterId == aggregatedArticle.CostCenterId
                && a.ProjectId == aggregatedArticle.ProjectId
                && a.SourceAppId == aggregatedArticle.SourceAppId
                && a.BankOrderNo == aggregatedArticle.BankOrderNo);
            return removedArticles;
        }

        private void DeleteArticlesGroup(
            IRepository<PayReceiveCashAccount> repository, IList<PayReceiveCashAccount> removedArticles)
        {
            foreach (var article in removedArticles)
            {
                DisconnectEntity(article);
                repository.Delete(article);
            }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
