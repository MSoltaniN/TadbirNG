
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using SPPC.Tadbir.DataAccess;
using Microsoft.EntityFrameworkCore;



//using SPPC.Tadbir.NHibernate;
//using SPPC.Tadbir.ViewModel.Finance;
//using SPPC.Tadbir.ViewModel.UI;


namespace SPPC.Tadbir.Business
{
    public class AccountRepository : IAccountRepository
    {
        public Task<bool> DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountViewModel> GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountViewModel>> GetAccounts(int fpId, int branchId, GridOption gridOption)
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return await(from a in db.AccountViewModels
                             select new AccountViewModel
                             {
                                 BranchId = a.BranchId,
                                 Code = a.Code,
                                 Description = a.Description,
                                 FiscalPeriodId = a.FiscalPeriodId,
                                 Id = a.Id,
                                 Name = a.Name
                             }).ToListAsync();

            }
        }

        public Task<bool> InsertAccount(AccountViewModel account)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAccount(AccountViewModel account)
        {
            throw new NotImplementedException();
        }
    }

}
