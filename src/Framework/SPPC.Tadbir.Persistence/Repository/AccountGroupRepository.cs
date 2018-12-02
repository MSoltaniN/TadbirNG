using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public AccountGroupRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log)
            : base(unitOfWork, mapper, metadata, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه گروه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اطلاعات نمایشی گروه های حساب</returns>
        public async Task<IList<AccountGroupViewModel>> GetAccountGroupsAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var accGroups = await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<AccountGroupViewModel>(grp))
                .Apply(gridOptions)
                .ToListAsync();
            return accGroups;
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
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای گروه حساب را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای گروه حساب</returns>
        public async Task<ViewViewModel> GetAccountGroupMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<AccountGroup>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد گروه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد گروه های حساب تعریف شده</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            return await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<AccountGroupViewModel>(grp))
                .Apply(gridOptions)
                .CountAsync();
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

        public async Task<bool> CanDeleteAccountGroupAsync(int groupId)
        {
            return true;
        }

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
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(AccountGroup entity)
        {
            return (entity != null)
                ? String.Format(
                    "Name : {1}{0}Category : {2}{0}Description : {3}",
                    Environment.NewLine, entity.Name, entity.Category, entity.Description)
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
    }
}
