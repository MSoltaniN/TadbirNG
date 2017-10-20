
using SPPC.Tadbir.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Business
{
    public interface IAccountRepository
    {
        Task<List<AccountViewModel>> GetAccounts(int fpId,int branchId,GridOption gridOption);

        Task<List<AccountViewModel>> GetAccounts(GridOption gridOption);

        Task<AccountViewModel> GetAccount(int id);

        Task<bool> DeleteAccount(int id);

        Task<bool> EditAccount(AccountViewModel account);

        Task<bool> InsertAccount(AccountViewModel account);

        Task<int> GetCount();

    }
}
