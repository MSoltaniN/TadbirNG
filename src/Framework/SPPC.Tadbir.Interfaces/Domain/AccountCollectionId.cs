using System;
using System.Collections.Generic;
using System.Text;

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
        /// مجموعه حساب فروش
        /// </summary>
        Sales = 4,

        /// <summary>
        /// مجموعه حساب برگشت از فروش
        /// </summary>
        SalesRefund = 4,

        /// <summary>
        /// مجموعه حساب تخفیفات فروش
        /// </summary>
        SalesDiscount = 4,

        /// <summary>
        /// مجموعه حساب صندوق
        /// </summary>
        Cashier = 16,

        /// <summary>
        /// مجموعه حساب بانک
        /// </summary>
        Bank = 17
    }
}
