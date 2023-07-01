using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with payments and receipts.
    /// </summary>
    public sealed class PayReceiveApi
    {
        #region Payment Normal Resources

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
        public const string PaymentUrl = "payments/{paymentId:min(1)}";

        /// <summary>
        /// API client URL for confirming a single payment specified by identifier
        /// </summary>
        public const string ConfirmPayment = "payments/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single payment specified by identifier
        /// </summary>
        public const string ConfirmPaymentUrl = "payments/{paymentId:min(1)}/confirm";

        /// <summary>
        /// API client URL for undoing confirmation of a single payment specified by identifier
        /// </summary>
        public const string UndoConfirmPayment = "payments/{0}/confirm/undo";

        /// <summary>
        /// API server route URL for undoing confirmation of a single payment specified by identifier
        /// </summary>
        public const string UndoConfirmPaymentUrl = "payments/{paymentId:min(1)}/confirm/undo";

        /// <summary>
        /// API client URL for approving a single payment specified by identifier
        /// </summary>
        public const string ApprovePayment = "payments/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single payment specified by identifier
        /// </summary>
        public const string ApprovePaymentUrl = "payments/{paymentId:min(1)}/approve";

        /// <summary>
        /// API client URL for undoing approval of a single payment specified by identifier
        /// </summary>
        public const string UndoApprovePayment = "payments/{0}/approve/undo";

        /// <summary>
        /// API server route URL for undoing approval of a single payment specified by identifier
        /// </summary>
        public const string UndoApprovePaymentUrl = "payments/{paymentId:min(1)}/approve/undo";

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
        /// API client URL for a newly payment initialized
        /// </summary>
        public const string NewPayment = "payments/new";

        /// <summary>
        /// API server route URL for a newly payment initialized
        /// </summary>
        public const string NewPaymentUrl = "payments/new";

        #endregion Payment Normal Resources

        #region Receipt Normal Resources

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
        public const string ReceiptUrl = "receipts/{receiptId:min(1)}";

        /// <summary>
        /// API client URL for confirming a single receipt specified by identifier
        /// </summary>
        public const string ConfirmReceipt = "receipts/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single receipt specified by identifier
        /// </summary>
        public const string ConfirmReceiptUrl = "receipts/{receiptId:min(1)}/confirm";

        /// <summary>
        /// API client URL for undoing confirmation of a single receipt specified by identifier
        /// </summary>
        public const string UndoConfirmReceipt = "receipts/{0}/confirm/undo";

        /// <summary>
        /// API server route URL for undoing confirmation of a single receipt specified by identifier
        /// </summary>
        public const string UndoConfirmReceiptUrl = "receipts/{receiptId:min(1)}/confirm/undo";

        /// <summary>
        /// API client URL for approving a single receipt specified by identifier
        /// </summary>
        public const string ApproveReceipt = "receipts/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single receipt specified by identifier
        /// </summary>
        public const string ApproveReceiptUrl = "receipts/{receiptId:min(1)}/approve";

        /// <summary>
        /// API client URL for undoing approval of a single receipt specified by identifier
        /// </summary>
        public const string UndoApproveReceipt = "receipts/{0}/approve/undo";

        /// <summary>
        /// API server route URL for undoing approval of a single receipt specified by identifier
        /// </summary>
        public const string UndoApproveReceiptUrl = "receipts/{receiptId:min(1)}/approve/undo";

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
        /// API client URL for a newly receipt initialized
        /// </summary>
        public const string NewReceipt = "receipts/new";

        /// <summary>
        /// API server route URL for a newly receipt initialized
        /// </summary>
        public const string NewReceiptUrl = "receipts/new";

        #endregion Receipt Normal Resources

        #region Payment Account Article Resources

        /// <summary>
        /// API client URL for all account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentAccountArticles = "payments/{0}/account-articles";

        /// <summary>
        /// API server route URL for all account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentAccountArticlesUrl = "payments/{paymentId:min(1)}/account-articles";

        /// <summary>
        /// API client URL for a single payment account article specified by identifier
        /// </summary>
        public const string PaymentAccountArticle = "payments/account-articles/{0}";

        /// <summary>
        /// API server route URL for a single payment account article specified by identifier
        /// </summary>
        public const string PaymentAccountArticleUrl = "payments/account-articles/{accountArticleId:min(1)}";

        /// <summary>
        /// API client URL for all available payment account articles
        /// </summary>
        public const string AllPaymentAccountArticles = "payments/account-articles";

        /// <summary>
        /// API server route URL for all available payment account articles
        /// </summary>
        public const string AllPaymentAccountArticlesUrl = "payments/account-articles";

        /// <summary>
        /// API client URL for remove invalid account articles a single payment specified by identifier
        /// </summary>
        public const string RemovePaymentAccountInvalidRows = "payments/{0}/account-articles/remove-Invalid-rows";

        /// <summary>
        /// API server route URL for remove invalid account articles a single payment specified by identifier
        /// </summary>
        public const string RemovePaymentAccountInvalidRowsUrl = "payments/{paymentId:min(1)}/account-articles/remove-Invalid-rows";

        /// <summary>
        /// API client URL for aggregate account articles a single payment specified by identifier
        /// </summary>
        public const string AggregatePaymentAccountArticleRows = "payments/{0}/account-articles/aggregate-rows";

        /// <summary>
        /// API server route URL for aggregate account articles a single payment specified by identifier
        /// </summary>
        public const string AggregatePaymentAccountArticleRowsUrl = "payments/{paymentId:min(1)}/account-articles/aggregate-rows";

        #endregion Payment Account Article Resources

        #region Receipt Account Article Resources

        /// <summary>
        /// API client URL for all account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptAccountArticles = "receipts/{0}/account-articles";

        /// <summary>
        /// API server route URL for all account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptAccountArticlesUrl = "receipts/{receiptId:min(1)}/account-articles";

        /// <summary>
        /// API client URL for a single receipt account article specified by identifier
        /// </summary>
        public const string ReceiptAccountArticle = "receipts/account-articles/{0}";

        /// <summary>
        /// API server route URL for a single receipt account article specified by identifier
        /// </summary>
        public const string ReceiptAccountArticleUrl = "receipts/account-articles/{accountArticleId:min(1)}";

        /// <summary>
        /// API client URL for all available receipt account articles
        /// </summary>
        public const string AllReceiptAccountArticles = "receipts/account-articles";

        /// <summary>
        /// API server route URL for all available receipt account articles
        /// </summary>
        public const string AllReceiptAccountArticlesUrl = "receipts/account-articles";

        /// <summary>
        /// API client URL for remove invalid account articles a single receipt specified by identifier
        /// </summary>
        public const string RemoveReceiptAccountInvalidRows = "receipts/{0}/account-articles/remove-Invalid-rows";

        /// <summary>
        /// API server route URL for remove invalid account articles a single receipt specified by identifier
        /// </summary>
        public const string RemoveReceiptAccountInvalidRowsUrl = "receipts/{receiptId:min(1)}/account-articles/remove-Invalid-rows";

        /// <summary>
        /// API client URL for aggregate account articles a single receipt specified by identifier
        /// </summary>
        public const string AggregateReceiptAccountArticleRows = "receipts/{0}/account-articles/aggregate-rows";

        /// <summary>
        /// API server route URL for aggregate account articles a single receipt specified by identifier
        /// </summary>
        public const string AggregateReceiptAccountArticleRowsUrl = "receipts/{receiptId:min(1)}/account-articles/aggregate-rows";

        #endregion Receipt Account Article Resources

        #region Payment Cash Account Article Resources

        /// <summary>
        /// API client URL for all cash account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentCashAccountArticles = "payments/{0}/cash-account-articles";

        /// <summary>
        /// API server route URL for all cash account articles in a single payment specified by identifier
        /// </summary>
        public const string PaymentCashAccountArticlesUrl = "payments/{paymentId:min(1)}/cash-account-articles";

        /// <summary>
        /// API client URL for a single payment cash account article specified by identifier
        /// </summary>
        public const string PaymentCashAccountArticle = "payments/cash-account-articles/{0}";

        /// <summary>
        /// API server route URL for a single payment cash account article specified by identifier
        /// </summary>
        public const string PaymentCashAccountArticleUrl = "payments/cash-account-articles/{cashAccountArticleId:min(1)}";

        /// <summary>
        /// API client URL for all available payment cash account articles
        /// </summary>
        public const string AllPaymentCashAccountArticles = "payments/cash-account-articles";

        /// <summary>
        /// API server route URL for all available payment cash account articles
        /// </summary>
        public const string AllPaymentCashAccountArticlesUrl = "payments/cash-account-articles";

        /// <summary>
        /// API client URL for remove invalid cash account articles a single payment specified by identifier
        /// </summary>
        public const string RemovePaymentCashAccountInvalidRows = "payments/{0}/cash-account- articles/remove-Invalid-rows";

        /// <summary>
        /// API server route URL for remove invalid cash account articles a single payment specified by identifier
        /// </summary>
        public const string RemovePaymentCashAccountInvalidRowsUrl = "payments/{paymentId:min(1)}/cash-account-articles/remove-Invalid-rows";

        /// <summary>
        /// API client URL for aggregate cash account articles a single payment specified by identifier
        /// </summary>
        public const string AggregatePaymentCashAccountArticleRows = "payments/{0}/cash-account- articles/aggregate-rows";

        /// <summary>
        /// API server route URL for aggregate cash account articles a single payment specified by identifier
        /// </summary>
        public const string AggregatePaymentCashAccountArticleRowsUrl = "payments/{paymentId:min(1)}/cash-account-articles/aggregate-rows";

        #endregion Payment Cash Account Article Resources

        #region Receipt Cash Account Article Resources

        /// <summary>
        /// API client URL for all cash account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptCashAccountArticles = "receipts/{0}/cash-account-articles";

        /// <summary>
        /// API server route URL for all cash account articles in a single receipt specified by identifier
        /// </summary>
        public const string ReceiptCashAccountArticlesUrl = "receipts/{receiptId:min(1)}/cash-account-articles";

        /// <summary>
        /// API client URL for a single receipt cash account article specified by identifier
        /// </summary>
        public const string ReceiptCashAccountArticle = "receipts/cash-account-articles/{0}";

        /// <summary>
        /// API server route URL for a single receipt cash account article specified by identifier
        /// </summary>
        public const string ReceiptCashAccountArticleUrl = "receipts/cash-account-articles/{cashAccountArticleId:min(1)}";

        /// <summary>
        /// API client URL for all available receipt cash account articles
        /// </summary>
        public const string AllReceiptCashAccountArticles = "receipts/cash-account-articles";

        /// <summary>
        /// API server route URL for all available receipt cash account articles
        /// </summary>
        public const string AllReceiptCashAccountArticlesUrl = "receipts/cash-account-articles";

        /// <summary>
        /// API client URL for remove invalid cash account articles a single receipt specified by identifier
        /// </summary>
        public const string RemoveReceiptCashAccountInvalidRows = "receipts/{0}/cash-account-articles/remove-Invalid-rows";

        /// <summary>
        /// API server route URL for remove invalid cash account articles a single receipt specified by identifier
        /// </summary>
        public const string RemoveReceiptCashAccountInvalidRowsUrl = "receipts/{receiptId:min(1)}/cash-account-articles/remove-Invalid-rows";

        /// <summary>
        /// API client URL for aggregate cash account articles a single receipt specified by identifier
        /// </summary>
        public const string AggregateReceiptCashAccountArticleRows = "receipts/{0}/cash-account-articles/aggregate-rows";

        /// <summary>
        /// API server route URL for aggregate cash account articles a single receipt specified by identifier
        /// </summary>
        public const string AggregateReceiptCashAccountArticleRowsUrl = "receipts/{receiptId:min(1)}/cash-account-articles/aggregate-rows";

        #endregion Receipt Cash Account Article Resources
    }
}
