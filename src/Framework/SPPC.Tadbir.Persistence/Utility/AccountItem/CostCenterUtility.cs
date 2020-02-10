using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class CostCenterUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        public CostCenterUtility(IRepositoryContext context, IConfigRepository config)
            : base(context, config)
        {
        }

        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<CostCenter>(itemId);
        }

        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity costCenter)
        {
            return line => line.CostCenter.FullCode.StartsWith(costCenter.FullCode);
        }
    }
}
