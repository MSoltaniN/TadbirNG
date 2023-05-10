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
        /// API client URL for all payment items
        /// </summary>
        public const string Payments = "payment";

        /// <summary>
        /// API server route URL for all payment items
        /// </summary>
        public const string PaymentsUrl = "payments";

        /// <summary>
        /// API client URL for a payment item specified by unique identifier
        /// </summary>
        public const string Payment = "payments/{0}";

        /// <summary>
        /// API server route URL for a payment item specified by unique identifier
        /// </summary>
        public const string PaymentUrl = "payments/{payReceiveId:min(1)}";

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
