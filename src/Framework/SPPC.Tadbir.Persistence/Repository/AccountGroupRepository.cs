﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
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
        /// <param name="repository">امکان اعمال فیلترهای سطری و شعبه را فراهم می کند</param>
        public AccountGroupRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
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
                .Apply(gridOptions, false)
                .CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های کل زیرمجموعه گروه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های کل زیرمجموعه گروه حساب</returns>
        public async Task<int> GetSubItemCountAsync(int groupId, GridOptions gridOptions = null)
        {
            int count = await _repository
                .GetAllQuery<Account>(ViewName.Account)
                .Where(acc => acc.GroupId == groupId)
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .Apply(gridOptions, false)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// مجوعه ای از حساب های کل زیرمجموعه گروه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه حساب های کل زیرمجموعه</returns>
        public async Task<IList<AccountViewModel>> GetGroupLedgerAccountsAsync(int groupId, GridOptions gridOptions = null)
        {
            var accounts = await _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => acc.GroupId == groupId)
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
            return accounts;
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
        /// اطلاعات محیطی کاربر جاری برنامه را برای برای خواندن اطلاعات وابسته به شعبه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، تمام گروه های حساب را خوانده و برمیگرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه گروه های حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountGroupsBriefAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountGroup>();
            var accGroups = await repository
                .GetEntityQuery()
                .Select(grp => Mapper.Map<AccountItemBriefViewModel>(grp))
                .ToListAsync();

            var accRepository = UnitOfWork.GetAsyncRepository<Account>();

            foreach (var item in accGroups)
            {
                var accounts = _repository
                    .GetAllQuery<Account>(ViewName.Account)
                    .Where(acc => acc.ParentId == null && acc.GroupId == item.Id);

                item.ChildCount = accounts.Count();
            }

            return accGroups;
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

        private readonly ISecureRepository _repository;
    }
}
