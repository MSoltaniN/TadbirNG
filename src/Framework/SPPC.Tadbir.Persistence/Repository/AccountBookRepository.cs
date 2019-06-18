using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر حساب را پیاده سازی می کند
    /// </summary>
    public class AccountBookRepository : RepositoryBase, IAccountBookRepository
    {
        public AccountBookRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper,
            IMetadataRepository metadata, ISecureRepository repository)
            : base(unitOfWork, mapper, metadata)
        {
            _repository = repository;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "ساده : مطابق ردیف های سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookByRowAsync(int viewId, int accountId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        private readonly ISecureRepository _repository;
    }
}
