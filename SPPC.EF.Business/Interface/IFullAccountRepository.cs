using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.DataAccess;

namespace SPPC.Tadbir.Business
{
    public interface IFullAccountRepository : IRepository<FullAccount>
    {
        Task<List<FullAccount>> GetFullAccount(int accountId);
    }
}
