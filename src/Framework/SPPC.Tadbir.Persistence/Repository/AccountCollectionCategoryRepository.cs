using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت گروه مجموعه حساب را تعریف می کند.
    /// </summary>
    public class AccountCollectionCategoryRepository : IAccountCollectionCategoryRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public AccountCollectionCategoryRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه مجموعه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی مجموعه های حساب</returns>
        public async Task<IList<AccountCollectionCategoryViewModel>> GetAccountCollectionCategoriesAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<AccountCollectionCategory>();
            var accCollectionCat = await repository
                .GetAllAsync(f => f.AccountCollections);

            return accCollectionCat.Select(a => _mapper.Map<AccountCollectionCategoryViewModel>(a)).ToList();
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
