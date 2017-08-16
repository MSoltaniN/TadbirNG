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

    /// <summary>
    /// نوع وضعیت های ثبتی یا ریالی را متناظر با شناسه های دیتابیسی تعریف می کند
    /// </summary>
    public enum DocumentStatuses
    {
        /// <summary>
        /// وضعیت نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// وضعیت پیش نویس
        /// </summary>
        Draft = 1,

        /// <summary>
        /// وضعیت ثبت نشده
        /// </summary>
        Unchecked = 2,

        /// <summary>
        /// وضعیت ثبت شده
        /// </summary>
        Checked = 3,

        /// <summary>
        /// وضعیت ثبت عادی
        /// </summary>
        NormalCheck = 4,

        /// <summary>
        /// وضعیت ثبت قطعی
        /// </summary>
        FinalCheck = 5,

        /// <summary>
        /// وضعیت ریالی نشده
        /// </summary>
        NotPriced = 6,

        /// <summary>
        /// وضعیت ریالی شده
        /// </summary>
        Priced = 7
    }
}
