namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with account collections.
    /// </summary>
    public sealed class AccountCollectionApi
    {
        private AccountCollectionApi()
        {
        }

        /// <summary>
        /// API client URL for account collections
        /// </summary>
        public const string AccountCollections = "acc-collections";

        /// <summary>
        /// API server route URL for account collections
        /// </summary>
        public const string AccountCollectionsUrl = "acc-collections";

        /// <summary>
        /// API client URL for account collection items
        /// </summary>
        public const string AccountCollectionAccounts = "acc-collections/{0}/accounts";

        /// <summary>
        /// API server route URL for account collection items
        /// </summary>
        public const string AccountCollectionAccountsUrl = "acc-collections/{collectionId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts currently assigned to Cash or Bank collections
        /// </summary>
        public const string CashBankAccounts = "acc-collections/cash-bank/accounts";

        /// <summary>
        /// API server route URL for all accounts currently assigned to Cash or Bank collections
        /// </summary>
        public const string CashBankAccountsUrl = "acc-collections/cash-bank/accounts";
    }
}
