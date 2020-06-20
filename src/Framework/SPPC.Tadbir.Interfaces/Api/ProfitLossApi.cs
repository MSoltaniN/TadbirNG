using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for profit/loss report
    /// </summary>
    public sealed class ProfitLossApi
    {
        private ProfitLossApi()
        {
        }

        /// <summary>
        /// API client URL for profit-loss report
        /// </summary>
        public const string ProfitLoss = "profitloss";

        /// <summary>
        /// API server route URL for profit-loss report
        /// </summary>
        public const string ProfitLossUrl = "profitloss";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimple = "profitloss/simple";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleUrl = "profitloss/simple";
    }
}
