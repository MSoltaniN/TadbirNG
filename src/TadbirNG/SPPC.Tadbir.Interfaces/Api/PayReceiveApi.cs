using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with payments and receivals.
    /// </summary>
    public sealed class PayReceiveApi
    {
        /// <summary>
        /// API client URL for all payment items
        /// </summary>
        public const string Payments = "payments";

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
        /// API client URL for all receival items
        /// </summary>
        public const string Receivals = "receivals";

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
        public const string ReceivalUrl = "receivals/{payReceiveId:min(1)}";

        /// <summary>
        /// API client URL for confirming a single payment specified by identifier
        /// </summary>
        public const string ConfirmPayment = "payments/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single payment specified by identifier
        /// </summary>
        public const string ConfirmPaymentUrl = "payments/{payReceiveId:min(1)}/confirm";

        /// <summary>
        /// API client URL for undoing confirmation of a single payment specified by identifier
        /// </summary>
        public const string UndoConfirmPayment = "payments/{0}/confirm/undo";

        /// <summary>
        /// API server route URL for undoing confirmation of a single payment specified by identifier
        /// </summary>
        public const string UndoConfirmPaymentUrl = "payments/{payReceiveId:min(1)}/confirm/undo";

        /// <summary>
        /// API client URL for approving a single payment specified by identifier
        /// </summary>
        public const string ApprovePayment = "payments/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single payment specified by identifier
        /// </summary>
        public const string ApprovePaymentUrl = "payments/{payReceiveId:min(1)}/approve";

        /// <summary>
        /// API client URL for undoing approval of a single payment specified by identifier
        /// </summary>
        public const string UndoApprovePayment = "payments/{0}/approve/undo";

        /// <summary>
        /// API server route URL for undoing approval of a single payment specified by identifier
        /// </summary>
        public const string UndoApprovePaymentUrl = "payments/{payReceiveId:min(1)}/approve/undo";

        /// <summary>
        /// API client URL for confirming a single receival specified by identifier
        /// </summary>
        public const string ConfirmReceival = "receivals/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single receival specified by identifier
        /// </summary>
        public const string ConfirmReceivalUrl = "receivals/{payReceiveId:min(1)}/confirm";

        /// <summary>
        /// API client URL for undoing confirmation of a single receival specified by identifier
        /// </summary>
        public const string UndoConfirmReceival = "receivals/{0}/confirm/undo";

        /// <summary>
        /// API server route URL for undoing confirmation of a single receival specified by identifier
        /// </summary>
        public const string UndoConfirmReceivalUrl = "receivals/{payReceiveId:min(1)}/confirm/undo";

        /// <summary>
        /// API client URL for approving a single receival specified by identifier
        /// </summary>
        public const string ApproveReceival = "receivals/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single receival specified by identifier
        /// </summary>
        public const string ApproveReceivalUrl = "receivals/{payReceiveId:min(1)}/approve";

        /// <summary>
        /// API client URL for undoing approval of a single receival specified by identifier
        /// </summary>
        public const string UndoApproveReceival = "receivals/{0}/approve/undo";

        /// <summary>
        /// API server route URL for undoing approval of a single receival specified by identifier
        /// </summary>
        public const string UndoApproveReceivalUrl = "receivals/{payReceiveId:min(1)}/approve/undo";

        /// <summary>
        /// API client URL for a single payment specified by number
        /// </summary>
        public const string PaymentByNo = "payments/by-no/{0}";

        /// <summary>
        /// API server route URL for a single payment specified by number
        /// </summary>
        public const string PaymentByNoUrl = "payments/by-no/{payReceiveNo:min(1)}";

        /// <summary>
        /// API client URL for the first payment in current environment
        /// </summary>
        public const string FirstPayment = "payments/first";

        /// <summary>
        /// API server route URL for the first payment in current environment
        /// </summary>
        public const string FirstPaymentUrl = "payments/first";

        /// <summary>
        /// API client URL for previous payment in current environment
        /// </summary>
        public const string PreviousPayment = "payments/{0}/previous";

        /// <summary>
        /// API server route URL for previous payment in current environment
        /// </summary>
        public const string PreviousPaymentUrl = "payments/{payReceiveNo:min(1)}/previous";

        /// <summary>
        /// API client URL for next payment in current environment
        /// </summary>
        public const string NextPayment = "payments/{0}/next";

        /// <summary>
        /// API server route URL for next payment in current environment
        /// </summary>
        public const string NextPaymentUrl = "payments/{payReceiveNo:min(1)}/next";

        /// <summary>
        /// API client URL for the last payment in current environment
        /// </summary>
        public const string LastPayment = "payments/last";

        /// <summary>
        /// API server route URL for the last payment in current environment
        /// </summary>
        public const string LastPaymentUrl = "payments/last";

        /// <summary>
        /// API client URL for a single receival specified by number
        /// </summary>
        public const string ReceivalByNo = "receivals/by-no/{0}";

        /// <summary>
        /// API server route URL for a single receival specified by number
        /// </summary>
        public const string ReceivalByNoUrl = "receivals/by-no/{payReceiveNo:min(1)}";

        /// <summary>
        /// API client URL for the first receival in current environment
        /// </summary>
        public const string FirstReceival = "receivals/first";

        /// <summary>
        /// API server route URL for the first receival in current environment
        /// </summary>
        public const string FirstReceivalUrl = "receivals/first";

        /// <summary>
        /// API client URL for previous receival in current environment
        /// </summary>
        public const string PreviousReceival = "receivals/{0}/previous";

        /// <summary>
        /// API server route URL for previous receival in current environment
        /// </summary>
        public const string PreviousReceivalUrl = "receivals/{payReceiveNo:min(1)}/previous";

        /// <summary>
        /// API client URL for next receival in current environment
        /// </summary>
        public const string NextReceival = "receivals/{0}/next";

        /// <summary>
        /// API server route URL for next receival in current environment
        /// </summary>
        public const string NextReceivalUrl = "receivals/{payReceiveNo:min(1)}/next";

        /// <summary>
        /// API client URL for the last receival in current environment
        /// </summary>
        public const string LastReceival = "receivals/last";

        /// <summary>
        /// API server route URL for the last receival in current environment
        /// </summary>
        public const string LastReceivalUrl = "receivals/last";

        /// <summary>
        /// API client URL for a newly payment initialized
        /// </summary>
        public const string NewPayment = "payments/new";

        /// <summary>
        /// API server route URL for a newly payment initialized
        /// </summary>
        public const string NewPaymentUrl = "payments/new";

        /// <summary>
        /// API client URL for a newly receival initialized
        /// </summary>
        public const string NewReceival = "receivals/new";

        /// <summary>
        /// API server route URL for a newly receival initialized
        /// </summary>
        public const string NewReceivalUrl = "receivals/new";
    }
}
