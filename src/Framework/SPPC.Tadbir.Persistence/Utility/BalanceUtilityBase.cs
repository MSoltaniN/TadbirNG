using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class BalanceUtilityBase : ReportUtilityBase
    {
        protected BalanceUtilityBase(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IConfigRepository config)
            : base(config, mapper)
        {
            UnitOfWork = unitOfWork;
        }

        protected IAppUnitOfWork UnitOfWork { get; }
    }
}
