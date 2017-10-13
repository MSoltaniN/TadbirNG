
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tadbir.GridViewCRUD
{
    public interface IAccountRepository
    {
        Task<List<AccountViewModel>> GetAllAccount();

        Task<bool> DeleteAccount(int id);

        Task<bool> SaveAccount(AccountViewModel account);

    }
}
