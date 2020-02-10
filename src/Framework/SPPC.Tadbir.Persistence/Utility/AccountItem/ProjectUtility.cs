using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class ProjectUtility : AccountItemUtilityBase, IAccountItemUtility
    {
        public ProjectUtility(IRepositoryContext context, IConfigRepository config)
            : base(context, config)
        {
        }

        public override async Task<TreeEntity> GetItemAsync(int itemId)
        {
            return await GetAccountItemAsync<Project>(itemId);
        }

        public override Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity project)
        {
            return line => line.Project.FullCode.StartsWith(project.FullCode);
        }
    }
}
