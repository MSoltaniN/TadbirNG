using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with currencies.
    /// </summary>
    public sealed class CurrencyApi
    {
        /// <summary>
        /// API client URL for all currency items
        /// </summary>
        public const string Currencies = "currencies";

        /// <summary>
        /// API server route URL for all currency items
        /// </summary>
        public const string CurrenciesUrl = "currencies";

        /// <summary>
        /// API client URL for a currency item specified by unique identifier
        /// </summary>
        public const string Currency = "currencies/{0}";

        /// <summary>
        /// API server route URL for a currency item specified by unique identifier
        /// </summary>
        public const string CurrencyUrl = "currencies/{currencyId:min(1)}";

        /// <summary>
        /// API client URL for all rates of a currency item specified by unique identifier
        /// </summary>
        public const string CurrencyRates = "currencies/{0}/rates";

        /// <summary>
        /// API server route URL for all rates of a currency item specified by unique identifier
        /// </summary>
        public const string CurrencyRatesUrl = "currencies/{currencyId:min(1)}/rates";

        /// <summary>
        /// API client URL for currency information by name key
        /// </summary>
        public const string CurrencyInfoByName = "currencies/info/{0}";

        /// <summary>
        /// API server route URL for currency information by name key
        /// </summary>
        public const string CurrencyInfoByNameUrl = "currencies/info/{nameKey}";

        /// <summary>
        /// API client URL for all currency names as key-value items
        /// </summary>
        public const string CurrencyNamesLookup = "currencies/names/lookup";

        /// <summary>
        /// API server route URL for all currency names as key-value items
        /// </summary>
        public const string CurrencyNamesLookupUrl = "currencies/names/lookup";
    }
}
