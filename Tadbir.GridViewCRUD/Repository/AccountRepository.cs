
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
        //public async Task<bool> DeleteAccount(int id)
        //{


        //    return true;
        //}


        //public async Task<bool> SaveAccount(AccountViewModel account)
        //{


        //    return true;
        //}

        //public async Task<List<AccountViewModel>> GetAllAccount()
        //{
        //    List<AccountViewModel> accounts = new List<AccountViewModel>();

        //    for (int i = 0;i<100;i++)
        //    {
        //        accounts.Add(new AccountViewModel() { Id = i, Code = i.ToString() , Name = string.Format("حساب {0}",i)
        //            , Description = "", FiscalPeriodId = 10 });
        //    }

        //    return accounts;
        //}
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
