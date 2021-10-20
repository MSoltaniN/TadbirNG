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
        public const string AccountCollections = "acccollections";

        /// <summary>
        /// API server route URL for account collections
        /// </summary>
        public const string AccountCollectionsUrl = "acccollections";

        /// <summary>
        /// API client URL for account collection items
        /// </summary>
        public const string AccountCollectionAccounts = "acccollections/{0}/accounts";

        /// <summary>
        /// API server route URL for account collection items
        /// </summary>
        public const string AccountCollectionAccountsUrl = "acccollections/{collectionId:min(1)}/accounts";
    }
}
