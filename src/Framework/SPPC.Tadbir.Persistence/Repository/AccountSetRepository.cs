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
        /// اطلاعات خلاصه کلیه حساب های تخصیص داده شده به یک مجموعه حساب خاص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <returns>اطلاعات حساب های تخصیص داده شده به مجموعه حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountSetItems(AccountCollectionId collectionId)
        {
            var repository = _unitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var bankAccounts = await repository
                .GetEntityQuery(aca => aca.Account)
                .Where(aca => aca.CollectionId == (int)collectionId)
                .Select(aca => _mapper.Map<AccountItemBriefViewModel>(aca.Account))
                .ToListAsync();
            return bankAccounts;
        }

        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب فروش خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب فروش</returns>
        public async Task<AccountItemBriefViewModel> GetSalesAccountAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var salesAccount = await repository.GetSingleByCriteriaAsync(
                acc => acc.FullCode == _salesAccount);
            return _mapper.Map<AccountItemBriefViewModel>(salesAccount);
        }

        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب برگشت از فروش و تخفیفات خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب برگشت از فروش و تخفیفات</returns>
        public async Task<AccountItemBriefViewModel> GetSalesDeficitAccountAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var deficitAccount = await repository.GetSingleByCriteriaAsync(
                acc => acc.FullCode == _salesDeficitAccount);
            return _mapper.Map<AccountItemBriefViewModel>(deficitAccount);
        }

        /// <summary>
        /// خلاصه اطلاعات حساب های کل را برای مجموعه حساب دارایی های جاری خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های کل برای مجموعه حساب دارایی های جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLiquidAssetAccountsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var assetAccounts = await repository.GetByCriteriaAsync(
                acc => _liquidAssetAccounts.Contains(acc.FullCode));
            return assetAccounts
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .ToList();
        }

        /// <summary>
        /// خلاصه اطلاعات حساب های کل را برای مجموعه حساب بدهی های جاری خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های کل برای مجموعه حساب بدهی های جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLiquidLiabilityAccountsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var assetAccounts = await repository.GetByCriteriaAsync(
                acc => _liquidLiabilityAccounts.Contains(acc.FullCode));
            return assetAccounts
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .ToList();
        }

        private const string _bankAccount = "112";
        private const string _cashierAccount = "111001";
        private const string _salesAccount = "411";
        private const string _salesDeficitAccount = "412";
        private readonly string[] _liquidAssetAccounts = new string[]
            {
                "111", "112", "113", "114", "115", "116", "117", "118", "119", "552"
            };
        private readonly string[] _liquidLiabilityAccounts = new string[]
            {
                "211", "212", "213", "214", "215", "216", "217"
            };
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
