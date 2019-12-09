using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class TestBalanceUtilityFactory : ITestBalanceUtilityFactory
    {
        public TestBalanceUtilityFactory(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IConfigRepository config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public ITestBalanceUtility Create(int viewId)
        {
            ITestBalanceUtility utility = null;
            switch (viewId)
            {
                case ViewName.DetailAccount:
                    utility = new DetailAccountBalanceUtility(_unitOfWork, _mapper, _config);
                    break;
                case ViewName.CostCenter:
                    utility = new CostCenterBalanceUtility(_unitOfWork, _mapper, _config);
                    break;
                case ViewName.Project:
                    utility = new ProjectBalanceUtility(_unitOfWork, _mapper, _config);
                    break;
                case ViewName.Account:
                default:
                    utility = new AccountBalanceUtility(_unitOfWork, _mapper, _config);
                    break;
            }

            return utility;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly IConfigRepository _config;
    }
}
