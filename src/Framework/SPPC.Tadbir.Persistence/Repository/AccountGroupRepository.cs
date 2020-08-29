using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات گروه های حساب را پیاده سازی می کند.
    /// </summary>
    public class AccountGroupRepository : LoggingRepository<AccountGroup, AccountGroupViewModel>, IAccountGroupRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public AccountGroupRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه گروه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اطلاعات نمایشی گروه های حساب</returns>
        public async Task<PagedList<AccountGroupViewModel>> GetAccountGroupsAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var accGroups = await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<AccountGroupViewModel>(grp))
                .ToListAsync();
            await ReadAsync(gridOptions);
            return new PagedList<AccountGroupViewModel>(accGroups, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گروه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>اطلاعات نمایشی گروه حساب</returns>
        public async Task<AccountGroupViewModel> GetAccountGroupAsync(int groupId)
        {
            var accountGroup = default(AccountGroupViewModel);
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var existing = await repository.GetByIDAsync(groupId);
            if (existing != null)
            {
                accountGroup = Mapper.Map<AccountGroupViewModel>(existing);
            }

            return accountGroup;
        }

        /// <summary>
        /// مجوعه ای از حساب های کل زیرمجموعه گروه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه حساب های کل زیرمجموعه</returns>
        public async Task<PagedList<AccountViewModel>> GetGroupLedgerAccountsAsync(int groupId, GridOptions gridOptions = null)
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => acc.GroupId == groupId)
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .ToListAsync();
            return new PagedList<AccountViewModel>(accounts, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گروه حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountGroupView">گروه حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی گروه حساب ایجاد یا اصلاح شده</returns>
        public async Task<AccountGroupViewModel> SaveAccountGroupAsync(
            AccountGroupViewModel accountGroupView)
        {
            Verify.ArgumentNotNull(accountGroupView, nameof(accountGroupView));
            AccountGroup accountGroup = default(AccountGroup);
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            if (accountGroupView.Id == 0)
            {
                accountGroup = Mapper.Map<AccountGroup>(accountGroupView);
                await InsertAsync(repository, accountGroup);
            }
            else
            {
                accountGroup = await repository.GetByIDAsync(accountGroupView.Id);
                if (accountGroup != null)
                {
                    await UpdateAsync(repository, accountGroup, accountGroupView);
                }
            }

            return Mapper.Map<AccountGroupViewModel>(accountGroup);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا گروه حساب مورد نظر قابل حذف هست یا نه؟
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر برای حذف</param>
        /// <returns>اگر قابل حذف باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> CanDeleteAccountGroupAsync(int groupId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            int usageCount = await repository
                .GetEntityQuery()
                .Where(acc => acc.GroupId == groupId)
                .CountAsync();
            return (usageCount == 0);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گروه حساب موجود را حذف می کند
        /// </summary>
        /// <param name="groupId">شناسه گروه حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountGroupAsync(int groupId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var accountGroup = await repository.GetByIDAsync(groupId);
            if (accountGroup != null)
            {
                await DeleteAsync(repository, accountGroup);
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مجموعه ای از گروه های حساب موجود را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteAccountGroupsAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            foreach (int item in items)
            {
                var accountGroup = await repository.GetByIDAsync(item);
                if (accountGroup != null)
                {
                    await DeleteNoLogAsync(repository, accountGroup);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، تمام گروه های حساب را خوانده و برمیگرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه گروه های حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountGroupsBriefAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var accountGroups = await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<AccountItemBriefViewModel>(grp))
                .ToListAsync();

            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            foreach (var item in accountGroups)
            {
                item.ChildCount = await Repository
                    .GetAllQuery<Account>(ViewId.Account)
                    .Where(acc => acc.GroupId == item.Id)
                    .CountAsync();
            }

            return accountGroups;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که گروه حساب داده شده تکراری است یا نه
        /// </summary>
        /// <param name="accountGroup">اطلاعات نمایشی گروه حساب مورد نظر برای بررسی</param>
        /// <returns>در صورتی که نام گروه حساب تکراری باشد، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsDuplicateGroupAsync(AccountGroupViewModel accountGroup)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            int count = await repository.GetCountByCriteriaAsync(
                grp => grp.Id != accountGroup.Id &&
                grp.Name == accountGroup.Name);
            return count > 0;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.AccountGroup; }
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(AccountGroup entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5}",
                    AppStrings.Name, entity.Name, AppStrings.Category, entity.Category,
                    AppStrings.Description, entity.Description)
                : null;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accGroupView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="accGroup">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(AccountGroupViewModel accGroupView, AccountGroup accGroup)
        {
            accGroup.Name = accGroupView.Name;
            accGroup.Category = accGroupView.Category;
            accGroup.Description = accGroupView.Description;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
