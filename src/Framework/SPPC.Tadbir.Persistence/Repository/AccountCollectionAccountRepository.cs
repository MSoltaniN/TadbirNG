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
                .Select(acc => Mapper.Map<AccountIdentityViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();

            var accCollection = await _repository
                .GetAllOperationQuery<AccountCollectionAccount>(ViewName.AccountCollectionAccount, col => col.Account, col => col.Account.Children)
                .Where(col => col.CollectionId == collectionId)
                .Select(col => Mapper.Map<AccountIdentityViewModel>(col))
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
            return await _repository.GetCountAsync<Account, AccountCollectionItemsViewModel>(
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
        /// <returns></returns>
        public async Task AddAccountCollectionAccountAsync(IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var existing = await repository.GetByCriteriaAsync(f => f.CollectionId == accCollectionsList.FirstOrDefault().CollectionId, f => f.Branch);
            if (AreAccountCollectionModified(existing, accCollectionsList))
            {
                if (existing.Count > 0)
                {
                    //RemoveInaccessibleBranches(repository, existing, roleBranches);
                }

                AddNewAccountCollections(repository, existing, accCollectionsList);
                await UnitOfWork.CommitAsync();
            }

            UnitOfWork.UseSystemContext();
        }

        private static bool AreAccountCollectionModified(IList<AccountCollectionAccount> existing, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var existingItems = existing
                .Select(rb => rb.BranchId)
                .ToArray();
            var selectedItems = accCollectionsList
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, selectedItems));
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private void AddNewAccountCollections(
            IRepository<AccountCollectionAccount> repository, IList<AccountCollectionAccount> existing, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            //var branchRepository = UnitOfWork.GetRepository<Branch>();
            //var accountRepository = UnitOfWork.GetRepository<Account>();
            //var fpRepository = UnitOfWork.GetRepository<FiscalPeriod>();

            var currentItems = existing.Select(rb => rb.CollectionId);
            var newItems = accCollectionsList
                .Where(item => !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var test = Mapper.Map<AccountCollectionAccount>(item);
                //test.Branch = branchRepository.GetByID(test.BranchId);
                //test.Account = accountRepository.GetByID(test.AccountId);
                //test.FiscalPeriod = fpRepository.GetByID(test.FiscalPeriodId);
                repository.Insert(test);
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
