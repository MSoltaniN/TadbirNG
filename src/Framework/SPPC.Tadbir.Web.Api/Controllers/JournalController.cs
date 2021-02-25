using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class JournalController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="config"></param>
        /// <param name="strings"></param>
        public JournalController(IJournalRepository repository, IConfigRepository config,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
        }

        #region Journal By Date

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-row
        [HttpGet]
        [Route(JournalApi.JournalByDateByRowUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateByRowAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-row-detail
        [HttpGet]
        [Route(JournalApi.JournalByDateByRowDetailUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateByRowDetailAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-ledger
        [HttpGet]
        [Route(JournalApi.JournalByDateByLedgerUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateByLedgerAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-subsid
        [HttpGet]
        [Route(JournalApi.JournalByDateBySubsidiaryUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateBySubsidiaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/summary
        [HttpGet]
        [Route(JournalApi.JournalByDateLedgerSummaryUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/sum-by-date
        [HttpGet]
        [Route(JournalApi.JournalByDateLedgerSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByDateAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummaryByDate;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/sum-by-month
        [HttpGet]
        [Route(JournalApi.JournalByDateMonthlyLedgerSummaryUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByDateMonthlyLedgerSummaryAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.MonthlyLedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, false);
        }

        #endregion

        #region Journal By Date By Branch

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-row/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateByRowByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-row-detail/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateByRowDetailByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateByRowDetailByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-ledger/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateByLedgerByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateByLedgerByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/by-subsid/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateBySubsidiaryByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateBySubsidiaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/summary/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateLedgerSummaryByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/sum-by-date/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateLedgerSummaryByDateByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByDateByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.LedgerSummaryByDate;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-date/sum-by-month/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByDateMonthlyLedgerSummaryByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByDateMonthlyLedgerSummaryByBranchAsync(
            DateTime? from, DateTime? to)
        {
            var mode = JournalMode.MonthlyLedgerSummary;
            return await JournalByDateResultAsync(from, to, mode, true);
        }

        #endregion

        #region Journal By No

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-row
        [HttpGet]
        [Route(JournalApi.JournalByNoByRowUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByNoByRowAsync(int from, int to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByNumberResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-row-detail
        [HttpGet]
        [Route(JournalApi.JournalByNoByRowDetailUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByNoByRowDetailAsync(int from, int to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByNumberResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-ledger
        [HttpGet]
        [Route(JournalApi.JournalByNoByLedgerUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByNoByLedgerAsync(int from, int to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByNumberResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-subsid
        [HttpGet]
        [Route(JournalApi.JournalByNoBySubsidiaryUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByNoBySubsidiaryAsync(int from, int to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByNumberResultAsync(from, to, mode, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/summary
        [HttpGet]
        [Route(JournalApi.JournalByNoLedgerSummaryUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.View)]
        public async Task<IActionResult> GetJournalByNoLedgerSummaryAsync(int from, int to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByNumberResultAsync(from, to, mode, false);
        }

        #endregion

        #region Journal By No By Branch

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-row/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByNoByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByNoByRowByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByRows;
            return await JournalByNumberResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-row-detail/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByNoByRowDetailByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByNoByRowDetailByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByRowsWithDetail;
            return await JournalByNumberResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-ledger/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByNoByLedgerByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByNoByLedgerByBranchAsync(int from, int to)
        {
            var mode = JournalMode.ByLedger;
            return await JournalByNumberResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/by-subsid/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByNoBySubsidiaryByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByNoBySubsidiaryByBranchAsync(int from, int to)
        {
            var mode = JournalMode.BySubsidiary;
            return await JournalByNumberResultAsync(from, to, mode, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/reports/journal/by-no/summary/by-branch
        [HttpGet]
        [Route(JournalApi.JournalByNoLedgerSummaryByBranchUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)(JournalPermissions.View | JournalPermissions.ByBranch))]
        public async Task<IActionResult> GetJournalByNoLedgerSummaryByBranchAsync(int from, int to)
        {
            var mode = JournalMode.LedgerSummary;
            return await JournalByNumberResultAsync(from, to, mode, true);
        }

        #endregion

        private async Task<IActionResult> JournalByDateResultAsync(
            DateTime? from, DateTime? to, JournalMode journalMode, bool isByBranch)
        {
            Sanitize(ref from, ref to);
            var parameters = new JournalParameters()
            {
                Mode = journalMode,
                FromDate = from.Value,
                ToDate = to.Value,
                GridOptions = GridOptions ?? new GridOptions()
            };
            var journal = isByBranch
                ? await _repository.GetJournalByDateByBranchAsync(parameters)
                : await _repository.GetJournalByDateAsync(parameters);
            PrepareJournal(journal);
            return Json(journal);
        }

        private async Task<IActionResult> JournalByNumberResultAsync(
            int from, int to, JournalMode journalMode, bool isByBranch)
        {
            var parameters = new JournalParameters()
            {
                Mode = journalMode,
                FromNo = from,
                ToNo = to,
                GridOptions = GridOptions ?? new GridOptions()
            };
            var journal = isByBranch
                ? await _repository.GetJournalByNoByBranchAsync(parameters)
                : await _repository.GetJournalByNoAsync(parameters);
            PrepareJournal(journal);
            return Json(journal);
        }

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = from ?? rangeFrom;
                to = to ?? rangeTo;
            }
        }

        private void PrepareJournal(JournalViewModel journal)
        {
            SetItemCount(journal.TotalCount);
            Localize(journal);
            SetRowNumbers(journal.Items);
        }

        private void Localize(JournalViewModel journal)
        {
            foreach (var journalItem in journal.Items)
            {
                journalItem.Description = _strings[journalItem.Description ?? String.Empty];
            }
        }

        private readonly IJournalRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}