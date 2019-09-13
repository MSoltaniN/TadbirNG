using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مجموعه حساب ها را پیاده سازی می کند
    /// </summary>
    public class AccountSetRepository : RepositoryBase, IAccountSetRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountSetRepository(IRepositoryContext context)
            : base(context)
        {
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
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var accounts = await repository
                .GetEntityQuery(aca => aca.Account)
                .Where(aca => aca.CollectionId == (int)collectionId)
                .Select(aca => Mapper.Map<AccountItemBriefViewModel>(aca.Account))
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
    }
}
