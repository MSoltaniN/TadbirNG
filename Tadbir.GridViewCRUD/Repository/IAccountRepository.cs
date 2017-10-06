
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tadbir.GridViewCRUD
{
    public interface IAccountRepository
    {
        Task<List<AccountViewModel>> GetAllAccount();
        
    }
}
