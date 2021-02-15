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
        /// <param name="config">امکان مدیریت تنظیمات شرکتی را فراهم می کند</param>
        /// <param name="repository">امکان اعمال فیلترهای شعبه و سطری را فراهم می کند</param>
        /// <param name="helper">امکانات کمکی برای محاسبات مانده را فراهم می کند</param>
        public TestBalanceUtilityFactory(IRepositoryContext context,
            IConfigRepository config, ISecureRepository repository, ITestBalanceHelper helper)
        {
            _context = context;
            _config = config;
            _repository = repository;
            _helper = helper;
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
                case ViewId.DetailAccount:
                    utility = new DetailAccountBalanceUtility(_context, _config, _repository, _helper);
                    break;
                case ViewId.CostCenter:
                    utility = new CostCenterBalanceUtility(_context, _config, _repository, _helper);
                    break;
                case ViewId.Project:
                    utility = new ProjectBalanceUtility(_context, _config, _repository, _helper);
                    break;
                case ViewId.Account:
                default:
                    utility = new AccountBalanceUtility(_context, _config, _repository, _helper);
                    break;
            }

            return utility;
        }

        private readonly IRepositoryContext _context;
        private readonly IConfigRepository _config;
        private readonly ISecureRepository _repository;
        private readonly ITestBalanceHelper _helper;
    }
}
