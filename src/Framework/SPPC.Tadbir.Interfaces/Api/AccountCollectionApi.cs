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
        public const string AccountCollectionAccount = "acccollections/collection/{0}";

        /// <summary>
        /// API server route URL for account collection items
        /// </summary>
        public const string AccountCollectionAccountUrl = "acccollections/collection/{collectionId:min(1)}";

        /// <summary>
        /// API client URL for account collection metadata
        /// </summary>
        public const string AccountCollectionMetadata = "acccollections/metadata";

        /// <summary>
        /// API server route URL for account collection metadata
        /// </summary>
        public const string AccountCollectionMetadataUrl = "acccollections/metadata";
    }
}
