
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using SPPC.Tadbir.DataAccess;
using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;

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
            try
            {
                using (AccountDBContext db = new AccountDBContext())
                {
                    var result = from a in db.AccountViewModels
                                 where a.BranchId == branchId && a.FiscalPeriodId == fpId
                                 select new AccountViewModel
                                 {
                                     BranchId = a.BranchId,
                                     Code = a.Code,
                                     Description = a.Description,
                                     FiscalPeriodId = a.FiscalPeriodId,
                                     AccountId = a.AccountId,
                                     Name = a.Name
                                 };

                    //var result = db.AccountViewModels.Where("City == @0 and Orders.Count >= @1", "London", 10)
                    //.OrderBy("CompanyName")
                    //.Select("new(CompanyName as Name, Phone)");

                    //add where clause
                    if (gridOption.Filters != null)
                    {
                        foreach (var item in gridOption.Filters)
                        {
                            string whereClause = string.Format("({0}).ToString() == \"{1}\"", item.Name, item.Value);
                            result = result.Where(whereClause);
                        }
                    }

                    if (!string.IsNullOrEmpty(gridOption.OrderBy))
                    {
                        result = result.OrderBy(gridOption.OrderBy);
                    }

                    return await result.Skip(gridOption.StartIndex.Value).Take(gridOption.Count.Value).ToListAsync();

                }



            }
            catch
            {
                //TODO: log exception 

                return null;
            }

        }

        public async Task<int> GetCount()
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return await (from a in db.AccountViewModels
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

        /// <summary>
        /// edit selected account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <param name="gridOption"></param>
        /// <returns></returns>
        public async Task<List<AccountViewModel>> GetAccounts(GridOption gridOption)
        {
            try
            {
                using (AccountDBContext db = new AccountDBContext())
                {
                    var result = from a in db.AccountViewModels
                                 select new AccountViewModel
                                 {
                                     BranchId = a.BranchId,
                                     Code = a.Code,
                                     Description = a.Description,
                                     FiscalPeriodId = a.FiscalPeriodId,
                                     AccountId = a.AccountId,
                                     Name = a.Name
                                 };

                    if (gridOption.Filters != null)
                    {
                        foreach (var item in gridOption.Filters)
                        {
                            string whereClause = string.Format("({0}).ToString() == \"{1}\"", item.Name, item.Value);
                            result = result.Where(whereClause);
                        }
                    }

                    if (!string.IsNullOrEmpty(gridOption.OrderBy))
                    {
                        result = result.OrderBy(gridOption.OrderBy);
                    }

                    return await result.Skip(gridOption.StartIndex.Value).Take(gridOption.Count.Value).ToListAsync();

                }



            }
            catch
            {
                //TODO: log exception 

                return null;
            }
        }

        public async Task<int> GetCount(GridOption gridOption)
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                var result = from a in db.AccountViewModels
                             select new AccountViewModel
                             {
                                 BranchId = a.BranchId,
                                 Code = a.Code,
                                 Description = a.Description,
                                 FiscalPeriodId = a.FiscalPeriodId,
                                 AccountId = a.AccountId,
                                 Name = a.Name
                             };

                if (gridOption.Filters != null)
                {
                    foreach (var item in gridOption.Filters)
                    {
                        string whereClause = string.Format("({0}).ToString() == \"{1}\"", item.Name, item.Value);
                        result = result.Where(whereClause);
                    }
                }



                return await result.CountAsync();

            }
        }

    }
}
