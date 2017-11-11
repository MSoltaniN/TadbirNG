namespace SPPC.Tadbir.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SPPC.Tadbir.DataAccess;    

    public class FullAccountRepository : IFullAccountRepository
    {
        public async Task<List<FullAccount>> GetFullAccount(int accountId)
        {
            try
            {
                using (SppcDBContext db = new SppcDBContext())
                {
                    var result = from a in db.FullAccount
                                 where a.AccountId == accountId
                                 select a;
                    
                    return await result.ToListAsync();
                }
            }
            catch
            {
                ////TODO: log exception 
                return null;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(FullAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<FullAccount>> Get(GridOption gridOption)
        {
            throw new NotImplementedException();
        }

        public Task<FullAccount> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(GridOption gridOption)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(FullAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
