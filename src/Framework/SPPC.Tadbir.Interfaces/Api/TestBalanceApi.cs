using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines client and server route URLs for all Test Balance reports
    /// </summary>
    public sealed class TestBalanceApi
    {
        private TestBalanceApi()
        {
        }

        /// <summary>
        /// Client URL for test balance types, based on account coding hierarchy
        /// </summary>
        public const string TestBalanceTypeLookup = "testbal/lookup/types";

        /// <summary>
        /// Server route URL for test balance types, based on account coding hierarchy
        /// </summary>
        public const string TestBalanceTypeLookupUrl = "testbal/lookup/types";

        #region Ledger Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all ledger level accounts
        /// </summary>
        public const string TwoColumnLedgerBalance = "testbal/ledger/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all ledger level accounts
        /// </summary>
        public const string TwoColumnLedgerBalanceUrl = "testbal/ledger/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all ledger level accounts
        /// </summary>
        public const string FourColumnLedgerBalance = "testbal/ledger/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all ledger level accounts
        /// </summary>
        public const string FourColumnLedgerBalanceUrl = "testbal/ledger/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all ledger level accounts
        /// </summary>
        public const string SixColumnLedgerBalance = "testbal/ledger/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all ledger level accounts
        /// </summary>
        public const string SixColumnLedgerBalanceUrl = "testbal/ledger/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all ledger level accounts
        /// </summary>
        public const string EightColumnLedgerBalance = "testbal/ledger/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all ledger level accounts
        /// </summary>
        public const string EightColumnLedgerBalanceUrl = "testbal/ledger/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all ledger level accounts
        /// </summary>
        public const string TenColumnLedgerBalance = "testbal/ledger/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all ledger level accounts
        /// </summary>
        public const string TenColumnLedgerBalanceUrl = "testbal/ledger/10-col";

        #endregion

        #region Subsidiary Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string TwoColumnSubsidiaryBalance = "testbal/subsid/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string TwoColumnSubsidiaryBalanceUrl = "testbal/subsid/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string FourColumnSubsidiaryBalance = "testbal/subsid/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string FourColumnSubsidiaryBalanceUrl = "testbal/subsid/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string SixColumnSubsidiaryBalance = "testbal/subsid/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string SixColumnSubsidiaryBalanceUrl = "testbal/subsid/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string EightColumnSubsidiaryBalance = "testbal/subsid/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string EightColumnSubsidiaryBalanceUrl = "testbal/subsid/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string TenColumnSubsidiaryBalance = "testbal/subsid/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all subsidiary level accounts
        /// </summary>
        public const string TenColumnSubsidiaryBalanceUrl = "testbal/subsid/10-col";

        #endregion

        #region Detail Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all detail level accounts
        /// </summary>
        public const string TwoColumnDetailBalance = "testbal/detail/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all detail level accounts
        /// </summary>
        public const string TwoColumnDetailBalanceUrl = "testbal/detail/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all detail level accounts
        /// </summary>
        public const string FourColumnDetailBalance = "testbal/detail/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all detail level accounts
        /// </summary>
        public const string FourColumnDetailBalanceUrl = "testbal/detail/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all detail level accounts
        /// </summary>
        public const string SixColumnDetailBalance = "testbal/detail/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all detail level accounts
        /// </summary>
        public const string SixColumnDetailBalanceUrl = "testbal/detail/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all detail level accounts
        /// </summary>
        public const string EightColumnDetailBalance = "testbal/detail/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all detail level accounts
        /// </summary>
        public const string EightColumnDetailBalanceUrl = "testbal/detail/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all detail level accounts
        /// </summary>
        public const string TenColumnDetailBalance = "testbal/detail/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all detail level accounts
        /// </summary>
        public const string TenColumnDetailBalanceUrl = "testbal/detail/10-col";

        #endregion

        #region Child Items Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all descendants of an account
        /// </summary>
        public const string TwoColumnChildItemsBalance = "testbal/{0}/items/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all descendants of an account
        /// </summary>
        public const string TwoColumnChildItemsBalanceUrl = "testbal/{accountId:min(1)}/items/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all descendants of an account
        /// </summary>
        public const string FourColumnChildItemsBalance = "testbal/{0}/items/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all descendants of an account
        /// </summary>
        public const string FourColumnChildItemsBalanceUrl = "testbal/{accountId:min(1)}/items/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all descendants of an account
        /// </summary>
        public const string SixColumnChildItemsBalance = "testbal/{0}/items/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all descendants of an account
        /// </summary>
        public const string SixColumnChildItemsBalanceUrl = "testbal/{accountId:min(1)}/items/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all descendants of an account
        /// </summary>
        public const string EightColumnChildItemsBalance = "testbal/{0}/items/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all descendants of an account
        /// </summary>
        public const string EightColumnChildItemsBalanceUrl = "testbal/{accountId:min(1)}/items/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all descendants of an account
        /// </summary>
        public const string TenColumnChildItemsBalance = "testbal/{0}/items/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all descendants of an account
        /// </summary>
        public const string TenColumnChildItemsBalanceUrl = "testbal/{accountId:min(1)}/items/10-col";

        #endregion
    }
}
