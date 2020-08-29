using System;
using System.Collections.Generic;

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
        OwnerEquities = 5,

        /// <summary>
        /// مجموعه حساب فروش - سود و زیان
        /// </summary>
        FinalSales = 9,

        /// <summary>
        /// مجموعه حساب برگشت از فروش و تخفیفات
        /// </summary>
        SalesRefundDiscount = 10,

        /// <summary>
        /// مجموعه حساب قیمت تمام شده کالای فروش رفته
        /// </summary>
        SoldProductCost = 11,

        /// <summary>
        /// مجموعه حساب خرید - سود و زیان
        /// </summary>
        FinalPurchase = 12,

        /// <summary>
        /// مجموعه حساب برگشت از خرید و تخفیفات
        /// </summary>
        PurchaseRefundDiscount = 13,

        /// <summary>
        /// مجموعه حساب هزینه های عملیاتی
        /// </summary>
        OperationalCosts = 14,

        /// <summary>
        /// مجموعه حساب سایر هزینه ها و درآمدها
        /// </summary>
        OtherCostRevenue = 15,

        /// <summary>
        /// مجموعه حساب صندوق
        /// </summary>
        Cashier = 16,

        /// <summary>
        /// مجموعه حساب بانک
        /// </summary>
        Bank = 17,

        /// <summary>
        /// مجموعه حساب فروش
        /// </summary>
        Sales = 25,

        /// <summary>
        /// مجموعه حساب برگشت از فروش
        /// </summary>
        SalesRefund = 26,

        /// <summary>
        /// مجموعه حساب تخفیفات فروش
        /// </summary>
        SalesDiscount = 33,

        /// <summary>
        /// مجموعه حساب قیمت تمام شده
        /// </summary>
        CalculatedCost = 35,

        /// <summary>
        /// مجموعه حساب افتتاحیه
        /// </summary>
        OpeningAccount = 41,

        /// <summary>
        /// مجموعه حساب اختتامیه
        /// </summary>
        ClosingAccount = 42,

        /// <summary>
        /// مجموعه حساب عملکرد
        /// </summary>
        Performance = 43,

        /// <summary>
        /// مجموعه حساب سود و زیان سال جاری
        /// </summary>
        CurrentProfitLoss = 44,

        /// <summary>
        /// مجموعه حساب سود و زیان انباشته
        /// </summary>
        AccumulatedProfitLoss = 45,

        /// <summary>
        /// مجموعه حساب موجودی کالا
        /// </summary>
        ProductInventory = 46
    }
}
