using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public class AccountCollectionAccountRepository : LoggingRepository<AccountCollectionAccount, AccountCollectionAccountViewModel>, IAccountCollectionAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public AccountCollectionAccountRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log, ISecureRepository repository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب و حساب های قابل انتخاب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های یک سطح و حساب های انتخاب شده در یک مجموعه حساب</returns>
        public async Task<AccountCollectionItemsViewModel> GetAccountCollectionAccountAsync(int collectionId, GridOptions gridOptions = null)
        {
            var accounts = await _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();

            var accCollection = await _repository
                .GetAllOperationQuery<AccountCollectionAccount>(ViewName.AccountCollectionAccount, col => col.Account, col => col.Account.Children)
                .Where(col => col.CollectionId == collectionId && col.BranchId == _currentContext.BranchId && col.FiscalPeriodId == _currentContext.FiscalPeriodId)
                .Select(col => Mapper.Map<AccountViewModel>(col))
                .ToListAsync();

            return new AccountCollectionItemsViewModel()
            {
                AllAccounts = accounts,
                SelectedAccounts = accCollection
            };
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// <para>توجه : فراخوانی این متد با اطلاعات محیطی معتبر برای موفقیت سایر عملیات این کلاس الزامی است</para>
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetCountAsync<Account, AccountViewModel>(
                ViewName.Account, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای مجموعه حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای مجموعه حساب</returns>
        public async Task<ViewViewModel> GetAccountCollectionMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<AccountCollectionAccount>();
        }

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه یا کم میکند
        /// </summary>
        /// <param name="accCollectionsList">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
        public async Task AddAccountCollectionAccountAsync(int collectionId, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var existing = await repository.GetByCriteriaAsync(
                f => f.CollectionId == collectionId &&
                f.FiscalPeriodId == _currentContext.FiscalPeriodId);

            if (existing.Count > 0)
            {
                await RemoveInaccessibleAccountCollections(repository, existing, accCollectionsList);
            }

            await AddNewAccountCollections(repository, existing, accCollectionsList);
            await UnitOfWork.CommitAsync();
        }

        private async Task AddNewAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository, IList<AccountCollectionAccount> existing, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();

            var branchID = _currentContext.BranchId;
            var accountItems = existing
                .Where(item => item.BranchId == branchID)
                .Select(item => item.AccountId);
            var newItems = accCollectionsList
                .Where(item => !accountItems.Contains(item.AccountId));
            foreach (var item in newItems)
            {
                var accountCollectionAccount = Mapper.Map<AccountCollectionAccount>(item);
                repository.Insert(accountCollectionAccount);
                await CascadeNewAccountCollection(repository, branchRepository, item);
            }
        }

        private async Task CascadeNewAccountCollection(IAsyncRepository<AccountCollectionAccount> repository,
            IAsyncRepository<Branch> branchRepository,
            AccountCollectionAccountViewModel accCollection)
        {
            var branches = await branchRepository.GetByCriteriaAsync(br => br.ParentId == accCollection.BranchId);
            foreach (var item in branches)
            {
                var accountCollectionAccount = Mapper.Map<AccountCollectionAccount>(accCollection);
                accountCollectionAccount.Id = 0;
                accountCollectionAccount.BranchId = item.Id;
                repository.Insert(accountCollectionAccount);
                accCollection.BranchId = item.Id;
                await CascadeNewAccountCollection(repository, branchRepository, accCollection);
            }
        }

        private async Task RemoveInaccessibleAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository,
            IList<AccountCollectionAccount> existing,
            IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();

            var accountItems = accCollectionsList
                .Select(item => item.AccountId);
            var branchID = _currentContext.BranchId;
            var removedItems = existing
                .Where(item => item.BranchId == branchID && !accountItems.Contains(item.AccountId))
                .ToArray();
            foreach (var item in removedItems)
            {
                await CascadeNewAccountCollection(repository, branchRepository, existing, item);
                repository.Delete(item);
            }
        }

        private async Task CascadeNewAccountCollection(
            IAsyncRepository<AccountCollectionAccount> repository,
            IAsyncRepository<Branch> branchRepository,
            IList<AccountCollectionAccount> existing,
            AccountCollectionAccount removedItem)
        {
            var branchChildes = await branchRepository.GetByCriteriaAsync(br => br.ParentId == removedItem.BranchId);
            foreach (var child in branchChildes)
            {
                var removed_Item = existing.Where(col => col.BranchId == child.Id && col.CollectionId == removedItem.CollectionId && col.AccountId == removedItem.AccountId).Single();
                await CascadeNewAccountCollection(repository, branchRepository, existing, removed_Item);
                repository.Delete(removed_Item);
            }
        }

        protected override string GetState(AccountCollectionAccount entity)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateExisting(AccountCollectionAccountViewModel entityView, AccountCollectionAccount entity)
        {
            throw new NotImplementedException();
        }

        private readonly ISecureRepository _repository;
    }
}
