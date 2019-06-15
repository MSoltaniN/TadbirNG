// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.492
//     Template Version: 1.0
//     Generation Date: 01/15/2019 11:42:26 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات مجموعه حساب را نگهداری می کند
    /// </summary>
    public partial class AccountCollection : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountCollection()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// نام مجموعه حساب
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// حالت انتخاب یک حساب یا چندین حساب
        /// </summary>
        public virtual bool MultiSelect { get; set; }

        /// <summary>
        /// سطح حساب انتخابی
        /// </summary>
        public virtual short TypeLevel { get; set; }

        /// <summary>
        /// حالت ادواری یا دوره ای دوره مالی
        /// </summary>
        public virtual short InventoryMode { get; set; }

        /// <summary>
        /// طبقه بندی اصلی دربرگیرنده این مجموعه حساب
        /// </summary>
        public virtual AccountCollectionCategory Category { get; set; }

        private void InitReferences()
        {
            Category = new AccountCollectionCategory();
        }
    }
}
