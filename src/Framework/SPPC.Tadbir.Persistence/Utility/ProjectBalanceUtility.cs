using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class ProjectBalanceUtility : ReportUtilityBase, ITestBalanceUtility
    {
        public ProjectBalanceUtility(IConfigRepository config, IDomainMapper mapper)
            : base(config, mapper)
        {
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.Account);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.ProjectLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.ProjectLevel >= level;
        }
    }
}
