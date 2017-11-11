using SPPC.Tadbir.Business;
using SPPC.Tadbir.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Business
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<List<Account>> Get(int fpId, int branchId, GridOption gridOption);
    }
}
