using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    public class RepositoryContext : IRepositoryContext
    {
        public RepositoryContext(IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecurityContext security)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            UserContext = security?.User;
        }

        public IAppUnitOfWork UnitOfWork { get; }

        public IDomainMapper Mapper { get; }

        public UserContextViewModel UserContext { get; }
    }
}
