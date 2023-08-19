// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1557
//     Template Version: 1.0
//     Generation Date: 8/2/2023 10:42:28 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات یک مرکز هزینه مورد استفاده برای ثبت پیشامدهای مالی سازمان را نگهداری می کند
    /// </summary>
    public partial class CostCenter : TreeEntity, IBaseEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public CostCenter()
        {
            CreatedByName = String.Empty;
            ModifiedByName = String.Empty;
            Code = String.Empty;
            FullCode = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// حساب والد (پدر) برای این مرکز هزینه که در سطح بالایی آن در ساختار درختی تعریف شده
        /// </summary>
        public virtual CostCenter Parent { get; set; }

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری مرتبط با این مرکز هزینه
        /// </summary>
        public virtual IList<AccountCostCenter> AccountCostCenters { get; protected set; }
    }
}
