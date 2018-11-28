using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
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
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(AccountGroup entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accGroupView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="accGroup">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(AccountGroupViewModel accGroupView, AccountGroup accGroup)
        {
            throw new NotImplementedException();
        }
    }
}
