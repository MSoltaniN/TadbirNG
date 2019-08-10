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

        #region Ledger Items Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string TwoColumnLedgerItemsBalance = "testbal/ledger/{0}/items/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string TwoColumnLedgerItemsBalanceUrl = "testbal/ledger/{accountId:min(1)}/items/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string FourColumnLedgerItemsBalance = "testbal/ledger/{0}/items/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string FourColumnLedgerItemsBalanceUrl = "testbal/ledger/{accountId:min(1)}/items/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string SixColumnLedgerItemsBalance = "testbal/ledger/{0}/items/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string SixColumnLedgerItemsBalanceUrl = "testbal/ledger/{accountId:min(1)}/items/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string EightColumnLedgerItemsBalance = "testbal/ledger/{0}/items/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string EightColumnLedgerItemsBalanceUrl = "testbal/ledger/{accountId:min(1)}/items/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string TenColumnLedgerItemsBalance = "testbal/ledger/{0}/items/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all descendants of a ledger account
        /// </summary>
        public const string TenColumnLedgerItemsBalanceUrl = "testbal/ledger/{accountId:min(1)}/items/10-col";

        #endregion

        #region Subsidiary Items Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string TwoColumnSubsidiaryItemsBalance = "testbal/subsid/{0}/items/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string TwoColumnSubsidiaryItemsBalanceUrl = "testbal/subsid/{accountId:min(1)}/items/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string FourColumnSubsidiaryItemsBalance = "testbal/subsid/{0}/items/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string FourColumnSubsidiaryItemsBalanceUrl = "testbal/subsid/{accountId:min(1)}/items/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string SixColumnSubsidiaryItemsBalance = "testbal/subsid/{0}/items/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string SixColumnSubsidiaryItemsBalanceUrl = "testbal/subsid/{accountId:min(1)}/items/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string EightColumnSubsidiaryItemsBalance = "testbal/subsid/{0}/items/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string EightColumnSubsidiaryItemsBalanceUrl = "testbal/subsid/{accountId:min(1)}/items/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string TenColumnSubsidiaryItemsBalance = "testbal/subsid/{0}/items/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all descendants of a subsidiary account
        /// </summary>
        public const string TenColumnSubsidiaryItemsBalanceUrl = "testbal/subsid/{accountId:min(1)}/items/10-col";

        #endregion

        #region DetailAccount Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string TwoColumnDetailLevelBalance = "testbal/detail/level/{0}/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string TwoColumnDetailLevelBalanceUrl = "testbal/detail/level/{levelId:min(1)}/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string FourColumnDetailLevelBalance = "testbal/detail/level/{0}/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string FourColumnDetailLevelBalanceUrl = "testbal/detail/level/{levelId:min(1)}/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string SixColumnDetailLevelBalance = "testbal/detail/level/{0}/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string SixColumnDetailLevelBalanceUrl = "testbal/detail/level/{levelId:min(1)}/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string EightColumnDetailLevelBalance = "testbal/detail/level/{0}/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string EightColumnDetailLevelBalanceUrl = "testbal/detail/level/{levelId:min(1)}/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string TenColumnDetailLevelBalance = "testbal/detail/level/{0}/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all detail accounts in a specific level
        /// </summary>
        public const string TenColumnDetailLevelBalanceUrl = "testbal/detail/level/{levelId:min(1)}/10-col";

        #endregion
    }
}
