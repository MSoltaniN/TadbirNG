using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with payments and receipts.
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
        /// API client URL for all receipt items
        /// </summary>
        public const string Receipts = "receipts";

        /// <summary>
        /// API server route URL for all receipt items
        /// </summary>
        public const string ReceiptsUrl = "receipts";

        /// <summary>
        /// API client URL for a receipt item specified by unique identifier
        /// </summary>
        public const string Receipt = "receipts/{0}";

        /// <summary>
        /// API server route URL for a receipt item specified by unique identifier
        /// </summary>
        public const string ReceiptUrl = "receipts/{payReceiveId:min(1)}";

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
        /// API client URL for confirming a single receipt specified by identifier
        /// </summary>
        public const string ConfirmReceipt = "receipts/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single receipt specified by identifier
        /// </summary>
        public const string ConfirmReceiptUrl = "receipts/{payReceiveId:min(1)}/confirm";

        /// <summary>
        /// API client URL for undoing confirmation of a single receipt specified by identifier
        /// </summary>
        public const string UndoConfirmReceipt = "receipts/{0}/confirm/undo";

        /// <summary>
        /// API server route URL for undoing confirmation of a single receipt specified by identifier
        /// </summary>
        public const string UndoConfirmReceiptUrl = "receipts/{payReceiveId:min(1)}/confirm/undo";

        /// <summary>
        /// API client URL for approving a single receipt specified by identifier
        /// </summary>
        public const string ApproveReceipt = "receipts/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single receipt specified by identifier
        /// </summary>
        public const string ApproveReceiptUrl = "receipts/{payReceiveId:min(1)}/approve";

        /// <summary>
        /// API client URL for undoing approval of a single receipt specified by identifier
        /// </summary>
        public const string UndoApproveReceipt = "receipts/{0}/approve/undo";

        /// <summary>
        /// API server route URL for undoing approval of a single receipt specified by identifier
        /// </summary>
        public const string UndoApproveReceiptUrl = "receipts/{payReceiveId:min(1)}/approve/undo";

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
        /// API client URL for a single receipt specified by number
        /// </summary>
        public const string ReceiptByNo = "receipts/by-no/{0}";

        /// <summary>
        /// API server route URL for a single receipt specified by number
        /// </summary>
        public const string ReceiptByNoUrl = "receipts/by-no/{payReceiveNo:min(1)}";

        /// <summary>
        /// API client URL for the first receipt in current environment
        /// </summary>
        public const string FirstReceipt = "receipts/first";

        /// <summary>
        /// API server route URL for the first receipt in current environment
        /// </summary>
        public const string FirstReceiptUrl = "receipts/first";

        /// <summary>
        /// API client URL for previous receipt in current environment
        /// </summary>
        public const string PreviousReceipt = "receipts/{0}/previous";

        /// <summary>
        /// API server route URL for previous receipt in current environment
        /// </summary>
        public const string PreviousReceiptUrl = "receipts/{payReceiveNo:min(1)}/previous";

        /// <summary>
        /// API client URL for next receipt in current environment
        /// </summary>
        public const string NextReceipt = "receipts/{0}/next";

        /// <summary>
        /// API server route URL for next receipt in current environment
        /// </summary>
        public const string NextReceiptUrl = "receipts/{payReceiveNo:min(1)}/next";

        /// <summary>
        /// API client URL for the last receipt in current environment
        /// </summary>
        public const string LastReceipt = "receipts/last";

        /// <summary>
        /// API server route URL for the last receipt in current environment
        /// </summary>
        public const string LastReceiptUrl = "receipts/last";

        /// <summary>
        /// API client URL for a newly payment initialized
        /// </summary>
        public const string NewPayment = "payments/new";

        /// <summary>
        /// API server route URL for a newly payment initialized
        /// </summary>
        public const string NewPaymentUrl = "payments/new";

        /// <summary>
        /// API client URL for a newly receipt initialized
        /// </summary>
        public const string NewReceipt = "receipts/new";

        /// <summary>
        /// API server route URL for a newly receipt initialized
        /// </summary>
        public const string NewReceiptUrl = "receipts/new";

        /// <summary>
        /// API client URL for all account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentAccountArticles = "payments/{0}/account-articles";

        /// <summary>
        /// API server route URL for all account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentAccountArticlesUrl = "payments/{payReceiveId:min(1)}/account-articles";

        /// <summary>
        /// API client URL for all account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptAccountArticles = "receipts/{0}/account-articles";

        /// <summary>
        /// API server route URL for all account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptAccountArticlesUrl = "receipts/{payReceiveId:min(1)}/account-articles";

    }
}
