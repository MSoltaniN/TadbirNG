// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-03-07 12:46:36 PM
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
    /// اطلاعات یک تفصیلی شناور مورد استفاده برای ثبت پیشامدهای مالی سازمان را نگهداری می کند
    /// </summary>
    public partial class DetailAccount : TreeEntity, IBaseEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند.
        /// </summary>
        public DetailAccount()
        {
            this.Code = String.Empty;
            this.FullCode = String.Empty;
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شرحی که اطلاعات تکمیلی برای این تفصیلی شناور را مشخص می کند
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// حساب والد (پدر) برای این تفصیلی شناور که در سطح بالایی آن در ساختار درختی تعریف شده
        /// </summary>
        public virtual DetailAccount Parent { get; set; }

        /// <summary>
        /// ارز پایه یا پیش فرض مورد استفاده برای این تفصیلی شناور
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری مرتبط با این تفصیلی شناور
        /// </summary>
        public IList<AccountDetailAccount> AccountDetailAccounts { get; protected set; }

        private void InitReferences()
        {
            FiscalPeriod = new FiscalPeriod();
            Branch = new Branch();
            Children = new List<DetailAccount>();
        }
    }
}
