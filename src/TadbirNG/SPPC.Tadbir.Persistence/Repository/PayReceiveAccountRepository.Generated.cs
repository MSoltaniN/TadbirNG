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
            var payReceiveAccounts = new List<PayReceiveAccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
                payReceiveAccounts = await repository
                    .GetEntityQuery(item => item.Account, 
                    item => item.CostCenter,
                    item => item.DetailAccount,
                    item => item.Project)
                    .Where(item => item.PayReceiveId == payReceiveId)
                    .Select(item => Mapper.Map<PayReceiveAccountViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<PayReceiveAccountViewModel>(payReceiveAccounts, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، طرف حساب با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveAccountId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        public async Task<PayReceiveAccountViewModel> GetPayReceiveAccountAsync(int payReceiveAccountId)
        {
            PayReceiveAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var payReceiveAccount = await repository.GetByIDAsync(payReceiveAccountId);
            if (payReceiveAccount != null)
            {
                item = Mapper.Map<PayReceiveAccountViewModel>(payReceiveAccount);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک طرف حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payReceiveAccount">طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی طرف حساب ایجاد یا اصلاح شده</returns>
        public async Task<PayReceiveAccountViewModel> SavePayReceiveAccountAsync(PayReceiveAccountViewModel payReceiveAccount)
        {
            Verify.ArgumentNotNull(payReceiveAccount, nameof(payReceiveAccount));
            PayReceiveAccount payReceiveAccountModel;
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            if (payReceiveAccount.Id == 0)
            {
                payReceiveAccountModel = Mapper.Map<PayReceiveAccount>(payReceiveAccount);
                await InsertAsync(repository, payReceiveAccountModel);
            }
            else
            {
                payReceiveAccountModel = await repository.GetByIDAsync(payReceiveAccount.Id);
                if (payReceiveAccountModel != null)
                {
                    await UpdateAsync(repository, payReceiveAccountModel, payReceiveAccount);
                }
            }

            return Mapper.Map<PayReceiveAccountViewModel>(payReceiveAccountModel);
        }

        /// <summary>
        /// به روش آسنکرون، طرف حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveAccountId">شناسه عددی طرف حساب مورد نظر برای حذف</param>
        public async Task DeletePayReceiveAccountAsync(int payReceiveAccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            var payReceiveAccount = await repository.GetByIDAsync(payReceiveAccountId);
            if (payReceiveAccount != null)
            {
                await DeleteAsync(repository, payReceiveAccount);
            }
        }

        /// <summary>
        /// به روش آسنکرون، طرف‌های حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveAccountIds">مجموعه ای از شناسه های عددی طرف‌های حساب مورد نظر برای حذف</param>
        public async Task DeletePayReceiveAccountsAsync(IList<int> payReceiveAccountIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<PayReceiveAccount>();
            foreach (int payReceiveAccountId in payReceiveAccountIds)
            {
                var payReceiveAccount = await repository.GetByIDAsync(payReceiveAccountId);
                if (payReceiveAccount != null)
                {
                    await DeleteNoLogAsync(repository, payReceiveAccount);
                }
            }

            await OnEntityGroupDeleted(payReceiveAccountIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.Receipt; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="payReceiveAccountViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="payReceiveAccount">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(PayReceiveAccountViewModel payReceiveAccountViewModel, PayReceiveAccount payReceiveAccount)
        {
            payReceiveAccount.Amount = payReceiveAccountViewModel.Amount;
            payReceiveAccount.Description = payReceiveAccountViewModel.Description;
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

        private int GetEntityTypeId(int type)
        {
            return (int)(type == (int)PayReceiveType.Receipt
                ? EntityTypeId.Receipt
                : EntityTypeId.Payment);
        }
    }
}
