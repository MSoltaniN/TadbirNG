using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines client and server URLs for balance sheet report
    /// </summary>
    public sealed class BalanceSheetApi
    {
        private BalanceSheetApi()
        {
        }

        /// <summary>
        /// API client URL for balance sheet report
        /// </summary>
        public const string BalanceSheet = "bal-sheet";

        /// <summary>
        /// API srver route URL for balance sheet report
        /// </summary>
        public const string BalanceSheetUrl = "bal-sheet";
    }
}
