namespace SPPC.Tadbir.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SPPC.Tadbir.DataAccess;
    
    /// <summary>
    /// account repository
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// delete a account
        /// </summary>
        /// <param name="id">id is AccountID</param>
        /// <returns>return bool type</returns>
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

        /// <summary>
        /// get a account by id 
        /// </summary>
        /// <param name="id">account id</param>
        /// <returns>Not Implemented method</returns>
        public Task<Account> GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get account by id and branchid 
        /// </summary>
        /// <param name="fId">fiscal period id</param>
        /// <param name="branchId">branch id</param>
        /// <param name="gridOption">grid option {start , count , filter , order }</param>
        /// <returns>return accounts list </returns>
        public async Task<List<Account>> GetAccounts(int fId, int branchId, GridOption gridOption)
        {
            try
            {
                using (AccountDBContext db = new AccountDBContext())
                {
                    var result = from a in db.Account
                                 where a.BranchId == branchId && a.FiscalPeriodId == fId
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
                ////TODO: log exception 
                return null;
            }
        }

        /// <summary>
        /// get accounts count 
        /// </summary>
        /// <returns>return count of accounts</returns>
        public async Task<int> GetCount()
        {
            using (AccountDBContext db = new AccountDBContext())
            {
                return await(from a in db.Account select a).CountAsync();
            }
        }

        /// <summary>
        /// insert account in db
        /// </summary>
        /// <param name="account">account entity</param>
        /// <returns>return true if account inserted in db</returns>
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
        /// <param name="account">account entity</param>
        /// <returns>return true if account edited</returns>
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
        /// <param name="gridOption">grid option {start , count , filter , order }</param>
        /// <returns>return accounts list </returns>
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
                ////TODO: log exception 

                return null;
            }
        }

        /// <summary>
        /// get account count
        /// </summary>
        /// <param name="gridOption">grid option {start , count , filter , order }</param>
        /// <returns>return accounts count</returns>
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
