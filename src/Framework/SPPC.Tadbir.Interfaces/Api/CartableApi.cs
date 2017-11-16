using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with work item store (cartable).
    /// </summary>
    public sealed class CartableApi
    {
        private CartableApi()
        {
        }

        /// <summary>
        /// API client URL for all cartable items
        /// </summary>
        public const string WorkItems = "cartable";

        /// <summary>
        /// API server route URL for all cartable items
        /// </summary>
        public const string WorkItemsUrl = "cartable";

        /// <summary>
        /// API client URL for all work items in inbound cartable.
        /// </summary>
        public const string UserInboxItems = "cartable/inbox/{0}";

        /// <summary>
        /// API server route URL for all work items in inbound cartable.
        /// </summary>
        public const string UserInboxItemsUrl = "cartable/inbox/{userId:int}";

        /// <summary>
        /// API client URL for all work items in outbound cartable.
        /// </summary>
        public const string UserOutboxItems = "cartable/outbox/{0}";

        /// <summary>
        /// API server route URL for all work items in outbound cartable.
        /// </summary>
        public const string UserOutboxItemsUrl = "cartable/outbox/{userId:int}";
    }
}
