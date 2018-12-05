// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.449
//     Template Version: 1.0
//     Generation Date: 2018-11-28 12:34:23 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات یکی از گروه های حساب را نگهداری می کند
    /// </summary>
    public partial class AccountGroup : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountGroup()
        {
            Name = String.Empty;
            Category = String.Empty;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// نام یا عنوان نمایشی گروه حساب به زبان پیش فرض برنامه
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// سیستم ثبت موجودی قابل استفاده برای این گروه
        /// </summary>
        public virtual short InventoryMode { get; set; }

        /// <summary>
        /// ماهیت گروه با توجه به مورد استفاده در گزارش های مالی 
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// شرح یا ملاحظات گروه حساب به زبان پیش فرض برنامه
        /// </summary>
        public virtual string Description { get; set; }

        private void InitReferences()
        {
        }
    }
}