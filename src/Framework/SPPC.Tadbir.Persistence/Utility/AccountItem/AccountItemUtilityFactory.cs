using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات مورد نیاز برای ساختن کلاس کمکی محاسبات مالی مولفه های حساب را پیاده سازی می کند
    /// </summary>
    public class AccountItemUtilityFactory : IAccountItemUtilityFactory
    {
        /// <summary>
        /// نمونه جدیدی از این حساب می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        /// <param name="repository">امکان اعمال فیلترهای سطری و شعبه روی اطلاعات را فراهم می کند</param>
        public AccountItemUtilityFactory(IRepositoryContext context, IConfigRepository config, ISecureRepository repository)
        {
            _context = context;
            _config = config;
            _repository = repository;
        }

        /// <summary>
        /// کلاس کمکی را برای مولفه حساب با شناسه داده می سازد
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب</param>
        /// <returns>کلاس کمکی ساخته شده</returns>
        public IAccountItemUtility Create(int viewId)
        {
            IAccountItemUtility utility = null;
            switch (viewId)
            {
                case ViewId.DetailAccount:
                    utility = new DetailAccountUtility(_context, _config, _repository);
                    break;
                case ViewId.CostCenter:
                    utility = new CostCenterUtility(_context, _config, _repository);
                    break;
                case ViewId.Project:
                    utility = new ProjectUtility(_context, _config, _repository);
                    break;
                case ViewId.Account:
                default:
                    utility = new AccountUtility(_context, _config, _repository);
                    break;
            }

            return utility;
        }

        private readonly IRepositoryContext _context;
        private readonly IConfigRepository _config;
        private readonly ISecureRepository _repository;
    }
}
