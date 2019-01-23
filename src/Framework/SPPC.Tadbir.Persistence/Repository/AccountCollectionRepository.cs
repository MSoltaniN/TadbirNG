using System;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    public class AccountCollectionRepository : LoggingRepository<AccountCollection, AccountCollectionViewModel>, IAccountCollectionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public AccountCollectionRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log)
            : base(unitOfWork, mapper, metadata, log)
        {
            UnitOfWork.UseSystemContext();
        }

        protected override string GetState(AccountCollection entity)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateExisting(AccountCollectionViewModel entityView, AccountCollection entity)
        {
            throw new NotImplementedException();
        }
    }
}
