using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class TestBalanceUtilityFactory : ITestBalanceUtilityFactory
    {
        public TestBalanceUtilityFactory(IRepositoryContext context,
            ISecureRepository repository, IConfigRepository config)
        {
            _context = context;
            _repository = repository;
            _config = config;
        }

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
