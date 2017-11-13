using SPPC.Tadbir.Business;
using SPPC.Tadbir.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Business
{
    public interface IFullAccountRepository : IRepository<FullAccount>
    {
        Task<List<FullAccount>> GetFullAccount(int accountId);
    }
}
