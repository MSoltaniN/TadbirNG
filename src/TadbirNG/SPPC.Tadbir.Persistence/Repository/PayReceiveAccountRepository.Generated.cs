using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
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
        /// به روش آسنکرون، اطلاعات یک طرف حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountArticle">طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی طرف حساب ایجاد یا اصلاح شده</returns>
        public async Task<PayReceiveAccountViewModel> SaveAccountArticleAsync(PayReceiveAccountViewModel accountArticle)
        {
            Verify.ArgumentNotNull(accountArticle, nameof(accountArticle));
            PayReceiveAccount accountArticleModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            if (accountArticle.Id == 0)
            {
                accountArticleModel = Mapper.Map<PayReceiveAccount>(accountArticle);
                await InsertAsync(repository, accountArticleModel);
            }
            else
            {
                accountArticleModel = await repository.GetByIDAsync(accountArticle.Id);
                if (accountArticleModel != null)
                {
                    await UpdateAsync(repository, accountArticleModel, accountArticle);
                }
            }

            return Mapper.Map<PayReceiveAccountViewModel>(accountArticleModel);
        }

        /// <summary>
        /// به روش آسنکرون، طرف حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی طرف حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountArticleAsync(int accountArticleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var accountArticle = await repository.GetByIDAsync(accountArticleId);
            if (accountArticle != null)
            {
                await DeleteAsync(repository, accountArticle);
            }
        }

        /// <summary>
        /// به روش آسنکرون، طرف‌های حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleIds">مجموعه ای از شناسه های عددی طرف‌های حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountArticlesAsync(IList<int> accountArticleIds)
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

            await OnEntityGroupDeleted(accountArticleIds);
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
            if(payReceive != null)
            {
                item = Mapper.Map<PayReceiveViewModel>(payReceive);
            }

            return item;
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
            return String.Empty;
        }

        private static int? GetNullableId(AccountItemBriefViewModel item)
        {
            return (item != null && item.Id > 0)
                ? item.Id
                : null;
        }
    }
}
