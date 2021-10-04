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
    public class ItemBalanceRepositoryTests : RepositoryTestBase
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
            _repository = new TestBalanceRepositoryDirect(repoContext, sysRepo, utility);
        }

        private TestBalanceParameters GetTestParameters(
            GridOptions gridOptions, int viewId, TestBalanceFormat format, TestBalanceMode mode,
            FinanceReportOptions options, bool byBranch)
        {
            return new TestBalanceParameters()
            {
                FromDate = DateTime.Parse("2018-06-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                FromNo = 100,
                ToNo = 1000,
                IsByBranch = byBranch,
                ViewId = viewId,
                GridOptions = gridOptions,
                Format = format,
                Mode = mode,
                Options = options
            };
        }

        private ITestBalanceRepository _repository;
    }
}
