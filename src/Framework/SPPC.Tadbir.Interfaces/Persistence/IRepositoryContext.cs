using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    public interface IRepositoryContext
    {
        IAppUnitOfWork UnitOfWork { get; }

        IDomainMapper Mapper { get; }

        UserContextViewModel UserContext { get; }
    }
}
