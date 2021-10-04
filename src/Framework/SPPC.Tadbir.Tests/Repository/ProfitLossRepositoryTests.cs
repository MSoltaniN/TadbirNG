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
    public class ProfitLossRepositoryTests : RepositoryTestBase
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
            _repository = new ProfitLossRepositoryDirect(repoContext, sysRepo, utility);
        }

        private ProfitLossParameters GetTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing)
        {
            return new ProfitLossParameters()
            {
                FromDate = DateTime.Parse("2018-06-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                GridOptions = gridOptions,
                TaxAmount = 10000,
                StartTurnoverAsInitBalance = startAsInit,
                UseClosingTempVoucher = useClosing
            };
        }

        private IProfitLossRepository _repository;
    }
}
