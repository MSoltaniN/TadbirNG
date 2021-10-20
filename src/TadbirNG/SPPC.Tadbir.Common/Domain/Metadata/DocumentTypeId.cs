using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// این نوع داده شمارشی مقادیر سیستمی را برای نوع یک مستند اداری تعریف می کند
    /// </summary>
    public enum DocumentTypeId
    {
        /// <summary>
        /// سند نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// مستند اداری از نوع سند مالی
        /// </summary>
        Voucher = 1,

        /// <summary>
        /// درخواست کالا
        /// </summary>
        RequisitionVoucher = 2,

        /// <summary>
        /// سفارش خرید کالا
        /// </summary>
        PurchaseOrder = 3,

        /// <summary>
        /// حواله انبار مقداری
        /// </summary>
        IssueVoucher = 4,

        /// <summary>
        /// جواله انبار ریالی
        /// </summary>
        PricedIssueVoucher = 5,

        /// <summary>
        /// رسید انبار مقداری
        /// </summary>
        ReceiptVoucher = 6,

        /// <summary>
        /// رسید انبار ریالی
        /// </summary>
        PricedReceiptVoucher = 7,

        /// <summary>
        /// فاکتور خرید
        /// </summary>
        PurchaseInvoice = 8,

        /// <summary>
        /// فاکتور فروش
        /// </summary>
        SalesInvoice = 9,

        /// <summary>
        /// فاکتور برگشت از خرید
        /// </summary>
        PurchaseRefundInvoice = 10,

        /// <summary>
        /// فاکتور برگشت از فروش
        /// </summary>
        SalesRefundInvoice = 11,

        /// <summary>
        /// پیش فاکتور
        /// </summary>
        InvoiceQuote = 12
    }
}
