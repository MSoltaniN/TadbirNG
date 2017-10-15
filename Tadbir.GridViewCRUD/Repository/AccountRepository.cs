
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;

using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.UI;


namespace Tadbir.GridViewCRUD
{
   public class AccountRepository : IAccountRepository
    {
        
        public void DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public SPPC.Tadbir.ViewModel.Finance.AccountViewModel GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public AccountFullViewModel GetAccountDetail(int accountId)
        {
            throw new NotImplementedException();
        }

        public IList<SPPC.Tadbir.ViewModel.Finance.AccountViewModel> GetAccounts(int fpId, int branchId, GridOptions options = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(int fpId, int branchId)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateAccount(SPPC.Tadbir.ViewModel.Finance.AccountViewModel accountViewModel)
        {
            throw new NotImplementedException();
        }

        public bool IsUsedAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public void SaveAccount(SPPC.Tadbir.ViewModel.Finance.AccountViewModel account)
        {
            throw new NotImplementedException();
        }
    }
}
