using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات مورد نیاز برای ساختن کلاس های کمکی گزارش گردش و مانده را پیاده سازی می کند
    /// </summary>
    public class TestBalanceUtilityFactory : ITestBalanceUtilityFactory
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="repository"></param>
        /// <param name="config">امکان مدیریت تنظیمات شرکتی را فراهم می کند</param>
        public TestBalanceUtilityFactory(IRepositoryContext context,
            ISecureRepository repository, IConfigRepository config)
        {
            _context = context;
            _repository = repository;
            _config = config;
        }

        /// <summary>
        /// کلاس کمکی مربوط به مولفه حساب داده شده را ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <returns>کلاس کمکی مربوط به مولفه حساب</returns>
        public ITestBalanceUtility Create(int viewId)
        {
            ITestBalanceUtility utility = null;
            switch (viewId)
            {
                case ViewName.DetailAccount:
                    utility = new DetailAccountBalanceUtility(_context, _repository, _config);
                    break;
                case ViewName.CostCenter:
                    utility = new CostCenterBalanceUtility(_context, _repository, _config);
                    break;
                case ViewName.Project:
                    utility = new ProjectBalanceUtility(_context, _repository, _config);
                    break;
                case ViewName.Account:
                default:
                    utility = new AccountBalanceUtility(_context, _repository, _config);
                    break;
            }

            return utility;
        }

        private readonly IRepositoryContext _context;
        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _config;
    }
}
