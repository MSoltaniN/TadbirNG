using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت طرف‌های حساب را پیاده سازی می کند
    /// </summary>
    public class PayReceiveAccountRepository : EntityLoggingRepository<PayReceiveAccount, PayReceiveAccountViewModel>, IPayReceiveAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public PayReceiveAccountRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه طرف‌های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="payReceiveId">شناسه یکی از فرم های دریافت/پرداخت موجود</param> 
        /// <returns>مجموعه ای از طرف‌های حساب تعریف شده</returns>
        public async Task<PagedList<PayReceiveAccountViewModel>> GetAccountArticlesAsync(
            int payReceiveId, GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var accountArticles = new List<PayReceiveAccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
                accountArticles = await repository
                    .GetEntityQuery(account => account.Account,
                    account => account.CostCenter,
                    account => account.DetailAccount,
                    account => account.Project)
                    .Where(account => account.PayReceiveId == payReceiveId)
                    .Select(account => Mapper.Map<PayReceiveAccountViewModel>(account))
                    .ToListAsync();
            }

            return new PagedList<PayReceiveAccountViewModel>(accountArticles, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، طرف حساب با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveAccountViewModel> GetAccountArticleAsync(int accountArticleId)
        {
            PayReceiveAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var accountArticle = await repository.GetByIDAsync(accountArticleId,
                account => account.PayReceive,
                account => account.Account,
                account => account.Project,
                account => account.DetailAccount,
                account => account.CostCenter);
            if (accountArticle != null)
            {
                item = Mapper.Map<PayReceiveAccountViewModel>(accountArticle);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاطه طرف حساب با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveAccountSummaryViewModel> GetAccountArticleSummaryAsync(int accountArticleId)
        {
            PayReceiveAccountSummaryViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var accountArticle = await repository.GetByIDAsync(accountArticleId);
            if (accountArticle != null)
            {
                item = Mapper.Map<PayReceiveAccountSummaryViewModel>(accountArticle);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک طرف حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountArticle">طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        /// <returns>اطلاعات نمایشی طرف حساب ایجاد یا اصلاح شده</returns>
        public async Task<PayReceiveAccountViewModel> SaveAccountArticleAsync(
            PayReceiveAccountViewModel accountArticle, int type)
        {
            Verify.ArgumentNotNull(accountArticle, nameof(accountArticle));
            PayReceiveAccount accountArticleModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            int? entityTypeId = GetEntityTypeId(type);
            if (accountArticle.Id == 0)
            {
                accountArticleModel = Mapper.Map<PayReceiveAccount>(accountArticle);
                await InsertAsync(repository, accountArticleModel, OperationId.CreateAccount, entityTypeId);
            }
            else
            {
                accountArticleModel = await repository.GetByIDAsync(accountArticle.Id);
                if (accountArticleModel != null)
                {
                    await UpdateAsync(
                        repository, accountArticleModel, accountArticle, OperationId.EditAccount, entityTypeId);
                }
            }

            return Mapper.Map<PayReceiveAccountViewModel>(accountArticleModel);
        }

        /// <summary>
        /// به روش آسنکرون، طرف حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی طرف حساب مورد نظر برای حذف</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param> 
        public async Task DeleteAccountArticleAsync(int accountArticleId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var accountArticle = await repository.GetByIDAsync(accountArticleId);
            if (accountArticle != null)
            {
                int? entityTypeId = GetEntityTypeId(type);
                await DeleteAsync(
                    repository, accountArticle, OperationId.DeleteAccount, entityTypeId);
            }
        }

        /// <summary>
        /// به روش آسنکرون، طرف‌های حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleIds">مجموعه ای از شناسه های عددی طرف‌های حساب مورد نظر برای حذف</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param> 
        public async Task DeleteAccountArticlesAsync(IList<int> accountArticleIds, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            foreach (int accountArticleId in accountArticleIds)
            {
                var accountArticle = await repository.GetByIDAsync(accountArticleId);
                if (accountArticle != null)
                {
                    await DeleteNoLogAsync(repository, accountArticle);
                }
            }

            int entityTypeId = GetEntityTypeId(type);
            await OnEntityGroupDeleted(accountArticleIds, OperationId.DeleteGroupAccounts, entityTypeId);
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت مربوط به شناسه های آرتیکل حساب ورودی را برمی گرداند
        /// </summary>
        /// <param name="accountArticleIds">لیست شناسه های آرتیکل حساب</param>
        /// <returns>فرم دریافت/پرداخت</returns>
        public async Task<PayReceiveViewModel> GetPayReceiveAsync(IList<int> accountArticleIds)
        {
            PayReceiveViewModel item = null;
            Verify.ArgumentNotNull(accountArticleIds, nameof(accountArticleIds));
            var repository = UnitOfWork.GetAsyncRepository<PayReceive>();
            var payReceive = await repository
                .GetEntityQuery()
                .Where(pr => pr.Accounts.Any(acc => accountArticleIds.Any(id => id == acc.Id)))
                .SingleOrDefaultAsync();
            if (payReceive != null)
            {
                item = Mapper.Map<PayReceiveViewModel>(payReceive);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، ردیف های نامعتبر طرف حساب در فرم دریافت/پرداخت داده شده را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        public async Task DeleteInvalidRowsAccountArticleAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var articles = repository
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId &&
                    (article.Amount <= Decimal.Zero || article.AccountId == null))
                .ToArray();

            foreach (var article in articles)
            {
                DisconnectEntity(article);
                repository.Delete(article);
            }

            await UnitOfWork.CommitAsync();
            int entityTypeId = GetEntityTypeId(type);
            var articleIds = articles.Select(article => article.Id).ToArray();
            await OnEntityGroupDeleted(articleIds, OperationId.RemoveInvalidRows, entityTypeId);
        }

        /// <summary>
        /// به روش آسنکرون، وجود ردیف های نامعتبر طرف حساب در فرم دریافت/پرداخت داده شده را بررسی می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns></returns>
        public async Task<bool> HasAccountArticleInvalidRowsAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            return await repository
                .GetEntityQuery()
                .AnyAsync(article => article.PayReceiveId == payReceiveId &&
                    (article.Amount <= Decimal.Zero || article.AccountId == null));
        }

        /// <summary>
        /// به روش آسنکرون، ردیف های طرف حساب با بردار حساب مشترک را 
        /// در فرم دریافت/پرداخت داده شده تجمیع می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">مشخص می کند که درخواست جاری از نوع پرداختی یا دریافتی می باشد</param>
        public async Task AggregateAccountArticleRowsAsync(int payReceiveId, int type)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var aggregatedRows = repository
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId && article.AccountId.HasValue)
                .AsEnumerable()
                .GroupBy(acc => new { acc.AccountId, acc.DetailAccountId, acc.CostCenterId, acc.ProjectId })
                .Where(articles => articles.Count() > 1)
                .Select(articles => new PayReceiveAccount
                {
                    Id = articles.Min(article => article.Id),
                    Amount = articles.Sum(article => article.Amount),
                    AccountId = articles.Key.AccountId,
                    DetailAccountId = articles.Key.DetailAccountId,
                    CostCenterId = articles.Key.CostCenterId,
                    ProjectId = articles.Key.ProjectId,
                    Description = String.Join(" - ",
                        articles.Select(
                            article => article.Description.Trim()).Where(a => !String.IsNullOrEmpty(a)).ToList()),
                })
                .ToArray();

            foreach (var item in aggregatedRows)
            {
                var aggregatedArticle = await repository.GetByIDAsync(item.Id);
                aggregatedArticle.Amount = item.Amount;
                aggregatedArticle.Description = item.Description;
                repository.Update(aggregatedArticle);

                var removedArticles = await repository.GetByCriteriaAsync(
                    article => article.PayReceiveId == payReceiveId
                        && article.Id != aggregatedArticle.Id
                        && article.AccountId == aggregatedArticle.AccountId
                        && article.DetailAccountId == aggregatedArticle.DetailAccountId
                        && article.CostCenterId == aggregatedArticle.CostCenterId
                        && article.ProjectId == aggregatedArticle.ProjectId);
                foreach (var article in removedArticles)
                {
                    DisconnectEntity(article);
                    repository.Delete(article);
                }
            }

            await UnitOfWork.CommitAsync();
            int entityTypeId = GetEntityTypeId(type);
            OnEntityAction(OperationId.RowsAggregation, entityTypeId);
            await TrySaveLogAsync();
        }

        /// <summary>
        /// به روش آسنکرون، وجود ردیف برای تجمیع طرف حساب در فرم دریافت/پرداخت داده شده را بررسی می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns></returns>
        public async Task<bool> HasAccountArticlestoAggregateAsync(int payReceiveId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var aggregateCount = repository
                .GetEntityQuery()
                .Where(article => article.PayReceiveId == payReceiveId && article.AccountId != null)
                .GroupBy(acc => new { acc.AccountId, acc.DetailAccountId, acc.CostCenterId, acc.ProjectId })
                .Where(articles => articles.Count() > 1)
                .Count();

            return await Task.Run(() => aggregateCount > 0);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receipt; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accountArticleView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="accountArticle">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(PayReceiveAccountViewModel accountArticleView, PayReceiveAccount accountArticle)
        {
            accountArticle.AccountId = GetNullableId(accountArticleView.FullAccount.Account);
            accountArticle.DetailAccountId = GetNullableId(accountArticleView.FullAccount.DetailAccount);
            accountArticle.CostCenterId = GetNullableId(accountArticleView.FullAccount.CostCenter);
            accountArticle.ProjectId = GetNullableId(accountArticleView.FullAccount.Project);
            accountArticle.Amount = accountArticleView.Amount;
            accountArticle.Description = accountArticleView.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(PayReceiveAccount entity)
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
