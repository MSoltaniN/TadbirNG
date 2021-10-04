using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Tests.Repository
{
    [TestFixture]
    [Category("RepositorySmokeTest")]
    public class BalanceByAccountRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext, sysRepo);
            _repository = new BalanceByAccountRepositoryDirect(repoContext, sysRepo, utility);
        }

        private BalanceByAccountParameters GetTestParameters(
            GridOptions gridOptions, int viewId, FinanceReportOptions options, bool byBranch)
        {
            return new BalanceByAccountParameters()
            {
                FromDate = DateTime.Parse("2018-06-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                FromNo = 100,
                ToNo = 1000,
                IsByBranch = byBranch,
                ViewId = viewId,
                GridOptions = gridOptions,
                Options = (int?)options
            };
        }

        private IBalanceByAccountRepository _repository;
    }
}
