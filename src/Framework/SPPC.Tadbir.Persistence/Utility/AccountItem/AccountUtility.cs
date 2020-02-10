using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class AccountUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        public AccountUtility(IRepositoryContext context, IConfigRepository config)
            : base(context, config)
        {
        }

        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<Account>(itemId);
        }

        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity account)
        {
            return line => line.Account.FullCode.StartsWith(account.FullCode);
        }
    }
}
