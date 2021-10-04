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
    public class BalanceSheetRepositoryTests : RepositoryTestBase
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
            _repository = new BalanceSheetRepositoryDirect(repoContext, sysRepo, utility);
        }

        private BalanceSheetParameters GetTestParameters(
            GridOptions gridOptions, bool useClosing,
            int? fpId = null, int? ccenterId = null, int? projectId = null)
        {
            return new BalanceSheetParameters()
            {
                GridOptions = gridOptions,
                Date = DateTime.Parse("2019-01-01"),
                UseClosingVoucher = useClosing,
                FiscalPeriodId = fpId,
                CostCenterId = ccenterId,
                ProjectId = projectId
            };
        }

        private IBalanceSheetRepository _repository;
    }
}
