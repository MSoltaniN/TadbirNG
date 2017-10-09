
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;

namespace Tadbir.GridViewCRUD
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<bool> DeleteAccount(int id)
        {
            

            return 1 > 0;
        }

        public async Task<List<AccountViewModel>> GetAllAccount()
        {
            List<AccountViewModel> accounts = new List<AccountViewModel>();

            for (int i = 0;i<100;i++)
            {
                accounts.Add(new AccountViewModel() { Id = i, Code = i.ToString() , Name = string.Format("حساب {0}",i)
                    , Description = "", FiscalPeriodId = 10 });
            }
            
            return accounts;
        }

       
    
    }
}
