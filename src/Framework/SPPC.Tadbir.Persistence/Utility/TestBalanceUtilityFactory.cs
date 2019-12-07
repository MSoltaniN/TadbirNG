using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class TestBalanceUtilityFactory : ITestBalanceUtilityFactory
    {
        public TestBalanceUtilityFactory(IConfigRepository config, IDomainMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public ITestBalanceUtility Create(int viewId)
        {
            ITestBalanceUtility utility = null;
            switch (viewId)
            {
                case ViewName.DetailAccount:
                    utility = new DetailAccountBalanceUtility(_config, _mapper);
                    break;
                case ViewName.CostCenter:
                    utility = new CostCenterBalanceUtility(_config, _mapper);
                    break;
                case ViewName.Project:
                    utility = new ProjectBalanceUtility(_config, _mapper);
                    break;
                case ViewName.Account:
                default:
                    utility = new AccountBalanceUtility(_config, _mapper);
                    break;
            }

            return utility;
        }

        private readonly IConfigRepository _config;
        private readonly IDomainMapper _mapper;
    }
}
