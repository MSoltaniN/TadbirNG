using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    public interface IRepositoryBase
    {
        Task SetCurrentCompanyAsync(int companyId);
    }
}
