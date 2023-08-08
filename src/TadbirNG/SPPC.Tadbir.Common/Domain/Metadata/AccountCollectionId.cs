using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده شمارشی برای استفاده از مجموعه حساب های سیستمی در برنامه
    /// </summary>
    public enum AccountCollectionId
    {
        /// <summary>
        /// مجموعه حساب نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// مجموعه حساب دارایی های جاری
        /// </summary>
        LiquidAssets = 1,

        /// <summary>
        /// مجموعه حساب دارایی های غیرجاری
        /// </summary>
        NonLiquidAssets = 2,

        /// <summary>
        /// مجموعه حساب بدهی های جاری
        /// </summary>
        LiquidLiabilities = 3,

        /// <summary>
        /// مجموعه حساب بدهی های غیرجاری
        /// </summary>
        NonLiquidLiabilities = 4,

        /// <summary>
        /// مجموعه حساب حقوق صاحبان سرمایه
        /// </summary>
        StakeholderEquity = 5,

        /// <summary>
        /// مجموعه حساب حساب های انتظامی
        /// </summary>
        ContraAccounts = 6,

        /// <summary>
        /// مجموعه حساب فروش - سود و زیان
        /// </summary>
        FinalSales = 9,

        /// <summary>
        /// مجموعه حساب برگشت از فروش و تخفیفات
        /// </summary>
        SalesRefundDiscounts = 10,

        /// <summary>
        /// مجموعه حساب قیمت تمام شده کالای فروش رفته
        /// </summary>
        CostOfGoodsSold = 11,

        /// <summary>
        /// مجموعه حساب خرید - سود و زیان
        /// </summary>
        FinalPurchase = 12,

        /// <summary>
        /// مجموعه حساب برگشت از خرید و تخفیفات
        /// </summary>
        PurchaseRefundDiscounts = 13,

        /// <summary>
        /// مجموعه حساب هزینه های عملیاتی
        /// </summary>
        OperationalCosts = 14,

        /// <summary>
        /// مجموعه حساب سایر هزینه ها و درآمدها
        /// </summary>
        OtherRevenuesCosts = 15,

        /// <summary>
        /// مجموعه حساب صندوق
        /// </summary>
        CashFund = 16,

        /// <summary>
        /// مجموعه حساب بانک
        /// </summary>
        Bank = 17,

        /// <summary>
        /// مجموعه حساب اسناد دریافتنی
        /// </summary>
        NotesReceivable = 18,

        /// <summary>
        /// مجموعه حساب اسناد پرداختنی
        /// </summary>
        NotesPayable = 19,

        /// <summary>
        /// مجموعه حساب اسناد دریافتنی تضمینی
        /// </summary>
        GuaranteedNotesReceivable = 20,

        /// <summary>
        /// مجموعه حساب اسناد پرداختنی تضمینی
        /// </summary>
        GuaranteedNotesPayable = 21,

        /// <summary>
        /// مجموعه حساب اسناد در جریان وصول
        /// </summary>
        FloatNotes = 22,

        /// <summary>
        /// مجموعه حساب اسناد برگشتی
        /// </summary>
        BouncedNotes = 23,

        /// <summary>
        /// مجموعه حساب تنخواه گردان ها
        /// </summary>
        PettyCash = 24,

        /// <summary>
        /// مجموعه حساب فروش
        /// </summary>
        Sales = 25,

        /// <summary>
        /// مجموعه حساب برگشت از فروش
        /// </summary>
        SalesRefund = 26,

        /// <summary>
        /// مجموعه حساب خرید
        /// </summary>
        Purchase = 27,

        /// <summary>
        /// مجموعه حساب برگشت از خرید
        /// </summary>
        PurchaseRefund = 28,

        /// <summary>
        /// مجموعه حساب اضافات فاکتور فروش
        /// </summary>
        SalesInvoiceCharges = 29,

        /// <summary>
        /// مجموعه حساب اضافات فاکتور خرید
        /// </summary>
        PurchaseInvoiceCharges = 30,

        /// <summary>
        /// مجموعه حساب بدهکاران تجاری
        /// </summary>
        TradeDebtors = 31,

        /// <summary>
        /// مجموعه حساب بستانکاران تجاری
        /// </summary>
        TradeCreditors = 32,

        /// <summary>
        /// مجموعه حساب تخفیفات فروش
        /// </summary>
        SalesDiscount = 33,

        /// <summary>
        /// مجموعه حساب تخفیفات خرید
        /// </summary>
        PurchaseDiscount = 34,

        /// <summary>
        /// مجموعه حساب قیمت تمام شده
        /// </summary>
        FinalCost = 35,

        /// <summary>
        /// مجموعه حساب فروشنده / خریدار  متفرقه
        /// </summary>
        OtherSellerPurchaser = 36,

        /// <summary>
        /// مجموعه حساب مالیات پرداختنی
        /// </summary>
        TaxPayable = 37,

        /// <summary>
        /// مجموعه حساب عوارض پرداختنی
        /// </summary>
        TollPayable = 38,

        /// <summary>
        /// مجموعه حساب مالیات دریافتنی
        /// </summary>
        TaxReceivable = 39,

        /// <summary>
        /// مجموعه حساب عوارض دریافتنی
        /// </summary>
        TollReceivable = 40,

        /// <summary>
        /// مجموعه حساب افتتاحیه
        /// </summary>
        Opening = 41,

        /// <summary>
        /// مجموعه حساب اختتامیه
        /// </summary>
        Closing = 42,

        /// <summary>
        /// مجموعه حساب عملکرد
        /// </summary>
        Performance = 43,

        /// <summary>
        /// مجموعه حساب سود و زیان سال جاری
        /// </summary>
        CurrentYearEarnings = 44,

        /// <summary>
        /// مجموعه حساب سود و زیان انباشته
        /// </summary>
        RetainedEarnings = 45,

        /// <summary>
        /// مجموعه حساب موجودی کالا
        /// </summary>
        Inventory = 46,

        /// <summary>
        /// مجموعه حساب کنترل دستمزد
        /// </summary>
        WageControl = 47,

        /// <summary>
        /// مجموعه حساب کنترل سربار
        /// </summary>
        OverheadControl = 48,

        /// <summary>
        /// مجموعه حساب اموال
        /// </summary>
        Property = 49,

        /// <summary>
        /// مجموعه حساب سود و زیان عملیات اموال
        /// </summary>
        PropertyEarnings = 50,

        /// <summary>
        /// مجموعه حساب اموال انتقالی
        /// </summary>
        TransitionalProperty = 51,
    }
}
