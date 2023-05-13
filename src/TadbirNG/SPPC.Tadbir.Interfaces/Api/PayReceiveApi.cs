using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with pays and receivals.
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
        public const string PaymentUrl = "payments/{payReceivalId:min(1)}";

        /// <summary>
        /// API client URL for all receival items
        /// </summary>
        public const string Receivals = "Receivals";

        /// <summary>
        /// API server route URL for all receival items
        /// </summary>
        public const string ReceivalsUrl = "receivals";

        /// <summary>
        /// API client URL for a receival item specified by unique identifier
        /// </summary>
        public const string Receival = "receivals/{0}";

        /// <summary>
        /// API server route URL for a receival item specified by unique identifier
        /// </summary>
        public const string ReceivalUrl = "Receivals/{payReceivalId:min(1)}";

    }
}
