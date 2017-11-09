
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

                Account account = db.Account.Where(x => x.AccountId == id).FirstOrDefault();
                if (account != null)
                {
                    db.Account.Remove(account);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }

        public Task<Account> GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Account>> GetAccounts(int fpId, int branchId, GridOption gridOption)
        {
            try
            {
                using (AccountDBContext db = new AccountDBContext())
                {
                    var result = from a in db.Account
                                 where a.BranchId == branchId && a.FiscalPeriodId == fpId
                                 select new Account
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
                return await (from a in db.Account
                              select a
                             ).CountAsync();

            }
        }

        public async Task<bool> InsertAccount(Account account)
        {
            using (AccountDBContext db = new AccountDBContext())
            {


                Account newAccount = new Account()
                {
                    BranchId = account.BranchId,
                    Code = account.Code,
                    Description = account.Description,
                    FiscalPeriodId = account.FiscalPeriodId,
                    Name = account.Name
                };
                db.Account.Add(newAccount);

                return await db.SaveChangesAsync() >= 1;
            }
        }

        /// <summary>
        /// edit selected account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> EditAccount(Account account)
        {
            using (AccountDBContext db = new AccountDBContext())
            {

                Account editAccount = db.Account.Where(x => x.AccountId == account.AccountId).FirstOrDefault();


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
        public async Task<List<Account>> GetAccounts(GridOption gridOption)
        {
            try
            {
                using (AccountDBContext db = new AccountDBContext())
                {
                    var result = from a in db.Account
                                 select new Account
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
                var result = from a in db.Account
                             select new Account
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
