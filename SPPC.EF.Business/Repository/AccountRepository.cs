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
        public async Task<bool> Delete(int id)
        {
            using (SppcDBContext db = new SppcDBContext())
            {
                var account = db.Account.Where(x => x.AccountId == id).FirstOrDefault();
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
        public Task<Account> Get(int id)
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
        public async Task<List<Account>> Get(int fId, int branchId, GridOption gridOption)
        {
            try
            {
                using (SppcDBContext db = new SppcDBContext())
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
            using (SppcDBContext db = new SppcDBContext())
            {
                return await(from a in db.Account select a).CountAsync();
            }
        }

        /// <summary>
        /// insert account in db
        /// </summary>
        /// <param name="account">account entity</param>
        /// <returns>return true if account inserted in db</returns>
        public async Task<bool> Insert(Account entity)
        {
            using (SppcDBContext db = new SppcDBContext())
            {
                Account newAccount = new Account()
                {
                    BranchId = entity.BranchId,
                    Code = entity.Code,
                    Description = entity.Description,
                    FiscalPeriodId = entity.FiscalPeriodId,
                    Name = entity.Name
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
        public async Task<bool> Edit(Account entity)
        {
            using (SppcDBContext db = new SppcDBContext())
            {
                Account editAccount = db.Account.Where(x => x.AccountId == entity.AccountId).FirstOrDefault();

                editAccount.BranchId = entity.BranchId;
                editAccount.Code = entity.Code;
                editAccount.Description = entity.Description;
                editAccount.FiscalPeriodId = entity.FiscalPeriodId;
                editAccount.Name = entity.Name;

                return await db.SaveChangesAsync() >= 1;
            }
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <param name="gridOption">grid option {start , count , filter , order }</param>
        /// <returns>return accounts list </returns>
        public async Task<List<Account>> Get(GridOption gridOption)
        {
            try
            {
                using (SppcDBContext db = new SppcDBContext())
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
                            string whereClause = "";
                            switch (item.Operator)
                            {
                                case "contains":
                                    whereClause = string.Format("({0}).ToString().Contains(\"{1}\")", item.Name, item.Value);                                    
                                    break;
                                case "eq":
                                    whereClause = string.Format("({0}).ToString() == \"{1}\"", item.Name, item.Value);
                                    break;
                            }
                            
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
            using (SppcDBContext db = new SppcDBContext())
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
