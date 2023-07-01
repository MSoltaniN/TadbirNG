using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
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
    public class PayReceiveCashAccountRepository : EntityLoggingRepository<PayReceiveCashAccount, PayReceiveCashAccountViewModel>, IPayReceiveCashAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public PayReceiveCashAccountRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
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
                        ca => ca.Project)
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
        public async Task<PayReceiveCashAccountSummaryViewModel> GetCashAccountArticleSummaryAsync(int cashAccountArticleId)
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
                    aa => cashAccountArticleIds.Any(id => id == aa.Id)))
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
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId &&
                    (article.Amount <= Decimal.Zero || article.AccountId == null))
                .ToArrayAsync();

            foreach (var article in articles)
            {
                DisconnectEntity(article);
                repository.Delete(article);
            }

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
            return await repository
                .GetEntityQuery()
                .AnyAsync(article => article.PayReceiveId == payReceiveId &&
                    (article.Amount <= Decimal.Zero || article.AccountId == null));
        }

        /// <inheritdoc/>
        public async Task AggregateCashAccountArticleRowsAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var aggregatedRows = repository
                .GetEntityQuery()
                .Where(a => a.PayReceiveId == payReceiveId && a.AccountId.HasValue)
                .AsEnumerable()
                .GroupBy(acc => new { acc.AccountId, acc.DetailAccountId, acc.CostCenterId, acc.ProjectId })
                .Where(group => group.Count() > 1)
                .Select(group => new
                {
                    Id = group.Min(article => article.Id),
                    Amount = group.Sum(article => article.Amount),
                    Descriptions = group
                        .Select(article => article.Description?.Trim())
                        .Where(d => !String.IsNullOrEmpty(d))
                        .ToArray()
                })
                .ToArray();

            foreach (var item in aggregatedRows)
            {
                var aggregatedArticle = await repository.GetByIDAsync(item.Id);
                aggregatedArticle.Amount = item.Amount;
                aggregatedArticle.Description = item.Descriptions.Length > 0
                    ? String.Join(" - ", item.Descriptions)
                    : null;
                repository.Update(aggregatedArticle);

                var removedArticles = await repository.GetByCriteriaAsync(
                    a => a.PayReceiveId == payReceiveId
                        && a.Id != aggregatedArticle.Id
                        && a.AccountId == aggregatedArticle.AccountId
                        && a.DetailAccountId == aggregatedArticle.DetailAccountId
                        && a.CostCenterId == aggregatedArticle.CostCenterId
                        && a.ProjectId == aggregatedArticle.ProjectId);
                foreach (var article in removedArticles)
                {
                    DisconnectEntity(article);
                    repository.Delete(article);
                }
            }

            await UnitOfWork.CommitAsync();
            int entityTypeId = GetEntityTypeId(type);
            OnEntityAction(OperationId.AggregateCashAccountLines, entityTypeId);
            await TrySaveLogAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> HasCashAccountArticlestoAggregateAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveCashAccount>();
            var aggregateCount = await repository
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId && article.AccountId != null)
                .GroupBy(acc => new { acc.AccountId, acc.DetailAccountId, acc.CostCenterId, acc.ProjectId })
                .Where(articles => articles.Count() > 1)
                .CountAsync();

            return aggregateCount > 0;
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receipt; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(PayReceiveCashAccountViewModel cashAccountArticleView, PayReceiveCashAccount cashAccountArticle)
        {
            cashAccountArticle.AccountId = GetNullableId(cashAccountArticleView.FullAccount.Account);
            cashAccountArticle.DetailAccountId = GetNullableId(cashAccountArticleView.FullAccount.DetailAccount);
            cashAccountArticle.CostCenterId = GetNullableId(cashAccountArticleView.FullAccount.CostCenter);
            cashAccountArticle.ProjectId = GetNullableId(cashAccountArticleView.FullAccount.Project);
            cashAccountArticle.SourceAppId = cashAccountArticleView.SourceAppId;
            cashAccountArticle.Amount = cashAccountArticleView.Amount;
            cashAccountArticle.IsBank = cashAccountArticleView.IsBank;
            cashAccountArticle.BankOrderNo = cashAccountArticleView.BankOrderNo;
            cashAccountArticle.Description = cashAccountArticleView.Description;
        }

        /// <inheritdoc/>
        protected override string GetState(PayReceiveCashAccount entity)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            string accountFullCode = String.Empty;
            if (entity.AccountId.HasValue)
            {
                int accountId = entity.AccountId ?? 0;
                var account = repository.GetByID(accountId);
                if (account != null)
                {
                    accountFullCode = $"{AppStrings.Account} : {account.FullCode}, ";
                }
            }

            return entity != null
                ? accountFullCode +
                    $"{AppStrings.Amount} : {entity.Amount}, {AppStrings.Description} : {entity.Description}"
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
    }
}
