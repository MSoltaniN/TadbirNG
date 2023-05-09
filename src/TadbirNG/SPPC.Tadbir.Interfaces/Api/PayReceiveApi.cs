using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with pays and receives.
    /// </summary>
    public sealed class PayReceiveApi
    {
        /// <summary>
        /// API client URL for all pay items
        /// </summary>
        public const string Pays = "pay";

        /// <summary>
        /// API server route URL for all pay items
        /// </summary>
        public const string PaysUrl = "pays";

        /// <summary>
        /// API client URL for a pay item specified by unique identifier
        /// </summary>
        public const string Pay = "pays/{0}";

        /// <summary>
        /// API server route URL for a pay item specified by unique identifier
        /// </summary>
        public const string PayUrl = "pays/{payReceiveId:min(1)}";

        /// <summary>
        /// API client URL for all receive items
        /// </summary>
        public const string Receives = "Receives";

        /// <summary>
        /// API server route URL for all receive items
        /// </summary>
        public const string ReceivesUrl = "receives";

        /// <summary>
        /// API client URL for a receive item specified by unique identifier
        /// </summary>
        public const string Receive = "receives/{0}";

        /// <summary>
        /// API server route URL for a receive item specified by unique identifier
        /// </summary>
        public const string ReceiveUrl = "Receives/{payReceiveId:min(1)}";

    }
}
