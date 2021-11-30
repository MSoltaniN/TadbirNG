using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;

namespace SPPC.Tadbir.Persistence.Tests
{
    [TestFixture]
    [Category("RepositorySmokeTest")]
    public class CurrencyBookRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        [Test]
        public async Task AllModes_WhenRun_UseCorrectQuery()
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .WithCurrencyFilter(9)
                .UseCheckedVouchers()
                .Build();

            // By Row
            var parameters = GetTestParameters(CurrencyBookMode.ByRows, gridOptions);
            await _repository.GetCurrencyBookAsync(parameters);

            // By Row By Branch
            parameters = GetTestParameters(CurrencyBookMode.ByRows, gridOptions, true);
            await _repository.GetCurrencyBookAsync(parameters);

            // Voucher Sum
            parameters = GetTestParameters(CurrencyBookMode.VoucherSum, gridOptions);
            await _repository.GetCurrencyBookAsync(parameters);

            // Voucher Sum By Branch
            parameters = GetTestParameters(CurrencyBookMode.VoucherSum, gridOptions, true);
            await _repository.GetCurrencyBookAsync(parameters);

            // Daily Sum
            parameters = GetTestParameters(CurrencyBookMode.DailySum, gridOptions);
            await _repository.GetCurrencyBookAsync(parameters);

            // Daily Sum By Branch
            parameters = GetTestParameters(CurrencyBookMode.DailySum, gridOptions, true);
            await _repository.GetCurrencyBookAsync(parameters);

            // Monthly Sum
            parameters = GetTestParameters(CurrencyBookMode.MonthlySum, gridOptions);
            await _repository.GetCurrencyBookAsync(parameters);

            // Monthly Sum By Branch
            parameters = GetTestParameters(CurrencyBookMode.MonthlySum, gridOptions, true);
            await _repository.GetCurrencyBookAsync(parameters);

            // All Currencies
            gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            parameters = GetTestParameters(CurrencyBookMode.AllCurrencies, gridOptions);
            await _repository.GetCurrencyBookAllCurrenciesAsync(parameters);

            // All Currencies By Branch
            parameters = GetTestParameters(CurrencyBookMode.AllCurrencies, gridOptions, true);
            await _repository.GetCurrencyBookAllCurrenciesAsync(parameters);

            // All Currencies + Show No Currency
            parameters = GetTestParameters(CurrencyBookMode.AllCurrencies, gridOptions);
            parameters.NoCurrency = true;
            await _repository.GetCurrencyBookAllCurrenciesAsync(parameters);

            // All Currencies By Branch + Show No Currency
            parameters = GetTestParameters(CurrencyBookMode.AllCurrencies, gridOptions, true);
            parameters.NoCurrency = true;
            await _repository.GetCurrencyBookAllCurrenciesAsync(parameters);
        }

        private static CurrencyBookParameters GetTestParameters(
            CurrencyBookMode mode, GridOptions gridOptions, bool byBranch = false)
        {
            return new CurrencyBookParameters()
            {
                FromDate = DateTime.Parse("2018-09-11"),
                ToDate = DateTime.Parse("2019-01-14"),
                Mode = mode,
                ByBranch = byBranch,
                GridOptions = gridOptions,
                DetailAccountId = 15,
                CostCenterId = 3,
                ProjectId = 4
            };
        }

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            repoContext.UserContext.FiscalPeriodId = 1;
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext);
            _repository = new CurrencyBookRepositoryDirect(repoContext, sysRepo, utility);
        }

        private ICurrencyBookRepository _repository;
    }
}
