using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مجموعه حساب ها را پیاده سازی می کند
    /// </summary>
    public class AccountSetRepository : IAccountSetRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public AccountSetRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه کلیه حساب های تخصیص داده شده
        /// به یک مجموعه حساب خاص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <returns>اطلاعات حساب های تخصیص داده شده به مجموعه حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountSetItemsAsync(
            AccountCollectionId collectionId)
        {
            var repository = _unitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var accounts = await repository
                .GetEntityQuery(aca => aca.Account)
                .Where(aca => aca.CollectionId == (int)collectionId)
                .Select(aca => _mapper.Map<AccountItemBriefViewModel>(aca.Account))
                .ToListAsync();
            return accounts;
        }

        /// <summary>
        /// خلاصه اطلاعات حساب های مرتبط با مجموعه حساب های کسر از فروش (برگشت از فروش و تخفیفات فروش) را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های مرتبط با مجموعه حساب های کسر از فروش</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetSalesDeficitAccountsAsync()
        {
            var salesDeficit = new List<AccountItemBriefViewModel>();
            salesDeficit.AddRange(await GetAccountSetItemsAsync(AccountCollectionId.SalesRefund));
            salesDeficit.AddRange(await GetAccountSetItemsAsync(AccountCollectionId.SalesDiscount));
            return salesDeficit;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
