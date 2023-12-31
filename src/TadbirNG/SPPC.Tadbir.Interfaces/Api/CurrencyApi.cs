﻿using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with currencies.
    /// </summary>
    public sealed class CurrencyApi
    {
        /// <summary>
        /// API client URL for all active currency items
        /// </summary>
        public const string Currencies = "currencies";

        /// <summary>
        /// API server route URL for all active currency items
        /// </summary>
        public const string CurrenciesUrl = "currencies";

        /// <summary>
        /// API client URL for all inactive currency items
        /// </summary>
        public const string InactiveCurrencies = "currencies/inactive";

        /// <summary>
        /// API server route URL for all inactive currency items
        /// </summary>
        public const string InactiveCurrenciesUrl = "currencies/inactive";

        /// <summary>
        /// API client URL for all currency items
        /// </summary>
        public const string AllCurrencies = "currencies/all";

        /// <summary>
        /// API server route URL for all currency items
        /// </summary>
        public const string AllCurrenciesUrl = "currencies/all";

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
        /// API client URL for a currency rate item specified by unique identifier
        /// </summary>
        public const string CurrencyRate = "currencies/rates/{0}";

        /// <summary>
        /// API server route URL for a currency rate item specified by unique identifier
        /// </summary>
        public const string CurrencyRateUrl = "currencies/rates/{rateId:min(1)}";

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

        /// <summary>
        /// API client URL for default currency usable for an account and a detail account
        /// </summary>
        public const string DefaultCurrencyByFullAccount = "currencies/default/account/{0}/faccount/{1}";

        /// <summary>
        /// API client URL for default currency usable for an account and a detail account
        /// </summary>
        public const string DefaultCurrencyByFullAccountUrl = "currencies/default/account/{accountId:min(1)}/faccount/{faccountId:min(0)}";

        /// <summary>
        /// API client URL for all tax currencies (as defined by formal authorities)
        /// </summary>
        public const string TaxCurrencies = "currencies/tax";

        /// <summary>
        /// API server route URL for all tax currencies (as defined by formal authorities)
        /// </summary>
        public const string TaxCurrenciesUrl = "currencies/tax";

        /// <summary>
        /// API client URL for default currency
        /// </summary>
        public const string DefaultCurrency = "currencies/default/{0}";

        /// <summary>
        /// API server route URL for default currency
        /// </summary>
        public const string DefaultCurrencyUrl = "currencies/default/{nameKey}";

        /// <summary>
        /// API client URL for for a currency has rate
        /// </summary>
        public const string CurrencyHasRates = "currencies/{0}/has-rates";

        /// <summary>
        /// API server route URL for a currency has rate
        /// </summary>
        public const string CurrencyHasRatesUrl = "currencies/{currencyId:min(1)}/has-rates";

        /// <summary>
        /// API client URL for delete selected rates of a currency
        /// </summary>
        public const string DeleteCurrencyRates = "currency/rates";

        /// <summary>
        /// API server route URL for delete selected rates of a currency
        /// </summary>
        public const string DeleteCurrencyRatesUrl = "currency/rates";

        /// <summary>
        /// API client URL for marking an active currency as inactive
        /// </summary>
        public const string DeactivateCurrency = "currencies/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active currency as inactive
        /// </summary>
        public const string DeactivateCurrencyUrl = "currencies/{currencyId:min(1)}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive currency as active
        /// </summary>
        public const string ReactivateCurrency = "currencies/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive currency as active
        /// </summary>
        public const string ReactivateCurrencyUrl = "currencies/{currencyId:min(1)}/reactivate";
    }
}
