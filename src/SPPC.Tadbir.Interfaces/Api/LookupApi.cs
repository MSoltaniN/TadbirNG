﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with entity lookup collections.
    /// </summary>
    public sealed class LookupApi
    {
        private LookupApi()
        {
        }

        /// <summary>
        /// API client URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccounts = "lookup/accounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsUrl = "lookup/accounts/fp/{fpId:int}/branch/{branchId:int}";

        /// <summary>
        /// API client URL for lookup collection of all currencies
        /// </summary>
        public const string Currencies = "lookup/currencies";

        /// <summary>
        /// API server route URL for lookup collection of all currencies
        /// </summary>
        public const string CurrenciesUrl = "lookup/currencies";
    }
}
