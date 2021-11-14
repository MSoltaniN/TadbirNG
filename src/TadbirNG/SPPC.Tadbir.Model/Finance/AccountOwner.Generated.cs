// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 4/8/2020 5:43:11 AM
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
    /// اطلاعات حساب بانکی یک حساب را نگهداری میکند
    /// </summary>
    public partial class AccountOwner : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountOwner"/> class.
        /// </summary>
        public AccountOwner()
        {
        }

        /// <summary>
        /// Gets or sets the نام بانک مربوط به حساب
        /// </summary>
        public virtual string BankName { get; set; }

        /// <summary>
        /// Gets or sets the نوع حساب (جاری:0) (پس انداز:1)
        /// </summary>
        public virtual int AccountType { get; set; }

        /// <summary>
        /// Gets or sets the نام شعبه بانک 
        /// </summary>
        public virtual string BankBranchName { get; set; }

        /// <summary>
        /// Gets or sets the شاخص شعبه
        /// </summary>
        public virtual string BranchIndex { get; set; }

        /// <summary>
        /// Gets or sets the شماره حساب
        /// </summary>
        public virtual string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the شماره کارت
        /// </summary>
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the شماره شبا
        /// </summary>
        public virtual string ShabaNumber { get; set; }

        /// <summary>
        /// Gets or sets the توضیحات
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the حساب انتخاب شده
        /// </summary>
        public virtual Account Account { get; set; }
    }
}
