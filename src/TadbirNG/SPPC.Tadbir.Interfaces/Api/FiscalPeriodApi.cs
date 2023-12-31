﻿namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with fiscal periods.
    /// </summary>
    public sealed class FiscalPeriodApi
    {
        private FiscalPeriodApi()
        {
        }

        /// <summary>
        /// API client URL for all fiscal period items
        /// </summary>
        public const string FiscalPeriods = "fperiods";

        /// <summary>
        /// API server route URL for all fiscal period items
        /// </summary>
        public const string FiscalPeriodsUrl = "fperiods";

        /// <summary>
        /// API client URL for a fiscal period item specified by unique identifier
        /// </summary>
        public const string FiscalPeriod = "fperiods/{0}";

        /// <summary>
        /// API server route URL for a fiscal period item specified by unique identifier
        /// </summary>
        public const string FiscalPeriodUrl = "fperiods/{fpId:min(1)}";

        /// <summary>
        /// API client URL for a fiscal period item specified by unique identifier and every data that depends on it
        /// </summary>
        public const string FiscalPeriodData = "fperiods/{0}/data";

        /// <summary>
        /// API server route URL for a fiscal period item specified by unique identifier and every data that depends on it
        /// </summary>
        public const string FiscalPeriodDataUrl = "fperiods/{fpId:min(1)}/data";

        /// <summary>
        /// API client URL for all roles that can access a fiscal period item specified by unique identifier
        /// </summary>
        public const string FiscalPeriodRoles = "fperiods/{0}/roles";

        /// <summary>
        /// API server route URL for all roles that can access a fiscal period item specified by unique identifier
        /// </summary>
        public const string FiscalPeriodRolesUrl = "fperiods/{fpId:min(1)}/roles";
    }
}
