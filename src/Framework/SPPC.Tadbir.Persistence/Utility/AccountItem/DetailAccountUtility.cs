using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class DetailAccountUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        public DetailAccountUtility(IRepositoryContext context, IConfigRepository config)
            : base(context, config)
        {
        }

        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<DetailAccount>(itemId);
        }

        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity detailAccount)
        {
            return line => line.DetailAccount.FullCode.StartsWith(detailAccount.FullCode);
        }
    }
}
