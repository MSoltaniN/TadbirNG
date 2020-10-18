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
        public const string ProfitLoss = "profit-loss";

        /// <summary>
        /// API server route URL for profit-loss report
        /// </summary>
        public const string ProfitLossUrl = "profit-loss";

        /// <summary>
        /// API client URL for profit-loss report : comparison by multiple cost centers
        /// </summary>
        public const string ProfitLossByCostCenters = "profit-loss/by-ccenters";

        /// <summary>
        /// API server route URL for profit-loss report : comparison by multiple cost centers
        /// </summary>
        public const string ProfitLossByCostCentersUrl = "profit-loss/by-ccenters";

        /// <summary>
        /// API client URL for profit-loss report : comparison by multiple projects
        /// </summary>
        public const string ProfitLossByProjects = "profit-loss/by-projects";

        /// <summary>
        /// API server route URL for profit-loss report : comparison by multiple projects
        /// </summary>
        public const string ProfitLossByProjectsUrl = "profit-loss/by-projects";

        /// <summary>
        /// API client URL for profit-loss report : comparison by multiple branches
        /// </summary>
        public const string ProfitLossByBranches = "profit-loss/by-branches";

        /// <summary>
        /// API server route URL for profit-loss report : comparison by multiple branches
        /// </summary>
        public const string ProfitLossByBranchesUrl = "profit-loss/by-branches";

        /// <summary>
        /// API client URL for profit-loss report : comparison by multiple fiscal periods
        /// </summary>
        public const string ProfitLossByFiscalPeriods = "profit-loss/by-fperiods";

        /// <summary>
        /// API server route URL for profit-loss report : comparison by multiple fiscal periods
        /// </summary>
        public const string ProfitLossByFiscalPeriodsUrl = "profit-loss/by-fperiods";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimple = "profit-loss/simple";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleUrl = "profit-loss/simple";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByCostCenters = "profit-loss/simple/by-ccenters";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByCostCentersUrl = "profit-loss/simple/by-ccenters";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByProjects = "profit-loss/simple/by-projects";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByProjectsUrl = "profit-loss/simple/by-projects";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByBranches = "profit-loss/simple/by-branches";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByBranchesUrl = "profit-loss/simple/by-branches";

        /// <summary>
        /// API client URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByFiscalPeriods = "profit-loss/simple/by-fperiods";

        /// <summary>
        /// API server route URL for profit-loss report in a specific date
        /// </summary>
        public const string ProfitLossSimpleByFiscalPeriodsUrl = "profit-loss/simple/by-fperiods";
    }
}
