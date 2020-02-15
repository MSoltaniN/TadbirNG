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
        public AccountItemUtilityFactory(IRepositoryContext context, IConfigRepository config)
        {
            _context = context;
            _config = config;
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
                case ViewName.DetailAccount:
                    utility = new DetailAccountUtility(_context, _config);
                    break;
                case ViewName.CostCenter:
                    utility = new CostCenterUtility(_context, _config);
                    break;
                case ViewName.Project:
                    utility = new ProjectUtility(_context, _config);
                    break;
                case ViewName.Account:
                default:
                    utility = new AccountUtility(_context, _config);
                    break;
            }

            return utility;
        }

        private readonly IRepositoryContext _context;
        private readonly IConfigRepository _config;
    }
}
