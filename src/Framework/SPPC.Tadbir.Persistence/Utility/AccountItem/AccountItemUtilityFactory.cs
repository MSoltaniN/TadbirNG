using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class AccountItemUtilityFactory : IAccountItemUtilityFactory
    {
        public AccountItemUtilityFactory(IRepositoryContext context, IConfigRepository config)
        {
            _context = context;
            _config = config;
        }

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
