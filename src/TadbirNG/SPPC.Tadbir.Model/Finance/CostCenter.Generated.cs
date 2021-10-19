// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-03-07 12:46:40 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;

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
            this.Code = String.Empty;
            this.FullCode = String.Empty;
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// حساب والد (پدر) برای این مرکز هزینه که در سطح بالایی آن در ساختار درختی تعریف شده
        /// </summary>
        public virtual CostCenter Parent { get; set; }

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری مرتبط با این مرکز هزینه
        /// </summary>
        public IList<AccountCostCenter> AccountCostCenters { get; protected set; }

        private void InitReferences()
        {
            FiscalPeriod = new FiscalPeriod();
            Branch = new Branch();
            Children = new List<CostCenter>();
        }
    }
}
