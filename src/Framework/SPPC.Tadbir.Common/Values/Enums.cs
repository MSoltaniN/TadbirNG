using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// نوع اسناد اداری در برنامه را متناظر با شناسه های دیتابیسی تعریف می کند
    /// </summary>
    public enum DocumentTypeId
    {
        /// <summary>
        /// سند نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// سند مالی
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

    /// <summary>
    /// نوع وضعیت های ثبتی یا ریالی را متناظر با شناسه های دیتابیسی تعریف می کند
    /// </summary>
    public enum DocumentStatusId
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

    /// <summary>
    /// مقادیر تعریف شده برای محدوده دسترسی به موجودیت ها در سطح شعبه های سازمانی را تعریف می کند
    /// </summary>
    public enum BranchScope
    {
        /// <summary>
        /// دسترسی در سطح کلیه شعب
        /// </summary>
        AllBranches = 0,

        /// <summary>
        /// دسترسی در سطح شعبه جاری و تمام شعب زیرمجموعه
        /// </summary>
        CurrentBranchAndChildren = 1,

        /// <summary>
        /// دسترسی در سطح شعبه جاری
        /// </summary>
        CurrentBranch = 2
    }

    /// <summary>
    /// نوع انتخاب حساب برای اضافه کردن در مجموعه حساب
    /// </summary>
    public enum TypeLevel
    {
        /// <summary>
        /// تمام حساب ها
        /// </summary>
        AllAccounts = 0,

        /// <summary>
        /// حساب های در سطح آخر
        /// </summary>
        LeafAccounts = 1,

        /// <summary>
        /// حساب های غیر از سطح آخر
        /// </summary>
        NonLeafAccounts = 2
    }

    /// <summary>
    /// انواع سیستمی اسناد را تعریف می کند
    /// </summary>
    public enum VoucherType
    {
        /// <summary>
        /// نوع سیستمی سند عادی
        /// </summary>
        NormalVoucher = 0,

        /// <summary>
        /// نوع سیستمی سند افتتاحیه
        /// </summary>
        OpeningVoucher = 1,

        /// <summary>
        /// نوع سیستمی سند اختتامیه
        /// </summary>
        ClosingVoucher = 2,

        /// <summary>
        /// نوع سیستمی بستن حسابهای موقت
        /// </summary>
        ClosingTempAccounts = 3
    }

    /// <summary>
    /// داده شمارشی برای انواع آرتیکل های سند مالی
    /// </summary>
    public enum VoucherLineType
    {
        /// <summary>
        /// آرتیکل عادی
        /// </summary>
        NormalLine = 0,

        /// <summary>
        /// آرتیکل مالیات و عوارض
        /// </summary>
        TaxAndToll = 1,

        /// <summary>
        /// آرتیکل اصلاحی
        /// </summary>
        Revised = 2
    }

    /// <summary>
    /// حالات ممکن برای محدودیت ثبت مالی را در سرفصل های حسابداری تعریف می کند
    /// </summary>
    public enum TurnoverMode : short
    {
        /// <summary>
        /// نداشتن محدودیت برای ثبت مالی
        /// </summary>
        Unlimited = -1,

        /// <summary>
        /// محدودیت بدهکار در طول دوره
        /// </summary>
        DebtorDuringPeriod = 0,

        /// <summary>
        /// محدودیت بستانکار در طول دوره
        /// </summary>
        CreditorDuringPeriod = 1,

        /// <summary>
        /// محدودیت بدهکار در انتهای دوره
        /// </summary>
        DebtorEndPeriod = 2,

        /// <summary>
        /// محدودیت بستانکار در انتهای دوره
        /// </summary>
        CreditorEndPeriod = 3
    }

    /// <summary>
    /// داده شمارشی برای انواع سیستم ثبت موجودی
    /// </summary>
    public enum InventoryModeEnum
    {
        /// <summary>
        /// سیستم ثبت ادواری
        /// </summary>
        Periodic = 0,

        /// <summary>
        /// سیستم ثبت دائمی
        /// </summary>
        Perpetual = 1
    }
}
