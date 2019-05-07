using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines client URL and server routes for Journal reports
    /// </summary>
    public sealed class JournalApi
    {
        private JournalApi()
        {
        }

        #region Journal By Date API

        /// <summary>
        /// API client URL for Journal-By Date-By Row report
        /// </summary>
        public const string JournalByDateByRow = "reports/journal/by-date/by-row";

        /// <summary>
        /// API server route URL for Journal-By Date-By Row report
        /// </summary>
        public const string JournalByDateByRowUrl = "reports/journal/by-date/by-row";

        /// <summary>
        /// API client URL for Journal-By Date-By Row Detail report
        /// </summary>
        public const string JournalByDateByRowDetail = "reports/journal/by-date/by-row-detail";

        /// <summary>
        /// API server route URL for Journal-By Date-By Row Detail report
        /// </summary>
        public const string JournalByDateByRowDetailUrl = "reports/journal/by-date/by-row-detail";

        /// <summary>
        /// API client URL for Journal-By Date-By Ledger report
        /// </summary>
        public const string JournalByDateByLedger = "reports/journal/by-date/by-ledger";

        /// <summary>
        /// API server route URL for Journal-By Date-By Ledger report
        /// </summary>
        public const string JournalByDateByLedgerUrl = "reports/journal/by-date/by-ledger";

        /// <summary>
        /// API client URL for Journal-By Date-By Subsidiary report
        /// </summary>
        public const string JournalByDateBySubsidiary = "reports/journal/by-date/by-subsid";

        /// <summary>
        /// API server route URL for Journal-By Date-By Subsidiary report
        /// </summary>
        public const string JournalByDateBySubsidiaryUrl = "reports/journal/by-date/by-subsid";

        /// <summary>
        /// API client URL for Journal-By Date-Ledger Summary report
        /// </summary>
        public const string JournalByDateLedgerSummary = "reports/journal/by-date/summary";

        /// <summary>
        /// API server route URL for Journal-By Date-Ledger Summary report
        /// </summary>
        public const string JournalByDateLedgerSummaryUrl = "reports/journal/by-date/summary";

        /// <summary>
        /// API client URL for Journal-By Date-Ledger Summary By Date report
        /// </summary>
        public const string JournalByDateLedgerSummaryByDate = "reports/journal/by-date/sum-by-date";

        /// <summary>
        /// API server route URL for Journal-By Date-Ledger Summary By Date report
        /// </summary>
        public const string JournalByDateLedgerSummaryByDateUrl = "reports/journal/by-date/sum-by-date";

        /// <summary>
        /// API client URL for Journal-By Date-Monthly Ledger Summary report
        /// </summary>
        public const string JournalByDateMonthlyLedgerSummary = "reports/journal/by-date/sum-by-month";

        /// <summary>
        /// API server route URL for Journal-By Date-Monthly Ledger Summary report
        /// </summary>
        public const string JournalByDateMonthlyLedgerSummaryUrl = "reports/journal/by-date/sum-by-month";

        #endregion

        #region Journal By Date By Branch API

        /// <summary>
        /// API client URL for Journal-By Date-By Row-By Branch report
        /// </summary>
        public const string JournalByDateByRowByBranch = "reports/journal/by-date/by-row/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-By Row-By Branch report
        /// </summary>
        public const string JournalByDateByRowByBranchUrl = "reports/journal/by-date/by-row/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-By Row Detail-By Branch report
        /// </summary>
        public const string JournalByDateByRowDetailByBranch = "reports/journal/by-date/by-row-detail/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-By Row Detail-By Branch report
        /// </summary>
        public const string JournalByDateByRowDetailByBranchUrl = "reports/journal/by-date/by-row-detail/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-By Ledger-By Branch report
        /// </summary>
        public const string JournalByDateByLedgerByBranch = "reports/journal/by-date/by-ledger/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-By Ledger-By Branch report
        /// </summary>
        public const string JournalByDateByLedgerByBranchUrl = "reports/journal/by-date/by-ledger/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-By Subsidiary-By Branch report
        /// </summary>
        public const string JournalByDateBySubsidiaryByBranch = "reports/journal/by-date/by-subsid/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-By Subsidiary-By Branch report
        /// </summary>
        public const string JournalByDateBySubsidiaryByBranchUrl = "reports/journal/by-date/by-subsid/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByDateLedgerSummaryByBranch = "reports/journal/by-date/summary/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByDateLedgerSummaryByBranchUrl = "reports/journal/by-date/summary/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-Ledger Summary By Date-By Branch report
        /// </summary>
        public const string JournalByDateLedgerSummaryByDateByBranch = "reports/journal/by-date/sum-by-date/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-Ledger Summary By Date-By Branch report
        /// </summary>
        public const string JournalByDateLedgerSummaryByDateByBranchUrl = "reports/journal/by-date/sum-by-date/by-branch";

        /// <summary>
        /// API client URL for Journal-By Date-Monthly Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByDateMonthlyLedgerSummaryByBranch = "reports/journal/by-date/sum-by-month/by-branch";

        /// <summary>
        /// API server route URL for Journal-By Date-Monthly Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByDateMonthlyLedgerSummaryByBranchUrl = "reports/journal/by-date/sum-by-month/by-branch";

        #endregion

        #region Journal By No API

        /// <summary>
        /// API client URL for Journal-By No-By Row report
        /// </summary>
        public const string JournalByNoByRow = "reports/journal/by-no/by-row";

        /// <summary>
        /// API server route URL for Journal-By No-By Row report
        /// </summary>
        public const string JournalByNoByRowUrl = "reports/journal/by-no/by-row";

        /// <summary>
        /// API client URL for Journal-By No-By Row Detail report
        /// </summary>
        public const string JournalByNoByRowDetail = "reports/journal/by-no/by-row-detail";

        /// <summary>
        /// API server route URL for Journal-By No-By Row Detail report
        /// </summary>
        public const string JournalByNoByRowDetailUrl = "reports/journal/by-no/by-row-detail";

        /// <summary>
        /// API client URL for Journal-By No-By Ledger report
        /// </summary>
        public const string JournalByNoByLedger = "reports/journal/by-no/by-ledger";

        /// <summary>
        /// API server route URL for Journal-By No-By Ledger report
        /// </summary>
        public const string JournalByNoByLedgerUrl = "reports/journal/by-no/by-ledger";

        /// <summary>
        /// API client URL for Journal-By No-By Subsidiary report
        /// </summary>
        public const string JournalByNoBySubsidiary = "reports/journal/by-no/by-subsid";

        /// <summary>
        /// API server route URL for Journal-By No-By Subsidiary report
        /// </summary>
        public const string JournalByNoBySubsidiaryUrl = "reports/journal/by-no/by-subsid";

        /// <summary>
        /// API client URL for Journal-By No-Ledger Summary report
        /// </summary>
        public const string JournalByNoLedgerSummary = "reports/journal/by-no/summary";

        /// <summary>
        /// API server route URL for Journal-By No-Ledger Summary report
        /// </summary>
        public const string JournalByNoLedgerSummaryUrl = "reports/journal/by-no/summary";

        #endregion

        #region Journal By No By Branch API

        /// <summary>
        /// API client URL for Journal-By No-By Row-By Branch report
        /// </summary>
        public const string JournalByNoByRowByBranch = "reports/journal/by-no/by-row/by-branch";

        /// <summary>
        /// API server route URL for Journal-By No-By Row-By Branch report
        /// </summary>
        public const string JournalByNoByRowByBranchUrl = "reports/journal/by-no/by-row/by-branch";

        /// <summary>
        /// API client URL for Journal-By No-By Row Detail-By Branch report
        /// </summary>
        public const string JournalByNoByRowDetailByBranch = "reports/journal/by-no/by-row-detail/by-branch";

        /// <summary>
        /// API server route URL for Journal-By No-By Row Detail-By Branch report
        /// </summary>
        public const string JournalByNoByRowDetailByBranchUrl = "reports/journal/by-no/by-row-detail/by-branch";

        /// <summary>
        /// API client URL for Journal-By No-By Ledger-By Branch report
        /// </summary>
        public const string JournalByNoByLedgerByBranch = "reports/journal/by-no/by-ledger/by-branch";

        /// <summary>
        /// API server route URL for Journal-By No-By Ledger-By Branch report
        /// </summary>
        public const string JournalByNoByLedgerByBranchUrl = "reports/journal/by-no/by-ledger/by-branch";

        /// <summary>
        /// API client URL for Journal-By No-By Subsidiary-By Branch report
        /// </summary>
        public const string JournalByNoBySubsidiaryByBranch = "reports/journal/by-no/by-subsid/by-branch";

        /// <summary>
        /// API server route URL for Journal-By No-By Subsidiary-By Branch report
        /// </summary>
        public const string JournalByNoBySubsidiaryByBranchUrl = "reports/journal/by-no/by-subsid/by-branch";

        /// <summary>
        /// API client URL for Journal-By No-Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByNoLedgerSummaryByBranch = "reports/journal/by-no/summary/by-branch";

        /// <summary>
        /// API server route URL for Journal-By No-Ledger Summary-By Branch report
        /// </summary>
        public const string JournalByNoLedgerSummaryByBranchUrl = "reports/journal/by-no/summary/by-branch";

        #endregion
    }
}
