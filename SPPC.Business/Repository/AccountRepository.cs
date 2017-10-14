using SPPC.Tadbir.DataAccess;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Business
{
    public class AccountRepository : IAccountRepository
    {
        public void DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Tadbir.ViewModel.Finance.AccountViewModel GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Tadbir.ViewModel.Finance.AccountFullViewModel GetAccountDetail(int accountId)
        {
            throw new NotImplementedException();
        }

        public  IList<Tadbir.ViewModel.Finance.AccountViewModel> GetAccounts(int fpId, int branchId, Tadbir.ViewModel.UI.GridOptions options = null)
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return (from a in db.AccountViewModels
                             select new AccountViewModel
                             {
                                 BranchId = a.BranchId,
                                 FiscalPeriodId = a.FiscalPeriodId,
                                 Code = a.Code,
                                 Description = a.Description,
                                 Id = a.Id,
                                 Name = a.Name
                             }).ToList();

            }
        }

        public int GetCount(int fpId, int branchId)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateAccount(Tadbir.ViewModel.Finance.AccountViewModel accountViewModel)
        {
            throw new NotImplementedException();
        }

        public bool IsUsedAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public void SaveAccount(Tadbir.ViewModel.Finance.AccountViewModel account)
        {
            throw new NotImplementedException();
        }
    }
}
