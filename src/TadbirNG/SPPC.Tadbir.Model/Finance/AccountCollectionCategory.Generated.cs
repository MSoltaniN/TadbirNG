// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.492
//     Template Version: 1.0
//     Generation Date: 01/16/2019 01:34:42 ب.ظ
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
    /// اطلاعات گروه مجموعه حساب را نگهداری می کند
    /// </summary>
    public partial class AccountCollectionCategory : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountCollectionCategory()
        {
            Name = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام گروه مجموعه حساب
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// مجموعه حساب های مرتبط با این گروه 
        /// </summary>
        public virtual IList<AccountCollection> AccountCollections { get; protected set; }
    }
}
