
using SPPC.Tadbir.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Business
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts(int fpId,int branchId,GridOption gridOption);

        Task<List<Account>> GetAccounts(GridOption gridOption);

        Task<Account> GetAccount(int id);

        Task<bool> DeleteAccount(int id);

        Task<bool> EditAccount(Account account);

        Task<bool> InsertAccount(Account account);

        Task<int> GetCount();

        Task<int> GetCount(GridOption gridOption);

    }
}
