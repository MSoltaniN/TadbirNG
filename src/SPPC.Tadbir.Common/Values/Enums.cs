using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// نوع اسناد اداری در برنامه را متناظر با شناسه های دیتابیسی تعریف می کند
    /// </summary>
    public enum DocumentTypes
    {
        /// <summary>
        /// سند نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// سند مالی
        /// </summary>
        Transaction = 1,

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

    public enum DocumentStatuses
    {
        None = 0,
        Draft = 1,
        Unchecked = 2,
        Checked = 3,
        NormalCheck = 4,
        FinalCheck = 5,
        NotPriced = 6,
        Priced = 7
    }
}
