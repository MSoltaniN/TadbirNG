using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class JournalController : ApiControllerBase
    {
        public JournalController(IJournalRepository repository, IConfigRepository config,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
        }

        #region Journal By Date

        // GET: api/reports/journal/by-date/by-row
        [Route(JournalApi.JournalByDateByRowUrl)]
        public async Task<IActionResult> GetJournalByDateByRowAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByDateResultAsync(from, to, mode, false, false, false);
        }

        // GET: api/reports/journal/by-date/by-row-detail
        [Route(JournalApi.JournalByDateByRowDetailUrl)]
        public async Task<IActionResult> GetJournalByDateByRowDetailAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByDateResultAsync(from, to, mode, false, false, false);
        }

        // GET: api/reports/journal/by-date/by-ledger
        [Route(JournalApi.JournalByDateByLedgerUrl)]
        public async Task<IActionResult> GetJournalByDateByLedgerAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByDateResultAsync(from, to, mode, false, true, false);
        }

        // GET: api/reports/journal/by-date/by-subsid
        [Route(JournalApi.JournalByDateBySubsidiaryUrl)]
        public async Task<IActionResult> GetJournalByDateBySubsidiaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByDateResultAsync(from, to, mode, false, true, false);
        }

        // GET: api/reports/journal/by-date/summary
        [Route(JournalApi.JournalByDateLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, false, false, true);
        }

        // GET: api/reports/journal/by-date/sum-by-date
        [Route(JournalApi.JournalByDateLedgerSummaryByDateUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByDateAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummaryByDate;
            return await JournalByDateResultAsync(from, to, mode, false, false, false);
        }

        // GET: api/reports/journal/by-date/sum-by-month
        [Route(JournalApi.JournalByDateMonthlyLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByDateMonthlyLedgerSummaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.MonthlyLedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, false, false, true);
        }

        #endregion

        #region Journal By Date By Branch

        // GET: api/reports/journal/by-date/by-row/by-branch
        [Route(JournalApi.JournalByDateByRowByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateByRowByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByDateResultAsync(from, to, mode, true, false, false);
        }

        // GET: api/reports/journal/by-date/by-row-detail/by-branch
        [Route(JournalApi.JournalByDateByRowDetailByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateByRowDetailByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByDateResultAsync(from, to, mode, true, false, false);
        }

        // GET: api/reports/journal/by-date/by-ledger/by-branch
        [Route(JournalApi.JournalByDateByLedgerByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateByLedgerByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByDateResultAsync(from, to, mode, true, true, false);
        }

        // GET: api/reports/journal/by-date/by-subsid/by-branch
        [Route(JournalApi.JournalByDateBySubsidiaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateBySubsidiaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByDateResultAsync(from, to, mode, true, true, false);
        }

        // GET: api/reports/journal/by-date/summary/by-branch
        [Route(JournalApi.JournalByDateLedgerSummaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, true, false, true);
        }

        // GET: api/reports/journal/by-date/sum-by-date/by-branch
        [Route(JournalApi.JournalByDateLedgerSummaryByDateByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByDateByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummaryByDate;
            return await JournalByDateResultAsync(from, to, mode, true, false, false);
        }

        // GET: api/reports/journal/by-date/sum-by-month/by-branch
        [Route(JournalApi.JournalByDateMonthlyLedgerSummaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateMonthlyLedgerSummaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.MonthlyLedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, true, false, true);
        }

        #endregion

        #region Journal By No

        // GET: api/reports/journal/by-no/by-row
        [Route(JournalApi.JournalByNoByRowUrl)]
        public async Task<IActionResult> GetJournalByNoByRowAsync(int from, int to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByNumberResultAsync(from, to, mode, false, false, false);
        }

        // GET: api/reports/journal/by-no/by-row-detail
        [Route(JournalApi.JournalByNoByRowDetailUrl)]
        public async Task<IActionResult> GetJournalByNoByRowDetailAsync(int from, int to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByNumberResultAsync(from, to, mode, false, false, false);
        }

        // GET: api/reports/journal/by-no/by-ledger
        [Route(JournalApi.JournalByNoByLedgerUrl)]
        public async Task<IActionResult> GetJournalByNoByLedgerAsync(int from, int to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByNumberResultAsync(from, to, mode, false, true, false);
        }

        // GET: api/reports/journal/by-no/by-subsid
        [Route(JournalApi.JournalByNoBySubsidiaryUrl)]
        public async Task<IActionResult> GetJournalByNoBySubsidiaryAsync(int from, int to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByNumberResultAsync(from, to, mode, false, true, false);
        }

        // GET: api/reports/journal/by-no/summary
        [Route(JournalApi.JournalByNoLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByNoLedgerSummaryAsync(int from, int to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByNumberResultAsync(from, to, mode, false, false, true);
        }

        #endregion

        #region Journal By No By Branch

        // GET: api/reports/journal/by-no/by-row/by-branch
        [Route(JournalApi.JournalByNoByRowByBranchUrl)]
        public async Task<IActionResult> GetJournalByNoByRowByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByNumberResultAsync(from, to, mode, true, false, false);
        }

        // GET: api/reports/journal/by-no/by-row-detail/by-branch
        [Route(JournalApi.JournalByNoByRowDetailByBranchUrl)]
        public async Task<IActionResult> GetJournalByNoByRowDetailByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByNumberResultAsync(from, to, mode, true, false, false);
        }

        // GET: api/reports/journal/by-no/by-ledger/by-branch
        [Route(JournalApi.JournalByNoByLedgerByBranchUrl)]
        public async Task<IActionResult> GetJournalByNoByLedgerByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByNumberResultAsync(from, to, mode, true, true, false);
        }

        // GET: api/reports/journal/by-no/by-subsid/by-branch
        [Route(JournalApi.JournalByNoBySubsidiaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByNoBySubsidiaryByBranchAsync(int from, int to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByNumberResultAsync(from, to, mode, true, true, false);
        }

        // GET: api/reports/journal/by-no/summary/by-branch
        [Route(JournalApi.JournalByNoLedgerSummaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByNoLedgerSummaryByBranchAsync(int from, int to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByNumberResultAsync(from, to, mode, true, false, true);
        }

        #endregion

        private async Task<IActionResult> JournalByDateResultAsync(
            DateTime? from, DateTime? to, JournalMode journalMode,
            bool isByBranch, bool isLocalized, bool isSummary)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = isByBranch
                ? await _repository.GetJournalByDateByBranchAsync(journalMode, from.Value, to.Value)
                : await _repository.GetJournalByDateAsync(journalMode, from.Value, to.Value);
            PrepareDelegate prepareJournal = isSummary
                ? (PrepareDelegate)PrepareSummaryJournal
                : PrepareJournal;
            prepareJournal(journal, gridOptions);

            if (isLocalized)
            {
                Localize(journal);
            }

            return Json(journal);
        }

        private async Task<IActionResult> JournalByNumberResultAsync(
            int from, int to, JournalMode journalMode, bool isByBranch,
            bool isLocalized, bool isSummary)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = isByBranch
                ? await _repository.GetJournalByNoByBranchAsync(journalMode, from, to)
                : await _repository.GetJournalByNoAsync(journalMode, from, to);
            PrepareDelegate prepareJournal = isSummary
                ? (PrepareDelegate)PrepareSummaryJournal
                : PrepareJournal;
            prepareJournal(journal, gridOptions);

            if (isLocalized)
            {
                Localize(journal);
            }

            return Json(journal);
        }

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.SetCurrentContext(SecurityContext.User);
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = from ?? rangeFrom;
                to = to ?? rangeTo;
            }
        }

        private void Localize(JournalViewModel journal)
        {
            foreach (var journalItem in journal.Items)
            {
                journalItem.Description = _strings[AppStrings.AsQuotedInVoucherLines];
            }
        }

        private void PrepareJournal(JournalViewModel journal, GridOptions gridOptions)
        {
            var userItems = journal.Items.Apply(gridOptions, false);
            journal.DebitSum = userItems.Sum(item => item.Debit);
            journal.CreditSum = userItems.Sum(item => item.Credit);
            SetItemCount(userItems.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

        private void PrepareSummaryJournal(JournalViewModel journal, GridOptions gridOptions)
        {
            journal.DebitSum = journal.Items.Sum(item => item.Debit);
            journal.CreditSum = journal.Items.Sum(item => item.Credit);
            SetItemCount(journal.Items.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

        private delegate void PrepareDelegate(JournalViewModel journal, GridOptions gridOptions);

        private readonly IJournalRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}