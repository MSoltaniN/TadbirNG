
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
        public async Task<bool> DeleteAccount(int id)
        {
            using (AccountDBContext db = new AccountDBContext())
            {

                AccountViewModel account = db.AccountViewModels.Where(x => x.AccountId == id).FirstOrDefault();
                if (account != null)
                {
                    db.AccountViewModels.Remove(account);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }

        public Task<AccountViewModel> GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountViewModel>> GetAccounts(int fpId, int branchId, GridOption gridOption)
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return await (from a in db.AccountViewModels
                              where a.BranchId == branchId && a.FiscalPeriodId == fpId
                              select new AccountViewModel
                                {
                                    BranchId = a.BranchId,
                                    Code = a.Code,
                                    Description = a.Description,
                                    FiscalPeriodId = a.FiscalPeriodId,
                                    AccountId = a.AccountId,
                                    Name = a.Name
                                }).ToListAsync();

            }
            
        }

        public async Task<int> GetCount()
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return await(from a in db.AccountViewModels
                             select a
                             ).CountAsync();

            }
        }

        public async Task<bool> InsertAccount(AccountViewModel account)
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                
                
                AccountViewModel newAccount = new AccountViewModel()
                {
                    BranchId = account.BranchId,
                    Code = account.Code,
                    Description = account.Description,
                    FiscalPeriodId = account.FiscalPeriodId,
                    Name = account.Name
                };
                db.AccountViewModels.Add(newAccount);
                
                return await db.SaveChangesAsync() >= 1;
            }
        }

        public async Task<bool> EditAccount(AccountViewModel account)
        {
            using (AccountDBContext db = new AccountDBContext())
            {

                AccountViewModel editAccount = db.AccountViewModels.Where(x => x.AccountId == account.AccountId).FirstOrDefault();


                editAccount.BranchId = account.BranchId;
                editAccount.Code = account.Code;
                editAccount.Description = account.Description;
                editAccount.FiscalPeriodId = account.FiscalPeriodId;
                editAccount.Name = account.Name;
                
                return await db.SaveChangesAsync() >= 1;
            }
        }
    }

}
