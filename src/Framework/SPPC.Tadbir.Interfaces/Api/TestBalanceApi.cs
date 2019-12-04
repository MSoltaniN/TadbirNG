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

        #region Specific Level reports

        /// <summary>
        /// Client URL for two-column test balance report for accounts in specified level
        /// </summary>
        public const string TwoColumnLevelBalance = "testbal/levels/{0}/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for accounts in specified level
        /// </summary>
        public const string TwoColumnLevelBalanceUrl = "testbal/levels/{level:min(1)}/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for accounts in specified level
        /// </summary>
        public const string FourColumnLevelBalance = "testbal/levels/{0}/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for accounts in specified level
        /// </summary>
        public const string FourColumnLevelBalanceUrl = "testbal/levels/{level:min(1)}/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for accounts in specified level
        /// </summary>
        public const string SixColumnLevelBalance = "testbal/views/{0}/levels/{0}/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for accounts in specified level
        /// </summary>
        public const string SixColumnLevelBalanceUrl = "testbal/views/{viewId:min(1)}/levels/{level:min(1)}/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for accounts in specified level
        /// </summary>
        public const string EightColumnLevelBalance = "testbal/levels/{0}/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for accounts in specified level
        /// </summary>
        public const string EightColumnLevelBalanceUrl = "testbal/levels/{level:min(1)}/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for accounts in specified level
        /// </summary>
        public const string TenColumnLevelBalance = "testbal/levels/{0}/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for accounts in specified level
        /// </summary>
        public const string TenColumnLevelBalanceUrl = "testbal/levels/{level:min(1)}/10-col";

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
