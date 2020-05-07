using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Inventory
{
    /// <summary>
    /// اطلاعات نمایشی مانده حساب های موجودی انبار را نگهداری می کند
    /// </summary>
    public class InventoryBalanceViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب موجودی انبار
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که حساب در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// مانده بدهکار حساب
        /// </summary>
        public decimal DebitBalance { get; set; }

        /// <summary>
        /// مانده بستانکار حساب
        /// </summary>
        public decimal CreditBalance { get; set; }
    }
}
