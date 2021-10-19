using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines client and server route URLs for all Item Turnover and Balance reports
    /// </summary>
    public sealed class ItemBalanceApi
    {
        private ItemBalanceApi()
        {
        }

        /// <summary>
        /// Client URL for test balance types, based on account item coding hierarchy
        /// </summary>
        public const string ItemBalanceTypeLookup = "itembal/views/{0}/lookup/types";

        /// <summary>
        /// Server route URL for test balance types, based on account item coding hierarchy
        /// </summary>
        public const string ItemBalanceTypeLookupUrl = "itembal/views/{viewId:min(2)}/lookup/types";

        #region Specific Level reports

        /// <summary>
        /// Client URL for two-column test balance report for account items in specified level
        /// </summary>
        public const string TwoColumnLevelBalance = "itembal/views/{0}/levels/{1}/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for account items in specified level
        /// </summary>
        public const string TwoColumnLevelBalanceUrl = "itembal/views/{viewId:min(2)}/levels/{level:min(1)}/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for account items in specified level
        /// </summary>
        public const string FourColumnLevelBalance = "itembal/views/{0}/levels/{1}/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for account items in specified level
        /// </summary>
        public const string FourColumnLevelBalanceUrl = "itembal/views/{viewId:min(2)}/levels/{level:min(1)}/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for account items in specified level
        /// </summary>
        public const string SixColumnLevelBalance = "itembal/views/{0}/levels/{1}/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for account items in specified level
        /// </summary>
        public const string SixColumnLevelBalanceUrl = "itembal/views/{viewId:min(2)}/levels/{level:min(1)}/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for account items in specified level
        /// </summary>
        public const string EightColumnLevelBalance = "itembal/views/{0}/levels/{1}/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for account items in specified level
        /// </summary>
        public const string EightColumnLevelBalanceUrl = "itembal/views/{viewId:min(2)}/levels/{level:min(1)}/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for account items in specified level
        /// </summary>
        public const string TenColumnLevelBalance = "itembal/views/{0}/levels/{1}/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for account items in specified level
        /// </summary>
        public const string TenColumnLevelBalanceUrl = "itembal/views/{viewId:min(2)}/levels/{level:min(1)}/10-col";

        #endregion

        #region Child Items Level reports

        /// <summary>
        /// Client URL for two-column test balance report for all descendants of an account item
        /// </summary>
        public const string TwoColumnChildItemsBalance = "itembal/views/{0}/items/{1}/2-col";

        /// <summary>
        /// Server route URL for two-column test balance report for all descendants of an account item
        /// </summary>
        public const string TwoColumnChildItemsBalanceUrl = "itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/2-col";

        /// <summary>
        /// Client URL for four-column test balance report for all descendants of an account item
        /// </summary>
        public const string FourColumnChildItemsBalance = "itembal/views/{0}/items/{1}/4-col";

        /// <summary>
        /// Server route URL for four-column test balance report for all descendants of an account item
        /// </summary>
        public const string FourColumnChildItemsBalanceUrl = "itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/4-col";

        /// <summary>
        /// Client URL for six-column test balance report for all descendants of an account item
        /// </summary>
        public const string SixColumnChildItemsBalance = "itembal/views/{0}/items/{1}/6-col";

        /// <summary>
        /// Server route URL for six-column test balance report for all descendants of an account item
        /// </summary>
        public const string SixColumnChildItemsBalanceUrl = "itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/6-col";

        /// <summary>
        /// Client URL for eight-column test balance report for all descendants of an account item
        /// </summary>
        public const string EightColumnChildItemsBalance = "itembal/views/{0}/items/{1}/8-col";

        /// <summary>
        /// Server route URL for eight-column test balance report for all descendants of an account item
        /// </summary>
        public const string EightColumnChildItemsBalanceUrl = "itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/8-col";

        /// <summary>
        /// Client URL for ten-column test balance report for all descendants of an account item
        /// </summary>
        public const string TenColumnChildItemsBalance = "itembal/views/{0}/items/{1}/10-col";

        /// <summary>
        /// Server route URL for ten-column test balance report for all descendants of an account item
        /// </summary>
        public const string TenColumnChildItemsBalanceUrl = "itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/10-col";

        #endregion
    }
}
