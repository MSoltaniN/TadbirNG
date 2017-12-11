using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.DataAccess;

namespace SPPC.Tadbir.Business
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<List<Account>> Get(int fpId, int branchId, GridOption gridOption);
    }
}
